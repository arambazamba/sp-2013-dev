using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;

namespace ExpensesTimerJob.Features.ContosoExpensesOverviewTimerJobInstaller
{
    /// <summary>
    /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
    /// </summary>
    /// <remarks>
    /// The GUID attached to this class may be used during packaging and should not be modified.
    /// </remarks>

    [Guid("ca162d5a-66db-403b-bd11-0ba41bff801e")]
    public class ContosoExpensesOverviewTimerJobInstallerEventReceiver : SPFeatureReceiver
    {
        const string timerJobName = "ExpensesOverviewJob";

        // Uncomment the method below to handle the event raised after a feature has been activated.

        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {          
            // DON'T FORGET TO WRITE UP NAMESPACE
            SPWebApplication webApplication = ((SPSite)properties.Feature.Parent).WebApplication;

            deleteJob(webApplication);
            
            ContosoExpensesOverviewTimerJob timerJob = new ContosoExpensesOverviewTimerJob(timerJobName, webApplication, null, SPJobLockType.Job);

            SPMinuteSchedule schedule = new SPMinuteSchedule();
            schedule.BeginSecond = 1;
            schedule.EndSecond = 5;
            schedule.Interval = 2;

            timerJob.Schedule = schedule;

            timerJob.Update();
        }


        // Uncomment the method below to handle the event raised before a feature is deactivated.

        public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        {
            SPWebApplication webApplication = ((SPSite)properties.Feature.Parent).WebApplication;
            deleteJob(webApplication);
        }

        private void deleteJob(SPWebApplication webApplication)
        {            
            foreach(SPJobDefinition job in webApplication.JobDefinitions)
            {
                if(job.Name.Equals(timerJobName))
                {
                    job.Delete();
                }
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
