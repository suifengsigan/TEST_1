using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using DataAccess.Model;

namespace DataAccess
{
    /// <summary>
    /// 导入EmanBom
    /// </summary>
    public abstract class Eman
    {
        /// <summary>
        /// 导入EMan
        /// </summary>
        public static void ImportEman(
              System.Data.IDbConnection conn
            , System.Data.IDbTransaction tran
            , List<EACT_CUPRUM> CupRumList
            , string mouldInteriorID
            , string loginID
            ) 
        {
            //验证模号对应的模具信息
            var mouldInfo = CheckMouldInfo(conn, tran, mouldInteriorID);

            //验证登录用户
            var human=CheckLoginID(conn, tran, loginID);

            //验证电极在Eman中是否存在
            CheckCuprumIsExist(conn, tran, CupRumList,mouldInfo);

            if (CupRumList.Count == 0) return;

            //验证对应标准工件名称
            CheckStandardPart(conn, tran, CupRumList);

            //验证对应的典型工件
            CheckTypicalSeparatePart(conn, tran, CupRumList);

            //验证Eact传入材质
            CheckMaterial(conn, tran, CupRumList);

            //获取系统中的工件ID集合
            var dbPartIDList = GetDBPartIDList(conn, tran, mouldInfo);

            //插入工件
            InsertMouldPart(conn, tran, CupRumList, mouldInfo,dbPartIDList,human);

            //插入工件历史
            InsertMouldPartHistory(conn, tran, CupRumList,mouldInfo,human);

            //插入BOM
            var vil = GetVerifyIDList(conn, tran, mouldInfo);
            InsertBOM(conn, tran, CupRumList,mouldInfo,human,vil);

            //插入工艺
            var tspts = GetTypicalSeparatePartTechnics(conn, tran, CupRumList);
            InsertMouldPartTechnics(conn, tran, CupRumList,mouldInfo,human,tspts);

            //插入工艺资源
            var tsrus = GetTypicalSeparateResourceUse(conn, tran, CupRumList);
            InsertResourceUse(conn, tran, CupRumList,tsrus);

            //插入工艺历史记录
            InsertDelMouldPartTechRecord(conn, tran, CupRumList,human);

            //插入工件相关性
            InsertTechnicsRestrict(conn,tran,CupRumList);

            //插入工步
            InsertWorkStepInfo(conn, tran, CupRumList);
        }

        /// <summary>
        /// 获取系统中此模具下的导入的BOM件号
        /// </summary>
        public static List<DataAccess.Model.BOM> GetVerifyIDList(
              System.Data.IDbConnection conn
            , System.Data.IDbTransaction tran
            ,Eman_Mould mouldInfo
            )
        {
            var sql = " select verifyID from BOM WITH (NOLOCK) where mark = 1 and mouldID = @mouldID ";
            var result=conn.Query<DataAccess.Model.BOM>(sql, mouldInfo, tran);
            return result.ToList();
        }

        /// <summary>
        /// 获取工艺资源数据
        /// </summary>
        public static List<TypicalSeparateResourceUseEx> GetTypicalSeparateResourceUse(
            System.Data.IDbConnection conn
            , System.Data.IDbTransaction tran
            , List<EACT_CUPRUM> CupRumList
            ) 
        {
            var sql = new StringBuilder();
            sql.AppendLine("select mpt.ID,tru.resourceID,tru.priorityID,tru.useUnit,mpt.ID as MPT_ID,tmpt.partID as TMPT_PARTID,mpt.partID as MPT_PARTID");
            sql.AppendLine("from MouldPartTechnics mpt");
            sql.AppendLine("inner join TypicalSeparatePartTechnics tmpt on tmpt.operationOrderID = mpt.operationOrderID ");
            sql.AppendLine("and mpt.operationNameID = tmpt.operationNameID");
            sql.AppendLine("inner join TypicalSeparateResourceUse tru on tmpt.operationID = tru.operationID");
            sql.AppendLine("where tmpt.partID+mpt.partID in (");
            sql.Append(string.Join(",", Enumerable.Select(CupRumList, u => string.Format("'{0}'", u.TypicalPartID + u.MouldPart_PartID)).Distinct().ToArray()));
            sql.AppendLine(")");
            var result = conn.Query<TypicalSeparateResourceUseEx>(sql.ToString(), new { }, tran).ToList();
            return result;
        }

        /// <summary>
        /// 获取工艺数据
        /// </summary>
        public static List<TypicalSeparatePartTechnic> GetTypicalSeparatePartTechnics(
            System.Data.IDbConnection conn
            , System.Data.IDbTransaction tran
            , List<EACT_CUPRUM> CupRumList
            ) 
        {
            var sql = new StringBuilder();
            sql.AppendLine("select * from TypicalSeparatePartTechnics tmpt");
            sql.AppendLine("inner join TypicalSeparatePart tp on tmpt.partID = tp.partID where tp.partID in (");
            sql.Append(string.Join(",", Enumerable.Select(CupRumList, u => string.Format("'{0}'", u.TypicalPartID)).Distinct().ToArray()));
            sql.AppendLine(")");
            var result=conn.Query<TypicalSeparatePartTechnic>(sql.ToString(), new { }, tran).ToList();
            return result;
        }

