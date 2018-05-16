using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Model
{
    public class StandardStoreEx:StandardStore
    {
        public string mcfMaterialID { get; set; }
        public int materialClass { get; set; }
        public int Material_MaterialID { get; set; }
        public string MaterialName { get; set; }

        public int? certainMaterialID { get; set; }
        public double X
        {
            get
            {
                var list = SpecificationEx();
                if (list.Count > 0)
                {
                    return list[0];
                }
                return double.MinValue;
            }
            set { }
        }
        public double Y
        {
            get
            {
                var list = SpecificationEx();
                if (list.Count > 1)
                {
                    return list[1];
                }
                return double.MinValue;
            }
            set { }
        }
        public double Z
        {
            get
            {
                var list = SpecificationEx();
                if (list.Count > 2)
                {
                    return list[2];
                }
                return double.MinValue;
            }
            set { }
        }
        private List<double> SpecificationEx() 
        {
            var list=new List<double>();
            foreach (var item in specifications.Split('X').ToList()) 
            {
                var dValue = double.MinValue;
                double.TryParse(item, out dValue);
                list.Add(dValue);
            }
            return list;
        }
    }
}
