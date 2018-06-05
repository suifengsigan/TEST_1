using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Text;

namespace DataAccess
{
    public class BomV2 : IBom
    {
        public void ImportCuprum(List<EACT_CUPRUM> CupRumList, string creator, string mouldInteriorID, bool isImportEman, List<EACT_CUPRUM_EXP> cuprumEXPs = null)
        {
            using (var conn = DAL.GetConn())
            {
                conn.Open();
                var _tran = conn.BeginTransaction();
                try
                {
                    //导入Eman
                    if (isImportEman)
                    { 
                        Eman.ImportEman(conn, _tran, CupRumList, mouldInteriorID, creator);
                    }

                    if (CupRumList.Count > 0)
                    {
                        string ids = string.Join(",", Enumerable.Select(CupRumList, u => string.Format("'{0}'", u.CUPRUMSN)).ToArray());
                        //TODO 先判断待导入的电极在库中是否已经存在，如果存在则先删除再插入


                        //插入模号并返回插入的模号ID
                        var eact_mould = new DataAccess.ModelV2.EACT_MOULD();
                        eact_mould.M_SN = CupRumList[0].STEELMODELSN;
                        eact_mould.M_UPDATEUSE = creator;
                        eact_mould.M_UPDATETIME = DateTime.Now;
                        var select_mould_sql = string.Format("select M_ID from EACT_MOULD where M_SN=@M_SN");
                        var insert_mould_sql = CreateEACT_MOULDInsertSql();
                        object mouldId = conn.ExecuteScalar(select_mould_sql, eact_mould, _tran, null, null);
                        if (mouldId == null)
                        {
                            mouldId = conn.ExecuteScalar(insert_mould_sql, eact_mould, _tran, null, null).ToString();
                        }
                        //插入钢件并返回插入的钢件ID
                        object steelID = null;
                        if (mouldId != null)
                        {
                            var eact_steel = new DataAccess.ModelV2.EACT_STEEL();
                            eact_steel.M_ID = double.Parse(mouldId.ToString());
                            eact_steel.ST_SN = CupRumList[0].STEELMODULESN;
                            eact_steel.ST_UPDATETIME = DateTime.Now;
                            eact_steel.ST_UPDATEUSE = creator;
                            var select_st_c_sql = string.Format("select * from EACT_STEEL where M_ID=@M_ID and ST_SN=@ST_SN");
                            steelID = conn.ExecuteScalar(select_st_c_sql, eact_steel, _tran, null, null);
                            if (steelID == null)
                            {
                                steelID = conn.ExecuteScalar(CreateEACT_STEELInsertSql(), eact_steel, _tran, null, null).ToString();
                            }
                        }
                        if (steelID != null)
                        {
                            foreach (var item in CupRumList.GroupBy(u=>u.PARTFILENAME).ToDictionary(group => group.Key, group => group.ToList()))
                            {
                                var cuprum =item.Value.FirstOrDefault();
                                if (cuprum != null)
                                {
                                    //TODO 插入电极表
                                    var eact_cuprum = new DataAccess.ModelV2.EACT_CUPRUM();
                                    eact_cuprum.C_INTIME = DateTime.Now;
                                    eact_cuprum.C_PRTNAME = item.Key;
                                    eact_cuprum.C_SN = item.Key;
                                    string cuprumId = conn.ExecuteScalar(CreateEACT_CUPRUMInsertSql(), eact_cuprum, _tran, null, null).ToString();

                                    //TODO 插入钢件关联电极
                                    var eact_steel_cuprum = new DataAccess.ModelV2.EACT_STEEL_CUPRUM();
                                    eact_steel_cuprum.C_ID = double.Parse(cuprumId);
                                    eact_steel_cuprum.ST_ID = double.Parse(steelID.ToString());
                                    string stcID= conn.ExecuteScalar(CreateEACT_STEEL_CUPRUMInsertSql(), eact_steel_cuprum, _tran, null, null).ToString();

                                    //TODO 插入跑位信息
                                    var eact_c_pos = new DataAccess.ModelV2.EACT_CUPRUM_POS();
                                    eact_c_pos.ST_ID= double.Parse(steelID.ToString());
                                    eact_c_pos.STC_ID = double.Parse(stcID);
                                    eact_c_pos.CP_ID= double.Parse(cuprumId);
                                    conn.Execute(CreateEACT_CUPRUM_POSInsertSql(), eact_c_pos, _tran);

                                    //TODO 插入投产信息
                                    foreach (var c in item.Value)
                                    {
                                        var eact_c_make = new DataAccess.ModelV2.EACT_CUPRUM_MAKE();
                                        conn.Execute(CreateEACT_CUPRUM_MAKEInsertSql(), eact_c_make, _tran);
                                    }
                                }
                            }
                        }
                    }

                    _tran.Commit();
                }
                catch (Exception ex)
                {
                    _tran.Rollback();
                    throw ex;
                }
            }
        }

