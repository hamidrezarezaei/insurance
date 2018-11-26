using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Insurance.Areas.Admin
{
    [HtmlTargetElement("nav", Attributes = "page-model")]
    public class PagerTagHelper : TagHelper
    {
        private IUrlHelperFactory urlHelperFactory;
        public PagerTagHelper(IUrlHelperFactory helperFactory)
        {
            urlHelperFactory = helperFactory;
        }
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }
        public PagingData PageModel { get; set; }
        public string PageAction { get; set; }
        public string PageContoller { get; set; }
        public string ItemId { get; set; }
        public string searchString { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
            TagBuilder result = new TagBuilder("ul");

            result.Attributes["class"] = "pagination";
            if (PageModel.totalPages == 1)
                return;
            for (int i = 1; i <= PageModel.totalPages; i++)
            {
                TagBuilder litag = new TagBuilder("li");
                TagBuilder tag = new TagBuilder("a");

                litag.Attributes["class"] = "page-item";
                tag.Attributes["class"] = "page-link";

                tag.Attributes["href"] = urlHelper.Action(PageAction, PageContoller, new { id = ItemId, pageNumber = i, searchString = searchString });

                tag.InnerHtml.Append(i.ToString());
                litag.InnerHtml.AppendHtml(tag);
                result.InnerHtml.AppendHtml(litag);
            }
            output.Content.AppendHtml(result);
        }
    }
}
