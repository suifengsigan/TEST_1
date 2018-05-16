using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
namespace DataAccess.Model
{
    #region WorkStepInfo
    /// <summary>
/// This object represents the properties and methods of a WorkStepInfo .
    /// </summary>
    public class WorkStepInfo
    {
    #region Properties //do not update!
        private long _workStepInfoID;
        //[Dapper.Key]
        public virtual long workStepInfoID
        {
        get {return _workStepInfoID;}
        set {_workStepInfoID = value;}
        }
        private double _operationID;
        public virtual double operationID
        {
        get {return _operationID;}
        set {_operationID = value;}
        }
        private int _workOrderID;
        public virtual int workOrderID
        {
        get {return _workOrderID;}
        set {_workOrderID = value;}
        }
        private double _workPlanHours;
        public virtual double workPlanHours
        {
        get {return _workPlanHours;}
        set {_workPlanHours = value;}
        }
        private double _workGrossHours;
        public virtual double workGrossHours
        {
        get {return _workGrossHours;}
        set {_workGrossHours = value;}
        }
        private short _workState;
        public virtual short workState
        {
        get {return _workState;}
        set {_workState = value;}
        }
        private string _workStepContent = String.Empty;
        public virtual string workStepContent
        {
        get {return _workStepContent;}
        set {_workStepContent = value;}
        }
        private DateTime _workBegTime;
        public virtual DateTime workBegTime
        {
        get {return _workBegTime;}
        set {_workBegTime = value;}
        }
        private DateTime _workEndTime;
        public virtual DateTime workEndTime
        {
        get {return _workEndTime;}
        set {_workEndTime = value;}
        }
        private DateTime _lastMonitorTime;
        public virtual DateTime lastMonitorTime
        {
        get {return _lastMonitorTime;}
        set {_lastMonitorTime = value;}
        }
        private double _resourceHours;
        public virtual double resourceHours
        {
        get {return _resourceHours;}
        set {_resourceHours = value;}
        }
        private double _humanHours;
        public virtual double humanHours
        {
        get {return _humanHours;}
        set {_humanHours = value;}
        }
        private string _remark = String.Empty;
        public virtual string remark
        {
        get {return _remark;}
        set {_remark = value;}
        }
        private short _disHoursState;
        public virtual short disHoursState
        {
        get {return _disHoursState;}
        set {_disHoursState = value;}
        }
        private short _disNumberState;
        public virtual short disNumberState
        {
        get {return _disNumberState;}
        set {_disNumberState = value;}
        }
        private string _addHumanID = String.Empty;
        public virtual string addHumanID
        {
        get {return _addHumanID;}
        set {_addHumanID = value;}
        }
        private string _disHumanID = String.Empty;
        public virtual string disHumanID
        {
        get {return _disHumanID;}
        set {_disHumanID = value;}
        }
        private double _planTotalHours;
        public virtual double planTotalHours
        {
        get {return _planTotalHours;}
        set {_planTotalHours = value;}
        }
        private string _matchCode = String.Empty;
        public virtual string matchCode
        {
        get {return _matchCode;}
        set {_matchCode = value;}
        }
        private string _dataFrom = String.Empty;
        public virtual string dataFrom
        {
        get {return _dataFrom;}
        set {_dataFrom = value;}
        }
    #endregion
        public WorkStepInfo Copy()
         {
         var model = new WorkStepInfo ();
         model.workStepInfoID = this.workStepInfoID;
         model.operationID = this.operationID;
         model.workOrderID = this.workOrderID;
         model.workPlanHours = this.workPlanHours;
         model.workGrossHours = this.workGrossHours;
         model.workState = this.workState;
         model.workStepContent = this.workStepContent;
         model.workBegTime = this.workBegTime;
         model.workEndTime = this.workEndTime;
         model.lastMonitorTime = this.lastMonitorTime;
         model.resourceHours = this.resourceHours;
         model.humanHours = this.humanHours;
         model.remark = this.remark;
         model.disHoursState = this.disHoursState;
         model.disNumberState = this.disNumberState;
         model.addHumanID = this.addHumanID;
         model.disHumanID = this.disHumanID;
         model.planTotalHours = this.planTotalHours;
         model.matchCode = this.matchCode;
         model.dataFrom = this.dataFrom;
         return model;
         }
    }
    #endregion
}
