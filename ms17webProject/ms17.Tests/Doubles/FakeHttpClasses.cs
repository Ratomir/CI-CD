using System.Collections.Specialized;
using System.Web;

namespace ms17.Web.Tests.Doubles
{
    public class FakeHttpContextForRouting : HttpContextBase
    {
        FakeHttpRequestForRouting _request;
        FakeHttpResponseForRouting _response;

        public FakeHttpContextForRouting(string appPath = "/", string requestUrl = "~/")
        {
            _request = new FakeHttpRequestForRouting(appPath, requestUrl);
            _response = new FakeHttpResponseForRouting();
        }

        public override HttpRequestBase Request
        {
            get { return _request; }
        }

        public override HttpResponseBase Response
        {
            get { return _response; }
        }
    }

    public class FakeHttpRequestForRouting : HttpRequestBase
    {
        string _appPath;
        string _requestUrl;

        public FakeHttpRequestForRouting(string appPath, string requestUrl)
        {
            _appPath = appPath;
            _requestUrl = requestUrl;
        }

        public override string ApplicationPath
        {
            get { return _appPath; }
        }

        public override string AppRelativeCurrentExecutionFilePath
        {
            get { return _requestUrl; }
        }

        public override string PathInfo
        {
            get { return ""; }
        }

        public override NameValueCollection ServerVariables
        {
            get { return new NameValueCollection(); }
        }
    }

    public class FakeHttpResponseForRouting : HttpResponseBase
    {
        public override string ApplyAppPathModifier(string virtualPath)
        {
            return virtualPath;
        }
    }


}
