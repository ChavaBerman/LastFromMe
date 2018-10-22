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
    public partial class ManagerMainScreen : Form
    {
        User manager=new User();
        public ManagerMainScreen(User user)
        {
            manager = user;
            IsMdiContainer = true;
            InitializeComponent();
            Text = "Hello " + manager.UserName;
        }

        private void addProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddProject addProject = new AddProject();
            addProject.MdiParent = this;
            addProject.Show();
        }

        private void logout_link_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ManagementTaskLogin managementTaskLogin = new ManagementTaskLogin();
            managementTaskLogin.Show();
            Close();

        }

        private void manageUsersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageUsers manageUsers = new ManageUsers(manager);
            manageUsers.MdiParent = this;
            manageUsers.Show();
        }
    }
}
