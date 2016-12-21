using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint.Diagnostics;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint.Administration.Claims;
using Microsoft.SharePoint.WebControls;

namespace Contoso.Security
{
    class LocationClaimsProvider
    {

        //Constructor
        public LocationClaimsProvider(string displayName) 
            : base(displayName)
        {

        }

        //Array of possible locations
        private string[] possibleLocations = new string[] { "North America", "South America", "Europe", "Africa", "Asia", "Australiasia" };
        //Keys for nodes in the people picker hierarchy
        private string[] locationKeys = new string[] { "keyAmericas", "keyEMEA", "keyAsia" }; 
        //Labels for nodes in the people picker hierarcy
        private string[] locationLabels = new string[] { "Americas", "Europe and Africa", "Asia and Australia" };

        //Internal and private properties
        //Add internal and private properties here

		//In this method, define the claim types you support by adding them
        //to the claimTypes list.
        protected override void FillClaimTypes(List<string> claimTypes)
        {
			//Make sure you have a list of claim types
            if (claimTypes == null)
            {
                throw new ArgumentNullException("claimTypes");
            }

			//Add the Location claim type
            claimTypes.Add(LocationClaimType);
        }

		//In this method, define the value types you support by adding them
        //to the claimValueTypes list
        protected override void FillClaimValueTypes(List<string> claimValueTypes)
        {
			//Make sure you have a list of claim value types
            if (claimValueTypes == null)
            {
                throw new ArgumentNullException("claimValueTypes");
            }

			//Add the Location claim value type, which is a string
            claimValueTypes.Add(LocationClaimValueType);
        }

		//This method adds claims to a user's security token
        protected override void FillClaimsForEntity(Uri context, SPClaim entity, List<SPClaim> claims)
        {
            throw new NotImplementedException();
        }

        //In this method, set the types of entities that this provider supports
        protected override void FillEntityTypes(List<string> entityTypes)
        {
			//This provider only supports forms-based authentication entities
            entityTypes.Add(SPClaimEntityTypes.FormsRole);
        }

		//You must implement this method if you want your provider to support
        //a hierarchy in the left of the people picker
        protected override void FillHierarchy(Uri context, string[] entityTypes, string hierarchyNodeID, int numberOfLevels, SPProviderHierarchyTree hierarchy)
        {
            //Make sure this is a request for a forms role
            if (!EntityTypesContain(entityTypes, SPClaimEntityTypes.FormsRole))
            {
                return;
            }
            if (hierarchyNodeID == null)
            {
			    //Add three nodes. America, EMEA, and Asia
                hierarchy.AddChild(
                    new Microsoft.SharePoint.WebControls.SPProviderHierarchyNode(
                        LocationClaimsProvider.ProviderInternalName,
                        locationLabels[0],
                        locationKeys[0],
                        true
                    ));

                hierarchy.AddChild(
                    new Microsoft.SharePoint.WebControls.SPProviderHierarchyNode(
                        LocationClaimsProvider.ProviderInternalName,
                        locationLabels[1],
                        locationKeys[1],
                        true
                    ));

                hierarchy.AddChild(
                    new Microsoft.SharePoint.WebControls.SPProviderHierarchyNode(
                        LocationClaimsProvider.ProviderInternalName,
                        locationLabels[2],
                        locationKeys[2],
                        true
                    ));

            }
        }

		//This method attempts to resolve a claim
        protected override void FillResolve(Uri context, string[] entityTypes, SPClaim resolveInput, List<PickerEntity> resolved)
        {
            if (!EntityTypesContain(entityTypes, SPClaimEntityTypes.FormsRole))
            {
                return;
            }
			//Loop through the possible locations
            foreach (string location in possibleLocations)
            {
				//Check if the entered claim matches the current list location
                if (location.ToLower() == resolveInput.Value.ToLower())
                {
					//There is a match so create an entity for the location 
                    //and add it to the resolved collection
                    PickerEntity entity = getPickerEntity(location);
                    resolved.Add(entity);
                }
            }
        }

		//This method attempts to resolve an entered string
        protected override void FillResolve(Uri context, string[] entityTypes, string resolveInput, List<PickerEntity> resolved)
        {
            throw new NotImplementedException();
        }

		//This method fills the schema for the people picker
        protected override void FillSchema(SPProviderSchema schema)
        {
            schema.AddSchemaElement(new SPSchemaElement(PeopleEditorEntityDataKeys.DisplayName, "Display Name", SPSchemaElementType.Both));
        }

		//This method searches for entities in the people picker
        protected override void FillSearch(Uri context, string[] entityTypes, string searchPattern, string hierarchyNodeID, int maxCount, SPProviderHierarchyTree searchTree)
        {
            throw new NotImplementedException();
        }

        //This method creates a picker entity to add to the hierarchy
        private PickerEntity getPickerEntity(string ClaimValue)
        {
            throw new NotImplementedException();
        }


        //This method resolves the location for an entity
        private string getLocation(SPClaim entity)
        {
            string location = string.Empty; 
            string userName = string.Empty; 
            int userPipe = entity.Value.LastIndexOf("|");

            if (userPipe > -1)
            {
                userName = entity.Value.Substring(userPipe + 1);
            }

            userName = userName.ToLower();

            if (userName == "contoso\\administrator")
            {
                //Put Administrator in North America
                location = possibleLocations[0];
            }
            else if (userName == "contoso\\bart")
            {
                //Put Bart in South America
                location = possibleLocations[1];
            }
            else if (userName == "contoso\\alexei")
            {
                //Put Alexei in Europe
                location = possibleLocations[2];
            }
            else if (userName == "contoso\\christa")
            {
                //Put Evan in Africa
                location = possibleLocations[3];
            }
            else if (userName == "contoso\\evan")
            {
                //Put Evan in Asia
                location = possibleLocations[4];
            }
            else
            {
                //Put all other users in Australiasia
                location = possibleLocations[5];
            }

            return location;

        }

        //Public properties
        public override string Name
        {
            get { return ProviderInternalName; }
        }

        public override bool SupportsEntityInformation
        {
            get { return false; }
        }

        public override bool SupportsHierarchy
        {
            get { return true; }
        }

        public override bool SupportsResolve
        {
            get { return false; }
        }

        public override bool SupportsSearch
        {
            get { return false; }
        }
    }
}
