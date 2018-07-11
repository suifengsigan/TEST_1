using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SnapEx;

namespace ElecManage
{
    /// <summary>
    /// 电极属性信息(默认为进玉电极)
    /// </summary>
    public class ElectrodeInfo
    {
        protected Snap.NX.Body _body;
        protected ElectrodeInfo() { }

        protected string ELEC_FINISH_NUMBER = "ELEC_FINISH_NUMBER";
        protected string ELEC_MIDDLE_NUMBER = "ELEC_MIDDLE_NUMBER";
        protected string ELEC_ROUGH_NUMBER = "ELEC_ROUGH_NUMBER";
        protected string ELEC_FINISH_SPACE = "ELEC_FINISH_SPACE";
        protected string ELEC_MIDDLE_SPACE = "ELEC_MIDDLE_SPACE";
        protected string ELEC_ROUGH_SPACE = "ELEC_ROUGH_SPACE";
        protected string MODEL_NUMBER = "MODEL_NUMBER";  //模号
        protected string MR_NUMBER = "MR_NUMBER";//件号
        protected string ELEC_MAT_NAME = "ELEC_MAT_NAME"; //材质
        protected string ELEC_F_MAT_NAME = "ELEC_F_MAT_NAME";
        protected string ELEC_M_MAT_NAME = "ELEC_M_MAT_NAME";
        protected string ELEC_R_MAT_NAME = "ELEC_R_MAT_NAME";
        protected string CLAMP_GENERAL_TYPE = "CLAMP_GENERAL_TYPE";//夹具类型
        protected string MR_MATERAL = "MR_MATERAL";//钢件材质
        protected string ELEC_MACH_TYPE = "ELEC_MACH_TYPE"; //电极类型
        protected string F_ELEC_SMOOTH = "F_ELEC_SMOOTH";//光洁度
        protected string M_ELEC_SMOOTH = "M_ELEC_SMOOTH";
        protected string R_ELEC_SMOOTH = "R_ELEC_SMOOTH";
        protected string NEW_OR_MODIFY_MODL = "NEW_OR_MODIFY_MODL";//开料原因
        protected string EACT_EDMPROCDIRECTION = "EACT_EDMPROCDIRECTION";//加工方向
        protected string EACT_EDMROCK = "EACT_EDMROCK"; //摇摆方式
        protected string EACT_SHAREELEC = "EACT_SHAREELEC";//共用电极

        void SetStringAttribute(string name, string value) 
        {
            if (!string.IsNullOrEmpty(value)) 
            {
                _body.SetStringAttribute(name, value);
            }
        }
        public ElectrodeInfo(Snap.NX.Body body) 
        {
            _body = body;
        }

        public string ElecSize 
        {
            get 
            {
                var elecBox = _body.AcsToWcsBox3d();
                return string.Format("{0}x{1}x{2}",
                                Math.Round(Math.Abs(elecBox.MaxX - elecBox.MinX), 4)
                                , Math.Round(Math.Abs(elecBox.MaxY - elecBox.MinY),4)
                                , Math.Round(Math.Abs(elecBox.MaxZ - elecBox.MinZ),4)
                                );
            }
            set { }
        }

        /// <summary>
        /// 电极名称
        /// </summary>
        public virtual string Elec_Name
        {
            get { return _body.Name; }
            set { _body.Name = value; }
        }

        /// <summary>
        /// 夹具类型
        /// </summary>
        public virtual string ELEC_CLAMP_GENERAL_TYPE 
        {
            get
            {
                return _body.GetAttrValue(CLAMP_GENERAL_TYPE);
            }
            set
            {
                SetStringAttribute(CLAMP_GENERAL_TYPE, value);
            }
        }

