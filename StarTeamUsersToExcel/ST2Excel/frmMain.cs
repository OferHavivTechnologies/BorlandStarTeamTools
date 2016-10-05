using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ST2Excel
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }
        System.IO.StreamWriter sw;
        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            sw = System.IO.File.CreateText("C:\\STresult.txt");

            Write2Log("Connect to server...");
            Borland.StarTeam.Server s = new Borland.StarTeam.Server("Venus", 49201);

            
            Write2Log("Loggin to server with user " + txtName.Text);
            s.LogOn(txtName.Text , txtPWD.Text );

            Write2Log("Read groups...");

            Borland.StarTeam.ServerAdministration sa = s.Administration;

            Borland.StarTeam.GroupAccountCollection gac = sa.GroupAccounts;

            foreach(Borland.StarTeam.GroupAccount ga in gac)
            {
                if (!ga.IsRoot)
                {
                    Write2Log(ga.Parent.FullName + ":" + ga.Name + ":" + getUsers(ga.UserAccounts));
                }
                else
                {
                    Write2Log("Root:" + ga.FullName + ":" + getUsers(ga.UserAccounts));
                }
            }
            sw.Close();

        }
        private string getUsers(Borland.StarTeam.UserAccountCollection uac)
        {
            string szResult = "";
            foreach (Borland.StarTeam.UserAccount  ua in uac)
            {
                szResult += "," + ua.Name;
            }
            return szResult;

        }

        private void Write2Log(string szLine)
        {
            listBox1.Items.Add(szLine);
            
            sw.WriteLine(szLine);
            
        }

            

    }
}