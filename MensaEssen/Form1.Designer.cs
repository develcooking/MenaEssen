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
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changeMensaUrlToolStripMenuItem;

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
        private void InitializeComponent()
        {
            ComponentResourceManager resources = new ComponentResourceManager(typeof(MensaEssen));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeMensaUrlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();

            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
    this.optionsToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(800, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip";

            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
    this.changeMensaUrlToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "Options";

            // 
            // changeMensaUrlToolStripMenuItem
            // 
            this.changeMensaUrlToolStripMenuItem.Name = "changeMensaUrlToolStripMenuItem";
            this.changeMensaUrlToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.changeMensaUrlToolStripMenuItem.Text = "Change Mensa URL";
            this.changeMensaUrlToolStripMenuItem.Click += new System.EventHandler(this.changeMensaUrlToolStripMenuItem_Click);

            // 
            // MensaEssen
            // 

            // Add the menuStrip to the form's control collection.
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;

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
            dishesDataGridView.Location = new Point(13, 40); // Adjusted location for the menu
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
            additionalTextLabel.Size = new Size(200, 15);
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
            Controls.Add(this.menuStrip); // Add the MenuStrip to the Controls
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            MdiChildrenMinimizedAnchorBottom = false;
            Name = "MensaEssen";
            SizeGripStyle = SizeGripStyle.Hide;
            Text = "Mensa Essen des Tages";
            this.MainMenuStrip = this.menuStrip;
            Load += MensaEssen_Load;
            ((ISupportInitialize)dishesDataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}