using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DataAccess.Model
{
	#region t_ZHCX_DelMouldPartTechRecord
	/// <summary>
	/// This object represents the properties and methods of a t_ZHCX_DelMouldPartTechRecord.
	/// </summary>
	public class t_ZHCX_DelMouldPartTechRecord
	{
        #region Properties //do not update!
		private long _recordID;
		private string _projectID = String.Empty;
		private string _productID = String.Empty;
		private string _mouldID = String.Empty;
		private string _partID = String.Empty;
		private int _partMonitorID;
		private string _operationNameID = String.Empty;
		private string _operationName = String.Empty;
		private string _operationContent = String.Empty;
		private decimal _processingHoursQuota;
		private byte _type;
		private string _state = String.Empty;
		private string _operator = String.Empty;
		private DateTime _recordTime;
		private string _verifyID = String.Empty;
		private string _partName = String.Empty;
		private decimal _operationID;
		private bool _actualExterior;
		private string _actualOperatorID = String.Empty;
		private string _actualResourceID = String.Empty;
		private DateTime _actualStartTime;
		private DateTime _actualStopTime;
		private string _associatedID = String.Empty;
		private string _autoMTID = String.Empty;
		private byte _canExterior;
		private string _color = String.Empty;
		private DateTime _confirmTime;
		private DateTime _demandTime;
		private int _dispatchState;
		private DateTime _earlistCanStartTime;
		private string _equipType = String.Empty;
		private string _expediteMan = String.Empty;
		private DateTime _expediteTime;
		private decimal _exteriorCost;
		private string _finishLevel = String.Empty;
		private decimal _grossHours;
		private bool _isAffirmManHour;
		private bool _isChange;
		private int _isUrgent;
		private bool _keyOp;
		private DateTime _latestNeedFinishTime;
		private string _machiningAttribute = String.Empty;
		private string _mark = String.Empty;
		private bool _milestone;
		private string _modifyExteriorHuman = String.Empty;
		private string _modifyRecordsPerson = String.Empty;
		private DateTime _modifyRecordsTime;
		private string _modifyType = String.Empty;
		private int _monitorOrder;
		private int _operationOrderID;
		private byte _operationType;
		private int _outsourcingNo;
		private decimal _partNumber;
		private short _pauseProcess;
		private bool _planExterior;
		private int _planID;
		private string _planResourceID = String.Empty;
		private DateTime _planRestartTime;
		private DateTime _planStartTime;
		private DateTime _planStartTime0;
		private DateTime _planStopTime;
		private DateTime _planStopTime0;
		private int _priority;
		private decimal _processingHoursActual;
		private bool _reCal;
		private string _remark = String.Empty;
		private string _resourceID = String.Empty;
		private DateTime _restrictTime;
		private byte _restrictType;
		private string _scheduledStartTime = String.Empty;
		private string _scheduledStopTime = String.Empty;
		private DateTime _scheduleStartTime;
		private DateTime _scheduleStopTime;
		private bool _schedulState;
		private string _selfCheckQuality = String.Empty;
		private decimal _singleOperationTime;
		private DateTime _specifyFinishTime;
		private string _specifyTimeMan = String.Empty;
		private DateTime _term;
		private string _versionInfo = String.Empty;
		private string _abnormalNums = String.Empty;
		private string _procesParameters = String.Empty;
		
		public long recordID
		{
			get {return _recordID;}
			set {_recordID = value;}
		}

		public string projectID
		{
			get {return _projectID;}
			set {_projectID = value;}
		}

		public string productID
		{
			get {return _productID;}
			set {_productID = value;}
		}

		public string mouldID
		{
			get {return _mouldID;}
			set {_mouldID = value;}
		}

		public string partID
		{
			get {return _partID;}
			set {_partID = value;}
		}

		public int partMonitorID
		{
			get {return _partMonitorID;}
			set {_partMonitorID = value;}
		}

		public string operationNameID
		{
			get {return _operationNameID;}
			set {_operationNameID = value;}
		}

		public string operationName
		{
			get {return _operationName;}
			set {_operationName = value;}
		}

		public string operationContent
		{
			get {return _operationContent;}
			set {_operationContent = value;}
		}

		public decimal processingHoursQuota
		{
			get {return _processingHoursQuota;}
			set {_processingHoursQuota = value;}
		}

		public byte type
		{
			get {return _type;}
			set {_type = value;}
		}

		public string state
		{
			get {return _state;}
			set {_state = value;}
		}

		public string @operator
		{
			get {return _operator;}
			set {_operator = value;}
		}

		public DateTime recordTime
		{
			get {return _recordTime;}
			set {_recordTime = value;}
		}

		public string verifyID
		{
			get {return _verifyID;}
			set {_verifyID = value;}
		}

		public string partName
		{
			get {return _partName;}
			set {_partName = value;}
		}

		public decimal operationID
		{
			get {return _operationID;}
			set {_operationID = value;}
		}

		public bool actualExterior
		{
			get {return _actualExterior;}
			set {_actualExterior = value;}
		}

		public string actualOperatorID
		{
			get {return _actualOperatorID;}
			set {_actualOperatorID = value;}
		}

		public string actualResourceID
		{
			get {return _actualResourceID;}
			set {_actualResourceID = value;}
		}

		public DateTime actualStartTime
		{
			get {return _actualStartTime;}
			set {_actualStartTime = value;}
		}

		public DateTime actualStopTime
		{
			get {return _actualStopTime;}
			set {_actualStopTime = value;}
		}

		public string associatedID
		{
			get {return _associatedID;}
			set {_associatedID = value;}
		}

		public string autoMTID
		{
			get {return _autoMTID;}
			set {_autoMTID = value;}
		}

		public byte canExterior
		{
			get {return _canExterior;}
			set {_canExterior = value;}
		}

		public string color
		{
			get {return _color;}
			set {_color = value;}
		}

		public DateTime confirmTime
		{
			get {return _confirmTime;}
			set {_confirmTime = value;}
		}

		public DateTime demandTime
		{
			get {return _demandTime;}
			set {_demandTime = value;}
		}

		public int dispatchState
		{
			get {return _dispatchState;}
			set {_dispatchState = value;}
		}

		public DateTime earlistCanStartTime
		{
			get {return _earlistCanStartTime;}
			set {_earlistCanStartTime = value;}
		}

		public string equipType
		{
			get {return _equipType;}
			set {_equipType = value;}
		}

		public string expediteMan
		{
			get {return _expediteMan;}
			set {_expediteMan = value;}
		}

		public DateTime expediteTime
		{
			get {return _expediteTime;}
			set {_expediteTime = value;}
		}

		public decimal exteriorCost
		{
			get {return _exteriorCost;}
			set {_exteriorCost = value;}
		}

		public string finishLevel
		{
			get {return _finishLevel;}
			set {_finishLevel = value;}
		}

		public decimal grossHours
		{
			get {return _grossHours;}
			set {_grossHours = value;}
		}

		public bool isAffirmManHour
		{
			get {return _isAffirmManHour;}
			set {_isAffirmManHour = value;}
		}

		public bool isChange
		{
			get {return _isChange;}
			set {_isChange = value;}
		}

		public int isUrgent
		{
			get {return _isUrgent;}
			set {_isUrgent = value;}
		}

		public bool keyOp
		{
			get {return _keyOp;}
			set {_keyOp = value;}
		}

		public DateTime latestNeedFinishTime
		{
			get {return _latestNeedFinishTime;}
			set {_latestNeedFinishTime = value;}
		}

		public string machiningAttribute
		{
			get {return _machiningAttribute;}
			set {_machiningAttribute = value;}
		}

		public string mark
		{
			get {return _mark;}
			set {_mark = value;}
		}

		public bool milestone
		{
			get {return _milestone;}
			set {_milestone = value;}
		}

		public string modifyExteriorHuman
		{
			get {return _modifyExteriorHuman;}
			set {_modifyExteriorHuman = value;}
		}

		public string modifyRecordsPerson
		{
			get {return _modifyRecordsPerson;}
			set {_modifyRecordsPerson = value;}
		}

		public DateTime modifyRecordsTime
		{
			get {return _modifyRecordsTime;}
			set {_modifyRecordsTime = value;}
		}

		public string modifyType
		{
			get {return _modifyType;}
			set {_modifyType = value;}
		}

		public int monitorOrder
		{
			get {return _monitorOrder;}
			set {_monitorOrder = value;}
		}

		public int operationOrderID
		{
			get {return _operationOrderID;}
			set {_operationOrderID = value;}
		}

		public byte operationType
		{
			get {return _operationType;}
			set {_operationType = value;}
		}

		public int outsourcingNo
		{
			get {return _outsourcingNo;}
			set {_outsourcingNo = value;}
		}

		public decimal partNumber
		{
			get {return _partNumber;}
			set {_partNumber = value;}
		}

		public short pauseProcess
		{
			get {return _pauseProcess;}
			set {_pauseProcess = value;}
		}

		public bool planExterior
		{
			get {return _planExterior;}
			set {_planExterior = value;}
		}

		public int planID
		{
			get {return _planID;}
			set {_planID = value;}
		}

		public string planResourceID
		{
			get {return _planResourceID;}
			set {_planResourceID = value;}
		}

		public DateTime planRestartTime
		{
			get {return _planRestartTime;}
			set {_planRestartTime = value;}
		}

		public DateTime planStartTime
		{
			get {return _planStartTime;}
			set {_planStartTime = value;}
		}

		public DateTime planStartTime0
		{
			get {return _planStartTime0;}
			set {_planStartTime0 = value;}
		}

		public DateTime planStopTime
		{
			get {return _planStopTime;}
			set {_planStopTime = value;}
		}

		public DateTime planStopTime0
		{
			get {return _planStopTime0;}
			set {_planStopTime0 = value;}
		}

		public int priority
		{
			get {return _priority;}
			set {_priority = value;}
		}

		public decimal processingHoursActual
		{
			get {return _processingHoursActual;}
			set {_processingHoursActual = value;}
		}

		public bool reCal
		{
			get {return _reCal;}
			set {_reCal = value;}
		}

		public string remark
		{
			get {return _remark;}
			set {_remark = value;}
		}

		public string resourceID
		{
			get {return _resourceID;}
			set {_resourceID = value;}
		}

		public DateTime restrictTime
		{
			get {return _restrictTime;}
			set {_restrictTime = value;}
		}

		public byte restrictType
		{
			get {return _restrictType;}
			set {_restrictType = value;}
		}

		public string scheduledStartTime
		{
			get {return _scheduledStartTime;}
			set {_scheduledStartTime = value;}
		}

		public string scheduledStopTime
		{
			get {return _scheduledStopTime;}
			set {_scheduledStopTime = value;}
		}

		public DateTime scheduleStartTime
		{
			get {return _scheduleStartTime;}
			set {_scheduleStartTime = value;}
		}

		public DateTime scheduleStopTime
		{
			get {return _scheduleStopTime;}
			set {_scheduleStopTime = value;}
		}

		public bool schedulState
		{
			get {return _schedulState;}
			set {_schedulState = value;}
		}

		public string selfCheckQuality
		{
			get {return _selfCheckQuality;}
			set {_selfCheckQuality = value;}
		}

		public decimal singleOperationTime
		{
			get {return _singleOperationTime;}
			set {_singleOperationTime = value;}
		}

		public DateTime specifyFinishTime
		{
			get {return _specifyFinishTime;}
			set {_specifyFinishTime = value;}
		}

		public string specifyTimeMan
		{
			get {return _specifyTimeMan;}
			set {_specifyTimeMan = value;}
		}

		public DateTime term
		{
			get {return _term;}
			set {_term = value;}
		}

		public string versionInfo
		{
			get {return _versionInfo;}
			set {_versionInfo = value;}
		}

		public string abnormalNums
		{
			get {return _abnormalNums;}
			set {_abnormalNums = value;}
		}
		public string procesParameters
		{
			get {return _procesParameters;}
			set {_procesParameters = value;}
		}
		#endregion
        public t_ZHCX_DelMouldPartTechRecord Copy()
        {
            var model = new t_ZHCX_DelMouldPartTechRecord ();
		    model.recordID = this.recordID;
		    model.projectID = this.projectID;
		    model.productID = this.productID;
		    model.mouldID = this.mouldID;
		    model.partID = this.partID;
		    model.partMonitorID = this.partMonitorID;
		    model.operationNameID = this.operationNameID;
		    model.operationName = this.operationName;
		    model.operationContent = this.operationContent;
		    model.processingHoursQuota = this.processingHoursQuota;
		    model.type = this.type;
		    model.state = this.state;
		    model.@operator = this.@operator;
		    model.recordTime = this.recordTime;
		    model.verifyID = this.verifyID;
		    model.partName = this.partName;
		    model.operationID = this.operationID;
		    model.actualExterior = this.actualExterior;
		    model.actualOperatorID = this.actualOperatorID;
		    model.actualResourceID = this.actualResourceID;
		    model.actualStartTime = this.actualStartTime;
		    model.actualStopTime = this.actualStopTime;
		    model.associatedID = this.associatedID;
		    model.autoMTID = this.autoMTID;
		    model.canExterior = this.canExterior;
		    model.color = this.color;
		    model.confirmTime = this.confirmTime;
		    model.demandTime = this.demandTime;
		    model.dispatchState = this.dispatchState;
		    model.earlistCanStartTime = this.earlistCanStartTime;
		    model.equipType = this.equipType;
		    model.expediteMan = this.expediteMan;
		    model.expediteTime = this.expediteTime;
		    model.exteriorCost = this.exteriorCost;
		    model.finishLevel = this.finishLevel;
		    model.grossHours = this.grossHours;
		    model.isAffirmManHour = this.isAffirmManHour;
		    model.isChange = this.isChange;
		    model.isUrgent = this.isUrgent;
		    model.keyOp = this.keyOp;
		    model.latestNeedFinishTime = this.latestNeedFinishTime;
		    model.machiningAttribute = this.machiningAttribute;
		    model.mark = this.mark;
		    model.milestone = this.milestone;
		    model.modifyExteriorHuman = this.modifyExteriorHuman;
		    model.modifyRecordsPerson = this.modifyRecordsPerson;
		    model.modifyRecordsTime = this.modifyRecordsTime;
		    model.modifyType = this.modifyType;
		    model.monitorOrder = this.monitorOrder;
		    model.operationOrderID = this.operationOrderID;
		    model.operationType = this.operationType;
		    model.outsourcingNo = this.outsourcingNo;
		    model.partNumber = this.partNumber;
		    model.pauseProcess = this.pauseProcess;
		    model.planExterior = this.planExterior;
		    model.planID = this.planID;
		    model.planResourceID = this.planResourceID;
		    model.planRestartTime = this.planRestartTime;
		    model.planStartTime = this.planStartTime;
		    model.planStartTime0 = this.planStartTime0;
		    model.planStopTime = this.planStopTime;
		    model.planStopTime0 = this.planStopTime0;
		    model.priority = this.priority;
		    model.processingHoursActual = this.processingHoursActual;
		    model.reCal = this.reCal;
		    model.remark = this.remark;
		    model.resourceID = this.resourceID;
		    model.restrictTime = this.restrictTime;
		    model.restrictType = this.restrictType;
		    model.scheduledStartTime = this.scheduledStartTime;
		    model.scheduledStopTime = this.scheduledStopTime;
		    model.scheduleStartTime = this.scheduleStartTime;
		    model.scheduleStopTime = this.scheduleStopTime;
		    model.schedulState = this.schedulState;
		    model.selfCheckQuality = this.selfCheckQuality;
		    model.singleOperationTime = this.singleOperationTime;
		    model.specifyFinishTime = this.specifyFinishTime;
		    model.specifyTimeMan = this.specifyTimeMan;
		    model.term = this.term;
		    model.versionInfo = this.versionInfo;
		    model.abnormalNums = this.abnormalNums;
		    model.procesParameters = this.procesParameters;
            return model;
        }
        
	}
	#endregion
}

