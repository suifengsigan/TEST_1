using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
namespace DataAccess.Model
{
    #region TypicalSeparateResourceUse
    /// <summary>
/// This object represents the properties and methods of a TypicalSeparateResourceUse .
    /// </summary>
    public class TypicalSeparateResourceUse
    {
    #region Properties //do not update!
        private double _operationID;
        //[Dapper.Key]
        public virtual double operationID
        {
        get {return _operationID;}
        set {_operationID = value;}
        }
        private string _resourceID = String.Empty;
        //[Dapper.Key]
        public virtual string resourceID
        {
        get {return _resourceID;}
        set {_resourceID = value;}
        }
        private int _priorityID;
        public virtual int priorityID
        {
        get {return _priorityID;}
        set {_priorityID = value;}
        }
        private byte _useUnit;
        public virtual byte useUnit
        {
        get {return _useUnit;}
        set {_useUnit = value;}
        }
    #endregion
        public TypicalSeparateResourceUse Copy()
         {
         var model = new TypicalSeparateResourceUse ();
         model.operationID = this.operationID;
         model.resourceID = this.resourceID;
         model.priorityID = this.priorityID;
         model.useUnit = this.useUnit;
         return model;
         }
    }
    #endregion
}
