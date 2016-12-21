using Microsoft.SharePoint;
using System;
using System.ComponentModel;
using System.Web.UI.WebControls.WebParts;

namespace WorkingWithListItems.VisualWebPart1
{
    [ToolboxItemAttribute(false)]
    public partial class WorkingWithListItems : WebPart
    {
        bool isSubmitted;

        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling using
        // the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public WorkingWithListItems()
        {
        }
        
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

        protected override void OnPreRender(EventArgs e)
        {
            if (isSubmitted)
            {
                pnlConfirm.Visible = true;
                pnlControls.Visible = false;
                litMessage.Text = "Your complaint was submitted successfully.";     
            }
            else
            {
                // Populate the rblPriority radio button list.
                

            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            // Add a new list item to the complaints list.
            

            isSubmitted = true;
        }
    }
}
