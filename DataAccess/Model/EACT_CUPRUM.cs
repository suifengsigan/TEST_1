using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Model
{
    //电极
    public class EACT_CUPRUM
    {

        /// <summary>
        /// 电极ID
        /// </summary>
        public virtual decimal CUPRUMID
        {
            get;
            set;
        }
        /// <summary>
        /// 模具ID
        /// </summary>
        public virtual decimal MOULDID
        {
            get;
            set;
        }
        /// <summary>
        /// 电极主名称
        /// </summary>
        public virtual string CUPRUMNAME
        {
            get;
            set;
        }
        /// <summary>
        /// 电极编号
        /// </summary>
        public virtual string CUPRUMSN
        {
            get;
            set;
        }
        /// <summary>
        /// 火花位
        /// </summary>
        public virtual string FRIENUM
        {
            get;
            set;
        }
        /// <summary>
        /// VDI
        /// </summary>
        public virtual string VDI
        {
            get;
            set;
        }
        /// <summary>
        /// 电极材质
        /// </summary>
        public virtual string STRUFF
        {
            get;
            set;
        }
        /// <summary>
        /// EDM条件编号
        /// </summary>
        public virtual string EDMCONDITIONSN
        {
            get;
            set;
        }
        /// <summary>
        /// 物料代码
        /// </summary>
        public virtual string STRUFFCODE
        {
            get;
            set;
        }
        /// <summary>
        /// 配合状态
        /// </summary>
        public virtual string COORDINATE
        {
            get;
            set;
        }
        /// <summary>
        /// 工时等级
        /// </summary>
        public virtual string TASKTIMERANK
        {
            get;
            set;
        }
        /// <summary>
        /// 开料原因
        /// </summary>
        public virtual string OPENSTRUFF
        {
            get;
            set;
        }
        /// <summary>
        /// 坯料类型
        /// </summary>
        public virtual string STRUFFTYPE
        {
            get;
            set;
        }
        /// <summary>
        /// 物料群组
        /// </summary>
        public virtual string STRUFFGROUPL
        {
            get;
            set;
        }
        /// <summary>
        /// 要求交货日期
        /// </summary>
        public virtual string DATEOFDELIVERY
        {
            get;
            set;
        }
        /// <summary>
        /// 工位类型
        /// </summary>
        public virtual string UNIT
        {
            get;
            set;
        }
        /// <summary>
        /// 开始放电面积
        /// </summary>
        public virtual string DISCHARGING0
        {
            get;
            set;
        }
        /// <summary>
        /// 放电面积
        /// </summary>
        public virtual string DISCHARGING
        {
            get;
            set;
        }
        /// <summary>
        /// 电极形状
        /// </summary>
        public virtual string SHAPE
        {
            get;
            set;
        }
        /// <summary>
        /// 摇摆方式
        /// </summary>
        public virtual string ROCK
        {
            get;
            set;
        }
        /// <summary>
        /// 精度等级
        /// </summary>
        public virtual string PRECISION
        {
            get;
            set;
        }
        /// <summary>
        /// 加工位置个数
        /// </summary>
        public virtual string PROCESSNUM
        {
            get;
            set;
        }
        /// <summary>
        /// X跑位值
        /// </summary>
        public virtual string X
        {
            get;
            set;
        }
        /// <summary>
        /// Y跑位值
        /// </summary>
        public virtual string Y
        {
            get;
            set;
        }
        /// <summary>
        /// Z跑位值
        /// </summary>
        public virtual string Z
        {
            get;
            set;
        }
        /// <summary>
        /// C轴旋转角度
        /// </summary>
        public virtual string C
        {
            get;
            set;
        }
        /// <summary>
        /// 加工方向
        /// </summary>
        public virtual string PROCDIRECTION
        {
            get;
            set;
        }
        /// <summary>
        /// 安全深度
        /// </summary>
        public virtual string SAFEDEPTH
        {
            get;
            set;
        }
        /// <summary>
        /// 电极基座规格
        /// </summary>
        public virtual string SUBSTRATENORMS
        {
            get;
            set;
        }
        /// <summary>
        /// 基准台C角象限
        /// </summary>
        public virtual string SUBSTRATECQUADRANT
        {
            get;
            set;
        }
        /// <summary>
        /// 基准台C角大小
        /// </summary>
        public virtual string SUBSTRATECSIZE
        {
            get;
            set;
        }
        /// <summary>
        /// 基准台R角大小
        /// </summary>
        public virtual string SUBSTRATERSIZE
        {
            get;
            set;
        }
        /// <summary>
        /// 基准台圆角
        /// </summary>
        public virtual string SUBSTRATEBEAD
        {
            get;
            set;
        }
        /// <summary>
        /// 基准台圆角大小
        /// </summary>
        public virtual string SUBSTRATEBEADSIZE
        {
            get;
            set;
        }
        /// <summary>
        /// 钢件模号
        /// </summary>
        public virtual string STEELMODELSN
        {
            get;
            set;
        }
        /// <summary>
        /// 钢件件号
        /// </summary>
        public virtual string STEELMODULESN
        {
            get;
            set;
        }
        /// <summary>
        /// 钢件版次
        /// </summary>
        public virtual string STEELVER
        {
            get;
            set;
        }
        /// <summary>
        /// 模穴数
        /// </summary>
        public virtual string HOLENUM
        {
            get;
            set;
        }
        /// <summary>
        /// 借用模号
        /// </summary>
        public virtual string BORROWMODELSN
        {
            get;
            set;
        }
        /// <summary>
        /// 借用件号
        /// </summary>
        public virtual string BORROWNODULESN
        {
            get;
            set;
        }
        /// <summary>
        /// 借用版次
        /// </summary>
        public virtual string BORROWVER
        {
            get;
            set;
        }
        /// <summary>
        /// 借用电极
        /// </summary>
        public virtual string BORROWCUPRUM
        {
            get;
            set;
        }
        /// <summary>
        /// 合陪模号
        /// </summary>
        public virtual string COMBINEMODELNO
        {
            get;
            set;
        }
        /// <summary>
        /// 合陪版本号
        /// </summary>
        public virtual string COMBINEVER
        {
            get;
            set;
        }
        /// <summary>
        /// 治具规格
        /// </summary>
        public virtual string CHUCK
        {
            get;
            set;
        }
        /// <summary>
        /// 工艺制程
        /// </summary>
        public virtual string CRAFTWORK
        {
            get;
            set;
        }
        /// <summary>
        /// 电极设计者
        /// </summary>
        public virtual string STYLIST
        {
            get;
            set;
        }
        /// <summary>
        /// 清根高度
        /// </summary>
        public virtual string CLEARROOTH
        {
            get;
            set;
        }
        /// <summary>
        /// 预置清根
        /// </summary>
        public virtual string CLEARROOT
        {
            get;
            set;
        }
        /// <summary>
        /// 要求料伸出长
        /// </summary>
        public virtual string STRETCHH
        {
            get;
            set;
        }
        /// <summary>
        /// NC加工纵深值
        /// </summary>
        public virtual string NCDEPTH
        {
            get;
            set;
        }
        /// <summary>
        /// 电极头加拉伸高度
        /// </summary>
        public virtual string HEADPULLUPH
        {
            get;
            set;
        }
        /// <summary>
        /// NC聚集编号
        /// </summary>
        public virtual string NCSN
        {
            get;
            set;
        }
        /// <summary>
        /// 备注
        /// </summary>
        public virtual string ASSEMBLYEXP
        {
            get;
            set;
        }

        /// <summary>
        /// 电极粗中精
        /// </summary>
        public virtual string RMF { set; get; }
        /// <summary>
        /// 文件PART的名称
        /// </summary>
        public virtual string PARTFILENAME { set; get; }

        public virtual decimal? OFFSETX { set; get; }
        public virtual decimal? OFFSETY { set; get; }
        public virtual decimal? OFFSETZ { set; get; }
        /// <summary>
        /// 钢件材质
        /// </summary>
        public virtual string STEEL { set; get; }
        /// <summary>
        /// 电极规格
        /// </summary>
        public virtual string SPEC { set; get; }
        /// <summary>
        /// 当前区域
        /// </summary>
        public string REGION
        {
            get;
            set;
        }
        #region for HTK
        /// <summary>
        /// 抛光留量
        /// </summary>
        public string BUFFING { get; set; }
        /// <summary>
        /// 材料组合
        /// </summary>
        public string STUFFCOMB { get; set; }
        /// <summary>
        /// 摇动平面形状
        /// </summary>
        public string ROCKSHAPE { get; set; }
        /// <summary>
        /// 腐蚀条件
        /// </summary>
        public string CORRCONDITION { get; set; }
        /// <summary>
        /// 加工条件
        /// </summary>
        public string PROCCONDITION { get; set; }
        /// <summary>
        /// 钢件Z值
        /// </summary>
        public virtual decimal STEELZ { get; set; }
        #endregion

        /// <summary>
        /// 自定义火花位Z补正
        /// </summary>
        public virtual decimal? FRIENUMZ { set; get; }
        /// <summary>
        /// 偏心方向
        /// </summary>
        public virtual string ECCDIRECTION { get; set; }
        /// <summary>
        /// 偏心值
        /// </summary>
        public virtual decimal? ECCENTRIC { get; set; }

        private string _OFFSETTYPE = "CMM";
        /// <summary>
        /// 偏移值类别(CMM EROWA)
        /// </summary>
        public virtual string OFFSETTYPE
        {
            get
            {
                return _OFFSETTYPE;
            }
            set
            {
                _OFFSETTYPE = value;
            }
        }

        /// <summary>
        /// 电极间隙已计算未计算
        /// </summary>
        public virtual string CAPSET { get; set; }
        /// <summary>
        /// EM-109921正泰--正泰EMan_EACT电极导入接口方案_V3
        /// </summary>
        public virtual string verifyIDName { get; set; }
        /// <summary>
        /// EM-109921正泰--正泰EMan_EACT电极导入接口方案_V3
        /// </summary>
        public virtual int MaterialID { get; set; }
        /// <summary>
        /// EM-109921正泰--正泰EMan_EACT电极导入接口方案_V3
        /// </summary>
        public virtual int MouldPart_ID { get; set; }
        /// <summary>
        /// EM-109921正泰--正泰EMan_EACT电极导入接口方案_V3
        /// </summary>
        public virtual string MouldPart_PartID { get; set; }
        /// <summary>
        /// EM-109921正泰--正泰EMan_EACT电极导入接口方案_V3
        /// </summary>
        public virtual string TypicalPartID { get; set; }
        /// <summary>
        /// EM-109921正泰--正泰EMan_EACT电极导入接口方案_V3
        /// </summary>
        public  int CUPRUMCOUNT = 1;
        /// <summary>
        /// EM-109921正泰--正泰EMan_EACT电极导入接口方案_V3
        /// </summary>
        public virtual string StandardPartNodeID { get; set; }
         /// <summary>
        /// EM-109921正泰--正泰EMan_EACT电极导入接口方案_V3
        /// </summary>
        public virtual int MaterialClass { get;set; }
        /// <summary>
        /// EM-109921正泰--正泰EMan_EACT电极导入接口方案_V3
        /// </summary>
        public virtual string StandardStore_Unit { get; set; }
        /// <summary>
        /// EM-109921正泰--正泰EMan_EACT电极导入接口方案_V3
        /// </summary>
        public virtual string McfMaterialID { get; set; }
        /// <summary>
        /// EM-109921正泰--正泰EMan_EACT电极导入接口方案_V3
        /// </summary>
        public virtual int CertainMaterialID { get; set; }
        /// <summary>
        /// EM-109921正泰--正泰EMan_EACT电极导入接口方案_V3
        /// </summary>
        public virtual int PartClassID { get; set; }
        public virtual string CREATOR { get; set; }
    }
}
