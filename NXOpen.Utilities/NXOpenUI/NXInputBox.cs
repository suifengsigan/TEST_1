namespace NXOpenUI
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public class NXInputBox
    {
        public static double GetInputNumber(string prompt)
        {
            return double.Parse(ShowCore(prompt, "", "", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, 0));
        }

        public static double GetInputNumber(string prompt, string caption)
        {
            return double.Parse(ShowCore(prompt, caption, "", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, 0));
        }

        public static double GetInputNumber(string prompt, string caption, double initialNumber)
        {
            return double.Parse(ShowCore(prompt, caption, initialNumber.ToString(), MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, 0));
        }

        public static double GetInputNumber(string prompt, string caption, string initialText)
        {
            return double.Parse(ShowCore(prompt, caption, initialText, MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, 0));
        }

        public static string GetInputString(string prompt)
        {
            return ShowCore(prompt, "", "", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, 0);
        }

        public static string GetInputString(string prompt, string caption)
        {
            return ShowCore(prompt, caption, "", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, 0);
        }

        public static string GetInputString(string prompt, string caption, string initialText)
        {
            return ShowCore(prompt, caption, initialText, MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, 0);
        }

        public static bool ParseInputNumber(string prompt, string caption, double initialNumber, NumberStyles style, IFormatProvider provider, out double result)
        {
            return double.TryParse(ShowCore(prompt, caption, initialNumber.ToString(provider), MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, 0), style, provider, out result);
        }

        public static bool ParseInputNumber(string prompt, string caption, string initialText, NumberStyles style, IFormatProvider provider, out double result)
        {
            result = 0.0;
            return double.TryParse(ShowCore(prompt, caption, initialText, MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, 0), style, provider, out result);
        }

        private static string ShowCore(string prompt, string caption, string initialText, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options)
        {
            NXInputBoxForm form = new NXInputBoxForm(prompt, caption, initialText);
            form.ShowDialog();
            return form.InputText;
        }

        internal class NXInputBoxForm : Form
        {
            private Button btnCancel;
            private Button btnOk;
            private Container components;
            private TextBox inputTextBox;
            private static Point lastLocation = new Point(-1, -1);
            private Label lblPromptText;

            public NXInputBoxForm(string prompt, string caption, string initialText)
            {
                this.InitializeComponent();
                if (lastLocation.X != -1)
                {
                    base.Location = lastLocation;
                    base.StartPosition = FormStartPosition.Manual;
                }
                this.InputText = initialText;
                this.Prompt = prompt;
                this.Caption = caption;
                FormUtilities.SetApplicationIcon(this);
                FormUtilities.ReparentForm(this);
            }

            private void btnCancel_Click(object sender, EventArgs e)
            {
                base.Close();
                this.inputTextBox.Text = "";
            }

            private void btnOk_Click(object sender, EventArgs e)
            {
                base.Close();
            }

            private void ClosingHandler(object sender, CancelEventArgs e)
            {
                lastLocation = base.Location;
            }

            protected override void Dispose(bool disposing)
            {
                if (disposing && (this.components != null))
                {
                    this.components.Dispose();
                }
                base.Dispose(disposing);
            }

            private void InitializeComponent()
            {
                this.inputTextBox = new TextBox();
                this.btnOk = new Button();
                this.btnCancel = new Button();
                this.lblPromptText = new Label();
                base.SuspendLayout();
                this.inputTextBox.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
                this.inputTextBox.Location = new Point(0x10, 40);
                this.inputTextBox.Name = "inputTextBox";
                this.inputTextBox.Size = new Size(0x128, 20);
                this.inputTextBox.TabIndex = 0;
                this.inputTextBox.Text = "";
                this.inputTextBox.KeyDown += new KeyEventHandler(this.inputTextBox_KeyDown);
                this.inputTextBox.MouseDown += new MouseEventHandler(this.inputTextBox_MouseDown);
                this.btnOk.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
                this.btnOk.Location = new Point(0x90, 0x48);
                this.btnOk.Name = "btnOk";
                this.btnOk.TabIndex = 1;
                this.btnOk.Text = "Ok";
                this.btnOk.Click += new EventHandler(this.btnOk_Click);
                this.btnCancel.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
                this.btnCancel.Location = new Point(0xe8, 0x48);
                this.btnCancel.Name = "btnCancel";
                this.btnCancel.TabIndex = 2;
                this.btnCancel.Text = "Cancel";
                this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
                this.lblPromptText.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
                this.lblPromptText.Location = new Point(0x10, 0x10);
                this.lblPromptText.Name = "lblPromptText";
                this.lblPromptText.Size = new Size(0x120, 0x10);
                this.lblPromptText.TabIndex = 3;
                this.lblPromptText.Text = "Enter Value:";
                this.AutoScaleBaseSize = new Size(5, 13);
                base.ClientSize = new Size(0x148, 0x69);
                base.Controls.Add(this.lblPromptText);
                base.Controls.Add(this.btnCancel);
                base.Controls.Add(this.btnOk);
                base.Controls.Add(this.inputTextBox);
                base.Closing += new CancelEventHandler(this.ClosingHandler);
                base.Name = "NXInputBoxForm";
                this.Text = "NXInputBox";
                base.ResumeLayout(false);
            }

            private void inputTextBox_KeyDown(object sender, KeyEventArgs e)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    base.Close();
                }
            }

            private void inputTextBox_MouseDown(object sender, MouseEventArgs e)
            {
                if (e.Button == MouseButtons.Middle)
                {
                    base.Close();
                }
            }

            public string Caption
            {
                get
                {
                    return this.Text;
                }
                set
                {
                    this.Text = value;
                }
            }

            public string InputText
            {
                get
                {
                    return this.inputTextBox.Text;
                }
                set
                {
                    this.inputTextBox.Text = value;
                }
            }

            public string Prompt
            {
                get
                {
                    return this.lblPromptText.Text;
                }
                set
                {
                    this.lblPromptText.Text = value;
                }
            }
        }
    }
}

