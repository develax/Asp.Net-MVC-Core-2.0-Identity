using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreIdentity.Core
{
public static class Constants
{
    public static class Validation
    {
        public static class CSS
        {
            public const string ValidInput = "is-valid";
            public const string InvalidInput = "is-invalid";
            public const string InvalidMessage = "invalid-feedback";
            // Bootstrap hides "invalid-feedback" if "form-control" is not "is-invalid",
            // Bootstrap CSS example: .form-control.is-invalid~.invalid-feedback { display: block; }
            public const string ValidMessage = InvalidMessage; 
        }
            
    }
}
}
