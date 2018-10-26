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
            /// <summary>
            /// 拓展字段1
            /// </summary>
            public string Ex1 = string.Empty;
            /// <summary>
            /// 拓展字段2
            /// </summary>
            public string Ex2 = string.Empty;
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
        public bool IsSetPropertyAllowMultiple = false;
        /// <summary>
        /// 版本 0默认叛变 1PZ版本 2鸿通 3宝讯
        /// </summary>
        public int Edition = 0;
        /// <summary>
        /// AutoCMM图档
        /// </summary>
        public bool IsAutoCMM = false;
        /// <summary>
        /// 是否删除图纸
        /// </summary>
        public bool IsDeleteDraft = false;
        /// <summary>
        /// 粗中精材质是否可选
        /// </summary>
        public bool IsMatNameSel = false;
        /// <summary>
        /// 异形电极
        /// </summary>
        public bool SpecialshapedElec = false;
        /// <summary>
        /// BOM工具是否可以选择电极
        /// </summary>
        public bool IsCanSelElecInBom = false;
        /// <summary>
        /// Bom工具是否可以选择图层
        /// </summary>
        public bool isCanSelLayerInBom = false;
        /// <summary>
        /// 是否导出物料单
        /// </summary>
        public bool IsExportBomXls = false;
        /// <summary>
        /// 毛坯余量
        /// </summary>
        public double PQBlankStock = 1.5;
        /// <summary>
        /// 是否识别侧放电极
        /// </summary>
        public bool IsDistinguishSideElec = true;
        /// <summary>
        /// 是否启用自动导图档功能
        /// </summary>
        public bool IsAutoPrtTool = false;
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
        /// <summary>
        /// 电极是否设置默认值
        /// </summary>
        public bool IsElecSetDefault = false;
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
        public int FtpPathType = 0;
    }
}
