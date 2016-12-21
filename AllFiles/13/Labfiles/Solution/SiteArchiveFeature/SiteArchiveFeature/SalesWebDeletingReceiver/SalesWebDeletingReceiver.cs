using System;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Workflow;

namespace SiteArchiveFeature.SalesWebDeletingReceiver
{
    /// <summary>
    /// Web Events
    /// </summary>
    public class SalesWebDeletingReceiver : SPWebEventReceiver
    {
        /// <summary>
        /// A site is being deleted.
        /// </summary>
        public override void WebDeleting(SPWebEventProperties properties)
        {
            using (var site = new SPSite(properties.SiteId))
            {
                string backupLocation = @"E:\Labfiles\Starter\Backup.backup";
                site.WebApplication.Sites.Backup(site.Url, backupLocation, true);
            }
        }


    }
}