using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpensesTimerJob
{
    public class ContosoExpensesOverviewTimerJob : SPJobDefinition
    {
        public ContosoExpensesOverviewTimerJob()
        {
        }

        public ContosoExpensesOverviewTimerJob(string name, SPWebApplication webApplication, SPServer server, SPJobLockType lockType)
            : base(name, webApplication, server, lockType)
        {
        }

        public override void Execute(Guid targetInstanceId)
        {
            // Obtain a reference to the maagers site collection.
            using (SPSite managerSite = new SPSite("http://managers.contoso.com"))
            {
                // Obtain a reference to the managers site.
                using (SPWeb managerWeb = managerSite.RootWeb)
                {
                    // Obtain a reference to the expense overview list.
                    SPList overviewList = managerWeb.Lists["Expenses Overview"];

                    // Remove all existing items from the list.
                    while (overviewList.Items.Count > 0)
                    {
                        overviewList.Items[0].Delete();
                        overviewList.Update();
                    }

                    // Iterate through each site collection in the current web application.
                    foreach (SPSite departmentSite in this.WebApplication.Sites)
                    {
                        using (SPWeb departmentWeb = departmentSite.RootWeb)
                        {
                            // Get the Contoso Expenses list, if one exists.
                            SPList expensesList = departmentWeb.Lists.TryGetList("Contoso Expenses");
                            if (expensesList != null)
                            {
                                // Calculate the total for the department. 
                                double departmentTotal = 0;
                                foreach (SPListItem expense in departmentWeb.Lists["Contoso Expenses"].Items)
                                {
                                    departmentTotal += (double)expense["InvoiceTotal"];
                                }

                                // Use the site URL to determine the department name.
                                Uri url = new Uri(departmentWeb.Url);
                                string hostName = url.GetComponents(UriComponents.Host, UriFormat.Unescaped);
                                string[] hostNameComponents = hostName.Split('.');

                                // Create a new item in the expense overview list.
                                SPListItem overviewItem = overviewList.Items.Add();
                                overviewItem["Title"] = hostNameComponents[0];
                                overviewItem["Expense Total"] = departmentTotal;
                                overviewItem.Update();
                                overviewList.Update();
                            }                            
                        }
                        departmentSite.Dispose();
                    }
                }
            }
        }
    }
}
