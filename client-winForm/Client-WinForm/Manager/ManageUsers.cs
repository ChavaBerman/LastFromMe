using Client_WinForm.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client_WinForm.Manager
{
    public partial class ManageUsers : Form
    {
        User manager = new User();
        public ManageUsers(User manager)
        {
            this.manager = manager;
            InitializeComponent();
        }

        private void addWorkerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddWorker addWorker = new AddWorker(manager);
            addWorker.MdiParent = this.MdiParent;
            addWorker.Show();
        }
    }
}
