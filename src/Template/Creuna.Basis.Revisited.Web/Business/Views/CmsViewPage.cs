using System;
using System.Web.Mvc;
using System.Web.WebPages;

namespace Creuna.Basis.Revisited.Web.Business.Views
{
    public abstract class CmsViewPage<TModel> : WebViewPage<TModel>
    {
        public virtual HelperResult RenderSection(string name, Func<HelperResult> defaultContents)
            => IsSectionDefined(name) ? RenderSection(name) : defaultContents();

        public virtual HelperResult RenderSection(string name, Func<MvcHtmlString> defaultContents)
            => RenderSection(name, () => new HelperResult(x => x.Write(defaultContents())));
    }
}