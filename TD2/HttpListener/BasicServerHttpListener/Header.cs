using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BasicServerHTTPlistener
{
    class Header
    {
        private string mime;
        private string from;
        private string host;
        private string charset;
        private string encoding;
        private string langage;
        private string methods;
        private string authorizations;
        private string cookie;
        private string date;

        public Header(HttpListenerRequest request)
        {
            mime = request.Headers.Get(HttpRequestHeader.Accept.ToString());
            from = request.Headers.Get(HttpRequestHeader.From.ToString());
            host = request.Headers.Get(HttpRequestHeader.Host.ToString());
            charset = request.Headers.Get(HttpRequestHeader.AcceptCharset.ToString());
            encoding = request.Headers.Get(HttpRequestHeader.AcceptEncoding.ToString());
            langage = request.Headers.Get(HttpRequestHeader.AcceptLanguage.ToString());
            methods = request.Headers.Get(HttpRequestHeader.Allow.ToString());
            authorizations = request.Headers.Get(HttpRequestHeader.Authorization.ToString());
            cookie = request.Headers.Get(HttpRequestHeader.Cookie.ToString());
            date = request.Headers.Get(HttpRequestHeader.Date.ToString());
        }

        public override string ToString()
        {
            return "MIME : " + mime + "\n" + 
                "From : " + from + "\n" + 
                "Host : " + host + "\n" + 
                "Charset : " + charset + "\n" + 
                "Encoding : " + encoding + "\n" + 
                "Langage  : " + langage + "\n" + 
                "Methods : " + methods + "\n" + 
                "Authorizations : " + authorizations + "\n" + 
                "Cookie : " + cookie + "\n" + 
                "Date : " +  date + "\n";
        }
    }
}