        /// <summary>
        /// 获取系统中的工件ID集合
        /// </summary>
        public static List<string> GetDBPartIDList(
             System.Data.IDbConnection conn
            , System.Data.IDbTransaction tran
             , Eman_Mould mouldInfo
            )
        {
            var dbPartIDList = new List<string>();
            string sql = "SELECT MouldPart.partID from MouldPart WITH (NOLOCK) where mark <> 'delete' and mouldID =@mouldID";
            var list = conn.Query<MouldPart>(sql, mouldInfo, tran);
            list.ToList().ForEach(u => {
                dbPartIDList.Add(u.partID);
            });
            return dbPartIDList.Distinct().ToList();
        }

        /// <summary>
        /// 验证电极在Eman中是否存在
        /// </summary>
        public static void CheckCuprumIsExist(
             System.Data.IDbConnection conn
            , System.Data.IDbTransaction tran
            , List<EACT_CUPRUM> CupRumList
            , Eman_Mould mouldInfo
            ) 
        {
            var sql = new StringBuilder();
            sql.AppendLine("SELECT * FROM MouldPart WHERE MouldPart.mouldID = @MouldInteriorID AND MouldPart.mark <> 'delete' AND MouldPart.verifyID in");
            sql.AppendLine("(");
            sql.Append(string.Join(",", Enumerable.Select(CupRumList, u => string.Format("'{0}'", u.CUPRUMSN)).Distinct().ToArray()));
            sql.AppendLine(")");
            var list = conn.Query<MouldPart>(sql.ToString(), new { MouldInteriorID = mouldInfo .mouldID}, tran);
            foreach (var item in CupRumList.ToList()) 
            {
                if (list.Where(u => u.verifyID == item.CUPRUMSN).Count() > 0)
                {
                    CupRumList.Remove(item);
                }
            }
        }
        
        /// <summary>
        /// 验证登录ID
        /// </summary>
        public static Human CheckLoginID(
              System.Data.IDbConnection conn
            , System.Data.IDbTransaction tran
            , string loginID
            )
        {
            var sql = "select * from Human where loginID=@loginID";
            var result = conn.Query<Human>(sql, new { loginID = loginID },tran).ToList();
            if (result.Count <= 0)
            {
                throw new Exception(string.Format("系统中不存在此登录用户：{0}", loginID));
            }

            return result.FirstOrDefault();
        }

        /// <summary>
        /// 插入工步
        /// </summary>
        public static void InsertWorkStepInfo(
               System.Data.IDbConnection conn
            , System.Data.IDbTransaction tran
            , List<EACT_CUPRUM> CupRumList
            ) 
        {

            var sql = new StringBuilder();
            sql.AppendLine("select mpt.ID as operationID,tws.workOrderID,tws.planHours as workPlanHours,");
            sql.AppendLine("tws.workStepContent,cast(isnull(tws.planHours,0) as decimal(18,6))*partNums as planTotalHours,");
            sql.AppendLine("'eact' as dataFrom from t_GYGH_TypicalSeparateWorkStepInfo tws");
            sql.AppendLine("inner join TypicalSeparatePartTechnics tmpt on tws.operationID = tmpt.operationID");
            sql.AppendLine("inner join MouldPartTechnics mpt on tmpt.operationNameID = mpt.operationNameID ");
            sql.AppendLine("and tmpt.operationOrderID = mpt.operationOrderID");
            sql.AppendLine("where tmpt.partID+mpt.partID in (");
            sql.Append(string.Join(",", Enumerable.Select(CupRumList, u => string.Format("'{0}'", u.TypicalPartID + u.MouldPart_PartID)).Distinct().ToArray()));
            sql.AppendLine(")");

            var list = conn.Query<WorkStepInfo>(sql.ToString(),null,tran);

            foreach (var item in list) 
            {
                var model = item;
                StringBuilder queryParameters = new StringBuilder();
                queryParameters.Append("@operationID");
                queryParameters.Append(", ");
                queryParameters.Append("@workOrderID");
                queryParameters.Append(", ");
                queryParameters.Append("@workPlanHours");
                queryParameters.Append(", ");
                queryParameters.Append("@workGrossHours");
                queryParameters.Append(", ");
                queryParameters.Append("@workState");
                queryParameters.Append(", ");
                queryParameters.Append("@workStepContent");
                queryParameters.Append(", ");
                queryParameters.Append("@resourceHours");
                queryParameters.Append(", ");
                queryParameters.Append("@humanHours");
                queryParameters.Append(", ");
                queryParameters.Append("@remark");
                queryParameters.Append(", ");
                queryParameters.Append("@disHoursState");
                queryParameters.Append(", ");
                queryParameters.Append("@disNumberState");
                queryParameters.Append(", ");
                queryParameters.Append("@addHumanID");
                queryParameters.Append(", ");
                queryParameters.Append("@disHumanID");
                queryParameters.Append(", ");
                queryParameters.Append("@planTotalHours");
                queryParameters.Append(", ");
                queryParameters.Append("@matchCode");
                queryParameters.Append(", ");
                queryParameters.Append("@dataFrom");

                string query = String.Format("Insert Into WorkStepInfo ({0}) Values ({1})", queryParameters.ToString().Replace("@", ""), queryParameters.ToString());
                conn.Execute(query, model, tran);
            }
        }

