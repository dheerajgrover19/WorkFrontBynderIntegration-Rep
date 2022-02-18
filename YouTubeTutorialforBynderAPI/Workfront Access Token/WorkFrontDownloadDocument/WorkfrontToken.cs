namespace Workfront_Access_Token.Common
{
    public class WorkfrontToken
    {
        public data data { get; set; }

        public AuthenticationCookie AuthenticationCookie { get; set; }

    }

    public class data
    {
        public string redirectUrl { get; set; }

        public string success { get; set; }
    }

    public class AuthenticationCookie
    {
        public string wf_node { get; set; }
        public string xsrf_token { get; set; }
        public string attask { get; set; }
        public string sessionExpiration { get; set; }
        public string webcache { get; set; }

        public string Cookie { get; set; }
    }
}
