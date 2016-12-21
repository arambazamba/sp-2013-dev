using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;

namespace CrossSiteNavigation.Features.CrossSiteNavigation
{
    /// <summary>
    /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
    /// </summary>
    /// <remarks>
    /// The GUID attached to this class may be used during packaging and should not be modified.
    /// </remarks>

    [Guid("38094f6d-6c70-4d59-b1ea-ac89fcaab962")]
    public class CrossSiteNavigationEventReceiver : SPFeatureReceiver
    {
        const string modificationName = "add[@Name='ContosoCrossSiteProvider']";

        // Uncomment the method below to handle the event raised after a feature has been activated.
        
        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            var webapp = properties.Feature.Parent as SPWebApplication;
            
            SPWebConfigModification modification = new SPWebConfigModification();
            modification.Path = "configuration/system.web/siteMap/providers";
            modification.Name = modificationName;
            modification.Sequence = 0;
            modification.Owner = "administrator@contoso.com";
            modification.Type = SPWebConfigModification.SPWebConfigModificationType.EnsureChildNode;
            modification.Value = "<add name='ContosoCrossSiteProvider' siteMapFile='_layouts/15/CrossSiteNavigation/Contoso.sitemap' type='Microsoft.SharePoint.Navigation.SPXmlContentMapProvider, Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c' />";

            webapp.WebConfigModifications.Add(modification);
            webapp.Update();
            webapp.WebService.ApplyWebConfigModifications();            
        }


        // Uncomment the method below to handle the event raised before a feature is deactivated.

        public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        {
            SPWebConfigModification modificationToRemove = null;
            var webapp = properties.Feature.Parent as SPWebApplication;
            var modifications = webapp.WebConfigModifications;
            
            foreach (var modification in modifications)
            {
                if (modification.Name == modificationName)
                {
                    modificationToRemove = modification;
                }
            }

            modifications.Remove(modificationToRemove);
            webapp.Update();
            webapp.WebService.ApplyWebConfigModifications();
        }


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
