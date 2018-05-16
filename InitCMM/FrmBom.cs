using CMMTool;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace InitCMM
{
    public partial class FrmBom : Form
    {
        void InitGridControl()
        {
            gridView1.OptionsBehavior.Editable = false;
            gridView1.OptionsView.ShowGroupPanel = false;

            gridView2.OptionsBehavior.Editable = false;
            gridView2.OptionsView.ShowGroupPanel = false;

            gridView3.OptionsBehavior.Editable = false;
            gridView3.OptionsView.ShowGroupPanel = false;
        }

        public FrmBom()
        {
            InitializeComponent();
            Snap.UI.WinForm.SetApplicationIcon(this);
            Snap.UI.WinForm.ReparentForm(this);
            Text = "EACT-益模智能加工系统";
            InitGridControl();
            this.Shown += FrmBom_Shown;
            gridView1.FocusedRowChanged += gridView1_FocusedRowChanged;
            gridView3.FocusedRowChanged += gridView3_FocusedRowChanged;
        }

        void gridView3_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle < 0) return;
            var info = gridView3.GetRow(e.FocusedRowHandle) as JYMouldInfo;
            if (info != null) 
            {
                gridControl1.DataSource = BomBusiness.Instance.GetElecList(info.MouldBody);
            }
            
        }

        void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle >= 0)
            {
                var info = gridView1.GetRow(e.FocusedRowHandle) as JYElecInfo;
                if (info != null) 
                {
                    BomBusiness.Instance.GetElecInfo(info);
                    txtFNum.Text = info.ELEC_FINISH_NUMBER.ToString();
                    txtFSpace.Text = info.ELEC_FINISH_SPACE.ToString();
                    txtMNum.Text = info.ELEC_MIDDLE_NUMBER.ToString();
                    txtMSpace.Text = info.ELEC_MIDDLE_SPACE.ToString();
                    txtRNum.Text = info.ELEC_ROUGH_NUMBER.ToString();
                    txtRSpace.Text = info.ELEC_ROUGH_SPACE.ToString();
                    txtMatName.Text = info.ELEC_MAT_NAME;
                    txtCLAMP_GENERAL_TYPE.Text = info.CLAMP_GENERAL_TYPE;
                    txtELEC_MACH_TYPE.Text = info.ELEC_MACH_TYPE;
                    txtF_ELEC_SMOOTH.Text = info.F_ELEC_SMOOTH;
                    txtM_ELEC_SMOOTH.Text = info.M_ELEC_SMOOTH;
                    txtR_ELEC_SMOOTH.Text = info.R_ELEC_SMOOTH;
                    txtSpec.Text = string.Format("{0}*{1}*{2}", decimal.Round((decimal)info.SPECL, 2), decimal.Round((decimal)info.SPECW, 2), decimal.Round((decimal)info.SPECH, 2));
                    gridControl2.DataSource = info.PositioningInfos;
                }
            }
        }

        void FrmBom_Shown(object sender, EventArgs e)
        {
            gridControl3.DataSource = BomBusiness.Instance.GetMouldInfo();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (gridView1.FocusedRowHandle < 0) return;
            var info = gridView1.GetRow(gridView1.FocusedRowHandle) as JYElecInfo;
            if (info != null) 
            {
                BomBusiness.Instance.ExportStp(info);
            }

        }
    }
}
