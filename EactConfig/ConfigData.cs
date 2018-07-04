using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

namespace EactConfig
{
    public class ConfigData
    {
        static string _path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Path.Combine("Config", "config.json"));
        public static ConfigData GetInstance()
        {
            var json = string.Empty;
            if (File.Exists(_path))
            {
                json = File.ReadAllText(_path);
            }

            if (!string.IsNullOrEmpty(json)) 
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<ConfigData>(json) ?? new ConfigData();
            }
            return new ConfigData();
        }

        public static void WriteConfig(ConfigData data) 
        {
            var json=Newtonsoft.Json.JsonConvert.SerializeObject(data);
            File.WriteAllText(_path, json);
        }
        

        /// <summary>
        /// 数据库信息
        /// </summary>
        public class DataBase
        {
            public string Name { get; set; }
            public string IP { get; set; }
            public string User { get; set; }
            public string Pass { get; set; }
            public string LoginUser { get; set; }
            public string LoginPass { get; set; }
        }

        /// <summary>
        /// FTP信息
        /// </summary>
        public class FTPInfo 
        {
            public string Address { get; set; }
            public string User { get; set; }
            public string Pass { get; set; }
        }

        /// <summary>
        /// 属性信息
        /// </summary>
        public class Poperty 
        {
            [DisplayName("名称")]
            public string DisplayName { get; set; }
            public List<PopertySelection> Selections = new List<PopertySelection>();
        }


        /// <summary>
        /// 属性选项
        /// </summary>
        public class PopertySelection
        {
            [DisplayName("选项")]
            public string Value { get; set; }
            public bool IsDefault = false;
        }

        /// <summary>
        /// 数据库信息
        /// </summary>
        public DataBase DataBaseInfo = new DataBase();
        /// <summary>
        /// FTP信息
        /// </summary>
        public FTPInfo FTP = new FTPInfo();
        /// <summary>
        /// 属性信息
        /// </summary>
        public List<Poperty> Poperties = new List<Poperty>();

        public QuadrantType QuadrantType = QuadrantType.Three;
        public int LicenseType = 1;
        public int UGVersion = 0;
        public int ElecNameRule = 0;
        public bool ExportStp = false;
        public bool ExportPrt = false;
        public bool ExportCNCPrt = false;
        public bool IsImportEman = false;

        private string _EleRType { get; set; }
        public string EleRType { set { _EleRType = value; } get { if (string.IsNullOrEmpty(_EleRType)) return "R"; return _EleRType; } }
        private string _EleMType { get; set; }
        public string EleMType { set { _EleMType = value; } get { if (string.IsNullOrEmpty(_EleMType)) return "M"; return _EleMType; } }
        private string _EleFType { get; set; }
        public string EleFType { set { _EleFType = value; } get { if (string.IsNullOrEmpty(_EleFType)) return "F"; return _EleFType; } }
        /// <summary>
        /// 是否进行电打面分析
        /// </summary>
        public bool IsSetPrtColor = true;
        /// <summary>
        /// CNC翻转规则
        /// </summary>
        public int CNCTranRule = 0;
        public bool ShareElec = false;
        /// <summary>
        /// 翻转规则
        /// </summary>
        public int EDMTranRule = 0;
        /// <summary>
        /// 电打面颜色
        /// </summary>
        public int EDMColor = System.Drawing.Color.Red.ToArgb();
        /// <summary>
        /// 是否支持属性修改
        /// </summary>
        public bool IsCanPropertyUpdate=false;
    }
}
