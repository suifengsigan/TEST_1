using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
namespace DataAccess.Model
{
    #region TypicalSeparatePart
    /// <summary>
/// This object represents the properties and methods of a TypicalSeparatePart .
    /// </summary>
    public class TypicalSeparatePart
    {
    #region Properties //do not update!
        private string _partID = String.Empty;
        //[Dapper.Key]
        public virtual string partID
        {
        get {return _partID;}
        set {_partID = value;}
        }
        private string _mouldID = String.Empty;
        public virtual string mouldID
        {
        get {return _mouldID;}
        set {_mouldID = value;}
        }
        private string _mouldClassID = String.Empty;
        public virtual string mouldClassID
        {
        get {return _mouldClassID;}
        set {_mouldClassID = value;}
        }
        private int _partClassID;
        public virtual int partClassID
        {
        get {return _partClassID;}
        set {_partClassID = value;}
        }
        private string _partName = String.Empty;
        public virtual string partName
        {
        get {return _partName;}
        set {_partName = value;}
        }
        private int _materialID;
        public virtual int materialID
        {
        get {return _materialID;}
        set {_materialID = value;}
        }
        private string _drawPath = String.Empty;
        public virtual string drawPath
        {
        get {return _drawPath;}
        set {_drawPath = value;}
        }
        private string _machiningAttribute = String.Empty;
        public virtual string machiningAttribute
        {
        get {return _machiningAttribute;}
        set {_machiningAttribute = value;}
        }
        private bool _isKey;
        public virtual bool isKey
        {
        get {return _isKey;}
        set {_isKey = value;}
        }
        private int _priority;
        public virtual int priority
        {
        get {return _priority;}
        set {_priority = value;}
        }
        private string _color = String.Empty;
        public virtual string color
        {
        get {return _color;}
        set {_color = value;}
        }
        private byte _shape;
        public virtual byte shape
        {
        get {return _shape;}
        set {_shape = value;}
        }
        private string _remark = String.Empty;
        public virtual string remark
        {
        get {return _remark;}
        set {_remark = value;}
        }
        private int _partOrderID;
        public virtual int partOrderID
        {
        get {return _partOrderID;}
        set {_partOrderID = value;}
        }
        private byte _partType;
        public virtual byte partType
        {
        get {return _partType;}
        set {_partType = value;}
        }
        private string _verifyID = String.Empty;
        public virtual string verifyID
        {
        get {return _verifyID;}
        set {_verifyID = value;}
        }
        private string _specifications = String.Empty;
        public virtual string specifications
        {
        get {return _specifications;}
        set {_specifications = value;}
        }
        private string _standardNumber = String.Empty;
        public virtual string standardNumber
        {
        get {return _standardNumber;}
        set {_standardNumber = value;}
        }
        private int _partProperties;
        public virtual int partProperties
        {
        get {return _partProperties;}
        set {_partProperties = value;}
        }
        private string _associatedID = String.Empty;
        public virtual string associatedID
        {
        get {return _associatedID;}
        set {_associatedID = value;}
        }
    #endregion
        public TypicalSeparatePart Copy()
         {
         var model = new TypicalSeparatePart ();
         model.mouldID = this.mouldID;
         model.mouldClassID = this.mouldClassID;
         model.partID = this.partID;
         model.partClassID = this.partClassID;
         model.partName = this.partName;
         model.materialID = this.materialID;
         model.drawPath = this.drawPath;
         model.machiningAttribute = this.machiningAttribute;
         model.isKey = this.isKey;
         model.priority = this.priority;
         model.color = this.color;
         model.shape = this.shape;
         model.remark = this.remark;
         model.partOrderID = this.partOrderID;
         model.partType = this.partType;
         model.verifyID = this.verifyID;
         model.specifications = this.specifications;
         model.standardNumber = this.standardNumber;
         model.partProperties = this.partProperties;
         model.associatedID = this.associatedID;
         return model;
         }
    }
    #endregion
}