        /// <summary>
        /// 插入工艺相关性
        /// </summary>
        public static void InsertTechnicsRestrict(
            System.Data.IDbConnection conn
            , System.Data.IDbTransaction tran
            , List<EACT_CUPRUM> CupRumList) 
        {
            var querySql = new StringBuilder();
            querySql.AppendLine("select mpt1.ID as preOperationID,mpt2.ID as operationID");
            querySql.AppendLine("from MouldPartTechnics mpt1");
            querySql.AppendLine("inner join MouldPartTechnics mpt2 on mpt1.partMonitorID = mpt2.partMonitorID ");
            querySql.AppendLine(" and mpt2.operationOrderID = mpt1.operationOrderID+1");
            querySql.AppendLine("where mpt1.partID in (");
            querySql.Append(string.Join(",", Enumerable.Select(CupRumList, u => string.Format("'{0}'", u.MouldPart_PartID)).Distinct().ToArray()));
            querySql.AppendLine(")");
            querySql.AppendLine("order by mpt1.operationOrderID");

            var list = conn.Query<TechnicsRestrict>(querySql.ToString(), null, tran);

            foreach (var item in list) 
            {
                string sql = " insert into TechnicsRestrict(preOperationID,operationID) values(@preOperationID,@operationID) ";
                conn.Execute(sql, item);
            }
            
        }

        /// <summary>
        /// 插入工艺历史记录
        /// </summary>
        public static void InsertDelMouldPartTechRecord(
             System.Data.IDbConnection conn
            , System.Data.IDbTransaction tran
            , List<EACT_CUPRUM> CupRumList
            ,Human human
            ) 
        {
            var sql = new StringBuilder();
            sql.AppendLine("select projectID,productID,mouldID,partID,partMonitorID");
            sql.AppendLine(",operationOrderID,operationName,operationNameID,operationContent,equipType");
            sql.AppendLine(",processingHoursQuota,type,priority,singleOperationTime,  addRecordsPerson as modifyRecordsPerson,");
            sql.AppendLine("getDate() AS modifyRecordsTime,'添加' AS modifyType,versionInfo , ");
            sql.AppendLine("(select top 1 resourceID from ResourceUse as dmp where dmp.operationID = ID ) AS resourceID,isnull(addRecordsPerson,@humanID) as operator");
            sql.AppendLine(",addRecordsTime AS recordTime,ID as autoMTID  ");
            sql.AppendLine("from MouldPartTechnics WHERE partID in (");
            sql.Append(string.Join(",", Enumerable.Select(CupRumList, u => string.Format("'{0}'", u.MouldPart_PartID)).Distinct().ToArray()));
            sql.AppendLine(")");
            var list = conn.Query<t_ZHCX_DelMouldPartTechRecord>(sql.ToString(), new { humanID=human.humanID }, tran);

            foreach (var item in list) 
            {
                var model = item;
                StringBuilder queryParameters = new StringBuilder();
                queryParameters.Append("@projectID");
                queryParameters.Append(", ");
                queryParameters.Append("@productID");
                queryParameters.Append(", ");
                queryParameters.Append("@mouldID");
                queryParameters.Append(", ");
                queryParameters.Append("@partID");
                queryParameters.Append(", ");
                queryParameters.Append("@partMonitorID");
                queryParameters.Append(", ");
                queryParameters.Append("@operationOrderID");
                queryParameters.Append(", ");
                queryParameters.Append("@operationName");
                queryParameters.Append(", ");
                queryParameters.Append("@operationContent");
                queryParameters.Append(", ");
                queryParameters.Append("@equipType");
                queryParameters.Append(", ");
                queryParameters.Append("@processingHoursQuota");
                queryParameters.Append(", ");
                queryParameters.Append("@type");
                queryParameters.Append(", ");
                queryParameters.Append("@priority");
                queryParameters.Append(", ");
                queryParameters.Append("@singleOperationTime");
                queryParameters.Append(", ");
                queryParameters.Append("@modifyRecordsPerson");
                queryParameters.Append(", ");
                queryParameters.Append("@modifyRecordsTime");
                queryParameters.Append(", ");
                queryParameters.Append("@modifyType");
                queryParameters.Append(", ");
                queryParameters.Append("@versionInfo");
                queryParameters.Append(", ");
                queryParameters.Append("@resourceID");
                queryParameters.Append(", ");
                queryParameters.Append("@operator");
                queryParameters.Append(", ");
                queryParameters.Append("@recordTime");
                queryParameters.Append(", ");
                queryParameters.Append("@autoMTID");

                string query = String.Format("Insert Into t_ZHCX_DelMouldPartTechRecord ({0}) Values ({1})", queryParameters.ToString().Replace("@", ""), queryParameters.ToString());

                conn.Execute(query, model, tran);
            } 
        }
        /// <summary>
        /// 插入工艺资源
        /// </summary>
        public static void InsertResourceUse(
             System.Data.IDbConnection conn
            , System.Data.IDbTransaction tran
            , List<EACT_CUPRUM> CupRumList
            ,List<TypicalSeparateResourceUseEx> tsrus
            ) 
        {
            foreach (var item in CupRumList) 
            {
                var tsru = tsrus.FirstOrDefault(u => u.TMPT_PARTID == item.TypicalPartID && u.MPT_PARTID == item.MouldPart_PartID);
                if (tsru == null) throw new Exception(string.Format("插入工艺资源失败 典型工件：{0} 工件ID:{1}", item.TypicalPartID, item.MouldPart_PartID));
                var model = new ResourceUse();
                model.operationID = tsru.MPT_ID;
                model.resourceID = tsru.resourceID;
                model.priorityID = tsru.priorityID;
                model.useUnit = tsru.useUnit;
                var sql = new StringBuilder();
                sql.AppendLine(" insert into ResourceUse(operationID,resourceID,priorityID,useUnit)");
                sql.AppendLine("values(@operationID,@resourceID,@priorityID,@useUnit)");
                conn.Execute(sql.ToString(), model, tran);
            }
        }

