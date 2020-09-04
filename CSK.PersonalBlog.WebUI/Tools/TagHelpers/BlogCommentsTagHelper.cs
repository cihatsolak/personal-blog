using CSK.PersonalBlog.WebUI.Models.Comments;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Collections.Generic;
using System.Text;

namespace CSK.PersonalBlog.WebUI.Tools.TagHelpers
{
    [HtmlTargetElement("getblogcomments")]
    public class BlogCommentsTagHelper : TagHelper
    {
        public List<CommentViewModel> Comments { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            StringBuilder builder = new StringBuilder();
            var commentsHtml = GetComments(Comments, builder);
            output.Content.SetHtmlContent(commentsHtml.ToString());
        }

        private StringBuilder GetComments(List<CommentViewModel> comments, StringBuilder stringBuilder)
        {
            if (comments.Count > 0)
            {
                foreach (var comment in comments)
                {
                    stringBuilder.Append("<div class='media mb-4 mt-2'>");
                    stringBuilder.Append("<img class='d-flex mr-3 rounded-circle' src='http://placehold.it/50x50' alt=''>");
                    stringBuilder.Append("<div class='media-body'>");
                    stringBuilder.Append($"<h5 class='mt-0'>{comment.AuthorName}</h5>");
                    stringBuilder.Append($"{comment.Description}");
                    stringBuilder.Append($"<button class='btn btn-sm btn-primary float-right' type='button' onclick='showCommentForm({comment.Id},{comment.BlogId})'>");
                    stringBuilder.Append($"<small>Cevapla</small>");
                    stringBuilder.Append("</button>");
                    stringBuilder.Append($"<div id='commentBox{comment.Id}'></div>");

                    GetComments(comment.SubComments, stringBuilder);

                    stringBuilder.Append("</div>");
                    stringBuilder.Append("</div>");
                }
            }

            return stringBuilder;
        }
    }
}
