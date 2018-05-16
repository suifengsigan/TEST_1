using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
namespace DataAccess.Model
{
    #region TypicalSeparatePartTechnic
    /// <summary>
/// This object represents the properties and methods of a TypicalSeparatePartTechnic .
    /// </summary>
    public class TypicalSeparatePartTechnic
    {
    #region Properties //do not update!
        private double _operationID;
        //[Dapper.Key]
        public virtual double operationID
        {
        get {return _operationID;}
        set {_operationID = value;}
        }
        private string _mouldID = String.Empty;
        public virtual string mouldID
        {
        get {return _mouldID;}
        set {_mouldID = value;}
        }
        private string _partID = String.Empty;
        public virtual string partID
        {
        get {return _partID;}
        set {_partID = value;}
        }
        private int _operationOrderID;
        public virtual int operationOrderID
        {
        get {return _operationOrderID;}
        set {_operationOrderID = value;}
        }
        private string _operationNameID = String.Empty;
        public virtual string operationNameID
        {
        get {return _operationNameID;}
        set {_operationNameID = value;}
        }
        private string _operationName = String.Empty;
        public virtual string operationName
        {
        get {return _operationName;}
        set {_operationName = value;}
        }
        private string _operationContent = String.Empty;
        public virtual string operationContent
        {
        get {return _operationContent;}
        set {_operationContent = value;}
        }
        private string _processingHoursQuota = String.Empty;
        public virtual string processingHoursQuota
        {
        get {return _processingHoursQuota;}
        set {_processingHoursQuota = value;}
        }
        private string _machiningAttribute = String.Empty;
        public virtual string machiningAttribute
        {
        get {return _machiningAttribute;}
        set {_machiningAttribute = value;}
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
        private DateTime _planStartTime;
        public virtual DateTime planStartTime
        {
        get {return _planStartTime;}
        set {_planStartTime = value;}
        }
        private DateTime _planStopTime;
        public virtual DateTime planStopTime
        {
        get {return _planStopTime;}
        set {_planStopTime = value;}
        }
        private DateTime _planRestartTime;
        public virtual DateTime planRestartTime
        {
        get {return _planRestartTime;}
        set {_planRestartTime = value;}
        }
        private string _planResourceID = String.Empty;
        public virtual string planResourceID
        {
        get {return _planResourceID;}
        set {_planResourceID = value;}
        }
        private DateTime _planStartTime0;
        public virtual DateTime planStartTime0
        {
        get {return _planStartTime0;}
        set {_planStartTime0 = value;}
        }
        private DateTime _planStopTime0;
        public virtual DateTime planStopTime0
        {
        get {return _planStopTime0;}
        set {_planStopTime0 = value;}
        }
        private bool _existsWorks;
        public virtual bool existsWorks
        {
        get {return _existsWorks;}
        set {_existsWorks = value;}
        }
    #endregion
        public TypicalSeparatePartTechnic Copy()
         {
         var model = new TypicalSeparatePartTechnic ();
         model.mouldID = this.mouldID;
         model.partID = this.partID;
         model.operationOrderID = this.operationOrderID;
         model.operationNameID = this.operationNameID;
         model.operationName = this.operationName;
         model.operationContent = this.operationContent;
         model.processingHoursQuota = this.processingHoursQuota;
         model.operationID = this.operationID;
         model.machiningAttribute = this.machiningAttribute;
         model.priority = this.priority;
         model.color = this.color;
         model.shape = this.shape;
         model.remark = this.remark;
         model.planStartTime = this.planStartTime;
         model.planStopTime = this.planStopTime;
         model.planRestartTime = this.planRestartTime;
         model.planResourceID = this.planResourceID;
         model.planStartTime0 = this.planStartTime0;
         model.planStopTime0 = this.planStopTime0;
         model.existsWorks = this.existsWorks;
         return model;
         }
    }
    #endregion
}
