using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint.Client;

namespace CSOM_ManageClient
{
    class Program
    {
        static void Main()
        {

            ClientContext cc = new ClientContext("http://SP2013c") { Credentials = CredentialCache.DefaultCredentials };
            Web site = cc.Web;
            ListCollection lists = site.Lists;

            // load site info
            cc.Load(site);
            cc.ExecuteQuery();

            Console.WriteLine("Site Title: " + site.Title);

            // create list
            ListCreationInformation newList = new ListCreationInformation
            {
                Title = "Customers CSOM",
                Url = "Lists/Customers_CSOM",
                QuickLaunchOption = QuickLaunchOptions.On,
                TemplateType = (int)ListTemplateType.Contacts
            };
            site.Lists.Add(newList);

            // refresh lists collection
            cc.Load(lists);

            // make round trip to Web server to do all the work
            cc.ExecuteQuery();

            foreach (List list in lists)
            {
                Console.WriteLine(list.Title);
            }


            //Create Custom List
            ListCreationInformation listInfo = new ListCreationInformation
            {
                Title = "Suggestions",
                Description = "A custom sample List",
                TemplateType = (int) ListTemplateType.GenericList
            };

            // Create the new list item and execute the query.
            List custList = cc.Web.Lists.Add(listInfo);
            cc.ExecuteQuery();

            // Create a list item

            // Get the suggestions list.
            List suggestionsList = cc.Web.Lists.GetByTitle("Suggestions");
            cc.Load(suggestionsList);
            // Create the List Item Creation Info object.
            ListItemCreationInformation itemInfo = new ListItemCreationInformation();
            // Create the new item and set its properties.
            ListItem newItem = suggestionsList.AddItem(itemInfo);
            newItem["Title"] = "A demo suggestion";
            // You must call the Update method before you execute the query.
            newItem.Update();
            cc.ExecuteQuery();


            suggestionsList = cc.Web.Lists.GetByTitle("Suggestions");
            cc.Load(suggestionsList);
            // Get the item and set its properties.
            ListItem item = suggestionsList.GetItemById(1);
            item["Title"] = "A new title";
            // You must call the Update method before you execute the query.
            item.Update();
            cc.ExecuteQuery();


            //Delete Item
            item.DeleteObject();
            cc.ExecuteQuery();
        }
    }
}
