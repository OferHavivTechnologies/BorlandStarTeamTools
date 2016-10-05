using System;
using System.Collections.Generic;
using System.Text;
using Borland.StarTeam;

namespace CreateCRs
{
    class SimpleStarTeam
    {
        private Server m_server;
        public void Connect(string szServer, int nPort, string szUser, string szPwd)  
        {
            m_server = new Server(szServer, nPort);

            m_server.LogOn(szUser,szPwd);

            if (!m_server.IsLoggedOn)
            {
                throw new Exception("Failed to loggon to server "+ szServer  + "("+ nPort.ToString() + 
                                ") with user: " + szUser +".");
            }

        }

        public void Logoff()
        {
            try
            {
                m_server.Disconnect();
            }
            finally { }

        }

        public System.Collections.ArrayList GetProjects()
        {
            System.Collections.ArrayList arrResult= new System.Collections.ArrayList();
            foreach (Borland.StarTeam.Project P in m_server.Projects)
            {
                arrResult.Add(P.Name);
            }

            return arrResult;
        }

        public System.Collections.ArrayList GetViews(string StarTeamProject)
        {
            Borland.StarTeam.Project prj = GetProjectByName(StarTeamProject);

            System.Collections.ArrayList arrResult = new System.Collections.ArrayList();
            foreach (Borland.StarTeam.View v in prj.Views)
            {
                arrResult.Add(v.Name);
            }

            return arrResult;
        }

        public System.Collections.ArrayList GetCRStatus()
        {
            return null;
            
        }

        private Borland.StarTeam.Project GetProjectByName(string prj_name)
        {
            foreach (Borland.StarTeam.Project P in m_server.Projects)
            {
                if (P.Name == prj_name)
                {
                    return P;
                }
            }
            return null;

        }


        internal void getviewByLabel()
        {
            /*ViewConfigurationDiffer vcf = new ViewConfigurationDiffer(m_view);

            vcf.Compare(ViewConfiguration.CreateFromLabel(1), ViewConfiguration.CreateFromLabel(2));
            m_project.Views.*/
            
        }

        private View m_view;
        private Project m_project;
        internal void setView(object p)
        {

            m_view = m_project.Views.FindByName(p.ToString(), true);
        }

        internal void setproject(object p)
        {
            m_project = m_server.Projects.FindByName(p.ToString(),true);
            
        }

        internal void CheckQC()
        {
            // test -->


            foreach (Borland.StarTeam.Project p in m_server.Projects)
            {
                if (p.Name == "TestDirector")
                {
                    foreach (Borland.StarTeam.View v in p.Views)
                    {
                        Console.WriteLine(v.Name + ":  ID:" + v.ID);
                        /* 
            QC92Patch_7:  ID:853
QC92Patch_7:  ID:854
QC92Patch_7:  ID:855
                         * 
                         * EUM8:  ID:867
EUM8:  ID:868
EUM8:  ID:869*/
/*QC92Patch_9_LP:  ID:927
QC92Patch_9_LP:  ID:928
QC92Patch_9_LP:  ID:929
QC92Patch_9_LP:  ID:930
QC92Patch_9_LP:  ID:931
QC92Patch_9_LP:  ID:932
QC92Patch_9_LP:  ID:933
QC92Patch_9_LP:  ID:935
QC92Patch_9_LP:  ID:936*/

                        //if (v.ID >= 927 & v.ID <=936 )
                        if(v.Name == "QC92Patch_9_LP")
                        {
                            Console.WriteLine("Removing view:" + v.Name);
                            RemoveView(p,v);
                        }
                    }

                }
            }

            Console.WriteLine("Fin.");


            //<- Till here
        }
        internal void RemoveView(Project p, View v)
        {
            Console.WriteLine("Rename view: "+v.Name +" ("+v.ID +")");
            v.Name = "To_remove_" + v.ID;
            

           /* v.ACL = new ACL();

            //v.ACL.Clear();
            ACE mACE = new ACE(-2147483647, true);
            mACE.AddPermission(Permission.GENERIC_SEE_OBJECT);
            mACE.AddPermission(Permission.GENERIC_MODIFY_OBJECT);
            mACE.AddPermission(Permission.GENERIC_DELETE_OBJECT);
            mACE.AddPermission(Permission.GENERIC_CHANGE_ACCESS_RIGHTS);
            v.ACL.Add(mACE);*/



            v.Update();
            v.Refresh();
            v.Discard();
            v.Update();
            v.Update();
            v.Refresh();

            p.Update();
            p.Refresh();
            

        }
    }
}
