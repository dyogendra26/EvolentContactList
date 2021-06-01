using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactListWeb
{
    public class SD
    {
        public static string APIBaseUrl = "https://localhost:44322/";
        public static string ContactGroupAPIPath = APIBaseUrl + "api/v1/ContactGroup/";
        public static string ContactAPIPath = APIBaseUrl + "api/v1/Contact/";
        public static string AccountAPIPath = APIBaseUrl + "api/v1/Users/";
    }
}
