using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
namespace DataAccess.Model
{
    #region StandardStore
    /// <summary>
/// This object represents the properties and methods of a StandardStore .
    /// </summary>
    public class StandardStore
    {
    #region Properties //do not update!
        private long _storeID;
        //[Dapper.Key]
        public virtual long storeID
        {
        get {return _storeID;}
        set {_storeID = value;}
        }
        private int _storeSaveID;
        public virtual int storeSaveID
        {
        get {return _storeSaveID;}
        set {_storeSaveID = value;}
        }
        private string _shelfID = String.Empty;
        public virtual string shelfID
        {
        get {return _shelfID;}
        set {_shelfID = value;}
        }
        private string _materialID = String.Empty;
        public virtual string materialID
        {
        get {return _materialID;}
        set {_materialID = value;}
        }
        private string _specifications = String.Empty;
        public virtual string specifications
        {
        get {return _specifications;}
        set {_specifications = value;}
        }
        private double _stocks;
        public virtual double stocks
        {
        get {return _stocks;}
        set {_stocks = value;}
        }
        private double _minStocks;
        public virtual double minStocks
        {
        get {return _minStocks;}
        set {_minStocks = value;}
        }
        private double _maxStocks;
        public virtual double maxStocks
        {
        get {return _maxStocks;}
        set {_maxStocks = value;}
        }
        private double _alertStocks;
        public virtual double alertStocks
        {
        get {return _alertStocks;}
        set {_alertStocks = value;}
        }
        private string _unit = String.Empty;
        public virtual string unit
        {
        get {return _unit;}
        set {_unit = value;}
        }
        private double _price;
        public virtual double price
        {
        get {return _price;}
        set {_price = value;}
        }
        private double _addUpPrices;
        public virtual double addUpPrices
        {
        get {return _addUpPrices;}
        set {_addUpPrices = value;}
        }
        private DateTime _actualTime;
        public virtual DateTime actualTime
        {
        get {return _actualTime;}
        set {_actualTime = value;}
        }
        private double _standardPrice;
        public virtual double standardPrice
        {
        get {return _standardPrice;}
        set {_standardPrice = value;}
        }
        private string _remark = String.Empty;
        public virtual string remark
        {
        get {return _remark;}
        set {_remark = value;}
        }
        private string _specNumber = String.Empty;
        public virtual string specNumber
        {
        get {return _specNumber;}
        set {_specNumber = value;}
        }
        private string _stockCode = String.Empty;
        public virtual string stockCode
        {
        get {return _stockCode;}
        set {_stockCode = value;}
        }
        private string _standardNumber = String.Empty;
        public virtual string standardNumber
        {
        get {return _standardNumber;}
        set {_standardNumber = value;}
        }
        private short _isImportPrice;
        public virtual short isImportPrice
        {
        get {return _isImportPrice;}
        set {_isImportPrice = value;}
        }
        private bool _mark;
        public virtual bool mark
        {
        get {return _mark;}
        set {_mark = value;}
        }
        private bool _useState;
        public virtual bool useState
        {
        get {return _useState;}
        set {_useState = value;}
        }
        private bool _isDirectUse;
        public virtual bool isDirectUse
        {
        get {return _isDirectUse;}
        set {_isDirectUse = value;}
        }
        private string _oldSpecnumber = String.Empty;
        public virtual string oldSpecnumber
        {
        get {return _oldSpecnumber;}
        set {_oldSpecnumber = value;}
        }
    #endregion
        public StandardStore Copy()
         {
         var model = new StandardStore ();
         model.storeID = this.storeID;
         model.storeSaveID = this.storeSaveID;
         model.shelfID = this.shelfID;
         model.materialID = this.materialID;
         model.specifications = this.specifications;
         model.stocks = this.stocks;
         model.minStocks = this.minStocks;
         model.maxStocks = this.maxStocks;
         model.alertStocks = this.alertStocks;
         model.unit = this.unit;
         model.price = this.price;
         model.addUpPrices = this.addUpPrices;
         model.actualTime = this.actualTime;
         model.standardPrice = this.standardPrice;
         model.remark = this.remark;
         model.specNumber = this.specNumber;
         model.stockCode = this.stockCode;
         model.standardNumber = this.standardNumber;
         model.isImportPrice = this.isImportPrice;
         model.mark = this.mark;
         model.useState = this.useState;
         model.isDirectUse = this.isDirectUse;
         model.oldSpecnumber = this.oldSpecnumber;
         return model;
         }
    }
    #endregion
}