        /// <summary>
        /// 插入工艺
        /// </summary>
        public static void InsertMouldPartTechnics(
            System.Data.IDbConnection conn
            , System.Data.IDbTransaction tran
            , List<EACT_CUPRUM> CupRumList
            ,Eman_Mould mouldInfo
            ,Human human,
            List<TypicalSeparatePartTechnic> tspts
            ) 
        {
            foreach (var item in CupRumList) 
            {
                var tspt = tspts.FirstOrDefault(u => u.partID == item.TypicalPartID);
                if (tspt == null) 
                {
                    throw new Exception(string.Format("插入工艺失败 典型工件ID:{0}",item.TypicalPartID));
                }
                var model = new MouldPartTechnic();
                var number = item.CUPRUMCOUNT;
                model.projectID = mouldInfo.projectID;
                model.productID = mouldInfo.productID;
                model.mouldID = mouldInfo.mouldID;
                model.partID = item.MouldPart_PartID;
                model.partMonitorID = item.MouldPart_ID;
                model.operationOrderID = tspt.operationOrderID;
                model.operationNameID = tspt.operationNameID;
                model.operationName = tspt.operationName;
                model.operationContent = tspt.operationContent;
                model.planResourceID = tspt.planResourceID;
                double tempProcessingHoursQuota = 0;
                double.TryParse(tspt.processingHoursQuota, out tempProcessingHoursQuota);
                model.processingHoursQuota = tempProcessingHoursQuota * number;
                model.singleOperationTime = tempProcessingHoursQuota;
                model.addRecordsPerson = human.humanID;
                model.addRecordsTime = DateTime.Now;
                model.dataFrom = "eact";
                model.partNums = number;
                model.existsWorks = tspt.existsWorks;
                StringBuilder queryParameters = new StringBuilder();
                queryParameters.Append("@projectID");
                queryParameters.Append(", ");
                queryParameters.Append("@productID");
                queryParameters.Append(", ");
                queryParameters.Append("@mouldID");
                queryParameters.Append(", ");
                queryParameters.Append("@partID");
                queryParameters.Append(", ");
                queryParameters.Append("@partMonitorID");
                queryParameters.Append(", ");
                queryParameters.Append("@operationOrderID");
                queryParameters.Append(", ");
                queryParameters.Append("@monitorOrder");
                queryParameters.Append(", ");
                queryParameters.Append("@operationNameID");
                queryParameters.Append(", ");
                queryParameters.Append("@operationName");
                queryParameters.Append(", ");
                queryParameters.Append("@operationContent");
                queryParameters.Append(", ");
                queryParameters.Append("@planResourceID");
                queryParameters.Append(", ");
                queryParameters.Append("@processingHoursQuota");
                queryParameters.Append(", ");
                queryParameters.Append("@singleOperationTime");
                queryParameters.Append(", ");
                queryParameters.Append("@addRecordsPerson");
                queryParameters.Append(", ");
                queryParameters.Append("@addRecordsTime");
                queryParameters.Append(", ");
                queryParameters.Append("@existsWorks");
                queryParameters.Append(", ");
                queryParameters.Append("@partNums");
                queryParameters.Append(", ");
                queryParameters.Append("@dataFrom");

                string query = String.Format("Insert Into MouldPartTechnics ({0}) Values ({1})", queryParameters.ToString().Replace("@", ""), queryParameters.ToString());
                conn.Execute(query, model, tran);
            }
        }

