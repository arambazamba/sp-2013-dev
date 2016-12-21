using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;

namespace FinancialsLibrary.Features.FinancialsLibrary
{
    /// <summary>
    /// This feature receiver secures access to the financials library
    /// </summary>
    /// <remarks>
    /// Only site owners and members of the central managers team have
    /// access to the Financials library
    /// </remarks>

    [Guid("df885c1c-f613-437c-9303-dc65abe6325d")]
    public class FinancialsLibraryEventReceiver : SPFeatureReceiver
    {

        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {

            //Get the SPWeb for this feature
            SPWeb parentWeb = (SPWeb)properties.Feature.Parent;
           
                //Get the Financials document library
                SPDocumentLibrary financialsLibrary = (SPDocumentLibrary)parentWeb.Lists["Financials"];

            if (financialsLibrary != null)
            {
                //Break permissions inheritance from the SPWeb
                //Do not copy initial permissions from parents
                financialsLibrary.BreakRoleInheritance(false);

                //Add permissions to site owners
                SPRoleAssignment ownersAssignment = new SPRoleAssignment(parentWeb.AssociatedOwnerGroup);
                ownersAssignment.RoleDefinitionBindings.Add(parentWeb.RoleDefinitions["Full Control"]);
                financialsLibrary.RoleAssignments.Add(ownersAssignment);
                financialsLibrary.Update();

                //Add the Managers AD group to the site
                parentWeb.AllUsers.Add("CONTOSO\\Managers", "", "", "Managers AD Group");

                //Add permissions to the managers group
                SPRoleAssignment managersAssignment = new SPRoleAssignment(parentWeb.AllUsers["CONTOSO\\Managers"]);
                managersAssignment.RoleDefinitionBindings.Add(parentWeb.RoleDefinitions["Contribute"]);
                financialsLibrary.RoleAssignments.Add(managersAssignment);
                financialsLibrary.Update();
            }

        }

    }
}
