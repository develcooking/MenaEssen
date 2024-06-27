using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MensaEssen
{
    partial class MensaEssen
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dishesDataGridView;
        private System.Windows.Forms.LinkLabel linkLabel;
        private System.Windows.Forms.Label additionalTextLabel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        const int CS_NOCLOSE = 0x200;

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_NOCLOSE;
                return cp;
            }
        }
        private bool _preventMove = true;
        protected override void WndProc(ref Message message)
        {
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_MOVE = 0xF010;

            if (_preventMove)
            {
                switch (message.Msg)
                {
                    case WM_SYSCOMMAND:
                        int command = message.WParam.ToInt32() & 0xfff0;
                        if (command == SC_MOVE)
                            return;
                        break;
                }
            }

            base.WndProc(ref message);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            ComponentResourceManager resources = new ComponentResourceManager(typeof(MensaEssen));
            dishesDataGridView = new DataGridView();
            linkLabel = new LinkLabel();
            additionalTextLabel = new Label();
            ((ISupportInitialize)dishesDataGridView).BeginInit();
            SuspendLayout();
            // 
            // dishesDataGridView
            // 
            dishesDataGridView.AllowUserToAddRows = false;
            dishesDataGridView.AllowUserToDeleteRows = false;
            dishesDataGridView.AllowUserToResizeColumns = false;
            dishesDataGridView.AllowUserToResizeRows = false;
            dishesDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dishesDataGridView.Location = new Point(13, 11);
            dishesDataGridView.Margin = new Padding(3, 4, 3, 4);
            dishesDataGridView.Name = "dishesDataGridView";
            dishesDataGridView.RowHeadersVisible = false;
            dishesDataGridView.Size = new Size(776, 336);
            dishesDataGridView.TabIndex = 1;
            // 
            // linkLabel
            // 
            linkLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            linkLabel.AutoSize = true;
            linkLabel.Location = new Point(12, 358);
            linkLabel.Name = "linkLabel";
            linkLabel.Size = new Size(146, 15);
            linkLabel.TabIndex = 0;
            linkLabel.TabStop = true;
            linkLabel.Text = "Besuchen Sie die Webseite";
            linkLabel.LinkClicked += LinkLabel_LinkClicked;
            // 
            // additionalTextLabel
            // 
            additionalTextLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            additionalTextLabel.AutoSize = true;
            additionalTextLabel.Location = new Point(12, 377);
            additionalTextLabel.Name = "additionalTextLabel";
            additionalTextLabel.Size = new Size(344, 15);
            additionalTextLabel.TabIndex = 2;
            additionalTextLabel.Text = "Um die Url zu ändern editere die WinFormsApp2.dll.config Datei";
            // 
            // MensaEssen
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(800, 450);
            Controls.Add(additionalTextLabel);
            Controls.Add(linkLabel);
            Controls.Add(dishesDataGridView);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            MdiChildrenMinimizedAnchorBottom = false;
            Name = "MensaEssen";
            SizeGripStyle = SizeGripStyle.Hide;
            Text = "Mensa Essen des Tages";
            TopMost = true;
            Load += MensaEssen_Load;
            ((ISupportInitialize)dishesDataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
        #endregion
    }
}