        /// <summary>
        /// 插入BOM
        /// </summary>
        public static void InsertBOM(
             System.Data.IDbConnection conn
            , System.Data.IDbTransaction tran
            , List<EACT_CUPRUM> CupRumList
            ,Eman_Mould mouldInfo
            ,Human human
            ,List<DataAccess.Model.BOM> vil
            ) 
        {
            int bomOrderID = vil.Count();
            foreach (var item in CupRumList) 
            {
                var vi = vil.FirstOrDefault(u => u.verifyID == item.CUPRUMSN);
                if (vi == null) 
                {
                    var model = new DataAccess.Model.BOM();
                    model.mouldID = mouldInfo.mouldID;
                    model.partID = item.StandardPartNodeID;
                    model.partName = item.verifyIDName;
                    model.partType = item.MaterialClass;
                    model.specifications = item.EDMCONDITIONSN.Replace('x', 'X');
                    model.material = item.MaterialID;
                    model.unit = item.StandardStore_Unit;
                    model.number = item.CUPRUMCOUNT;
                    model.state = "planStock";
                    model.orderID = ++bomOrderID;
                    model.createTime = DateTime.Now;
                    model.orderState = "needOrderd";
                    model.isElectrodeImport = 1;
                    model.materialID = item.McfMaterialID;
                    model.isSubmit = false;
                    model.verifyID = item.CUPRUMSN;
                    model.dataFrom = "eact";
                    model.createMan = human.humanID;
                    model.certainMaterialID = item.CertainMaterialID;
                    model.brandID = 0;
                    StringBuilder queryParameters = new StringBuilder();
                    queryParameters.Append("@mouldID");
                    queryParameters.Append(", ");
                    queryParameters.Append("@partID");
                    queryParameters.Append(", ");
                    queryParameters.Append("@partName");
                    queryParameters.Append(", ");
                    queryParameters.Append("@partType");
                    queryParameters.Append(", ");
                    queryParameters.Append("@specifications");
                    queryParameters.Append(", ");
                    queryParameters.Append("@material");
                    queryParameters.Append(", ");
                    queryParameters.Append("@number");
                    queryParameters.Append(", ");
                    queryParameters.Append("@unit");
                    queryParameters.Append(", ");
                    queryParameters.Append("@state");
                    queryParameters.Append(", ");
                    queryParameters.Append("@orderID");
                    queryParameters.Append(", ");
                    queryParameters.Append("@createTime");
                    queryParameters.Append(", ");
                    queryParameters.Append("@orderState");
                    queryParameters.Append(", ");
                    queryParameters.Append("@isElectrodeImport");
                    queryParameters.Append(", ");
                    queryParameters.Append("@materialID");
                    queryParameters.Append(", ");
                    queryParameters.Append("@isSubmit");
                    queryParameters.Append(", ");
                    queryParameters.Append("@verifyID");
                    queryParameters.Append(", ");
                    queryParameters.Append("@dataFrom");
                    queryParameters.Append(", ");
                    queryParameters.Append("@createMan");
                    queryParameters.Append(", ");
                    queryParameters.Append("@certainMaterialID");
                    queryParameters.Append(", ");
                    queryParameters.Append("@brandID");

                    string query = String.Format("Insert Into BOM ({0}) Values ({1})", queryParameters.ToString().Replace("@", ""), queryParameters.ToString());
                    conn.Execute(query, model, tran);
                }
            }
        }
        /// <summary>
        /// 插入工件历史
        /// </summary>
        public static void InsertMouldPartHistory(
              System.Data.IDbConnection conn
            , System.Data.IDbTransaction tran
            , List<EACT_CUPRUM> CupRumList
            , Eman_Mould mouldInfo
            ,Human human
            ) 
        {
            var sql = new StringBuilder();
            sql.AppendLine("SELECT projectID ,productID ,mouldID ,partID ,partOrderID");
            sql.AppendLine(",partName ,partClassID ,materialID ,designer ,designDate");
            sql.AppendLine(",machiningAttribute ,remark ,STATE ,partType ,partNums");
            sql.AppendLine(",partFigID ,verifyID ,priority ,standardNumber ,specifications");
            sql.AppendLine(",addRecordsPerson AS modifyRecordsPerson ,addRecordsTime AS modifyRecordsTime ,'添加' AS modifyType ,isnull(versionInfo, 0) versionInfo ,ID");
            sql.AppendLine(",userField1 ,userField2 ,spareNum");
            sql.AppendLine("FROM mouldpart");
            sql.AppendLine("where partID in (");
            sql.Append(string.Join(",", Enumerable.Select(CupRumList, u => string.Format("'{0}'", u.MouldPart_PartID)).Distinct().ToArray()));
            sql.AppendLine(")");
            var list = conn.Query<HistoryMouldPart>(sql.ToString(), new { humanID = human.humanID }, tran);

            foreach (var item in list) 
            {
                var model = item;
                StringBuilder queryParameters = new StringBuilder();
                queryParameters.Append("@projectID");
                queryParameters.Append(", ");
                queryParameters.Append("@productID");
                queryParameters.Append(", ");
                queryParameters.Append("@mouldID");
                queryParameters.Append(", ");
                queryParameters.Append("@partID");
                queryParameters.Append(", ");
                queryParameters.Append("@partOrderID");
                queryParameters.Append(", ");
                queryParameters.Append("@partName");
                queryParameters.Append(", ");
                queryParameters.Append("@partClassID");
                queryParameters.Append(", ");
                queryParameters.Append("@materialID");
                queryParameters.Append(", ");
                queryParameters.Append("@designer");
                queryParameters.Append(", ");
                queryParameters.Append("@designDate");
                queryParameters.Append(", ");
                queryParameters.Append("@machiningAttribute");
                queryParameters.Append(", ");
                queryParameters.Append("@remark");
                queryParameters.Append(", ");
                queryParameters.Append("@state");
                queryParameters.Append(", ");
                queryParameters.Append("@partType");
                queryParameters.Append(", ");
                queryParameters.Append("@partNums");
                queryParameters.Append(", ");
                queryParameters.Append("@partFigID");
                queryParameters.Append(", ");
                queryParameters.Append("@verifyID");
                queryParameters.Append(", ");
                queryParameters.Append("@priority");
                queryParameters.Append(", ");
                queryParameters.Append("@standardNumber");
                queryParameters.Append(", ");
                queryParameters.Append("@specifications");
                queryParameters.Append(", ");
                queryParameters.Append("@modifyRecordsPerson");
                queryParameters.Append(", ");
                queryParameters.Append("@modifyRecordsTime");
                queryParameters.Append(", ");
                queryParameters.Append("@modifyType");
                queryParameters.Append(", ");
                queryParameters.Append("@versionInfo");
                queryParameters.Append(", ");
                queryParameters.Append("@ID");
                queryParameters.Append(", ");
                queryParameters.Append("@userField1");
                queryParameters.Append(", ");
                queryParameters.Append("@userField2");
                queryParameters.Append(", ");
                queryParameters.Append("@spareNum");

                string query = String.Format("Insert Into HistoryMouldPart ({0}) Values ({1})", queryParameters.ToString().Replace("@", ""), queryParameters.ToString());
                conn.Execute(query, model, tran);
            }
        }

