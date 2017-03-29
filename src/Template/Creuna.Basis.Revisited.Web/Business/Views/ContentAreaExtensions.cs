using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using EPiServer.Core;
using EPiServer.Web.Mvc.Html;

namespace Creuna.Basis.Revisited.Web.Business.Views
{
    public class ContentAreaOptions
    {
        public string RenderingTag { get; set; }

        public string CustomTag { get; set; }

        public string CssClass { get; set; }

        public string ChildrenCustomTag { get; set; }

        public string ChildrenCssClass { get; set; }
    }

    public static class ContentAreaExtensions
    {
        public static MvcHtmlString ContentArea<TModel>(this HtmlHelper<TModel> html, Expression<Func<TModel, ContentArea>> contentAreaExpression, ContentAreaOptions options = null)
        {
            if (html == null) throw new ArgumentNullException(nameof(html));
            if (contentAreaExpression == null) throw new ArgumentNullException(nameof(contentAreaExpression));

            return html.PropertyFor(contentAreaExpression, string.Empty, GetContentAreaViewData(options));
        }

        private static object GetContentAreaViewData(ContentAreaOptions options)
        {
            return new
            {
                Tag = options?.RenderingTag,
                options?.CustomTag,
                ChildrenCustomTagName = options?.ChildrenCustomTag,
                CssClass = options?.CssClass,
                options?.ChildrenCssClass
            };
        }
    }
}