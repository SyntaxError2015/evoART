using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;

namespace evoART.App_Start
{
    public static class AuthConfig
    {
        public static class FacebookClient
        {
            private const string _appId = "1476654745881710";
            private const string _appSecret = "e08f6c51bdf46e7d281c56a2143685a0";

            public static string AppId
            {
                get { return _appId; }
            }

            public static string AppSecret
            {
                get { return _appSecret; }
            }
        }

#if MICROSOFT
/*public static class MicrosoftClient
        {
            private const string _clientId = "";
            private const string _clientSecret = "";

            public static string ClientId
            {
                get { return _clientId; }
            }

            public static string ClientSecret
            {
                get { return _clientSecret; }
            }
        }*/
#endif


        public static class TwitterClient
        {
            private const string _consumerKey = "lf7o41V9mWQwWMOLM9U4A";
            private const string _consumerSecret = "LFNKAu0uul19Ifu53eYbFogDAod3Ykf62TB2LtCkBKE";

            public static string ConsumerKey
            {
                get { return _consumerKey; }
            }

            public static string ConsumerSecret
            {
                get { return _consumerSecret; }
            }
        }

        public static class GoogleClient
        {
            private const string _clientId = "269030395406.apps.googleusercontent.com";
            private const string _clientSecret = "OoPSXEJkABP3QJpn0_-MwmYY";

            public static string ClientId
            {
                get { return _clientId; }
            }

            public static string ClientSecret
            {
                get { return _clientSecret; }
            }
        }
    }
}