        /// <summary>
        /// 插入工件
        /// </summary>
        public static void InsertMouldPart(
             System.Data.IDbConnection conn
            , System.Data.IDbTransaction tran
            , List<EACT_CUPRUM> CupRumList
            ,Eman_Mould mouldInfo
            ,List<string> dbPartIDList
            ,Human human
            ) 
        {
            foreach (var item in CupRumList) 
            {
                var model=new MouldPart();
                model.projectID = mouldInfo.projectID;
                model.productID = mouldInfo.productID;
                model.mouldID = mouldInfo.mouldID;
                model.partID = string.Format("{0}{1}",mouldInfo.mouldID, GenerateAutoCode.GetRandomString(dbPartIDList));
                item.MouldPart_PartID = model.partID;
                model.partClassID = item.PartClassID;
                model.materialID = item.MaterialID;
                model.designer=human.humanID;
                model.partName = item.verifyIDName;
                model.designDate=DateTime.Now;
                model.remark="电极设计导入";
                model.partType = 20;
                model.state="已设计";
                model.specifications=item.EDMCONDITIONSN.Replace('x','X');
                model.verifyID=item.CUPRUMSN;
                model.addRecordsPerson=model.designer;
                model.partNums = item.CUPRUMCOUNT;
                model.dataFrom = "eact";
                StringBuilder queryParameters = new StringBuilder();
                queryParameters.Append("@projectID");
                queryParameters.Append(", ");
                queryParameters.Append("@productID");
                queryParameters.Append(", ");
                queryParameters.Append("@mouldID");
                queryParameters.Append(", ");
                queryParameters.Append("@partID");
                queryParameters.Append(", ");
                queryParameters.Append("@partClassID");
                queryParameters.Append(", ");
                queryParameters.Append("@partName");
                queryParameters.Append(", ");
                queryParameters.Append("@materialID");
                queryParameters.Append(", ");
                queryParameters.Append("@designer");
                queryParameters.Append(", ");
                queryParameters.Append("@designDate");
                queryParameters.Append(", ");
                queryParameters.Append("@remark");
                queryParameters.Append(", ");
                queryParameters.Append("@partType");
                queryParameters.Append(", ");
                queryParameters.Append("@state");
                queryParameters.Append(", ");
                queryParameters.Append("@specifications");
                queryParameters.Append(", ");
                queryParameters.Append("@verifyID");
                queryParameters.Append(", ");
                queryParameters.Append("@addRecordsPerson");
                queryParameters.Append(", ");
                queryParameters.Append("@partNums");
                queryParameters.Append(", ");
                queryParameters.Append("@spareNum");
                queryParameters.Append(", ");
                queryParameters.Append("@dataFrom");

                string query = String.Format("Insert Into MouldPart ({0}) output inserted.ID Values ({1})", queryParameters.ToString().Replace("@", ""), queryParameters.ToString());
                var result = conn.ExecuteScalar(query, model, tran, null, null);
                model.ID = int.Parse(result.ToString());
                item.MouldPart_ID = model.ID;
            }  
        }

        /// <summary>
        /// 匹配典型电极工件
        /// </summary>
        public static void CheckTypicalSeparatePart(
            System.Data.IDbConnection conn
            , System.Data.IDbTransaction tran
            , List<EACT_CUPRUM> CupRumList
            ) 
        {
            var sql = new StringBuilder();
            sql.AppendLine(" select * ");
            sql.AppendLine(" from TypicalSeparatePart ");
            sql.AppendLine(" where partType = 20");
            var tsps = conn.Query<TypicalSeparatePart>(sql.ToString(), null, tran).ToList();
            foreach (var item in CupRumList) 
            {
                var tsp1=tsps.FirstOrDefault(u => u.partName == item.verifyIDName);
                if (tsp1 == null)
                {
                    var tsp2 = tsps.FirstOrDefault(u => u.partName == "电极");
                    if (tsps == null)
                    {
                        throw new Exception(string.Format("系统中不存在典型电极工件：{0}", item.verifyIDName));
                    }
                    else 
                    {
                        item.TypicalPartID = tsp2.partID;
                        item.PartClassID = tsp2.partClassID;
                    }
                }
                else 
                {
                    item.TypicalPartID = tsp1.partID;
                    item.PartClassID = tsp1.partClassID;
                }
            }
        }

        /// <summary>
        /// 匹配电极类型标准工件
        /// </summary>
        public static void CheckStandardPart(
            System.Data.IDbConnection conn
            , System.Data.IDbTransaction tran
            , List<EACT_CUPRUM> CupRumList
            ) 
        {
            var sql = new StringBuilder();
            sql.AppendLine("SELECT *,standardPartID as standardPartNodeID,ID as standarPartID ");
            sql.AppendLine(",standardPartName as partName ");
            sql.AppendLine("	FROM StandardPart ");
            sql.AppendLine("	WHERE partType = 1 AND mark <> 'delete' ");
            sql.AppendLine("	ORDER BY standardPartID desc ");
            var sps = conn.Query<StandardPart>(sql.ToString(), null, tran).ToList();
            foreach (var item in CupRumList) 
            {
                item.verifyIDName = string.Format("{0}电极",item.STRUFF);
                var sp = sps.FirstOrDefault(u => u.standardPartName == item.verifyIDName);
                if (sp == null)
                {
                    throw new Exception(string.Format("系统中不存在电极类型标准工件名称：{0}", item.verifyIDName));
                }
                else 
                {
                    item.StandardPartNodeID = sp.standardPartID;
                }
            }
        }

