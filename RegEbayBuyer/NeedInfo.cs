using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreDogeTool
{
    class NeedInfo
    {
        internal string newRcvMail;
        internal string mail;
        internal string cookie_rcv;
        internal string pass_mail;

        public int index { get; set; }
        public string Data_input { get; set; }
        public string proxy { get; set; }
        public string Result { get; set; }
        public string status { get; set; }
        public int errorCodeStatus { get; set; }
        public string Password { get; internal set; }
        public string Cookie { get; internal set; }
        public string Email { get; internal set; }
        public string InfoAcc { get; internal set; }
    }

    class dCookie
    {
        public string domain { get; set; }
        public double expirationDate { get; set; }
        public bool hostOnly { get; set; }
        public bool httpOnly { get; set; }
        public string name { get; set; }
        public string path { get; set; }
        public string sameSite { get; set; }
        public bool secure { get; set; }
        public bool session { get; set; }
        public string storeId { get; set; }
        public string value { get; set; }
    }

    class dCookieJ2TeamFormat
    {
        public string url { get; set; }
        public List<dCookie> cookies { get; set; }

    }
}
