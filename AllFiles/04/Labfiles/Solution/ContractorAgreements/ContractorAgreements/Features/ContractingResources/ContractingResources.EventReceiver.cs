using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;

namespace ContractorAgreements.Features.ContractingResources
{
    /// <summary>
    /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
    /// </summary>
    /// <remarks>
    /// The GUID attached to this class may be used during packaging and should not be modified.
    /// </remarks>

    [Guid("4726e720-2821-4b8d-a892-f7ed60670e22")]
    public class ContractingResourcesEventReceiver : SPFeatureReceiver
    {
        public static readonly SPContentTypeId ctid = new SPContentTypeId("0x010100C3316E15A95F420F8187FBBE1B9636F9");

        // Uncomment the method below to handle the event raised after a feature has been activated.

        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            var site = properties.Feature.Parent as SPSite;
            var web = site.RootWeb;
            SPContentType contractingCT = web.ContentTypes[ctid];
                        
            if (contractingCT == null)
            {
                // Create the content type and add it to the content types collection on the root web.
                contractingCT = new SPContentType(ctid, web.ContentTypes, "Contractor Agreement");
                web.ContentTypes.Add(contractingCT);
            }
            
            // Configure the properties of the content type.
            contractingCT.Description = "A contractual agreement between Contoso Pharmaceuticals and a contractor";
            contractingCT.Group = "Contoso Content Types";

            // Add site columns to the content type.
            SPField fldFullName = web.AvailableFields["Full Name"];
            SPFieldLink fldLinkFullName = new SPFieldLink(fldFullName);
            if (contractingCT.FieldLinks[fldLinkFullName.Id] == null)
            {
                contractingCT.FieldLinks.Add(fldLinkFullName);
            }

            SPField fldContosoManager = web.AvailableFields["Contoso Manager"];
            SPFieldLink fldLinkContosoManager = new SPFieldLink(fldContosoManager);
            if (contractingCT.FieldLinks[fldLinkContosoManager.Id] == null)
            {
                contractingCT.FieldLinks.Add(fldLinkContosoManager);
            }

            SPField fldContosoTeam = web.AvailableFields["Contoso Team"];
            SPFieldLink fldLinkContosoTeam = new SPFieldLink(fldContosoTeam);
            if (contractingCT.FieldLinks[fldLinkContosoTeam.Id] == null)
            {
                contractingCT.FieldLinks.Add(fldLinkContosoTeam);
            }

            SPField fldDailyRate = web.AvailableFields["Daily Rate"];
            SPFieldLink fldLinkDailyRate = new SPFieldLink(fldDailyRate);
            if (contractingCT.FieldLinks[fldLinkDailyRate.Id] == null)
            {
                contractingCT.FieldLinks.Add(fldLinkDailyRate);
            }

            SPField fldStartDate = web.AvailableFields["Agreement Start Date"];
            SPFieldLink fldLinkStartDate = new SPFieldLink(fldStartDate);
            if (contractingCT.FieldLinks[fldLinkStartDate.Id] == null)
            {
                contractingCT.FieldLinks.Add(fldLinkStartDate);
            }

            SPField fldEndDate = web.AvailableFields["Agreement End Date"];
            SPFieldLink fldLinkEndDate = new SPFieldLink(fldEndDate);
            if (contractingCT.FieldLinks[fldLinkEndDate.Id] == null)
            {
                contractingCT.FieldLinks.Add(fldLinkEndDate);
            }
            
            SPField fldSecurityCleared = web.AvailableFields["Security Cleared"];
            SPFieldLink fldLinkSecurityCleared = new SPFieldLink(fldSecurityCleared);
            if (contractingCT.FieldLinks[fldLinkSecurityCleared.Id] == null)
            {
                contractingCT.FieldLinks.Add(fldLinkSecurityCleared);
            }

            contractingCT.Update(true);
        }


        // Uncomment the method below to handle the event raised before a feature is deactivated.

        public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        {
            var site = properties.Feature.Parent as SPSite;
            var web = site.RootWeb;
            SPContentType contractingCT = web.ContentTypes[ctid];

            if (contractingCT != null)
            {
                // Delete the site content type.
                web.ContentTypes.Delete(ctid);
            }
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
