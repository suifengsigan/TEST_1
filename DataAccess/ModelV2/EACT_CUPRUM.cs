using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
namespace DataAccess.ModelV2
{
    #region EACT_CUPRUM
    /// <summary>
/// This object represents the properties and methods of a EACT_CUPRUM .
    /// </summary>
    public class EACT_CUPRUM
    {
    #region Properties //do not update!
        private double _c_ID;
        //[Dapper.Key]
        public virtual double C_ID
        {
        get {return _c_ID;}
        set {_c_ID = value;}
        }
        private double _cT_ID;
        public virtual double CT_ID
        {
        get {return _cT_ID;}
        set {_cT_ID = value;}
        }
        private double _cK_ID;
        public virtual double CK_ID
        {
        get {return _cK_ID;}
        set {_cK_ID = value;}
        }
        private DateTime? _c_INTIME;
        public virtual DateTime? C_INTIME
        {
        get {return _c_INTIME;}
        set {_c_INTIME = value;}
        }
        private string _c_PRTNAME = String.Empty;
        public virtual string C_PRTNAME
        {
        get {return _c_PRTNAME;}
        set {_c_PRTNAME = value;}
        }
        private string _c_SN = String.Empty;
        public virtual string C_SN
        {
        get {return _c_SN;}
        set {_c_SN = value;}
        }
        private double? _c_HEIGHT;
        public virtual double? C_HEIGHT
        {
        get {return _c_HEIGHT;}
        set {_c_HEIGHT = value;}
        }
        private double? _c_HEADH;
        public virtual double? C_HEADH
        {
        get {return _c_HEADH;}
        set {_c_HEADH = value;}
        }
        private double? _c_BASEH;
        public virtual double? C_BASEH
        {
        get {return _c_BASEH;}
        set {_c_BASEH = value;}
        }
        private int? _c_RCOUNT;
        public virtual int? C_RCOUNT
        {
        get {return _c_RCOUNT;}
        set {_c_RCOUNT = value;}
        }
        private int? _c_MCOUNT;
        public virtual int? C_MCOUNT
        {
        get {return _c_MCOUNT;}
        set {_c_MCOUNT = value;}
        }
        private int? _c_FCOUNT;
        public virtual int? C_FCOUNT
        {
        get {return _c_FCOUNT;}
        set {_c_FCOUNT = value;}
        }
        private string _c_SPEC = String.Empty;
        public virtual string C_SPEC
        {
        get {return _c_SPEC;}
        set {_c_SPEC = value;}
        }
        private string _c_STOCKSPEC = String.Empty;
        public virtual string C_STOCKSPEC
        {
        get {return _c_STOCKSPEC;}
        set {_c_STOCKSPEC = value;}
        }
        private double? _c_STOCKBULK;
        public virtual double? C_STOCKBULK
        {
        get {return _c_STOCKBULK;}
        set {_c_STOCKBULK = value;}
        }
        private string _c_SPECCODE = String.Empty;
        public virtual string C_SPECCODE
        {
        get {return _c_SPECCODE;}
        set {_c_SPECCODE = value;}
        }
        private double? _c_ACCURACY;
        public virtual double? C_ACCURACY
        {
        get {return _c_ACCURACY;}
        set {_c_ACCURACY = value;}
        }
        private string _c_VDI = String.Empty;
        public virtual string C_VDI
        {
        get {return _c_VDI;}
        set {_c_VDI = value;}
        }
        private string _c_CQUADRANT = String.Empty;
        public virtual string C_CQUADRANT
        {
        get {return _c_CQUADRANT;}
        set {_c_CQUADRANT = value;}
        }
        private double? _c_AREA;
        public virtual double? C_AREA
        {
        get {return _c_AREA;}
        set {_c_AREA = value;}
        }
        private string _c_ROCK = String.Empty;
        public virtual string C_ROCK
        {
        get {return _c_ROCK;}
        set {_c_ROCK = value;}
        }
        private string _c_PROCDIRECTION = String.Empty;
        public virtual string C_PROCDIRECTION
        {
        get {return _c_PROCDIRECTION;}
        set {_c_PROCDIRECTION = value;}
        }
        private string _c_MATERIAL = String.Empty;
        public virtual string C_MATERIAL
        {
        get {return _c_MATERIAL;}
        set {_c_MATERIAL = value;}
        }
        private string _c_SWAYFLAT = String.Empty;
        public virtual string C_SWAYFLAT
        {
        get {return _c_SWAYFLAT;}
        set {_c_SWAYFLAT = value;}
        }
        private string _c_PC = String.Empty;
        public virtual string C_PC
        {
        get {return _c_PC;}
        set {_c_PC = value;}
        }
        private string _c_ETCH = String.Empty;
        public virtual string C_ETCH
        {
        get {return _c_ETCH;}
        set {_c_ETCH = value;}
        }
        private string _c_EDMPLACE = String.Empty;
        public virtual string C_EDMPLACE
        {
        get {return _c_EDMPLACE;}
        set {_c_EDMPLACE = value;}
        }
        private DateTime? _c_UPDATETIME;
        public virtual DateTime? C_UPDATETIME
        {
        get {return _c_UPDATETIME;}
        set {_c_UPDATETIME = value;}
        }
        private string _c_UPDATEUSE = String.Empty;
        public virtual string C_UPDATEUSE
        {
        get {return _c_UPDATEUSE;}
        set {_c_UPDATEUSE = value;}
        }
        private string _c_REMARK = String.Empty;
        public virtual string C_REMARK
        {
        get {return _c_REMARK;}
        set {_c_REMARK = value;}
        }
        private string _c_REGION = String.Empty;
        public virtual string C_REGION
        {
        get {return _c_REGION;}
        set {_c_REGION = value;}
        }
        private int? _c_ORD;
        public virtual int? C_ORD
        {
        get {return _c_ORD;}
        set {_c_ORD = value;}
        }
        private string _c_TOEMANNAME = String.Empty;
        public virtual string C_TOEMANNAME
        {
        get {return _c_TOEMANNAME;}
        set {_c_TOEMANNAME = value;}
        }
    #endregion
        public EACT_CUPRUM Copy()
         {
         var model = new EACT_CUPRUM ();
         model.C_ID = this.C_ID;
         model.CT_ID = this.CT_ID;
         model.CK_ID = this.CK_ID;
         model.C_INTIME = this.C_INTIME;
         model.C_PRTNAME = this.C_PRTNAME;
         model.C_SN = this.C_SN;
         model.C_HEIGHT = this.C_HEIGHT;
         model.C_HEADH = this.C_HEADH;
         model.C_BASEH = this.C_BASEH;
         model.C_RCOUNT = this.C_RCOUNT;
         model.C_MCOUNT = this.C_MCOUNT;
         model.C_FCOUNT = this.C_FCOUNT;
         model.C_SPEC = this.C_SPEC;
         model.C_STOCKSPEC = this.C_STOCKSPEC;
         model.C_STOCKBULK = this.C_STOCKBULK;
         model.C_SPECCODE = this.C_SPECCODE;
         model.C_ACCURACY = this.C_ACCURACY;
         model.C_VDI = this.C_VDI;
         model.C_CQUADRANT = this.C_CQUADRANT;
         model.C_AREA = this.C_AREA;
         model.C_ROCK = this.C_ROCK;
         model.C_PROCDIRECTION = this.C_PROCDIRECTION;
         model.C_MATERIAL = this.C_MATERIAL;
         model.C_SWAYFLAT = this.C_SWAYFLAT;
         model.C_PC = this.C_PC;
         model.C_ETCH = this.C_ETCH;
         model.C_EDMPLACE = this.C_EDMPLACE;
         model.C_UPDATETIME = this.C_UPDATETIME;
         model.C_UPDATEUSE = this.C_UPDATEUSE;
         model.C_REMARK = this.C_REMARK;
         model.C_REGION = this.C_REGION;
         model.C_ORD = this.C_ORD;
         model.C_TOEMANNAME = this.C_TOEMANNAME;
         return model;
         }
    }
    #endregion
}
