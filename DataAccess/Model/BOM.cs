using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
namespace DataAccess.Model
{
    #region BOM
    /// <summary>
/// This object represents the properties and methods of a BOM .
    /// </summary>
    public class BOM
    {
    #region Properties //do not update!
        private int _iD;
        //[Dapper.Key]
        public virtual int ID
        {
        get {return _iD;}
        set {_iD = value;}
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
        private string _partName = String.Empty;
        public virtual string partName
        {
        get {return _partName;}
        set {_partName = value;}
        }
        private int _partType;
        public virtual int partType
        {
        get {return _partType;}
        set {_partType = value;}
        }
        private string _specifications = String.Empty;
        public virtual string specifications
        {
        get {return _specifications;}
        set {_specifications = value;}
        }
        private int _material;
        public virtual int material
        {
        get {return _material;}
        set {_material = value;}
        }
        private double _number;
        public virtual double number
        {
        get {return _number;}
        set {_number = value;}
        }
        private string _unit = String.Empty;
        public virtual string unit
        {
        get {return _unit;}
        set {_unit = value;}
        }
        private string _state = String.Empty;
        public virtual string state
        {
        get {return _state;}
        set {_state = value;}
        }
        private int _orderID;
        public virtual int orderID
        {
        get {return _orderID;}
        set {_orderID = value;}
        }
        private int _taskID;
        public virtual int taskID
        {
        get {return _taskID;}
        set {_taskID = value;}
        }
        private double _outStoreNums;
        public virtual double outStoreNums
        {
        get {return _outStoreNums;}
        set {_outStoreNums = value;}
        }
        private DateTime _createTime;
        public virtual DateTime createTime
        {
        get {return _createTime;}
        set {_createTime = value;}
        }
        private DateTime _modifyTime;
        public virtual DateTime modifyTime
        {
        get {return _modifyTime;}
        set {_modifyTime = value;}
        }
        private string _orderState = String.Empty;
        public virtual string orderState
        {
        get {return _orderState;}
        set {_orderState = value;}
        }
        private string _remark = String.Empty;
        public virtual string remark
        {
        get {return _remark;}
        set {_remark = value;}
        }
        private string _rawSpecifications = String.Empty;
        public virtual string rawSpecifications
        {
        get {return _rawSpecifications;}
        set {_rawSpecifications = value;}
        }
        private DateTime _modifyTimeOfDeal;
        public virtual DateTime modifyTimeOfDeal
        {
        get {return _modifyTimeOfDeal;}
        set {_modifyTimeOfDeal = value;}
        }
        private bool _dealState;
        public virtual bool dealState
        {
        get {return _dealState;}
        set {_dealState = value;}
        }
        private byte _isElectrodeImport;
        public virtual byte isElectrodeImport
        {
        get {return _isElectrodeImport;}
        set {_isElectrodeImport = value;}
        }
        private string _materialID = String.Empty;
        public virtual string materialID
        {
        get {return _materialID;}
        set {_materialID = value;}
        }
        private int _version;
        public virtual int version
        {
        get {return _version;}
        set {_version = value;}
        }
        private bool _isSubmit;
        public virtual bool isSubmit
        {
        get {return _isSubmit;}
        set {_isSubmit = value;}
        }
        private string _verifyID = String.Empty;
        public virtual string verifyID
        {
        get {return _verifyID;}
        set {_verifyID = value;}
        }
        private string _attribute1 = String.Empty;
        public virtual string attribute1
        {
        get {return _attribute1;}
        set {_attribute1 = value;}
        }
        private string _attribute2 = String.Empty;
        public virtual string attribute2
        {
        get {return _attribute2;}
        set {_attribute2 = value;}
        }
        private string _attribute3 = String.Empty;
        public virtual string attribute3
        {
        get {return _attribute3;}
        set {_attribute3 = value;}
        }
        private string _attribute4 = String.Empty;
        public virtual string attribute4
        {
        get {return _attribute4;}
        set {_attribute4 = value;}
        }
        private string _attribute5 = String.Empty;
        public virtual string attribute5
        {
        get {return _attribute5;}
        set {_attribute5 = value;}
        }
        private string _attribute6 = String.Empty;
        public virtual string attribute6
        {
        get {return _attribute6;}
        set {_attribute6 = value;}
        }
        private string _attribute7 = String.Empty;
        public virtual string attribute7
        {
        get {return _attribute7;}
        set {_attribute7 = value;}
        }
        private string _attribute8 = String.Empty;
        public virtual string attribute8
        {
        get {return _attribute8;}
        set {_attribute8 = value;}
        }
        private string _attribute9 = String.Empty;
        public virtual string attribute9
        {
        get {return _attribute9;}
        set {_attribute9 = value;}
        }
        private string _attribute10 = String.Empty;
        public virtual string attribute10
        {
        get {return _attribute10;}
        set {_attribute10 = value;}
        }
        private bool _isImport;
        public virtual bool isImport
        {
        get {return _isImport;}
        set {_isImport = value;}
        }
        private double _modifyNumber;
        public virtual double modifyNumber
        {
        get {return _modifyNumber;}
        set {_modifyNumber = value;}
        }
        private byte _modifyState;
        public virtual byte modifyState
        {
        get {return _modifyState;}
        set {_modifyState = value;}
        }
        private byte _orderDealState;
        public virtual byte orderDealState
        {
        get {return _orderDealState;}
        set {_orderDealState = value;}
        }
        private double _actualNumber;
        public virtual double actualNumber
        {
        get {return _actualNumber;}
        set {_actualNumber = value;}
        }
        private int _bomState;
        public virtual int bomState
        {
        get {return _bomState;}
        set {_bomState = value;}
        }
        private string _rejectReason = String.Empty;
        public virtual string rejectReason
        {
        get {return _rejectReason;}
        set {_rejectReason = value;}
        }
        private short _mark;
        public virtual short mark
        {
        get {return _mark;}
        set {_mark = value;}
        }
        private double _spareNumber;
        public virtual double spareNumber
        {
        get {return _spareNumber;}
        set {_spareNumber = value;}
        }
        private string _associatedFileID = String.Empty;
        public virtual string associatedFileID
        {
        get {return _associatedFileID;}
        set {_associatedFileID = value;}
        }
        private short _bomAlteration;
        public virtual short bomAlteration
        {
        get {return _bomAlteration;}
        set {_bomAlteration = value;}
        }
        private short _isAlterationInAnalyse;
        public virtual short isAlterationInAnalyse
        {
        get {return _isAlterationInAnalyse;}
        set {_isAlterationInAnalyse = value;}
        }
        private DateTime _auditTime;
        public virtual DateTime auditTime
        {
        get {return _auditTime;}
        set {_auditTime = value;}
        }
        private string _standardNumber = String.Empty;
        public virtual string standardNumber
        {
        get {return _standardNumber;}
        set {_standardNumber = value;}
        }
        private string _dataFrom = String.Empty;
        public virtual string dataFrom
        {
        get {return _dataFrom;}
        set {_dataFrom = value;}
        }
        private DateTime _inventorysplyTime;
        public virtual DateTime inventorysplyTime
        {
        get {return _inventorysplyTime;}
        set {_inventorysplyTime = value;}
        }
        private string _createMan = String.Empty;
        public virtual string createMan
        {
        get {return _createMan;}
        set {_createMan = value;}
        }
        private int _mouldframe;
        public virtual int mouldframe
        {
        get {return _mouldframe;}
        set {_mouldframe = value;}
        }
        private string _outsideSupplier = String.Empty;
        public virtual string outsideSupplier
        {
        get {return _outsideSupplier;}
        set {_outsideSupplier = value;}
        }
        private DateTime _outsideStartTime;
        public virtual DateTime outsideStartTime
        {
        get {return _outsideStartTime;}
        set {_outsideStartTime = value;}
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
        private string _machiningAttribute = String.Empty;
        public virtual string machiningAttribute
        {
        get {return _machiningAttribute;}
        set {_machiningAttribute = value;}
        }
        private string _contentsOutsourcing = String.Empty;
        public virtual string contentsOutsourcing
        {
        get {return _contentsOutsourcing;}
        set {_contentsOutsourcing = value;}
        }
        private string _singleWeight = String.Empty;
        public virtual string singleWeight
        {
        get {return _singleWeight;}
        set {_singleWeight = value;}
        }
        private string _totalWeight = String.Empty;
        public virtual string totalWeight
        {
        get {return _totalWeight;}
        set {_totalWeight = value;}
        }
        private int _certainMaterialID;
        public virtual int certainMaterialID
        {
        get {return _certainMaterialID;}
        set {_certainMaterialID = value;}
        }
        private int _brandID;
        public virtual int brandID
        {
        get {return _brandID;}
        set {_brandID = value;}
        }
        private double _preCount;
        public virtual double preCount
        {
        get {return _preCount;}
        set {_preCount = value;}
        }
        private string _preHumanID = String.Empty;
        public virtual string preHumanID
        {
        get {return _preHumanID;}
        set {_preHumanID = value;}
        }
        private DateTime _preTime;
        public virtual DateTime preTime
        {
        get {return _preTime;}
        set {_preTime = value;}
        }
        private string _preReMark = String.Empty;
        public virtual string preReMark
        {
        get {return _preReMark;}
        set {_preReMark = value;}
        }
        private int _groupNodeID;
        public virtual int groupNodeID
        {
        get {return _groupNodeID;}
        set {_groupNodeID = value;}
        }
        private int _changesProcurement;
        public virtual int changesProcurement
        {
        get {return _changesProcurement;}
        set {_changesProcurement = value;}
        }
        private string _centerOrder = String.Empty;
        public virtual string CenterOrder
        {
        get {return _centerOrder;}
        set {_centerOrder = value;}
        }
        private short _techStatus;
        public virtual short techStatus
        {
        get {return _techStatus;}
        set {_techStatus = value;}
        }
        private short _materialStatus;
        public virtual short materialStatus
        {
        get {return _materialStatus;}
        set {_materialStatus = value;}
        }
        private bool _hasFile;
        public virtual bool hasFile
        {
        get {return _hasFile;}
        set {_hasFile = value;}
        }
        private bool _splitOrMerged;
        public virtual bool splitOrMerged
        {
        get {return _splitOrMerged;}
        set {_splitOrMerged = value;}
        }
        private int _uniqueDeleteID;
        public virtual int uniqueDeleteID
        {
        get {return _uniqueDeleteID;}
        set {_uniqueDeleteID = value;}
        }
        private bool _dealAlterState;
        public virtual bool dealAlterState
        {
        get {return _dealAlterState;}
        set {_dealAlterState = value;}
        }
        private string _referenceProcess = String.Empty;
        public virtual string referenceProcess
        {
        get {return _referenceProcess;}
        set {_referenceProcess = value;}
        }
        private string _spareNum = String.Empty;
        public virtual string spareNum
        {
        get {return _spareNum;}
        set {_spareNum = value;}
        }
    #endregion
        public BOM Copy()
         {
         var model = new BOM ();
         model.ID = this.ID;
         model.mouldID = this.mouldID;
         model.partID = this.partID;
         model.partName = this.partName;
         model.partType = this.partType;
         model.specifications = this.specifications;
         model.material = this.material;
         model.number = this.number;
         model.unit = this.unit;
         model.state = this.state;
         model.orderID = this.orderID;
         model.taskID = this.taskID;
         model.outStoreNums = this.outStoreNums;
         model.createTime = this.createTime;
         model.modifyTime = this.modifyTime;
         model.orderState = this.orderState;
         model.remark = this.remark;
         model.rawSpecifications = this.rawSpecifications;
         model.modifyTimeOfDeal = this.modifyTimeOfDeal;
         model.dealState = this.dealState;
         model.isElectrodeImport = this.isElectrodeImport;
         model.materialID = this.materialID;
         model.version = this.version;
         model.isSubmit = this.isSubmit;
         model.verifyID = this.verifyID;
         model.attribute1 = this.attribute1;
         model.attribute2 = this.attribute2;
         model.attribute3 = this.attribute3;
         model.attribute4 = this.attribute4;
         model.attribute5 = this.attribute5;
         model.attribute6 = this.attribute6;
         model.attribute7 = this.attribute7;
         model.attribute8 = this.attribute8;
         model.attribute9 = this.attribute9;
         model.attribute10 = this.attribute10;
         model.isImport = this.isImport;
         model.modifyNumber = this.modifyNumber;
         model.modifyState = this.modifyState;
         model.orderDealState = this.orderDealState;
         model.actualNumber = this.actualNumber;
         model.bomState = this.bomState;
         model.rejectReason = this.rejectReason;
         model.mark = this.mark;
         model.spareNumber = this.spareNumber;
         model.associatedFileID = this.associatedFileID;
         model.bomAlteration = this.bomAlteration;
         model.isAlterationInAnalyse = this.isAlterationInAnalyse;
         model.auditTime = this.auditTime;
         model.standardNumber = this.standardNumber;
         model.dataFrom = this.dataFrom;
         model.inventorysplyTime = this.inventorysplyTime;
         model.createMan = this.createMan;
         model.mouldframe = this.mouldframe;
         model.outsideSupplier = this.outsideSupplier;
         model.outsideStartTime = this.outsideStartTime;
         model.requiredCompleteTime = this.requiredCompleteTime;
         model.expectedCompletionTime = this.expectedCompletionTime;
         model.machiningAttribute = this.machiningAttribute;
         model.contentsOutsourcing = this.contentsOutsourcing;
         model.singleWeight = this.singleWeight;
         model.totalWeight = this.totalWeight;
         model.certainMaterialID = this.certainMaterialID;
         model.brandID = this.brandID;
         model.preCount = this.preCount;
         model.preHumanID = this.preHumanID;
         model.preTime = this.preTime;
         model.preReMark = this.preReMark;
         model.groupNodeID = this.groupNodeID;
         model.changesProcurement = this.changesProcurement;
         model.CenterOrder = this.CenterOrder;
         model.techStatus = this.techStatus;
         model.materialStatus = this.materialStatus;
         model.hasFile = this.hasFile;
         model.splitOrMerged = this.splitOrMerged;
         model.uniqueDeleteID = this.uniqueDeleteID;
         model.dealAlterState = this.dealAlterState;
         model.referenceProcess = this.referenceProcess;
         model.spareNum = this.spareNum;
         return model;
         }
    }
    #endregion
}
