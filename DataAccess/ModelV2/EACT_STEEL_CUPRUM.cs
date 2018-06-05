using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
namespace DataAccess.ModelV2
{
    #region EACT_STEEL_CUPRUM
    /// <summary>
/// This object represents the properties and methods of a EACT_STEEL_CUPRUM .
    /// </summary>
    public class EACT_STEEL_CUPRUM
    {
    #region Properties //do not update!
        private double _sT_ID;
        //[Dapper.Key]
        public virtual double ST_ID
        {
        get {return _sT_ID;}
        set {_sT_ID = value;}
        }
        private double _c_ID;
        //[Dapper.Key]
        public virtual double C_ID
        {
        get {return _c_ID;}
        set {_c_ID = value;}
        }
        private double _sTC_ID;
        //[Dapper.Key]
        public virtual double STC_ID
        {
        get {return _sTC_ID;}
        set {_sTC_ID = value;}
        }
        private int? _sTC_BORROW;
        public virtual int? STC_BORROW
        {
        get {return _sTC_BORROW;}
        set {_sTC_BORROW = value;}
        }
        private int? _sTC_BORROWTYPE;
        public virtual int? STC_BORROWTYPE
        {
        get {return _sTC_BORROWTYPE;}
        set {_sTC_BORROWTYPE = value;}
        }
        private int? _sTC_BORROWID;
        public virtual int? STC_BORROWID
        {
        get {return _sTC_BORROWID;}
        set {_sTC_BORROWID = value;}
        }
        private string _sTC_CNCFILEPATH = String.Empty;
        public virtual string STC_CNCFILEPATH
        {
        get {return _sTC_CNCFILEPATH;}
        set {_sTC_CNCFILEPATH = value;}
        }
        private int? _sTC_CNCFILEEXIST;
        public virtual int? STC_CNCFILEEXIST
        {
        get {return _sTC_CNCFILEEXIST;}
        set {_sTC_CNCFILEEXIST = value;}
        }
        private DateTime? _sTC_CNCFILETIME;
        public virtual DateTime? STC_CNCFILETIME
        {
        get {return _sTC_CNCFILETIME;}
        set {_sTC_CNCFILETIME = value;}
        }
        private string _sTC_CMMFILEPATH = String.Empty;
        public virtual string STC_CMMFILEPATH
        {
        get {return _sTC_CMMFILEPATH;}
        set {_sTC_CMMFILEPATH = value;}
        }
        private int? _sTC_CMMFILEEXIST;
        public virtual int? STC_CMMFILEEXIST
        {
        get {return _sTC_CMMFILEEXIST;}
        set {_sTC_CMMFILEEXIST = value;}
        }
        private DateTime? _sTC_CMMFILETIME;
        public virtual DateTime? STC_CMMFILETIME
        {
        get {return _sTC_CMMFILETIME;}
        set {_sTC_CMMFILETIME = value;}
        }
        private DateTime? _sTC_UPDATETIME;
        public virtual DateTime? STC_UPDATETIME
        {
        get {return _sTC_UPDATETIME;}
        set {_sTC_UPDATETIME = value;}
        }
        private string _sTC_UPDATEUSE = String.Empty;
        public virtual string STC_UPDATEUSE
        {
        get {return _sTC_UPDATEUSE;}
        set {_sTC_UPDATEUSE = value;}
        }
    #endregion
        public EACT_STEEL_CUPRUM Copy()
         {
         var model = new EACT_STEEL_CUPRUM ();
         model.ST_ID = this.ST_ID;
         model.C_ID = this.C_ID;
         model.STC_ID = this.STC_ID;
         model.STC_BORROW = this.STC_BORROW;
         model.STC_BORROWTYPE = this.STC_BORROWTYPE;
         model.STC_BORROWID = this.STC_BORROWID;
         model.STC_CNCFILEPATH = this.STC_CNCFILEPATH;
         model.STC_CNCFILEEXIST = this.STC_CNCFILEEXIST;
         model.STC_CNCFILETIME = this.STC_CNCFILETIME;
         model.STC_CMMFILEPATH = this.STC_CMMFILEPATH;
         model.STC_CMMFILEEXIST = this.STC_CMMFILEEXIST;
         model.STC_CMMFILETIME = this.STC_CMMFILETIME;
         model.STC_UPDATETIME = this.STC_UPDATETIME;
         model.STC_UPDATEUSE = this.STC_UPDATEUSE;
         return model;
         }
    }
    #endregion
}
