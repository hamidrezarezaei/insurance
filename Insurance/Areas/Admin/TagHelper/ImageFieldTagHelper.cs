using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Insurance.Areas.Admin
{
    [HtmlTargetElement(tag: "img", Attributes = "rowid")]
    public class ImageFieldTagHelper : TagHelper
    {
        public string rowid { get; set; }
        public string imagesrc { get; set; }
        public bool hideinput { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {

            output.Attributes.Add(new TagHelperAttribute("id", $"img_{rowid}"));
            output.Attributes.Add(new TagHelperAttribute("class", "image-field"));
            output.Attributes.Add(new TagHelperAttribute("onclick", $"$('#modal_{rowid}').show();"));

            var input = new TagBuilder("input");
            input.Attributes.Add("type", "file");
            input.Attributes.Add("name", "image");
            input.Attributes.Add("id", "image");
            input.Attributes.Add("onchange", "readURL(this);");

            var tagModal = new TagBuilder("div");
            tagModal.Attributes.Add("id", $"modal_{rowid}");
            tagModal.Attributes.Add("class", "modal");

            var span = new TagBuilder("span");

            tagModal.InnerHtml.AppendHtml($"<span class='close' onclick=\"$('#modal_{rowid}').hide(); \">&times;</span>");
            tagModal.InnerHtml.AppendHtml($"<img class='modal-content' src='{imagesrc}'>");

            if (!this.hideinput)
                output.PreElement.AppendHtml(input);

            output.PostElement.AppendHtml(tagModal);

            base.Process(context, output);
        }

    }
}
