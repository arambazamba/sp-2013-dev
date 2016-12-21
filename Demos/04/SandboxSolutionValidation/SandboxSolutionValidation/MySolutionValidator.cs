using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint.UserCode;

namespace SandboxSolutionValidation
{
    [Guid("29e3702d-5d8c-45ad-b1aa-a2087b9e8585")]
    public class MySolutionValidator : SPSolutionValidator
    {
        private const string myValidator = "MySolutionValidator";

        // Not used, but needed for deployment and compilation 
        public MySolutionValidator()
        {
        }

        public MySolutionValidator(SPUserCodeService sandboxService) : base(myValidator, sandboxService)
        {
            // Use this to define a unique identification number 
            // You may need this when updating/modifying the validator 
            Signature = 666;
        }

        public override void ValidateSolution(SPSolutionValidationProperties properties)
        {
            base.ValidateSolution(properties);

            // Set to false if you want invalidate the solution 
            // Set to true (default) if you want to validate 
            properties.Valid = false; // Being evil and invalidates all solutions for test!

            // Then specify an errorpage to display 
            // Tip: Create a nice application page for this.. 
            properties.ValidationErrorUrl = "/_layouts/SandboxSolutionValidation/MyErrorPage.aspx";
        }
    }
}