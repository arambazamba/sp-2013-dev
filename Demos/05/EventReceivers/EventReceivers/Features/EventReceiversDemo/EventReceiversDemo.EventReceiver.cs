using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;

namespace EventReceivers.Features.EventReceiversDemo
{
    /// <summary>
    /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
    /// </summary>
    /// <remarks>
    /// The GUID attached to this class may be used during packaging and should not be modified.
    /// </remarks>

    [Guid("14ace242-d3f2-4d58-bacf-8ba1233bed71")]
    public class EventReceiversDemoEventReceiver : SPFeatureReceiver
    {
        // Uncomment the method below to handle the event raised after a feature has been activated.

        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            SPWeb web = properties.Feature.Parent as SPWeb;

            if (web!=null)
            {
                SPList list = web.Lists.TryGetList("MyContacts");
                if (list == null)
                {
                    Guid id = web.Lists.Add("MyContacts", "A demo contact list", SPListTemplateType.Contacts);
                    list = web.Lists[id];
                }

                string recClass = "EventReceivers.TitleChangeSyncrReceiver";
                string recAssembls = Assembly.GetExecutingAssembly().FullName;
                list.EventReceivers.Add(SPEventReceiverType.ItemUpdating, recAssembls, recClass);
            }
        }


        // Uncomment the method below to handle the event raised before a feature is deactivated.

        //public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        //{
        //}


        // Uncomment the method below to handle the event raised after a feature has been installed.

        //public override void FeatureInstalled(SPFeatureReceiverProperties properties)
        //{
        //}


        // Uncomment the method below to handle the event raised before a feature is uninstalled.

        //public override void FeatureUninstalling(SPFeatureReceiverProperties properties)
        //{
        //}

        // Uncomment the method below to handle the event raised when a feature is upgrading.

        //public override void FeatureUpgrading(SPFeatureReceiverProperties properties, string upgradeActionName, System.Collections.Generic.IDictionary<string, string> parameters)
        //{
        //}
    }
}
