## SEO and Open Graphs

All pages inheriting from **ContentPageBase** will have SEO fields and Open Graph-tags enabled.

Edit the information on the SEO-tab, as the defaults are only to render title, og:title, og:type and og:url.

To change the rendering or number of fields, edit Metadata.cshtml and SeoSettingsBlock respectively.

## OG:Type
og:type defaults to "article", which is appropriate for most pages.
The type is chosen using a dropdown using the values in the enumerable OpenGraphType.

To add additional types, add them to the enum and add translations for all languages in lang/translations.[lang].xml under /Enums/OpenGraphType/

