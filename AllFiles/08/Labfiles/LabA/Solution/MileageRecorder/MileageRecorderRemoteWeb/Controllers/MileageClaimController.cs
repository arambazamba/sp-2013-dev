using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MileageRecorderRemoteWeb.Models;
using Microsoft.SharePoint.Client;

namespace MileageRecorderRemoteWeb.Controllers
{
    public class MileageClaimController : Controller
    {

        public PartialViewResult Index()
        {
            List<MileageClaim> claimsToDisplay = new List<MileageClaim>();

            string appWebUrl = Session["SPAppWebUrl"].ToString();
            using (ClientContext context = new ClientContext(appWebUrl))
            {
                //Load the Claims list
                List claimsList = context.Web.Lists.GetByTitle("Claims");
                context.Load(claimsList);

                //Load the items in the claims list
                CamlQuery camlQuery = new CamlQuery();
                ListItemCollection allClaims = claimsList.GetItems(camlQuery);
                context.Load(allClaims);

                //Execute the query
                context.ExecuteQuery();

                foreach (ListItem sharepointClaim in allClaims)
                {
                    MileageClaim currentClaim = new MileageClaim();
                    currentClaim.Destination = sharepointClaim["Destination"].ToString();
                    currentClaim.ReasonForTrip = sharepointClaim["ReasonForTrip"].ToString();
                    currentClaim.Miles = (int)sharepointClaim["Miles"];
                    if (sharepointClaim["EngineSize"] != null)
                    {
                        currentClaim.EngineSize = (int)sharepointClaim["EngineSize"];
                    }
                    currentClaim.Amount = (double)sharepointClaim["Amount"];
                    claimsToDisplay.Add(currentClaim);
                }


            }

            return PartialView("Index", claimsToDisplay);
        }

        public ActionResult Create()
        {
            MileageClaim newClaim = new MileageClaim();
            return View("Create", newClaim);
        }

        [HttpPost]
        public ActionResult Create(MileageClaim claim)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", claim);
            }
            else
            {
                //Perform the mileage calculation
                claim.calculateAmount();
                //Get the app web 
                string appWebUrl = Session["SPAppWebUrl"].ToString();
                using (ClientContext context = new ClientContext(appWebUrl))
                {
                    List claimsList = context.Web.Lists.GetByTitle("Claims");
                    context.Load(claimsList);

                    ListItemCreationInformation creationInfo = new ListItemCreationInformation();
                    ListItem newClaim = claimsList.AddItem(creationInfo);
                    newClaim["Destination"] = claim.Destination;
                    newClaim["ReasonForTrip"] = claim.ReasonForTrip;
                    newClaim["Miles"] = claim.Miles;
                    newClaim["EngineSize"] = claim.EngineSize;
                    newClaim["Amount"] = claim.Amount;                    
                    newClaim.Update();

                    context.ExecuteQuery();
                }
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
