namespace Client_WinForm
{
    partial class TeamHeadScreen
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.hoursChartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewMyProjectsStateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateMyWorkersHoursToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hoursChartToolStripMenuItem,
            this.viewMyProjectsStateToolStripMenuItem,
            this.updateMyWorkersHoursToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // hoursChartToolStripMenuItem
            // 
            this.hoursChartToolStripMenuItem.Name = "hoursChartToolStripMenuItem";
            this.hoursChartToolStripMenuItem.Size = new System.Drawing.Size(83, 20);
            this.hoursChartToolStripMenuItem.Text = "Hours Chart";
            // 
            // viewMyProjectsStateToolStripMenuItem
            // 
            this.viewMyProjectsStateToolStripMenuItem.Name = "viewMyProjectsStateToolStripMenuItem";
            this.viewMyProjectsStateToolStripMenuItem.Size = new System.Drawing.Size(141, 20);
            this.viewMyProjectsStateToolStripMenuItem.Text = "View My Projects State ";
            // 
            // updateMyWorkersHoursToolStripMenuItem
            // 
            this.updateMyWorkersHoursToolStripMenuItem.Name = "updateMyWorkersHoursToolStripMenuItem";
            this.updateMyWorkersHoursToolStripMenuItem.Size = new System.Drawing.Size(158, 20);
            this.updateMyWorkersHoursToolStripMenuItem.Text = "Update My Workers Hours";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // TeamHeadScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "TeamHeadScreen";
            this.Text = "TeamHeadScreen";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem hoursChartToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewMyProjectsStateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateMyWorkersHoursToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    }
}