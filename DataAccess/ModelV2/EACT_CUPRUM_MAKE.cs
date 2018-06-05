using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
namespace DataAccess.ModelV2
{
    #region EACT_CUPRUM_MAKE
    /// <summary>
/// This object represents the properties and methods of a EACT_CUPRUM_MAKE .
    /// </summary>
    public class EACT_CUPRUM_MAKE
    {
    #region Properties //do not update!
        private double _cMK_ID;
        //[Dapper.Key]
        public virtual double CMK_ID
        {
        get {return _cMK_ID;}
        set {_cMK_ID = value;}
        }
        private double _c_ID;
        public virtual double C_ID
        {
        get {return _c_ID;}
        set {_c_ID = value;}
        }
        private double? _eM_ID;
        public virtual double? EM_ID
        {
        get {return _eM_ID;}
        set {_eM_ID = value;}
        }
        private string _cMK_NAME = String.Empty;
        public virtual string CMK_NAME
        {
        get {return _cMK_NAME;}
        set {_cMK_NAME = value;}
        }
        private string _cMK_CKNO = String.Empty;
        public virtual string CMK_CKNO
        {
        get {return _cMK_CKNO;}
        set {_cMK_CKNO = value;}
        }
        private string _cMK_RFID = String.Empty;
        public virtual string CMK_RFID
        {
        get {return _cMK_RFID;}
        set {_cMK_RFID = value;}
        }
        private string _cMK_BRACODE = String.Empty;
        public virtual string CMK_BRACODE
        {
        get {return _cMK_BRACODE;}
        set {_cMK_BRACODE = value;}
        }
        private int? _cMK_RMF;
        public virtual int? CMK_RMF
        {
        get {return _cMK_RMF;}
        set {_cMK_RMF = value;}
        }
        private double? _cMK_CNCH;
        public virtual double? CMK_CNCH
        {
        get {return _cMK_CNCH;}
        set {_cMK_CNCH = value;}
        }
        private int? _cMK_CNCHTYPE;
        public virtual int? CMK_CNCHTYPE
        {
        get {return _cMK_CNCHTYPE;}
        set {_cMK_CNCHTYPE = value;}
        }
        private int? _cMK_MKSTATE;
        public virtual int? CMK_MKSTATE
        {
        get {return _cMK_MKSTATE;}
        set {_cMK_MKSTATE = value;}
        }
        private DateTime? _cMK_MKTIME;
        public virtual DateTime? CMK_MKTIME
        {
        get {return _cMK_MKTIME;}
        set {_cMK_MKTIME = value;}
        }
        private string _cMK_MKUSE = String.Empty;
        public virtual string CMK_MKUSE
        {
        get {return _cMK_MKUSE;}
        set {_cMK_MKUSE = value;}
        }
        private int? _cMK_ISPRINT;
        public virtual int? CMK_ISPRINT
        {
        get {return _cMK_ISPRINT;}
        set {_cMK_ISPRINT = value;}
        }
        private DateTime? _cMK_PRINTTIME;
        public virtual DateTime? CMK_PRINTTIME
        {
        get {return _cMK_PRINTTIME;}
        set {_cMK_PRINTTIME = value;}
        }
        private string _cMK_PRINTUSE = String.Empty;
        public virtual string CMK_PRINTUSE
        {
        get {return _cMK_PRINTUSE;}
        set {_cMK_PRINTUSE = value;}
        }
        private double? _cMK_OFFSETX;
        public virtual double? CMK_OFFSETX
        {
        get {return _cMK_OFFSETX;}
        set {_cMK_OFFSETX = value;}
        }
        private double? _cMK_OFFSETY;
        public virtual double? CMK_OFFSETY
        {
        get {return _cMK_OFFSETY;}
        set {_cMK_OFFSETY = value;}
        }
        private double? _cMK_OFFSETZ;
        public virtual double? CMK_OFFSETZ
        {
        get {return _cMK_OFFSETZ;}
        set {_cMK_OFFSETZ = value;}
        }
        private int? _cMK_OFFSETTYPE;
        public virtual int? CMK_OFFSETTYPE
        {
        get {return _cMK_OFFSETTYPE;}
        set {_cMK_OFFSETTYPE = value;}
        }
        private int? _cMK_CMMDETAILID;
        public virtual int? CMK_CMMDETAILID
        {
        get {return _cMK_CMMDETAILID;}
        set {_cMK_CMMDETAILID = value;}
        }
        private string _cMK_OFFSETUSE = String.Empty;
        public virtual string CMK_OFFSETUSE
        {
        get {return _cMK_OFFSETUSE;}
        set {_cMK_OFFSETUSE = value;}
        }
        private DateTime? _cMK_OFFSETTIME;
        public virtual DateTime? CMK_OFFSETTIME
        {
        get {return _cMK_OFFSETTIME;}
        set {_cMK_OFFSETTIME = value;}
        }
        private double? _cMK_INITFIRENUM;
        public virtual double? CMK_INITFIRENUM
        {
        get {return _cMK_INITFIRENUM;}
        set {_cMK_INITFIRENUM = value;}
        }
        private double? _cMK_FIRENUM;
        public virtual double? CMK_FIRENUM
        {
        get {return _cMK_FIRENUM;}
        set {_cMK_FIRENUM = value;}
        }
        private string _cMK_FIREEDITUSE = String.Empty;
        public virtual string CMK_FIREEDITUSE
        {
        get {return _cMK_FIREEDITUSE;}
        set {_cMK_FIREEDITUSE = value;}
        }
        private DateTime? _cMK_FIREEDITTIME;
        public virtual DateTime? CMK_FIREEDITTIME
        {
        get {return _cMK_FIREEDITTIME;}
        set {_cMK_FIREEDITTIME = value;}
        }
    #endregion
        public EACT_CUPRUM_MAKE Copy()
         {
         var model = new EACT_CUPRUM_MAKE ();
         model.CMK_ID = this.CMK_ID;
         model.C_ID = this.C_ID;
         model.EM_ID = this.EM_ID;
         model.CMK_NAME = this.CMK_NAME;
         model.CMK_CKNO = this.CMK_CKNO;
         model.CMK_RFID = this.CMK_RFID;
         model.CMK_BRACODE = this.CMK_BRACODE;
         model.CMK_RMF = this.CMK_RMF;
         model.CMK_CNCH = this.CMK_CNCH;
         model.CMK_CNCHTYPE = this.CMK_CNCHTYPE;
         model.CMK_MKSTATE = this.CMK_MKSTATE;
         model.CMK_MKTIME = this.CMK_MKTIME;
         model.CMK_MKUSE = this.CMK_MKUSE;
         model.CMK_ISPRINT = this.CMK_ISPRINT;
         model.CMK_PRINTTIME = this.CMK_PRINTTIME;
         model.CMK_PRINTUSE = this.CMK_PRINTUSE;
         model.CMK_OFFSETX = this.CMK_OFFSETX;
         model.CMK_OFFSETY = this.CMK_OFFSETY;
         model.CMK_OFFSETZ = this.CMK_OFFSETZ;
         model.CMK_OFFSETTYPE = this.CMK_OFFSETTYPE;
         model.CMK_CMMDETAILID = this.CMK_CMMDETAILID;
         model.CMK_OFFSETUSE = this.CMK_OFFSETUSE;
         model.CMK_OFFSETTIME = this.CMK_OFFSETTIME;
         model.CMK_INITFIRENUM = this.CMK_INITFIRENUM;
         model.CMK_FIRENUM = this.CMK_FIRENUM;
         model.CMK_FIREEDITUSE = this.CMK_FIREEDITUSE;
         model.CMK_FIREEDITTIME = this.CMK_FIREEDITTIME;
         return model;
         }
    }
    #endregion
}
