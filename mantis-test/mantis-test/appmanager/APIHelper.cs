using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mts = mantis_test.Mantis;
using OpenQA.Selenium;

namespace mantis_test
{
    public class APIHelper : MBaseHelper
    {
        public APIHelper(IWebDriver driver, AppManager appManager) : base(driver, appManager)
        {
        }

        public void CreateNewIssue(AccountData acc, IssData iss, ProjData pj)
        {
            mts.MantisConnectPortTypeClient cl = new mts.MantisConnectPortTypeClient();
            mts.IssueData apiiss = new mts.IssueData();
            apiiss.summary = iss.Summary;
            apiiss.description = iss.Descr;
            apiiss.category = iss.Catgr;
            apiiss.project = new mts.ObjectRef();
            apiiss.project.id = pj.ID;
            cl.mc_issue_add(acc.Name, acc.Password, apiiss);
        }
    }
}
