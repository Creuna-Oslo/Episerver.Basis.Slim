using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.Globalization;
using EPiServer.ServiceLocation;
using EPiServer.Web.Mvc.Html;
using System.Web;
using System.Web.Mvc;

namespace Creuna.Basis.Revisited.Web.Business.Views
{
    public static class UrlHelperExtensions
    {
        /// <summary>
        /// Returns the HtmlString representation of the currently active language segment
        /// </summary>
        public static IHtmlString LanguageURLSegment(this UrlHelper html)
        {
            var segment = LanguageURLSegment();
            return new HtmlString(segment);
        }

        /// <summary>
        /// Returns the string representation of the currently active language segment
        /// </summary>
        public static string LanguageURLSegment()
        {
            var service = ServiceLocator.Current.GetInstance<ILanguageBranchRepository>();
            return service.Load(ContentLanguage.PreferredCulture).URLSegment;
        }

        /// <summary>
        /// Returns the current url with a single query parameter changed to a new value
        /// </summary>
        public static string UrlWithQueryParameter(this UrlHelper url, string parameter, string newValue)
        {
            var request = HttpContext.Current.Request;
            var nameValues = HttpUtility.ParseQueryString(request.QueryString.ToString());
            nameValues.Set(parameter, newValue);
            return $"{request.Url.AbsolutePath}?{nameValues}";
        }


        /// <summary>
        /// Convert contentreference to absolute Url
        /// </summary>
        public static string AbsoluteUrl(this UrlHelper helper, ContentReference content)
            => AbsoluteUrl(helper, helper.ContentUrl(content));

        /// <summary>
        /// Convert url to absolute
        /// </summary>
        public static string AbsoluteUrl(this UrlHelper helper, Url url)
            => url.IsAbsoluteUri
                ? url.OriginalString
                : AbsoluteUrl(helper, url.OriginalString);

        /// <summary>
        /// Convert relative url string to absolute
        /// </summary>
        public static string AbsoluteUrl(this UrlHelper helper, string relativeUrl)
        {
            var baseUrl = GetSiteBaseUrl();
            return $"{baseUrl}{relativeUrl}";
        }

        static string GetSiteBaseUrl()
        {
            var url = HttpContext.Current.Request.Url;
            var applicationPath = HttpContext.Current.Request.ApplicationPath.TrimEnd('/');
            return $"{url.Scheme}://{url.Authority}{applicationPath}";
        }
    }
}