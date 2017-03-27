using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.Globalization;
using EPiServer.ServiceLocation;
using EPiServer.Web.Mvc.Html;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace Creuna.Basis.Revisited.Web.Business.Extensions
{
    public static class CmsExtensions
    {
        /// <summary>
        /// Returns the HtmlString representation of the currently active language segment
        /// </summary>
        public static IHtmlString LanguageURLSegment(this HtmlHelper html)
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

        /// <summary>
        /// Returns a resized version of the image from url string.
        /// Resizing is done using the RestImageResize nuget package.
        /// </summary>
        public static string Image(this UrlHelper helper, string url, int width = 0, int height = 0, bool crop = true)
            => ResizedImage(url, width, height, crop);

        /// <summary>
        /// Returns a resized version of the image content.
        /// Resizing is done using the RestImageResize nuget package.
        /// </summary>
        public static string Image(this UrlHelper helper, ImageData image, int width = 0, int height = 0, bool crop = true)
        {
            var url = helper.ContentUrl(image.ContentLink);
            return ResizedImage(url, width, height, crop);
        }

        static string ResizedImage(string url, int width, int height, bool crop)
        {
            if (string.IsNullOrEmpty(url))
                return url;

            if (Regex.IsMatch(url, @",,\d+\?"))
                url = Regex.Replace(url, @",,\d+\?", match => "?");

            url += $"{(url.Contains('?') ? '&' : '?')}transform={(crop ? "downfill" : "downfit")}";

            if (width > 0)
                url += "&width=" + width;

            if (height > 0)
                url += "&height=" + height;

            return url;
        }

        static string GetSiteBaseUrl()
        {
            var url = HttpContext.Current.Request.Url;
            var applicationPath = HttpContext.Current.Request.ApplicationPath.TrimEnd('/');
            return $"{url.Scheme}://{url.Authority}{applicationPath}";
        }
    }
}