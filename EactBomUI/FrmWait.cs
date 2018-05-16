using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EactBom
{
    public partial class FrmWait : Form
    {
         bool _run = true;
         public FrmWait()
         {
             InitializeComponent();
         }
         public object DoWait(object param)
         {
             List<string> list = new List<string>();
             int count = (int)param;
             progressBar1.Maximum = count;
 
             //---------------------以下代码片段可以使用线程代替
             ((Action)delegate()
             {
                 System.Threading.Thread.Sleep(1000);
 
                     for (int i = 0; i < count; ++i) //耗时操作
                     {
                         if (_run)
                         {
                             string s = DateTime.Now.ToLongTimeString();
                             list.Add(s);
                             this.Invoke((Action)delegate()
                             {
                                 if (!IsDisposed)
                                 {
                                     progressBar1.Value = i;
                                     label1.Text = "正在载入字符串 \"" + s + "\"";
                                 }
                             });
                             System.Threading.Thread.Sleep(500);
                         }
                         else
                         {
                             break;
                         }
                     }
 
             }).BeginInvoke(new AsyncCallback(OnAsync), null);  //异步执行后台工作
             //------------------------
 
             ShowDialog(); //UI界面等待
             return list; //后台工作执行完毕 可以使用结果
         }
         private void OnAsync(IAsyncResult ar)
         {
             if (_run) //后台工作正常结束
                 DialogResult = DialogResult.OK;
         }
         private void frmWait_Load(object sender, EventArgs e)
         {
 
         }
 
         private void button1_Click(object sender, EventArgs e)
         {
             _run = false; //UI界面控制后台结束
             DialogResult = DialogResult.Cancel;
         }
    }
}
