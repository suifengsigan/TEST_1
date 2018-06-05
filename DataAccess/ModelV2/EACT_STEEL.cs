using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
namespace DataAccess.ModelV2
{
    #region EACT_STEEL
    /// <summary>
/// This object represents the properties and methods of a EACT_STEEL .
    /// </summary>
    public class EACT_STEEL
    {
    #region Properties //do not update!
        private double _sT_ID;
        //[Dapper.Key]
        public virtual double ST_ID
        {
        get {return _sT_ID;}
        set {_sT_ID = value;}
        }
        private double _m_ID;
        public virtual double M_ID
        {
        get {return _m_ID;}
        set {_m_ID = value;}
        }
        private string _sT_SN = String.Empty;
        public virtual string ST_SN
        {
        get {return _sT_SN;}
        set {_sT_SN = value;}
        }
        private string _sT_DSIZE = String.Empty;
        public virtual string ST_DSIZE
        {
        get {return _sT_DSIZE;}
        set {_sT_DSIZE = value;}
        }
        private string _sT_MTYPE = String.Empty;
        public virtual string ST_MTYPE
        {
        get {return _sT_MTYPE;}
        set {_sT_MTYPE = value;}
        }
        private string _sT_REMARK = String.Empty;
        public virtual string ST_REMARK
        {
        get {return _sT_REMARK;}
        set {_sT_REMARK = value;}
        }
        private DateTime? _sT_UPDATETIME;
        public virtual DateTime? ST_UPDATETIME
        {
        get {return _sT_UPDATETIME;}
        set {_sT_UPDATETIME = value;}
        }
        private string _sT_UPDATEUSE = String.Empty;
        public virtual string ST_UPDATEUSE
        {
        get {return _sT_UPDATEUSE;}
        set {_sT_UPDATEUSE = value;}
        }
    #endregion
        public EACT_STEEL Copy()
         {
         var model = new EACT_STEEL ();
         model.ST_ID = this.ST_ID;
         model.M_ID = this.M_ID;
         model.ST_SN = this.ST_SN;
         model.ST_DSIZE = this.ST_DSIZE;
         model.ST_MTYPE = this.ST_MTYPE;
         model.ST_REMARK = this.ST_REMARK;
         model.ST_UPDATETIME = this.ST_UPDATETIME;
         model.ST_UPDATEUSE = this.ST_UPDATEUSE;
         return model;
         }
    }
    #endregion
}
