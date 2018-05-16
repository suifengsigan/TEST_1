using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnapEx
{
    /// <summary>
    /// 电极信息
    /// </summary>
    public class ElecInfo
    {
        public string ElectName { get; set; }
        public double QingGenValue { get; set; }
        public double DistanceValue { get; set; }
        public double JizhutaiValue { get; set; }
        public double DaojiaoValue { get; set; }
        public double DaoyuanValue { get; set; }
        public int QuadrantType { get; set; }
        public int BodyColor { get; set; }

        public void Serialize(string path)
        {
            var str= Newtonsoft.Json.JsonConvert.SerializeObject(this);
            var fileName = System.IO.Path.Combine(path, SnapEx.ConstString.AttributeInfo);
            System.IO.File.WriteAllText(fileName, str);
        }

        public static ElecInfo Deserialize(string path) 
        {
            var fileName = System.IO.Path.Combine(path, SnapEx.ConstString.AttributeInfo);
            var text = System.IO.File.ReadAllText(fileName);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<ElecInfo>(text);
        }
    }
}