        /// <summary>
        /// 匹配材质
        /// </summary>
        public static void CheckMaterial(
            System.Data.IDbConnection conn
            , System.Data.IDbTransaction tran,
            List<EACT_CUPRUM> CupRumList
            ) 
        {
            var sql = new StringBuilder();
            sql.AppendLine("SELECT   MaterialID, MaterialName, CASE WHEN materialID = 'mc_001' THEN '' ELSE substring(materialID, 0, len(materialID) - 4) END AS ParentID ");
            sql.AppendLine("FROM    dbo.MaterialClassify where dbo.MaterialClassify.materialName ='电极设计'");
            //获取所有的材质类型
            //TODO 匹配标准料
            var mcs = conn.Query<MaterialClassify>(sql.ToString(), null, tran).ToList();
            var tempMcs = mcs.ToList();
            mcs.Clear();
            foreach (var item in CupRumList)
            {
                var mc = tempMcs.FirstOrDefault(u => u.MaterialName == item.STRUFF);
                if (!string.IsNullOrEmpty(item.STRUFF)
                    && mc != null
                    && mcs.FirstOrDefault(u => u.MaterialName == item.STRUFF) == null)
                {
                    mcs.Add(mc);
                }
            }

            var sds = new List<StandardStoreEx>();
            if (mcs.Count > 0)
            {
                sql = new StringBuilder();
                sql.AppendLine("select *,m.materialID as Material_MaterialID,mc.materialID as mcfMaterialID,mc.materialClass,t_JCXX_CertainMaterial.materialID as certainMaterialID");
                sql.AppendLine("from StandardStore ss inner join  MaterialClassify mc on ss.materialID=mc.MaterialID inner join Material m on m.materialName=ss.specifications  ");
                sql.AppendLine("left join t_JCXX_CertainMaterial on ss.storeID = t_JCXX_CertainMaterial.storeID and t_JCXX_CertainMaterial.brandId = 0 and t_JCXX_CertainMaterial.useState = 1");
                sql.AppendLine("where ss.materialID in");
                sql.AppendLine("(");
                sql.Append(string.Join(",", Enumerable.Select(mcs, u => string.Format("'{0}'", u.MaterialID)).Distinct().ToArray()));
                sql.AppendLine(")");
                sql.AppendLine("and ss.mark <> 1");
                sds = conn.Query<StandardStoreEx>(sql.ToString(), null, tran).ToList();
            }

            //TODO 匹配长宽高
            foreach (var item in CupRumList)
            {
                var spec=Enumerable.Select(item.EDMCONDITIONSN.Split('x').ToList(),u=>{var tempDValue=double.MaxValue;double.TryParse(u,out tempDValue);return tempDValue;}).ToList();
                if (spec.Count >= 3)
                {
                    var tempSds = sds.Where(u => u.MaterialName == item.STRUFF && u.X > 0 && u.Y > 0 && u.Z > 0).OrderBy(u => u.X).ThenBy(u => u.Y).ThenBy(u => u.Z).ToList();
                    var sd = tempSds.FirstOrDefault(u => u.X >= spec[0] && u.Y >= spec[1] && u.Z >= spec[2]);
                    if (sd != null && sd.certainMaterialID != null)
                    {
                        //匹配到标准料
                        item.STRUFFCODE = sd.specNumber;
                        item.MaterialID = sd.Material_MaterialID;
                        item.McfMaterialID = sd.mcfMaterialID;
                        item.MaterialClass = sd.materialClass;
                        item.StandardStore_Unit = sd.unit;
                        item.CertainMaterialID = (int)sd.certainMaterialID;
                    }
                } 
            }

            var ms = new List<string>();
            foreach (var item in CupRumList.Where(u=>string.IsNullOrEmpty(u.STRUFFCODE)))
            {
                if (!string.IsNullOrEmpty(item.STRUFF) && !ms.Contains(item.STRUFF)) 
                {
                    ms.Add(item.STRUFF);
                }
            }
            
            //TODO 匹配主材质
            sql = new StringBuilder();
            sql.AppendLine("select *,m.materialID as Material_MaterialID,mc.materialID as mcfMaterialID,mc.materialClass,t_JCXX_CertainMaterial.materialID as certainMaterialID");
            sql.AppendLine("from StandardStore ss ");
            sql.AppendLine("inner join  MaterialClassify mc on ss.materialID=mc.MaterialID inner join Material m on m.materialName=ss.specifications ");
            sql.AppendLine("left join t_JCXX_CertainMaterial on ss.storeID = t_JCXX_CertainMaterial.storeID and t_JCXX_CertainMaterial.brandId = 0 and t_JCXX_CertainMaterial.useState = 1");
            sql.AppendLine("where ss.materialID like 'mc_001_0001_%' ");
            sql.AppendLine("and ss.mark <> 1");
            if (ms.Count > 0)
            {
                sql.AppendLine("and m.materialName in ");
                sql.AppendLine("(");
                sql.Append(string.Join(",", Enumerable.Select(ms, u => string.Format("'{0}'", u)).Distinct().ToArray()));
                sql.AppendLine(")");
            }
            else 
            {
                sql.AppendLine("and 1<>1");
            }
           

            var sses=conn.Query<StandardStoreEx>(sql.ToString(), null, tran).ToList();

            foreach (var item in CupRumList.Where(u => string.IsNullOrEmpty(u.STRUFFCODE)))
            {
                var sse = sses.FirstOrDefault(u => u.specifications == item.STRUFF);
                if (sse!=null&&sse.certainMaterialID != null)
                {
                    item.STRUFFCODE = sse.specNumber;
                    item.MaterialID = sse.Material_MaterialID;
                    item.McfMaterialID = sse.mcfMaterialID;
                    item.MaterialClass = sse.materialClass;
                    item.StandardStore_Unit = sse.unit;
                    item.CertainMaterialID = (int)sse.certainMaterialID;
                }
                else
                {
                    throw new Exception(string.Format("系统中不存在此标准料唯一标识 {0}", item.STRUFF));
                }
            }

            var cuprumList = CupRumList.Where(u => string.IsNullOrEmpty(u.STRUFFCODE));

            if (cuprumList.Count() > 0) 
            {
                throw new Exception(string.Format("系统中不存在此材质{0}",cuprumList.First().STRUFF));
            }
        }

