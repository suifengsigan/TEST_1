using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
namespace DataAccess.Model
{
    #region HistoryMouldPart
    /// <summary>
/// This object represents the properties and methods of a HistoryMouldPart .
    /// </summary>
    public class HistoryMouldPart
    {
    #region Properties //do not update!
        private string _projectID = String.Empty;
        public virtual string projectID
        {
        get {return _projectID;}
        set {_projectID = value;}
        }
        private string _productID = String.Empty;
        public virtual string productID
        {
        get {return _productID;}
        set {_productID = value;}
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
        private string _state = String.Empty;
        public virtual string state
        {
        get {return _state;}
        set {_state = value;}
        }
        private string _designer = String.Empty;
        public virtual string designer
        {
        get {return _designer;}
        set {_designer = value;}
        }
        private DateTime _designDate;
        public virtual DateTime designDate
        {
        get {return _designDate;}
        set {_designDate = value;}
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
        private string _mark = String.Empty;
        public virtual string mark
        {
        get {return _mark;}
        set {_mark = value;}
        }
        private string _jobType = String.Empty;
        public virtual string jobType
        {
        get {return _jobType;}
        set {_jobType = value;}
        }
        private string _jobProgress = String.Empty;
        public virtual string jobProgress
        {
        get {return _jobProgress;}
        set {_jobProgress = value;}
        }
        private string _destination = String.Empty;
        public virtual string destination
        {
        get {return _destination;}
        set {_destination = value;}
        }
        private int _iD;
        public virtual int ID
        {
        get {return _iD;}
        set {_iD = value;}
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
        private byte _canExterior;
        public virtual byte canExterior
        {
        get {return _canExterior;}
        set {_canExterior = value;}
        }
        private int _schedulState;
        public virtual int schedulState
        {
        get {return _schedulState;}
        set {_schedulState = value;}
        }
        private byte _partType;
        public virtual byte partType
        {
        get {return _partType;}
        set {_partType = value;}
        }
        private double _partNums;
        public virtual double partNums
        {
        get {return _partNums;}
        set {_partNums = value;}
        }
        private double _disUseNums;
        public virtual double disUseNums
        {
        get {return _disUseNums;}
        set {_disUseNums = value;}
        }
        private DateTime _subTime;
        public virtual DateTime subTime
        {
        get {return _subTime;}
        set {_subTime = value;}
        }
        private string _subMan = String.Empty;
        public virtual string subMan
        {
        get {return _subMan;}
        set {_subMan = value;}
        }
        private short _pauseProcess;
        public virtual short pauseProcess
        {
        get {return _pauseProcess;}
        set {_pauseProcess = value;}
        }
        private string _partFigID = String.Empty;
        public virtual string partFigID
        {
        get {return _partFigID;}
        set {_partFigID = value;}
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
        private string _userField1 = String.Empty;
        public virtual string userField1
        {
        get {return _userField1;}
        set {_userField1 = value;}
        }
        private string _userField2 = String.Empty;
        public virtual string userField2
        {
        get {return _userField2;}
        set {_userField2 = value;}
        }
        private int _outsourcingNo;
        public virtual int outsourcingNo
        {
        get {return _outsourcingNo;}
        set {_outsourcingNo = value;}
        }
        private bool _actualExterior;
        public virtual bool actualExterior
        {
        get {return _actualExterior;}
        set {_actualExterior = value;}
        }
        private string _bomAttribute2 = String.Empty;
        public virtual string bomAttribute2
        {
        get {return _bomAttribute2;}
        set {_bomAttribute2 = value;}
        }
        private string _bomAttribute3 = String.Empty;
        public virtual string bomAttribute3
        {
        get {return _bomAttribute3;}
        set {_bomAttribute3 = value;}
        }
        private DateTime _scheduleStartTime;
        public virtual DateTime scheduleStartTime
        {
        get {return _scheduleStartTime;}
        set {_scheduleStartTime = value;}
        }
        private double _pauseTechnicsID;
        public virtual double pauseTechnicsID
        {
        get {return _pauseTechnicsID;}
        set {_pauseTechnicsID = value;}
        }
        private DateTime _scheduleStopTime;
        public virtual DateTime scheduleStopTime
        {
        get {return _scheduleStopTime;}
        set {_scheduleStopTime = value;}
        }
        private string _associatedID = String.Empty;
        public virtual string associatedID
        {
        get {return _associatedID;}
        set {_associatedID = value;}
        }
        private DateTime _createTime;
        public virtual DateTime createTime
        {
        get {return _createTime;}
        set {_createTime = value;}
        }
        private int _processAttribute;
        public virtual int processAttribute
        {
        get {return _processAttribute;}
        set {_processAttribute = value;}
        }
        private int _partClassIDonGS;
        public virtual int partClassIDonGS
        {
        get {return _partClassIDonGS;}
        set {_partClassIDonGS = value;}
        }
        private string _modifyRecordsPerson = String.Empty;
        public virtual string modifyRecordsPerson
        {
        get {return _modifyRecordsPerson;}
        set {_modifyRecordsPerson = value;}
        }
        private DateTime _modifyRecordsTime;
        public virtual DateTime modifyRecordsTime
        {
        get {return _modifyRecordsTime;}
        set {_modifyRecordsTime = value;}
        }
        private string _modifyType = String.Empty;
        public virtual string modifyType
        {
        get {return _modifyType;}
        set {_modifyType = value;}
        }
        private string _versionInfo = String.Empty;
        public virtual string versionInfo
        {
        get {return _versionInfo;}
        set {_versionInfo = value;}
        }
        private string _heft = String.Empty;
        public virtual string heft
        {
        get {return _heft;}
        set {_heft = value;}
        }
        private string _thickness = String.Empty;
        public virtual string thickness
        {
        get {return _thickness;}
        set {_thickness = value;}
        }
        private string _spareNum = String.Empty;
        public virtual string spareNum
        {
        get {return _spareNum;}
        set {_spareNum = value;}
        }
        private string _sapVerifyID = String.Empty;
        public virtual string sapVerifyID
        {
        get {return _sapVerifyID;}
        set {_sapVerifyID = value;}
        }
        private int _numberSingleSets;
        public virtual int numberSingleSets
        {
        get {return _numberSingleSets;}
        set {_numberSingleSets = value;}
        }
    #endregion
        public HistoryMouldPart Copy()
         {
         var model = new HistoryMouldPart ();
         model.projectID = this.projectID;
         model.productID = this.productID;
         model.mouldID = this.mouldID;
         model.partID = this.partID;
         model.partClassID = this.partClassID;
         model.partName = this.partName;
         model.materialID = this.materialID;
         model.drawPath = this.drawPath;
         model.state = this.state;
         model.designer = this.designer;
         model.designDate = this.designDate;
         model.machiningAttribute = this.machiningAttribute;
         model.isKey = this.isKey;
         model.mark = this.mark;
         model.jobType = this.jobType;
         model.jobProgress = this.jobProgress;
         model.destination = this.destination;
         model.ID = this.ID;
         model.priority = this.priority;
         model.color = this.color;
         model.shape = this.shape;
         model.remark = this.remark;
         model.partOrderID = this.partOrderID;
         model.canExterior = this.canExterior;
         model.schedulState = this.schedulState;
         model.partType = this.partType;
         model.partNums = this.partNums;
         model.disUseNums = this.disUseNums;
         model.subTime = this.subTime;
         model.subMan = this.subMan;
         model.pauseProcess = this.pauseProcess;
         model.partFigID = this.partFigID;
         model.verifyID = this.verifyID;
         model.specifications = this.specifications;
         model.standardNumber = this.standardNumber;
         model.userField1 = this.userField1;
         model.userField2 = this.userField2;
         model.outsourcingNo = this.outsourcingNo;
         model.actualExterior = this.actualExterior;
         model.bomAttribute2 = this.bomAttribute2;
         model.bomAttribute3 = this.bomAttribute3;
         model.scheduleStartTime = this.scheduleStartTime;
         model.pauseTechnicsID = this.pauseTechnicsID;
         model.scheduleStopTime = this.scheduleStopTime;
         model.associatedID = this.associatedID;
         model.createTime = this.createTime;
         model.processAttribute = this.processAttribute;
         model.partClassIDonGS = this.partClassIDonGS;
         model.modifyRecordsPerson = this.modifyRecordsPerson;
         model.modifyRecordsTime = this.modifyRecordsTime;
         model.modifyType = this.modifyType;
         model.versionInfo = this.versionInfo;
         model.heft = this.heft;
         model.thickness = this.thickness;
         model.spareNum = this.spareNum;
         model.sapVerifyID = this.sapVerifyID;
         model.numberSingleSets = this.numberSingleSets;
         return model;
         }
    }
    #endregion
}
