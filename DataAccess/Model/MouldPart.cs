using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
namespace DataAccess.Model
{
    #region MouldPart
    /// <summary>
/// This object represents the properties and methods of a MouldPart .
    /// </summary>
    public class MouldPart
    {
    #region Properties //do not update!
        private string _partID = String.Empty;
        //[Dapper.Key]
        public virtual string partID
        {
        get {return _partID;}
        set {_partID = value;}
        }
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
        private DateTime _scheduleStopTime;
        public virtual DateTime scheduleStopTime
        {
        get {return _scheduleStopTime;}
        set {_scheduleStopTime = value;}
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
        private string _addRecordsPerson = String.Empty;
        public virtual string addRecordsPerson
        {
        get {return _addRecordsPerson;}
        set {_addRecordsPerson = value;}
        }
        private DateTime _addRecordsTime;
        public virtual DateTime addRecordsTime
        {
        get {return _addRecordsTime;}
        set {_addRecordsTime = value;}
        }
        private string _versionInfo = String.Empty;
        public virtual string versionInfo
        {
        get {return _versionInfo;}
        set {_versionInfo = value;}
        }
        private DateTime _submitTime;
        public virtual DateTime submitTime
        {
        get {return _submitTime;}
        set {_submitTime = value;}
        }
        private double _partsMarked;
        public virtual double partsMarked
        {
        get {return _partsMarked;}
        set {_partsMarked = value;}
        }
        private DateTime _requiredCompleteTime;
        public virtual DateTime requiredCompleteTime
        {
        get {return _requiredCompleteTime;}
        set {_requiredCompleteTime = value;}
        }
        private DateTime _expectedCompletionTime;
        public virtual DateTime expectedCompletionTime
        {
        get {return _expectedCompletionTime;}
        set {_expectedCompletionTime = value;}
        }
        private string _outsourcingSuppliers = String.Empty;
        public virtual string outsourcingSuppliers
        {
        get {return _outsourcingSuppliers;}
        set {_outsourcingSuppliers = value;}
        }
        private DateTime _planStartTime;
        public virtual DateTime planStartTime
        {
        get {return _planStartTime;}
        set {_planStartTime = value;}
        }
        private string _contentsOutsourcing = String.Empty;
        public virtual string contentsOutsourcing
        {
        get {return _contentsOutsourcing;}
        set {_contentsOutsourcing = value;}
        }
        private int _stockState;
        public virtual int stockState
        {
        get {return _stockState;}
        set {_stockState = value;}
        }
        private byte _exteriorStatus;
        public virtual byte exteriorStatus
        {
        get {return _exteriorStatus;}
        set {_exteriorStatus = value;}
        }
        private byte _exteriorType;
        public virtual byte exteriorType
        {
        get {return _exteriorType;}
        set {_exteriorType = value;}
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
        private long _lastTransferRecordID;
        public virtual long lastTransferRecordID
        {
        get {return _lastTransferRecordID;}
        set {_lastTransferRecordID = value;}
        }
        private double _weight;
        public virtual double weight
        {
        get {return _weight;}
        set {_weight = value;}
        }
        private string _userField3 = String.Empty;
        public virtual string userField3
        {
        get {return _userField3;}
        set {_userField3 = value;}
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
        private int _isUpdateSameSapVerifyID;
        public virtual int isUpdateSameSapVerifyID
        {
        get {return _isUpdateSameSapVerifyID;}
        set {_isUpdateSameSapVerifyID = value;}
        }
        private int _numberSingleSets;
        public virtual int numberSingleSets
        {
        get {return _numberSingleSets;}
        set {_numberSingleSets = value;}
        }
        private string _dataFrom = String.Empty;
        public virtual string dataFrom
        {
        get {return _dataFrom;}
        set {_dataFrom = value;}
        }
    #endregion
        public MouldPart Copy()
         {
         var model = new MouldPart ();
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
         model.associatedID = this.associatedID;
         model.createTime = this.createTime;
         model.scheduleStopTime = this.scheduleStopTime;
         model.processAttribute = this.processAttribute;
         model.partClassIDonGS = this.partClassIDonGS;
         model.addRecordsPerson = this.addRecordsPerson;
         model.addRecordsTime = this.addRecordsTime;
         model.versionInfo = this.versionInfo;
         model.submitTime = this.submitTime;
         model.partsMarked = this.partsMarked;
         model.requiredCompleteTime = this.requiredCompleteTime;
         model.expectedCompletionTime = this.expectedCompletionTime;
         model.outsourcingSuppliers = this.outsourcingSuppliers;
         model.planStartTime = this.planStartTime;
         model.contentsOutsourcing = this.contentsOutsourcing;
         model.stockState = this.stockState;
         model.exteriorStatus = this.exteriorStatus;
         model.exteriorType = this.exteriorType;
         model.heft = this.heft;
         model.thickness = this.thickness;
         model.lastTransferRecordID = this.lastTransferRecordID;
         model.weight = this.weight;
         model.userField3 = this.userField3;
         model.spareNum = this.spareNum;
         model.sapVerifyID = this.sapVerifyID;
         model.isUpdateSameSapVerifyID = this.isUpdateSameSapVerifyID;
         model.numberSingleSets = this.numberSingleSets;
         model.dataFrom = this.dataFrom;
         return model;
         }
    }
    #endregion
}
