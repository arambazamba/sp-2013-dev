using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.SharePoint.Client;

namespace ContosoLedgersAppWeb.Pages
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // The following code gets the client context and Title property by using TokenHelper.
            // To access other properties, you may need to request permissions on the host web.

            Uri hostWeb = new Uri(Request.QueryString["SPHostUrl"]);

            using (var clientContext = TokenHelper.GetS2SClientContextWithWindowsIdentity(hostWeb, Request.LogonUserIdentity))
            {

                //Get the three relevant lists
                List salesLedger = clientContext.Web.Lists.GetByTitle("Sales Ledger");
                clientContext.Load(salesLedger);
                List purchaseLedger = clientContext.Web.Lists.GetByTitle("Purchase Ledger");
                clientContext.Load(purchaseLedger);

                //Get the items collections
                Microsoft.SharePoint.Client.ListItemCollection saleItems = salesLedger.GetItems(new CamlQuery());
                clientContext.Load(saleItems);
                Microsoft.SharePoint.Client.ListItemCollection purchaseItems = purchaseLedger.GetItems(new CamlQuery());
                clientContext.Load(purchaseItems);

                clientContext.ExecuteQuery();

                //Update the total numbers table
                lblTotalNumberOfSales.Text = salesLedger.ItemCount.ToString();
                lblTotalNumberOfPurchases.Text = purchaseLedger.ItemCount.ToString();

            }
        }

        private TableRow createRow (Microsoft.SharePoint.Client.ListItem regionItem,
            Microsoft.SharePoint.Client.ListItemCollection transactionItems) {
            
            //Create a new table row to return
            TableRow newRegionRow = new TableRow();
            //Total variables
            int numberOfTransactions = 0;
            double totalLocalCurrency = 0.0;
            double totalDollars = 0.0;

            //Total the sales
            foreach (Microsoft.SharePoint.Client.ListItem transItem in transactionItems)
            {
                FieldLookupValue saleRegionLookupValue = (FieldLookupValue)transItem["RegionLookup"];
                if (saleRegionLookupValue.LookupValue == (string)regionItem["Title"])
                {
                    numberOfTransactions++;
                    totalLocalCurrency += (double)transItem["Amount"];
                    double dollarAmount = (double)transItem["Amount"] * (double)regionItem["ExchangeRate"];
                    totalDollars += dollarAmount;
                }
            }

            //The first cell displays the region name
            TableCell regionCell = new TableCell();
            regionCell.Text = regionItem["Title"].ToString();
            newRegionRow.Cells.Add(regionCell);
            
            //The second cell displays the local currency
            TableCell localCurrency = new TableCell();
            localCurrency.Text = regionItem["CurrencyName"].ToString();
            newRegionRow.Cells.Add(localCurrency);

            //The third cell displays the total number of sales in the region
            TableCell totalNumberOfTransactions = new TableCell();
            totalNumberOfTransactions.Text = numberOfTransactions.ToString();
            newRegionRow.Cells.Add(totalNumberOfTransactions);

            //The fourth cell displays the total sales in the local currency
            TableCell totalInLocalCurrency = new TableCell();
            totalInLocalCurrency.Text = totalLocalCurrency.ToString();
            newRegionRow.Cells.Add(totalInLocalCurrency);

            //The fifth cell displays the Exchange Rate
            TableCell exchRate = new TableCell();
            exchRate.Text = regionItem["ExchangeRate"].ToString();
            newRegionRow.Cells.Add(exchRate);
            
            //The fifth cell displays the total sales in US Dollars
            TableCell totalInDollars = new TableCell();
            totalInDollars.Text = totalDollars.ToString();
            newRegionRow.Cells.Add(totalInDollars);

            return newRegionRow;
        }
    }
}