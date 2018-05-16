using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
namespace DataAccess.Model
{
    #region Human
    /// <summary>
/// This object represents the properties and methods of a Human .
    /// </summary>
    public class Human
    {
    #region Properties //do not update!
        private string _humanID = String.Empty;
        //[Dapper.Key]
        public virtual string humanID
        {
        get {return _humanID;}
        set {_humanID = value;}
        }
        private string _loginID = String.Empty;
        public virtual string loginID
        {
        get {return _loginID;}
        set {_loginID = value;}
        }
        private string _name = String.Empty;
        public virtual string name
        {
        get {return _name;}
        set {_name = value;}
        }
        private string _gender = String.Empty;
        public virtual string gender
        {
        get {return _gender;}
        set {_gender = value;}
        }
        private string _officeTel = String.Empty;
        public virtual string officeTel
        {
        get {return _officeTel;}
        set {_officeTel = value;}
        }
        private string _homeTel = String.Empty;
        public virtual string homeTel
        {
        get {return _homeTel;}
        set {_homeTel = value;}
        }
        private string _mobilePhone = String.Empty;
        public virtual string mobilePhone
        {
        get {return _mobilePhone;}
        set {_mobilePhone = value;}
        }
        private string _email = String.Empty;
        public virtual string email
        {
        get {return _email;}
        set {_email = value;}
        }
        private DateTime _startDate;
        public virtual DateTime startDate
        {
        get {return _startDate;}
        set {_startDate = value;}
        }
        private string _education = String.Empty;
        public virtual string education
        {
        get {return _education;}
        set {_education = value;}
        }
        private string _levelOfFLangurage = String.Empty;
        public virtual string levelOfFLangurage
        {
        get {return _levelOfFLangurage;}
        set {_levelOfFLangurage = value;}
        }
        private string _studyExperience = String.Empty;
        public virtual string studyExperience
        {
        get {return _studyExperience;}
        set {_studyExperience = value;}
        }
        private string _workTypeID = String.Empty;
        public virtual string workTypeID
        {
        get {return _workTypeID;}
        set {_workTypeID = value;}
        }
        private int _dutyID;
        public virtual int dutyID
        {
        get {return _dutyID;}
        set {_dutyID = value;}
        }
        private string _shift = String.Empty;
        public virtual string shift
        {
        get {return _shift;}
        set {_shift = value;}
        }
        private string _interests = String.Empty;
        public virtual string interests
        {
        get {return _interests;}
        set {_interests = value;}
        }
        private string _trainingExperience = String.Empty;
        public virtual string trainingExperience
        {
        get {return _trainingExperience;}
        set {_trainingExperience = value;}
        }
        private string _jobRecord = String.Empty;
        public virtual string jobRecord
        {
        get {return _jobRecord;}
        set {_jobRecord = value;}
        }
        private string _family = String.Empty;
        public virtual string family
        {
        get {return _family;}
        set {_family = value;}
        }
        private string _skill = String.Empty;
        public virtual string skill
        {
        get {return _skill;}
        set {_skill = value;}
        }
        private string _contractNo = String.Empty;
        public virtual string contractNo
        {
        get {return _contractNo;}
        set {_contractNo = value;}
        }
        private DateTime _stopDate;
        public virtual DateTime stopDate
        {
        get {return _stopDate;}
        set {_stopDate = value;}
        }
        private string _isStop = String.Empty;
        public virtual string isStop
        {
        get {return _isStop;}
        set {_isStop = value;}
        }
        private string _deptID = String.Empty;
        public virtual string deptID
        {
        get {return _deptID;}
        set {_deptID = value;}
        }
        private string _remark = String.Empty;
        public virtual string remark
        {
        get {return _remark;}
        set {_remark = value;}
        }
        private string _imageFile = String.Empty;
        public virtual string imageFile
        {
        get {return _imageFile;}
        set {_imageFile = value;}
        }
        private string _workExperience = String.Empty;
        public virtual string workExperience
        {
        get {return _workExperience;}
        set {_workExperience = value;}
        }
        private string _humanMonitorID = String.Empty;
        public virtual string humanMonitorID
        {
        get {return _humanMonitorID;}
        set {_humanMonitorID = value;}
        }
        private string _humanMonitorLengthID = String.Empty;
        public virtual string humanMonitorLengthID
        {
        get {return _humanMonitorLengthID;}
        set {_humanMonitorLengthID = value;}
        }
        private short _mark;
        public virtual short mark
        {
        get {return _mark;}
        set {_mark = value;}
        }
        private double _unitHoursPrice;
        public virtual double unitHoursPrice
        {
        get {return _unitHoursPrice;}
        set {_unitHoursPrice = value;}
        }
        private string _languageID = String.Empty;
        public virtual string languageID
        {
        get {return _languageID;}
        set {_languageID = value;}
        }
        private string _canChgeHumanID = String.Empty;
        public virtual string canChgeHumanID
        {
        get {return _canChgeHumanID;}
        set {_canChgeHumanID = value;}
        }
        private string _address = String.Empty;
        public virtual string address
        {
        get {return _address;}
        set {_address = value;}
        }
    #endregion
        public Human Copy()
         {
         var model = new Human ();
         model.humanID = this.humanID;
         model.loginID = this.loginID;
         model.name = this.name;
         model.gender = this.gender;
         model.officeTel = this.officeTel;
         model.homeTel = this.homeTel;
         model.mobilePhone = this.mobilePhone;
         model.email = this.email;
         model.startDate = this.startDate;
         model.education = this.education;
         model.levelOfFLangurage = this.levelOfFLangurage;
         model.studyExperience = this.studyExperience;
         model.workTypeID = this.workTypeID;
         model.dutyID = this.dutyID;
         model.shift = this.shift;
         model.interests = this.interests;
         model.trainingExperience = this.trainingExperience;
         model.jobRecord = this.jobRecord;
         model.family = this.family;
         model.skill = this.skill;
         model.contractNo = this.contractNo;
         model.stopDate = this.stopDate;
         model.isStop = this.isStop;
         model.deptID = this.deptID;
         model.remark = this.remark;
         model.imageFile = this.imageFile;
         model.workExperience = this.workExperience;
         model.humanMonitorID = this.humanMonitorID;
         model.humanMonitorLengthID = this.humanMonitorLengthID;
         model.mark = this.mark;
         model.unitHoursPrice = this.unitHoursPrice;
         model.languageID = this.languageID;
         model.canChgeHumanID = this.canChgeHumanID;
         model.address = this.address;
         return model;
         }
    }
    #endregion
}
