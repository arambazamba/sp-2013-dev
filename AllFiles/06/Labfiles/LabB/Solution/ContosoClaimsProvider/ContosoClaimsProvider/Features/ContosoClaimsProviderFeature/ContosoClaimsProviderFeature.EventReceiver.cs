using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint.Administration.Claims;

namespace ContosoClaimsProvider.Features.ContosoClaimsProviderFeature
{

    [Microsoft.SharePoint.Security.SharePointPermission(System.Security.Permissions.SecurityAction.Demand, ObjectModel = true)]
    [Guid("a30cdf3e-2fa3-4875-a4bf-f6bf0bf6896c")]
    public class ContosoClaimsProviderFeatureEventReceiver : SPClaimProviderFeatureReceiver
    {
        public override string ClaimProviderAssembly
        {
            get { return typeof(Contoso.Security.LocationClaimsProvider).Assembly.FullName; }
        }

        public override string ClaimProviderDescription
        {
            get { return "A claim provider that certifies the user's location"; }
        }

        public override string ClaimProviderDisplayName
        {
            get { return Contoso.Security.LocationClaimsProvider.ProviderDisplayName; }
        }

        public override string ClaimProviderType
        {
            get { return typeof(Contoso.Security.LocationClaimsProvider).FullName; }
        }

        private void ExecBaseFeatureActivated(SPFeatureReceiverProperties properties)
        {
            base.FeatureActivated(properties);
        }

        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            ExecBaseFeatureActivated(properties);
        }
    }
}
