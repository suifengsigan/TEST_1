using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
namespace DataAccess
{
    #region EACT_MOULD
    /// <summary>
/// This object represents the properties and methods of a EACT_MOULD .
    /// </summary>
    public class EACT_MOULD
    {
    #region Properties //do not update!
        private double _m_ID;
        //[Dapper.Key]
        public virtual double M_ID
        {
        get {return _m_ID;}
        set {_m_ID = value;}
        }
        private string _m_SN = String.Empty;
        public virtual string SN
        {
        get {return _m_SN;}
        set {_m_SN = value;}
        }
        private DateTime? _m_UPDATETIME;
        public virtual DateTime? M_UPDATETIME
        {
        get {return _m_UPDATETIME;}
        set {_m_UPDATETIME = value;}
        }
        private string _m_UPDATEUSE = String.Empty;
        public virtual string M_UPDATEUSE
        {
        get {return _m_UPDATEUSE;}
        set {_m_UPDATEUSE = value;}
        }
    #endregion
        public EACT_MOULD Copy()
         {
         var model = new EACT_MOULD ();
         model.M_ID = this.M_ID;
         model.SN = this.SN;
         model.M_UPDATETIME = this.M_UPDATETIME;
         model.M_UPDATEUSE = this.M_UPDATEUSE;
         return model;
         }
    }
    #endregion
}
