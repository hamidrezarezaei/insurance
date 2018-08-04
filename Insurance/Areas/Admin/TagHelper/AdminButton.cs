using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Insurance.Areas.Admin
{
    [HtmlTargetElement(tag: "a", Attributes = "btntype")]
    [HtmlTargetElement(tag: "input", Attributes = "btntype")]

    public class AdminButton:TagHelper
    {
        public string btntype { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            switch (btntype.ToLower())
            {
                case "create":
                    output.Attributes.Add(new TagHelperAttribute("class", "btn btn-success btn-rounded"));
                    output.PostContent.AppendHtml(" <i class=\"fa fa-plus-circle\"></i>");
                    break;
                case "edit":
                    output.Attributes.Add(new TagHelperAttribute("class", "btn btn-primary btn-rounded"));
                    output.PostContent.AppendHtml(" <i class=\"far fa-edit\"></i>");
                    break;
                case "delete":
                    output.Attributes.Add(new TagHelperAttribute("class", "btn btn-danger btn-rounded"));
                    output.PostContent.AppendHtml(" <i class=\"far fa-trash-alt\"></i>");
                    break;
                case "back":
                    output.Attributes.Add(new TagHelperAttribute("class", "btn btn-cancel btn-rounded"));
                    output.PostContent.AppendHtml(" <i class=\"fas fa-arrow-up\"></i>");
                    break;
                case "save":
                    output.Attributes.Add(new TagHelperAttribute("class", "btn btn-success btn-rounded"));
                    output.PostContent.AppendHtml(" <i class=\"far fa-save\"></i>");
                    break;
                case "cancel":
                    output.Attributes.Add(new TagHelperAttribute("class", "btn btn-cancel btn-rounded"));
                    break;
                case "submitdelete":
                    output.Attributes.Add(new TagHelperAttribute("class", "btn btn-danger btn-rounded"));
                    output.PostContent.AppendHtml(" <i class=\"far fa-trash-alt\"></i>");
                    break;
                    
                default:
                    break;
            }

            base.Process(context, output);
        }

    }
}
