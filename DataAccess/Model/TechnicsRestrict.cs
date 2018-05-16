using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
namespace DataAccess.Model
{
    #region TechnicsRestrict
    /// <summary>
/// This object represents the properties and methods of a TechnicsRestrict .
    /// </summary>
    public class TechnicsRestrict
    {
    #region Properties //do not update!
        private double _preOperationID;
        //[Dapper.Key]
        public virtual double preOperationID
        {
        get {return _preOperationID;}
        set {_preOperationID = value;}
        }
        private double _operationID;
        //[Dapper.Key]
        public virtual double operationID
        {
        get {return _operationID;}
        set {_operationID = value;}
        }
        private string _restrictType = String.Empty;
        public virtual string restrictType
        {
        get {return _restrictType;}
        set {_restrictType = value;}
        }
        private double _intervalTime;
        public virtual double intervalTime
        {
        get {return _intervalTime;}
        set {_intervalTime = value;}
        }
        private string _mark = String.Empty;
        public virtual string mark
        {
        get {return _mark;}
        set {_mark = value;}
        }
        private bool _isEP;
        public virtual bool isEP
        {
        get {return _isEP;}
        set {_isEP = value;}
        }
    #endregion
        public TechnicsRestrict Copy()
         {
         var model = new TechnicsRestrict ();
         model.preOperationID = this.preOperationID;
         model.operationID = this.operationID;
         model.restrictType = this.restrictType;
         model.intervalTime = this.intervalTime;
         model.mark = this.mark;
         model.isEP = this.isEP;
         return model;
         }
    }
    #endregion
}