        public List<EACT_CUPRUM> GetCuprumList(List<string> cuprumNames, string modelNo, string partNo)
        {
            return new List<EACT_CUPRUM>();
        }

        string CreateEACT_CUPRUM_MAKEInsertSql()
        {
            StringBuilder queryParameters = new StringBuilder();
            queryParameters.Append("@C_ID");
            queryParameters.Append(", ");
            queryParameters.Append("@EM_ID");
            queryParameters.Append(", ");
            queryParameters.Append("@CMK_NAME");
            queryParameters.Append(", ");
            queryParameters.Append("@CMK_CKNO");
            queryParameters.Append(", ");
            queryParameters.Append("@CMK_RFID");
            queryParameters.Append(", ");
            queryParameters.Append("@CMK_BRACODE");
            queryParameters.Append(", ");
            queryParameters.Append("@CMK_RMF");
            queryParameters.Append(", ");
            queryParameters.Append("@CMK_CNCH");
            queryParameters.Append(", ");
            queryParameters.Append("@CMK_CNCHTYPE");
            queryParameters.Append(", ");
            queryParameters.Append("@CMK_MKSTATE");
            queryParameters.Append(", ");
            queryParameters.Append("@CMK_MKTIME");
            queryParameters.Append(", ");
            queryParameters.Append("@CMK_MKUSE");
            queryParameters.Append(", ");
            queryParameters.Append("@CMK_ISPRINT");
            queryParameters.Append(", ");
            queryParameters.Append("@CMK_PRINTTIME");
            queryParameters.Append(", ");
            queryParameters.Append("@CMK_PRINTUSE");
            queryParameters.Append(", ");
            queryParameters.Append("@CMK_OFFSETX");
            queryParameters.Append(", ");
            queryParameters.Append("@CMK_OFFSETY");
            queryParameters.Append(", ");
            queryParameters.Append("@CMK_OFFSETZ");
            queryParameters.Append(", ");
            queryParameters.Append("@CMK_OFFSETTYPE");
            queryParameters.Append(", ");
            queryParameters.Append("@CMK_CMMDETAILID");
            queryParameters.Append(", ");
            queryParameters.Append("@CMK_OFFSETUSE");
            queryParameters.Append(", ");
            queryParameters.Append("@CMK_OFFSETTIME");
            queryParameters.Append(", ");
            queryParameters.Append("@CMK_INITFIRENUM");
            queryParameters.Append(", ");
            queryParameters.Append("@CMK_FIRENUM");
            queryParameters.Append(", ");
            queryParameters.Append("@CMK_FIREEDITUSE");
            queryParameters.Append(", ");
            queryParameters.Append("@CMK_FIREEDITTIME");

            string query = String.Format("Insert Into EACT_CUPRUM_MAKE ({0}) Values ({1})", queryParameters.ToString().Replace("@", ""), queryParameters.ToString());

            return query;
        }

        string CreateEACT_CUPRUM_POSInsertSql()
        {
            StringBuilder queryParameters = new StringBuilder();
            queryParameters.Append("@ST_ID");
            queryParameters.Append(", ");
            queryParameters.Append("@C_ID");
            queryParameters.Append(", ");
            queryParameters.Append("@STC_ID");
            queryParameters.Append(", ");
            queryParameters.Append("@CP_INDEX");
            queryParameters.Append(", ");
            queryParameters.Append("@CP_X");
            queryParameters.Append(", ");
            queryParameters.Append("@CP_Y");
            queryParameters.Append(", ");
            queryParameters.Append("@CP_Z");
            queryParameters.Append(", ");
            queryParameters.Append("@CP_C");
            queryParameters.Append(", ");
            queryParameters.Append("@CP_OK");
            queryParameters.Append(", ");
            queryParameters.Append("@UPDATETIME");
            queryParameters.Append(", ");
            queryParameters.Append("@UPDATEUSE");

            string query = String.Format("Insert Into EACT_CUPRUM_POS ({0}) Values ({1})", queryParameters.ToString().Replace("@", ""), queryParameters.ToString());

            return query;
        }

        string CreateEACT_MOULDInsertSql()
        {
            StringBuilder queryParameters = new StringBuilder();
            queryParameters.Append("@M_SN");
            queryParameters.Append(", ");
            queryParameters.Append("@M_UPDATETIME");
            queryParameters.Append(", ");
            queryParameters.Append("@M_UPDATEUSE");

            string query = String.Format("Insert Into EACT_MOULD ({0}) Values ({1})", queryParameters.ToString().Replace("@", ""), queryParameters.ToString());

            return query;
        }

        string CreateEACT_STEELInsertSql()
        {
            StringBuilder queryParameters = new StringBuilder();
            queryParameters.Append("@M_ID");
            queryParameters.Append(", ");
            queryParameters.Append("@ST_SN");
            queryParameters.Append(", ");
            queryParameters.Append("@ST_DSIZE");
            queryParameters.Append(", ");
            queryParameters.Append("@ST_MTYPE");
            queryParameters.Append(", ");
            queryParameters.Append("@ST_REMARK");
            queryParameters.Append(", ");
            queryParameters.Append("@ST_UPDATETIME");
            queryParameters.Append(", ");
            queryParameters.Append("@ST_UPDATEUSE");

            string query = String.Format("Insert Into EACT_STEEL ({0}) Values ({1})", queryParameters.ToString().Replace("@", ""), queryParameters.ToString());

            return query;
        }