        /// <summary>
        /// 获取模具信息
        /// </summary>
        public static Eman_Mould CheckMouldInfo(
            System.Data.IDbConnection conn
            , System.Data.IDbTransaction tran, string mouldInteriorID
            ) 
        {
            //找到EMan系统对应的模具
            var sql = new StringBuilder();
            sql.AppendLine("SELECT Mould.projectID");
            sql.AppendLine(",Mould.productID");
            sql.AppendLine(",Mould.mouldID");
            sql.AppendLine("FROM Mould");
            sql.AppendLine("INNER JOIN Project ON Mould.projectID = Project.projectID");
            sql.AppendLine("INNER JOIN Product ON Mould.productID = Product.productID");
            sql.AppendLine("WHERE Project.mark <> 'delete'");
            sql.AppendLine("AND Product.productID <> 'delete'");
            sql.AppendLine("AND Mould.mark <> 'delete'");
            sql.AppendLine("AND Mould.mouldInteriorID = @mouldInteriorID");

            var eman_Mould = conn.Query<Eman_Mould>(sql.ToString(), new { mouldInteriorID = mouldInteriorID }, tran).FirstOrDefault();
            if (eman_Mould == null)
            {
                throw new Exception(string.Format("系统中不存在此模具:{0}", mouldInteriorID));
            }
            return eman_Mould;
        }

        /// <summary>
        /// 导入Eman
        /// </summary>
        public static void ImportEman(
            System.Data.IDbConnection conn
            , System.Data.IDbTransaction tran
            , List<EACT_CUPRUM> CupRumList
            )
        {
            string projectID = string.Empty; //项目 ID
            string productID = string.Empty; //制品 ID
            string mouldID = string.Empty; //模具 ID
            string partName = string.Empty; //工件 名称
            string partID = string.Empty; //工件 ID
            string verifyID = string.Empty; //件号
            string materialID = string.Empty; //Eact 传入的材质 ID

            //找到EMan系统对应的标准工件名称
            var sql = new StringBuilder();
            sql.AppendLine("SELECT TOP 1  standardPartName");
            sql.AppendLine("FROM StandardPart");
            sql.AppendLine("WHERE partType = 1");
            sql.AppendLine("AND mark <> 'delete'");
            sql.AppendLine("ORDER BY standardPartID");

            var partNameObj = conn.ExecuteScalar(sql.ToString(), null, tran, null, null);
            if (partNameObj != null)
            {
                partName = partNameObj.ToString();
            }
            else
            {
                partName = "电极";
            }

            //找到EMan系统对应的模具
            sql = new StringBuilder();
            sql.AppendLine("SELECT Mould.projectID");
            sql.AppendLine(",Mould.productID");
            sql.AppendLine(",Mould.mouldID");
            sql.AppendLine("FROM Mould");
            sql.AppendLine("INNER JOIN Project ON Mould.projectID = Project.projectID");
            sql.AppendLine("INNER JOIN Product ON Mould.productID = Product.productID");
            sql.AppendLine("WHERE Project.mark <> 'delete'");
            sql.AppendLine("AND Product.productID <> 'delete'");
            sql.AppendLine("AND Mould.mark <> 'delete'");
            sql.AppendLine("AND Mould.mouldInteriorID = @mouldInteriorID");

            var eman_Mould = conn.Query<Eman_Mould>(sql.ToString(), new { mouldInteriorID = "" }, tran).FirstOrDefault();
            if (eman_Mould == null)
            {
                throw new Exception(string.Format("系统中不存在此模具:{0}", ""));
            }
            else
            {
                mouldID = eman_Mould.mouldID;
                productID = eman_Mould.productID;
                projectID = eman_Mould.projectID;
            }

            //匹配标准料
            sql = new StringBuilder();
            sql.AppendLine("SELECT   materialID, materialName, CASE WHEN materialID = 'mc_001' THEN '' ELSE substring(materialID, 0, len(materialID) - 4) ");
            sql.AppendLine(" END AS parentID");
            sql.AppendLine("FROM      dbo.MaterialClassify");
            sql.AppendLine("WHERE   materialID in (select materialID from MaterialClassify where materialName='电极设计') and (type = 1) AND (mark <> 1)");
            sql.AppendLine("and materialName=@materialName");

            //匹配主材料
            sql = new StringBuilder();
            sql.AppendLine("select * from Material where materialName=@materialName");
        }
    }
}
