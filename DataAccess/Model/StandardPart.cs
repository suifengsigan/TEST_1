using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
namespace DataAccess.Model
{
    #region StandardPart
    /// <summary>
/// This object represents the properties and methods of a StandardPart .
    /// </summary>
    public class StandardPart
    {
    #region Properties //do not update!
        private string _standardPartID = String.Empty;
        //[Dapper.Key]
        public virtual string standardPartID
        {
        get {return _standardPartID;}
        set {_standardPartID = value;}
        }
        private string _standardPartName = String.Empty;
        public virtual string standardPartName
        {
        get {return _standardPartName;}
        set {_standardPartName = value;}
        }
        private string _remark = String.Empty;
        public virtual string remark
        {
        get {return _remark;}
        set {_remark = value;}
        }
        private string _mark = String.Empty;
        public virtual string mark
        {
        get {return _mark;}
        set {_mark = value;}
        }
        private string _standardPartNumID = String.Empty;
        public virtual string standardPartNumID
        {
        get {return _standardPartNumID;}
        set {_standardPartNumID = value;}
        }
        private string _materialID = String.Empty;
        public virtual string materialID
        {
        get {return _materialID;}
        set {_materialID = value;}
        }
        private int _iD;
        public virtual int ID
        {
        get {return _iD;}
        set {_iD = value;}
        }
        private byte _orderState;
        public virtual byte orderState
        {
        get {return _orderState;}
        set {_orderState = value;}
        }
        private byte _partType;
        public virtual byte partType
        {
        get {return _partType;}
        set {_partType = value;}
        }
        private string _partCode = String.Empty;
        public virtual string partCode
        {
        get {return _partCode;}
        set {_partCode = value;}
        }
    #endregion
        public StandardPart Copy()
         {
         var model = new StandardPart ();
         model.standardPartID = this.standardPartID;
         model.standardPartName = this.standardPartName;
         model.remark = this.remark;
         model.mark = this.mark;
         model.standardPartNumID = this.standardPartNumID;
         model.materialID = this.materialID;
         model.ID = this.ID;
         model.orderState = this.orderState;
         model.partType = this.partType;
         model.partCode = this.partCode;
         return model;
         }
    }
    #endregion
}
