using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SportsStore.Models.ViewModels;

namespace SportsStore.Infrastructure
{
    [HtmlTargetElement("div", Attributes = "page-model")]
    public class PageLinkTagHelper : TagHelper
    {
        private readonly IUrlHelperFactory _urlHelperFactory;

        public PageLinkTagHelper(IUrlHelperFactory urlHelperFactory)
        {
            _urlHelperFactory = urlHelperFactory;
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext? ViewContext {get; set;}

        public PagingInfo? PageModel {get; set;}

        public string? PageAction {get; set;}

        public bool PageClassesEnabled {get; set;} = false;

        public string PageClass { get; set;} = string.Empty;

        public string PageClassNormal {get; set;} = string.Empty;

        public string PageClassSelected {get; set;} = string.Empty;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (ViewContext == null || PageModel == null)
            {
                return;
            }

            IUrlHelper helper = _urlHelperFactory.GetUrlHelper(ViewContext);

            var result = new TagBuilder("div");

            for (var i = 1; i <= PageModel.TotalPages; i++)
            {
                var tag = new TagBuilder("a");

                tag.Attributes["href"] = helper.Action(PageAction, new { productPage = i });

                if (PageClassesEnabled == true)
                {
                    tag.AddCssClass(PageClass);

                    tag.AddCssClass(i == PageModel.CurrentPage
                        ? PageClassSelected
                        : PageClassNormal);
                }

                tag.InnerHtml.Append(i.ToString());
                result.InnerHtml.AppendHtml(tag);
            }

            output.Content.AppendHtml(result.InnerHtml);
        }
    }
}