        string CreateEACT_CUPRUMInsertSql()
        {
            StringBuilder queryParameters = new StringBuilder();
            queryParameters.Append("@CT_ID");
            queryParameters.Append(", ");
            queryParameters.Append("@CK_ID");
            queryParameters.Append(", ");
            queryParameters.Append("@C_INTIME");
            queryParameters.Append(", ");
            queryParameters.Append("@C_PRTNAME");
            queryParameters.Append(", ");
            queryParameters.Append("@C_SN");
            queryParameters.Append(", ");
            queryParameters.Append("@C_HEIGHT");
            queryParameters.Append(", ");
            queryParameters.Append("@C_HEADH");
            queryParameters.Append(", ");
            queryParameters.Append("@C_BASEH");
            queryParameters.Append(", ");
            queryParameters.Append("@C_RCOUNT");
            queryParameters.Append(", ");
            queryParameters.Append("@C_MCOUNT");
            queryParameters.Append(", ");
            queryParameters.Append("@C_FCOUNT");
            queryParameters.Append(", ");
            queryParameters.Append("@C_SPEC");
            queryParameters.Append(", ");
            queryParameters.Append("@C_STOCKSPEC");
            queryParameters.Append(", ");
            queryParameters.Append("@C_STOCKBULK");
            queryParameters.Append(", ");
            queryParameters.Append("@C_SPECCODE");
            queryParameters.Append(", ");
            queryParameters.Append("@C_ACCURACY");
            queryParameters.Append(", ");
            queryParameters.Append("@C_VDI");
            queryParameters.Append(", ");
            queryParameters.Append("@C_CQUADRANT");
            queryParameters.Append(", ");
            queryParameters.Append("@C_AREA");
            queryParameters.Append(", ");
            queryParameters.Append("@C_ROCK");
            queryParameters.Append(", ");
            queryParameters.Append("@C_PROCDIRECTION");
            queryParameters.Append(", ");
            queryParameters.Append("@C_MATERIAL");
            queryParameters.Append(", ");
            queryParameters.Append("@C_SWAYFLAT");
            queryParameters.Append(", ");
            queryParameters.Append("@C_PC");
            queryParameters.Append(", ");
            queryParameters.Append("@C_ETCH");
            queryParameters.Append(", ");
            queryParameters.Append("@C_EDMPLACE");
            queryParameters.Append(", ");
            queryParameters.Append("@C_UPDATETIME");
            queryParameters.Append(", ");
            queryParameters.Append("@C_UPDATEUSE");
            queryParameters.Append(", ");
            queryParameters.Append("@C_REMARK");
            queryParameters.Append(", ");
            queryParameters.Append("@C_REGION");
            queryParameters.Append(", ");
            queryParameters.Append("@C_ORD");
            queryParameters.Append(", ");
            queryParameters.Append("@C_TOEMANNAME");

            string query = String.Format("Insert Into EACT_CUPRUM ({0}) Values ({1})", queryParameters.ToString().Replace("@", ""), queryParameters.ToString());

            return query;
        }

        string CreateEACT_STEEL_CUPRUMInsertSql()
        {
            StringBuilder queryParameters = new StringBuilder();
            queryParameters.Append("@ST_ID");
            queryParameters.Append(", ");
            queryParameters.Append("@C_ID");
            queryParameters.Append(", ");
            queryParameters.Append("@STC_BORROW");
            queryParameters.Append(", ");
            queryParameters.Append("@STC_BORROWTYPE");
            queryParameters.Append(", ");
            queryParameters.Append("@STC_BORROWID");
            queryParameters.Append(", ");
            queryParameters.Append("@STC_CNCFILEPATH");
            queryParameters.Append(", ");
            queryParameters.Append("@STC_CNCFILEEXIST");
            queryParameters.Append(", ");
            queryParameters.Append("@STC_CNCFILETIME");
            queryParameters.Append(", ");
            queryParameters.Append("@STC_CMMFILEPATH");
            queryParameters.Append(", ");
            queryParameters.Append("@STC_CMMFILEEXIST");
            queryParameters.Append(", ");
            queryParameters.Append("@STC_CMMFILETIME");
            queryParameters.Append(", ");
            queryParameters.Append("@STC_UPDATETIME");
            queryParameters.Append(", ");
            queryParameters.Append("@STC_UPDATEUSE");

            string query = String.Format("Insert Into EACT_STEEL_CUPRUM ({0}) Values ({1})", queryParameters.ToString().Replace("@", ""), queryParameters.ToString());

            return query;
        }
    }
}
