using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
namespace DataAccess.ModelV2
{
    #region EACT_CUPRUM_POS
    /// <summary>
/// This object represents the properties and methods of a EACT_CUPRUM_POS .
    /// </summary>
    public class EACT_CUPRUM_POS
    {
    #region Properties //do not update!
        private double _cP_ID;
        //[Dapper.Key]
        public virtual double CP_ID
        {
        get {return _cP_ID;}
        set {_cP_ID = value;}
        }
        private double _sT_ID;
        public virtual double ST_ID
        {
        get {return _sT_ID;}
        set {_sT_ID = value;}
        }
        private double _c_ID;
        public virtual double C_ID
        {
        get {return _c_ID;}
        set {_c_ID = value;}
        }
        private double _sTC_ID;
        public virtual double STC_ID
        {
        get {return _sTC_ID;}
        set {_sTC_ID = value;}
        }
        private int? _cP_INDEX;
        public virtual int? CP_INDEX
        {
        get {return _cP_INDEX;}
        set {_cP_INDEX = value;}
        }
        private double? _cP_X;
        public virtual double? CP_X
        {
        get {return _cP_X;}
        set {_cP_X = value;}
        }
        private double? _cP_Y;
        public virtual double? CP_Y
        {
        get {return _cP_Y;}
        set {_cP_Y = value;}
        }
        private double? _cP_Z;
        public virtual double? CP_Z
        {
        get {return _cP_Z;}
        set {_cP_Z = value;}
        }
        private double? _cP_C;
        public virtual double? CP_C
        {
        get {return _cP_C;}
        set {_cP_C = value;}
        }
        private int? _cP_OK;
        public virtual int? CP_OK
        {
        get {return _cP_OK;}
        set {_cP_OK = value;}
        }
        private byte[] _uPDATETIME;
        public virtual byte[] UPDATETIME
        {
        get {return _uPDATETIME;}
        set {_uPDATETIME = value;}
        }
        private string _uPDATEUSE = String.Empty;
        public virtual string UPDATEUSE
        {
        get {return _uPDATEUSE;}
        set {_uPDATEUSE = value;}
        }
    #endregion
        public EACT_CUPRUM_POS Copy()
         {
         var model = new EACT_CUPRUM_POS ();
         model.CP_ID = this.CP_ID;
         model.ST_ID = this.ST_ID;
         model.C_ID = this.C_ID;
         model.STC_ID = this.STC_ID;
         model.CP_INDEX = this.CP_INDEX;
         model.CP_X = this.CP_X;
         model.CP_Y = this.CP_Y;
         model.CP_Z = this.CP_Z;
         model.CP_C = this.CP_C;
         model.CP_OK = this.CP_OK;
         model.UPDATETIME = this.UPDATETIME;
         model.UPDATEUSE = this.UPDATEUSE;
         return model;
         }
    }
    #endregion
}
