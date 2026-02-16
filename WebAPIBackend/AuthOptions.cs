using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace WebAPIBackend
{
    public class AuthOptions
    {
        public const string ISSUER = "WebAPI_App_Server";
        public const string AUDIENCE = "WebAPI_App_Client";
        public const string KEY = "A211B41B-F95B-4BA1-BCF3-1F5219EBC1CF";
        public const int LIFETIME = 1;

        public static SymmetricSecurityKey GetSummetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
    
}
