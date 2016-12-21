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
        // TODO: Ex 1 Task 1 Add variables to track the web part state

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
            // TODO: Ex 1 Task 2 Hide the update controls by default
            

            // TODO: Ex 1 Task 2 Populate the ListBox with the list of webs in the current site
            

            // TODO: Ex 1 Task 4 If a site has been updated, clear the selected item and display the results label, otherwise hide the results label 
            
            // TODO: Ex 1 Task 3 If the user has selected an item, reselect the item in the list and display the update controls 
            
        }

        protected void Page_Load(object sender, EventArgs e)
        {            
        }

        protected void lstWebs_SelectedIndexChanged(object sender, EventArgs e)
        {

            // TODO: Ex 1 Task 3 Get the GUID of the selected list item
            

            // TODO: Ex 1 Task 3 Set the title text box to the title of the selected site

        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            // TODO: Ex 1 Task 4 Get the GUID of the selected list site
 
            
            // TODO: Ex 1 Task 4 Get the new title for the selected site

                        
            // TODO: Ex 1 Task 4 Update the title of the selected site and display a confirmation message

        }
    }
}
