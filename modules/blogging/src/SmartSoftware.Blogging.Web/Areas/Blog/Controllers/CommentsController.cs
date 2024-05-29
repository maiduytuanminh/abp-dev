using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartSoftware.AspNetCore.Mvc;
using SmartSoftware.Blogging.Comments;
using SmartSoftware.Blogging.Comments.Dtos;

namespace SmartSoftware.Blogging.Areas.Blog.Controllers
{
    //TODO: Is that being used?

    [Area("Blog")]
    [Route("Blog/[controller]/[action]")]
    public class CommentsController : BloggingControllerBase
    {
        private readonly ICommentAppService _commentAppService;

        public CommentsController(ICommentAppService commentAppService)
        {
            _commentAppService = commentAppService;

        }

        [HttpPost]
        public virtual async Task Delete(Guid id)
        {
            await _commentAppService.DeleteAsync(id);
        }

        [HttpPost]
        public virtual async Task Update(Guid id, UpdateCommentDto commentDto)
        {
            await _commentAppService.UpdateAsync(id, commentDto);
        }
    }
}
