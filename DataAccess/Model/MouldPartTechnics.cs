using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
namespace DataAccess.Model
{
    #region MouldPartTechnic
    /// <summary>
/// This object represents the properties and methods of a MouldPartTechnic .
    /// </summary>
    public class MouldPartTechnic
    {
    #region Properties //do not update!
        private double _iD;
        //[Dapper.Key]
        public virtual double ID
        {
        get {return _iD;}
        set {_iD = value;}
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
        private string _partID = String.Empty;
        public virtual string partID
        {
        get {return _partID;}
        set {_partID = value;}
        }
        private int _partMonitorID;
        public virtual int partMonitorID
        {
        get {return _partMonitorID;}
        set {_partMonitorID = value;}
        }
        private int _operationOrderID;
        public virtual int operationOrderID
        {
        get {return _operationOrderID;}
        set {_operationOrderID = value;}
        }
        private int _monitorOrder;
        public virtual int monitorOrder
        {
        get {return _monitorOrder;}
        set {_monitorOrder = value;}
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
        private string _planResourceID = String.Empty;
        public virtual string planResourceID
        {
        get {return _planResourceID;}
        set {_planResourceID = value;}
        }
        private string _equipType = String.Empty;
        public virtual string equipType
        {
        get {return _equipType;}
        set {_equipType = value;}
        }
        private double _processingHoursQuota;
        public virtual double processingHoursQuota
        {
        get {return _processingHoursQuota;}
        set {_processingHoursQuota = value;}
        }
        private double _processingHoursActual;
        public virtual double processingHoursActual
        {
        get {return _processingHoursActual;}
        set {_processingHoursActual = value;}
        }
        private string _mark = String.Empty;
        public virtual string mark
        {
        get {return _mark;}
        set {_mark = value;}
        }
        private string _machiningAttribute = String.Empty;
        public virtual string machiningAttribute
        {
        get {return _machiningAttribute;}
        set {_machiningAttribute = value;}
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
        private bool _reCal;
        public virtual bool reCal
        {
        get {return _reCal;}
        set {_reCal = value;}
        }
        private string _state = String.Empty;
        public virtual string state
        {
        get {return _state;}
        set {_state = value;}
        }
        private DateTime _term;
        public virtual DateTime term
        {
        get {return _term;}
        set {_term = value;}
        }
        private byte _restrictType;
        public virtual byte restrictType
        {
        get {return _restrictType;}
        set {_restrictType = value;}
        }
        private DateTime _restrictTime;
        public virtual DateTime restrictTime
        {
        get {return _restrictTime;}
        set {_restrictTime = value;}
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
        private bool _milestone;
        public virtual bool milestone
        {
        get {return _milestone;}
        set {_milestone = value;}
        }
        private bool _keyOp;
        public virtual bool keyOp
        {
        get {return _keyOp;}
        set {_keyOp = value;}
        }
        private byte _canExterior;
        public virtual byte canExterior
        {
        get {return _canExterior;}
        set {_canExterior = value;}
        }
        private bool _planExterior;
        public virtual bool planExterior
        {
        get {return _planExterior;}
        set {_planExterior = value;}
        }
        private bool _actualExterior;
        public virtual bool actualExterior
        {
        get {return _actualExterior;}
        set {_actualExterior = value;}
        }
        private byte _type;
        public virtual byte type
        {
        get {return _type;}
        set {_type = value;}
        }
        private double _exteriorCost;
        public virtual double exteriorCost
        {
        get {return _exteriorCost;}
        set {_exteriorCost = value;}
        }
        private DateTime _earlistCanStartTime;
        public virtual DateTime earlistCanStartTime
        {
        get {return _earlistCanStartTime;}
        set {_earlistCanStartTime = value;}
        }
        private DateTime _latestNeedFinishTime;
        public virtual DateTime latestNeedFinishTime
        {
        get {return _latestNeedFinishTime;}
        set {_latestNeedFinishTime = value;}
        }
        private string _finishLevel = String.Empty;
        public virtual string finishLevel
        {
        get {return _finishLevel;}
        set {_finishLevel = value;}
        }
        private bool _isAffirmManHour;
        public virtual bool isAffirmManHour
        {
        get {return _isAffirmManHour;}
        set {_isAffirmManHour = value;}
        }
        private string _selfCheckQuality = String.Empty;
        public virtual string selfCheckQuality
        {
        get {return _selfCheckQuality;}
        set {_selfCheckQuality = value;}
        }
        private DateTime _demandTime;
        public virtual DateTime demandTime
        {
        get {return _demandTime;}
        set {_demandTime = value;}
        }
        private double _partNumber;
        public virtual double partNumber
        {
        get {return _partNumber;}
        set {_partNumber = value;}
        }
        private DateTime _expediteTime;
        public virtual DateTime expediteTime
        {
        get {return _expediteTime;}
        set {_expediteTime = value;}
        }
        private string _expediteMan = String.Empty;
        public virtual string expediteMan
        {
        get {return _expediteMan;}
        set {_expediteMan = value;}
        }
        private DateTime _specifyFinishTime;
        public virtual DateTime specifyFinishTime
        {
        get {return _specifyFinishTime;}
        set {_specifyFinishTime = value;}
        }
        private byte _operationType;
        public virtual byte operationType
        {
        get {return _operationType;}
        set {_operationType = value;}
        }
        private int _outsourcingNo;
        public virtual int outsourcingNo
        {
        get {return _outsourcingNo;}
        set {_outsourcingNo = value;}
        }
        private string _specifyTimeMan = String.Empty;
        public virtual string specifyTimeMan
        {
        get {return _specifyTimeMan;}
        set {_specifyTimeMan = value;}
        }
        private DateTime _confirmTime;
        public virtual DateTime confirmTime
        {
        get {return _confirmTime;}
        set {_confirmTime = value;}
        }
        private DateTime _actualStartTime;
        public virtual DateTime actualStartTime
        {
        get {return _actualStartTime;}
        set {_actualStartTime = value;}
        }
        private DateTime _actualStopTime;
        public virtual DateTime actualStopTime
        {
        get {return _actualStopTime;}
        set {_actualStopTime = value;}
        }
        private string _actualResourceID = String.Empty;
        public virtual string actualResourceID
        {
        get {return _actualResourceID;}
        set {_actualResourceID = value;}
        }
        private string _actualOperatorID = String.Empty;
        public virtual string actualOperatorID
        {
        get {return _actualOperatorID;}
        set {_actualOperatorID = value;}
        }
        private short _pauseProcess;
        public virtual short pauseProcess
        {
        get {return _pauseProcess;}
        set {_pauseProcess = value;}
        }
        private string _modifyExteriorHuman = String.Empty;
        public virtual string modifyExteriorHuman
        {
        get {return _modifyExteriorHuman;}
        set {_modifyExteriorHuman = value;}
        }
        private string _associatedID = String.Empty;
        public virtual string associatedID
        {
        get {return _associatedID;}
        set {_associatedID = value;}
        }
        private double _singleOperationTime;
        public virtual double singleOperationTime
        {
        get {return _singleOperationTime;}
        set {_singleOperationTime = value;}
        }
        private int _planID;
        public virtual int planID
        {
        get {return _planID;}
        set {_planID = value;}
        }
        private double _grossHours;
        public virtual double grossHours
        {
        get {return _grossHours;}
        set {_grossHours = value;}
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
        private bool _schedulState;
        public virtual bool schedulState
        {
        get {return _schedulState;}
        set {_schedulState = value;}
        }
        private int _isUrgent;
        public virtual int isUrgent
        {
        get {return _isUrgent;}
        set {_isUrgent = value;}
        }
        private int _dispatchState;
        public virtual int dispatchState
        {
        get {return _dispatchState;}
        set {_dispatchState = value;}
        }
        private DateTime _scheduleStartTime;
        public virtual DateTime scheduleStartTime
        {
        get {return _scheduleStartTime;}
        set {_scheduleStartTime = value;}
        }
        private DateTime _scheduleStopTime;
        public virtual DateTime scheduleStopTime
        {
        get {return _scheduleStopTime;}
        set {_scheduleStopTime = value;}
        }
        private bool _isChange;
        public virtual bool isChange
        {
        get {return _isChange;}
        set {_isChange = value;}
        }
        private string _scheduledStartTime = String.Empty;
        public virtual string scheduledStartTime
        {
        get {return _scheduledStartTime;}
        set {_scheduledStartTime = value;}
        }
        private string _scheduledStopTime = String.Empty;
        public virtual string scheduledStopTime
        {
        get {return _scheduledStopTime;}
        set {_scheduledStopTime = value;}
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
        private string _autoMTID = String.Empty;
        public virtual string autoMTID
        {
        get {return _autoMTID;}
        set {_autoMTID = value;}
        }
        private string _versionInfo = String.Empty;
        public virtual string versionInfo
        {
        get {return _versionInfo;}
        set {_versionInfo = value;}
        }
        private DateTime _firstWorkPlanStopTime;
        public virtual DateTime firstWorkPlanStopTime
        {
        get {return _firstWorkPlanStopTime;}
        set {_firstWorkPlanStopTime = value;}
        }
        private string _processCode = String.Empty;
        public virtual string processCode
        {
        get {return _processCode;}
        set {_processCode = value;}
        }
        private string _abnormalNums = String.Empty;
        public virtual string abnormalNums
        {
        get {return _abnormalNums;}
        set {_abnormalNums = value;}
        }
        private string _freeLogoShow = String.Empty;
        public virtual string freeLogoShow
        {
        get {return _freeLogoShow;}
        set {_freeLogoShow = value;}
        }
        private int _reasonOpID;
        public virtual int reasonOpID
        {
        get {return _reasonOpID;}
        set {_reasonOpID = value;}
        }
        private bool _existsWorks;
        public virtual bool existsWorks
        {
        get {return _existsWorks;}
        set {_existsWorks = value;}
        }
        private double _partNums;
        public virtual double partNums
        {
        get {return _partNums;}
        set {_partNums = value;}
        }
        private string _supplierID = String.Empty;
        public virtual string supplierID
        {
        get {return _supplierID;}
        set {_supplierID = value;}
        }
        private DateTime _requiredCompleteTime;
        public virtual DateTime requiredCompleteTime
        {
        get {return _requiredCompleteTime;}
        set {_requiredCompleteTime = value;}
        }
        private DateTime _expectedStartTime;
        public virtual DateTime expectedStartTime
        {
        get {return _expectedStartTime;}
        set {_expectedStartTime = value;}
        }
        private DateTime _expectedCompleteTime;
        public virtual DateTime expectedCompleteTime
        {
        get {return _expectedCompleteTime;}
        set {_expectedCompleteTime = value;}
        }
        private string _contentsOutsourcing = String.Empty;
        public virtual string contentsOutsourcing
        {
        get {return _contentsOutsourcing;}
        set {_contentsOutsourcing = value;}
        }
        private string _outpartMark = String.Empty;
        public virtual string outpartMark
        {
        get {return _outpartMark;}
        set {_outpartMark = value;}
        }
        private string _dispatchingResources = String.Empty;
        public virtual string dispatchingResources
        {
        get {return _dispatchingResources;}
        set {_dispatchingResources = value;}
        }
        private bool _urgent;
        public virtual bool urgent
        {
        get {return _urgent;}
        set {_urgent = value;}
        }
        private bool _isDispatching;
        public virtual bool isDispatching
        {
        get {return _isDispatching;}
        set {_isDispatching = value;}
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
        private bool _exteriorAfter;
        public virtual bool exteriorAfter
        {
        get {return _exteriorAfter;}
        set {_exteriorAfter = value;}
        }
        private bool _planType;
        public virtual bool planType
        {
        get {return _planType;}
        set {_planType = value;}
        }
        private DateTime _closeCTApplyTime;
        public virtual DateTime closeCTApplyTime
        {
        get {return _closeCTApplyTime;}
        set {_closeCTApplyTime = value;}
        }
        private int _no2DProcedures;
        public virtual int no2DProcedures
        {
        get {return _no2DProcedures;}
        set {_no2DProcedures = value;}
        }
        private int _no3DProcedures;
        public virtual int no3DProcedures
        {
        get {return _no3DProcedures;}
        set {_no3DProcedures = value;}
        }
        private string _ePoperator = String.Empty;
        public virtual string ePoperator
        {
        get {return _ePoperator;}
        set {_ePoperator = value;}
        }
        private DateTime _ePtime;
        public virtual DateTime ePtime
        {
        get {return _ePtime;}
        set {_ePtime = value;}
        }
        private int _preparationStatus;
        public virtual int preparationStatus
        {
        get {return _preparationStatus;}
        set {_preparationStatus = value;}
        }
        private DateTime _outsourcingSubmitTime;
        public virtual DateTime outsourcingSubmitTime
        {
        get {return _outsourcingSubmitTime;}
        set {_outsourcingSubmitTime = value;}
        }
        private string _procesParameters = String.Empty;
        public virtual string procesParameters
        {
        get {return _procesParameters;}
        set {_procesParameters = value;}
        }
        private string _dataFrom = String.Empty;
        public virtual string dataFrom
        {
        get {return _dataFrom;}
        set {_dataFrom = value;}
        }
    #endregion
        public MouldPartTechnic Copy()
         {
         var model = new MouldPartTechnic ();
         model.ID = this.ID;
         model.projectID = this.projectID;
         model.productID = this.productID;
         model.mouldID = this.mouldID;
         model.partID = this.partID;
         model.partMonitorID = this.partMonitorID;
         model.operationOrderID = this.operationOrderID;
         model.monitorOrder = this.monitorOrder;
         model.operationNameID = this.operationNameID;
         model.operationName = this.operationName;
         model.operationContent = this.operationContent;
         model.planResourceID = this.planResourceID;
         model.equipType = this.equipType;
         model.processingHoursQuota = this.processingHoursQuota;
         model.processingHoursActual = this.processingHoursActual;
         model.mark = this.mark;
         model.machiningAttribute = this.machiningAttribute;
         model.planStartTime = this.planStartTime;
         model.planStopTime = this.planStopTime;
         model.planRestartTime = this.planRestartTime;
         model.reCal = this.reCal;
         model.state = this.state;
         model.term = this.term;
         model.restrictType = this.restrictType;
         model.restrictTime = this.restrictTime;
         model.priority = this.priority;
         model.color = this.color;
         model.shape = this.shape;
         model.remark = this.remark;
         model.milestone = this.milestone;
         model.keyOp = this.keyOp;
         model.canExterior = this.canExterior;
         model.planExterior = this.planExterior;
         model.actualExterior = this.actualExterior;
         model.type = this.type;
         model.exteriorCost = this.exteriorCost;
         model.earlistCanStartTime = this.earlistCanStartTime;
         model.latestNeedFinishTime = this.latestNeedFinishTime;
         model.finishLevel = this.finishLevel;
         model.isAffirmManHour = this.isAffirmManHour;
         model.selfCheckQuality = this.selfCheckQuality;
         model.demandTime = this.demandTime;
         model.partNumber = this.partNumber;
         model.expediteTime = this.expediteTime;
         model.expediteMan = this.expediteMan;
         model.specifyFinishTime = this.specifyFinishTime;
         model.operationType = this.operationType;
         model.outsourcingNo = this.outsourcingNo;
         model.specifyTimeMan = this.specifyTimeMan;
         model.confirmTime = this.confirmTime;
         model.actualStartTime = this.actualStartTime;
         model.actualStopTime = this.actualStopTime;
         model.actualResourceID = this.actualResourceID;
         model.actualOperatorID = this.actualOperatorID;
         model.pauseProcess = this.pauseProcess;
         model.modifyExteriorHuman = this.modifyExteriorHuman;
         model.associatedID = this.associatedID;
         model.singleOperationTime = this.singleOperationTime;
         model.planID = this.planID;
         model.grossHours = this.grossHours;
         model.planStartTime0 = this.planStartTime0;
         model.planStopTime0 = this.planStopTime0;
         model.schedulState = this.schedulState;
         model.isUrgent = this.isUrgent;
         model.dispatchState = this.dispatchState;
         model.scheduleStartTime = this.scheduleStartTime;
         model.scheduleStopTime = this.scheduleStopTime;
         model.isChange = this.isChange;
         model.scheduledStartTime = this.scheduledStartTime;
         model.scheduledStopTime = this.scheduledStopTime;
         model.addRecordsPerson = this.addRecordsPerson;
         model.addRecordsTime = this.addRecordsTime;
         model.autoMTID = this.autoMTID;
         model.versionInfo = this.versionInfo;
         model.firstWorkPlanStopTime = this.firstWorkPlanStopTime;
         model.processCode = this.processCode;
         model.abnormalNums = this.abnormalNums;
         model.freeLogoShow = this.freeLogoShow;
         model.reasonOpID = this.reasonOpID;
         model.existsWorks = this.existsWorks;
         model.partNums = this.partNums;
         model.supplierID = this.supplierID;
         model.requiredCompleteTime = this.requiredCompleteTime;
         model.expectedStartTime = this.expectedStartTime;
         model.expectedCompleteTime = this.expectedCompleteTime;
         model.contentsOutsourcing = this.contentsOutsourcing;
         model.outpartMark = this.outpartMark;
         model.dispatchingResources = this.dispatchingResources;
         model.urgent = this.urgent;
         model.isDispatching = this.isDispatching;
         model.exteriorStatus = this.exteriorStatus;
         model.exteriorType = this.exteriorType;
         model.exteriorAfter = this.exteriorAfter;
         model.planType = this.planType;
         model.closeCTApplyTime = this.closeCTApplyTime;
         model.no2DProcedures = this.no2DProcedures;
         model.no3DProcedures = this.no3DProcedures;
         model.ePoperator = this.ePoperator;
         model.ePtime = this.ePtime;
         model.preparationStatus = this.preparationStatus;
         model.outsourcingSubmitTime = this.outsourcingSubmitTime;
         model.procesParameters = this.procesParameters;
         model.dataFrom = this.dataFrom;
         return model;
         }
    }
    #endregion
}
