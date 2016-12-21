using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.ComponentModel;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace TitleChecker
{
    [ToolboxItemAttribute(false)]
    public partial class TitleCheckerWebPart : WebPart
    {
        Guid selectedSiteGuid = Guid.Empty;
        bool siteUpdated = false;
        //TODO: Ex 1 Task 1 Declare a GUID variable
        Guid siteCollID = Guid.Empty;

        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling using
        // the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public TitleCheckerWebPart()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

        protected override void OnPreRender(EventArgs e)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                // Runs with elevated privileges.
            });

            // Hide the update controls by default.
            pnlUpdateControls.Visible = false;

            // TODO: Ex 1 Task 1 Populate the ListBox with the list of webs in the current site
            siteCollID = SPContext.Current.Site.ID;
            var populateWebsList = new SPSecurity.CodeToRunElevated(PopulateWebsList);
            SPSecurity.RunWithElevatedPrivileges(populateWebsList);

            // If a site has been updated, clear the selected item and display the results label. 
            // Otherwise, hide the results label.
            if (siteUpdated)
            {
                lstWebs.SelectedIndex = -1;
                selectedSiteGuid = Guid.Empty;
                pnlResult.Visible = true;
            }
            else
            {
                pnlResult.Visible = false;
            }

            // If the user has selected an item, reselect the item in the repopulated list 
            // and display the update controls.
            if (!selectedSiteGuid.Equals(Guid.Empty))
            {
                lstWebs.Items.FindByValue(selectedSiteGuid.ToString()).Selected = true;
                pnlUpdateControls.Visible = true;
            }
        }

        private void PopulateWebsList()
        {
            using (var site = new SPSite(siteCollID))
            {
                lstWebs.Items.Clear();
                foreach (SPWeb web in site.AllWebs)
                {
                    try
                    {
                        lstWebs.Items.Add(new ListItem(web.Title, web.ID.ToString()));
                    }
                    finally
                    {
                        web.Dispose();
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void lstWebs_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the GUID of the selected list item.
            selectedSiteGuid = new Guid(lstWebs.SelectedValue);

            // Set the title text box to the title of the selected site.
            txtTitle.Text = lstWebs.SelectedItem.Text;
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            // Get the GUID of the selected site.
            selectedSiteGuid = new Guid(lstWebs.SelectedValue);

            // Get the new title for the selected site.
            string newTitle = txtTitle.Text;

            // Update the title of the selected site, and display a confirmation message.
            if (!String.IsNullOrEmpty(newTitle) && !selectedSiteGuid.Equals(Guid.Empty))
            {
                using (SPWeb web = SPContext.Current.Site.OpenWeb(selectedSiteGuid))
                {
                    web.Title = newTitle;
                    web.Update();
                    litResult.Text = String.Format("The title of the site at <i>{0}</i> has been changed to <i>{1}</i>.", web.Url, newTitle);
                }
                siteUpdated = true;
            }
        }
    }
}
