using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CreateCRs
{
    public partial class frmMain : Form
    {
        private SimpleStarTeam sST = new SimpleStarTeam();
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            ShowButtons(false);

            // for debug
            txtServer.Text = "ServerName";
            txtPort.Text = "port";
            txtUser.Text = "user";
            txtPassword.Text = "pwd";
        }

        private void btnLogon_Click(object sender, EventArgs e)
        {
            //Validate fields

            // check logon / Logoff
            if (btnLogon.Text == "Logon")
            {
                // connect
                try
                {
                    sST.Connect(txtServer.Text, Int32.Parse(txtPort.Text), txtUser.Text, txtPassword.Text);

                    //Test only !
                    sST.CheckQC();
                    //till here~!

                    ShowButtons(true);

                    //get ST project
                    cmbProjects.Items.AddRange(sST.GetProjects().ToArray());
                    cmbProjects.Sorted = true;
                    cmbProjects.SelectedIndex = 0;

                    //get valid CR status
                    //cmbStatus.Items.AddRange();
                }
                catch (Exception exp)
                {
                    MessageBox.Show("Logon failure " + exp.Message);
                }
            }
            else
            {
                //disconnect
                sST.Logoff();

                ShowButtons(false);
            }

        }

        private void ShowButtons(bool bshow)
        {
            // Just for make the GUI nicer :)

            cmbViews.Enabled = bshow;
            cmbProjects.Enabled = bshow;
            txtLocation.Enabled = bshow;
            txtSynopsis.Enabled = bshow;
            cmbStatus.Enabled = bshow;
            cmbType.Enabled = bshow;
            numCreateCR.Enabled = bshow;
            btnCreate.Enabled = true;

            txtPort.Enabled = !bshow;
            txtServer.Enabled = !bshow;
            txtUser.Enabled = !bshow;
            txtPassword.Enabled = !bshow;

            if (bshow)
            {
                btnLogon.Text = "Logoff";
            }
            else
            {
                btnLogon.Text = "Logon";
            }
        }

        private void cmbProjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbViews.Items.Clear();
            cmbViews.Items.AddRange(sST.GetViews(cmbProjects.SelectedItem.ToString()).ToArray());
            cmbViews.Sorted = true;
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            sST.setproject(cmbProjects.SelectedItem);
            sST.setView(cmbViews.SelectedItem);

            sST.getviewByLabel();

        }

    }
}