        /// <summary>
        /// 精公数量
        /// </summary>
        public virtual int FINISH_NUMBER 
        {
            get 
            {
                return _body.GetAttrIntegerValue(ELEC_FINISH_NUMBER);
            }
            set 
            {
                _body.SetIntegerAttribute(ELEC_FINISH_NUMBER, value);
            }
        }
        /// <summary>
        /// 中公数量
        /// </summary>
        public virtual int MIDDLE_NUMBER
        {
            get
            {
                return _body.GetAttrIntegerValue(ELEC_MIDDLE_NUMBER);
            }
            set
            {
                _body.SetIntegerAttribute(ELEC_MIDDLE_NUMBER, value);
            }
        }
        /// <summary>
        /// 粗公数量
        /// </summary>
        public virtual int ROUGH_NUMBER
        {
            get
            {
                return _body.GetAttrIntegerValue(ELEC_ROUGH_NUMBER);
            }
            set
            {
                _body.SetIntegerAttribute(ELEC_ROUGH_NUMBER, value);
            }
        }
        /// <summary>
        /// 精公火花位
        /// </summary>
        public virtual double FINISH_SPACE
        {
            get
            {
                double result = _body.GetAttrRealValue(ELEC_FINISH_SPACE);
                return (-Math.Abs(result));
            }
            set
            {
                _body.SetRealAttribute(ELEC_FINISH_SPACE, value);
            }
        }
        /// <summary>
        /// 中公火花位
        /// </summary>
        public virtual double MIDDLE_SPACE
        {
            get
            {
                double result = _body.GetAttrRealValue(ELEC_MIDDLE_SPACE);
                return (-Math.Abs(result));
            }
            set
            {
                _body.SetRealAttribute(ELEC_MIDDLE_SPACE, value);
            }
        }
        /// <summary>
        /// 粗公火花位
        /// </summary>
        public virtual double ROUGH_SPACE
        {
            get
            {
                double result = _body.GetAttrRealValue(ELEC_ROUGH_SPACE);
                return (-Math.Abs(result));
            }
            set
            {
                _body.SetRealAttribute(ELEC_ROUGH_SPACE, value);
            }
        }

        /// <summary>
        /// 材质
        /// </summary>
        public virtual string MAT_NAME
        {
            get
            {
                return _body.GetAttrValue(ELEC_MAT_NAME);
            }
            set
            {
                SetStringAttribute(ELEC_MAT_NAME, value);
            }
        }

        /// <summary>
        /// 精公光洁度
        /// </summary>
        public virtual string F_SMOOTH
        {
            get
            {
                return _body.GetAttrValue(F_ELEC_SMOOTH);
            }
            set
            {
                SetStringAttribute(F_ELEC_SMOOTH, value);
            }
        }


        /// <summary>
        /// 中公光洁度
        /// </summary>
        public virtual string M_SMOOTH
        {
            get
            {
                return _body.GetAttrValue(M_ELEC_SMOOTH);
            }
            set
            {
                SetStringAttribute(M_ELEC_SMOOTH, value);
            }
        }

        /// <summary>
        /// 粗公光洁度
        /// </summary>
        public virtual string R_SMOOTH
        {
            get
            {
                return _body.GetAttrValue(R_ELEC_SMOOTH);
            }
            set
            {
                SetStringAttribute(R_ELEC_SMOOTH, value);
            }
        }

        public virtual string OPENSTRUFF 
        {
            get
            {
                return _body.GetAttrValue(NEW_OR_MODIFY_MODL);
            }
            set
            {
                SetStringAttribute(NEW_OR_MODIFY_MODL, value);
            }
        }

        /// <summary>
        /// 工位类型
        /// </summary>
        public virtual string UNIT
        {
            get
            {
                return _body.GetAttrValue(ELEC_MACH_TYPE);
            }
            set
            {
                SetStringAttribute(ELEC_MACH_TYPE, value);
            }
        }

        /// <summary>
        /// 加工方向
        /// </summary>
        public virtual string EDMPROCDIRECTION 
        {
            get
            {
                return _body.GetAttrValue(EACT_EDMPROCDIRECTION);
            }
            set
            {
                SetStringAttribute(EACT_EDMPROCDIRECTION, value);
            }
        }

        /// <summary>
        /// 摇摆方式
        /// </summary>
        public virtual string EDMROCK
        {
            get
            {
                return _body.GetAttrValue(EACT_EDMROCK);
            }
            set
            {
                SetStringAttribute(EACT_EDMROCK, value);
            }
        }

        /// <summary>
        /// 共用电极
        /// </summary>
        public virtual string ShareElec
        {
            get
            {
                return _body.GetAttrValue(EACT_SHAREELEC);
            }
            set
            {
                SetStringAttribute(EACT_SHAREELEC, value);
            }
        }
    }
}
