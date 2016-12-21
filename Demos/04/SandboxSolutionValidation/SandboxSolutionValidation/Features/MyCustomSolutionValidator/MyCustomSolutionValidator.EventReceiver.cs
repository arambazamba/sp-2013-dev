using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint.UserCode;

namespace SandboxSolutionValidation.Features.Feature1
{
    [Guid("f9ac3b3c-0574-49fd-8b19-6be5537609eb")]
    public class Feature1EventReceiver : SPFeatureReceiver
    {
        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            SPUserCodeService sandboxService = SPUserCodeService.Local;
            SPSolutionValidator myValidator = new MySolutionValidator(sandboxService);
            sandboxService.SolutionValidators.Add(myValidator);
        }

        public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        {
            SPUserCodeService sandboxService = SPUserCodeService.Local;
            Guid zimmerValidatorId = sandboxService.SolutionValidators["MySolutionValidator"].Id;
            sandboxService.SolutionValidators.Remove(zimmerValidatorId);
        }
    }
}
