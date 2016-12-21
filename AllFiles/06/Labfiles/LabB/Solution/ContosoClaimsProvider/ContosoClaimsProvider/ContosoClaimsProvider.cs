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
    class LocationClaimsProvider : SPClaimProvider
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

        //This property is the display name for the claims provider
        internal static string ProviderDisplayName
        {
            get { return "Location Claims Provider"; }
        }

        //This property is the name used by SharePoint
        internal static string ProviderInternalName
        {
            get { return "LocationClaimsProvider"; }
        }

        //This is the type of claim. It should be unique amongst all
        //claim providers, so use a URL fomat string
        private static string LocationClaimType
        {
            get { return "http://schema.contoso.com/location"; }
        }

        //This property tells SharePoint that the claim is a string
        private static string LocationClaimValueType
        {
            get { return Microsoft.IdentityModel.Claims.ClaimValueTypes.String; }
        }

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
            //Check parameters
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            if (claims == null)
            {
                throw new ArgumentNullException("claims");
            }

            //Find out what the user's location is
            string currentLocation = getLocation(entity);

            //Add the claim
            claims.Add(CreateClaim(LocationClaimType, currentLocation, LocationClaimValueType));

        }

        //In this method, set the types of entities that this provider supports
        protected override void FillEntityTypes(List<string> entityTypes)
        {
            //This provider only support forms-based authentication entities
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
            if (!EntityTypesContain(entityTypes, SPClaimEntityTypes.FormsRole))
            {
                return;
            }
            //Loop through the possible locations
            foreach (string location in possibleLocations)
            {
                //Check if the entered string matches the current list location
                if (location.ToLower() == resolveInput.ToLower())
                {
                    //There is a match so create an entity for the location 
                    //and add it to the resolved collection
                    PickerEntity newEntity = getPickerEntity(location);
                    resolved.Add(newEntity);
                }
            }
        }

        //This method fills the schema for the people picker
        protected override void FillSchema(SPProviderSchema schema)
        {
            schema.AddSchemaElement(new SPSchemaElement(PeopleEditorEntityDataKeys.DisplayName, "Display Name", SPSchemaElementType.Both));
        }

        //This method searches for entities in the people picker
        protected override void FillSearch(Uri context, string[] entityTypes, string searchPattern, string hierarchyNodeID, int maxCount, SPProviderHierarchyTree searchTree)
        {
            if(!EntityTypesContain(entityTypes, SPClaimEntityTypes.FormsRole))
            {
                return;
            }

            //This integer tracks the current node
            int locationNode = -1;
            //Create a picker node in which to display matches
            SPProviderHierarchyNode matchesNode = null;
            
            //Loop through the possible locations to check for matches
            foreach (string location in possibleLocations)
            {
                //Increment the tracker
                locationNode ++;

                if (location.ToLower().StartsWith(searchPattern.ToLower()))
                {
                    //There is a match
                    //Create a picker entity
                    PickerEntity newEntity = getPickerEntity(location);

                    //Check that the current entity is not already in place
                    if (!searchTree.HasChild(locationKeys[locationNode]))
                    {
                        //Create a new node
                        matchesNode = new SPProviderHierarchyNode(
                            ProviderInternalName,
                            locationLabels[locationNode],
                            locationKeys[locationNode],
                            true);

                        //Add the node to the search tree
                        searchTree.AddChild(matchesNode);
                    }
                    else
                    {
                        //The current entity is already in place
                        matchesNode = searchTree.Children.Where(
                            theNode => theNode.HierarchyNodeID == locationKeys[locationNode]).First();
                    }
                    //Add the picker entity to the tree node
                    matchesNode.AddEntity(newEntity);
                }
            }
        }

        //This method creates a picker entity to add to the hierarchy
        private PickerEntity getPickerEntity(string ClaimValue)
        {
            PickerEntity newEntity = CreatePickerEntity();
            //Set the claim associated with the picker entity
            newEntity.Claim = CreateClaim(LocationClaimType, ClaimValue, LocationClaimValueType);
            //Set the tooltip
            newEntity.Description = ProviderDisplayName + ":" + ClaimValue;
            //Set the display text
            newEntity.DisplayText = ClaimValue;
            newEntity.EntityData[PeopleEditorEntityDataKeys.DisplayName] = ClaimValue;
            //Set the entity type
            newEntity.EntityType = SPClaimEntityTypes.FormsRole;
            //Set the entity as resolved
            newEntity.IsResolved = true;
            newEntity.EntityGroupName = "Location";
            return newEntity;
        }

        //This method resolves the location for an entity
        //For simplicity, this method puts users into locations based on 
        //their username.
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
            get { return true; }
        }

        public override bool SupportsHierarchy
        {
            get { return true; }
        }

        public override bool SupportsResolve
        {
            get { return true; }
        }

        public override bool SupportsSearch
        {
            get { return true; }
        }
    }
}
