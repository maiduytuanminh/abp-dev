using System;
using System.Linq;
using System.Threading.Tasks;
using Shouldly;
using SmartSoftware.Blogging.Blogs;
using SmartSoftware.Blogging.Comments;
using SmartSoftware.Blogging.Comments.Dtos;
using SmartSoftware.Blogging.Posts;
using Xunit;

namespace SmartSoftware.Blogging
{
    public class CommentAppService_Tests : BloggingApplicationTestBase
    {
        private readonly ICommentAppService _commentAppService;
        private readonly ICommentRepository _commentRepository;
        private readonly IPostRepository _postRepository;
        private readonly IBlogRepository _blogRepository;

        public CommentAppService_Tests()
        {
            _commentAppService = GetRequiredService<ICommentAppService>();
            _commentRepository = GetRequiredService<ICommentRepository>();
            _postRepository = GetRequiredService<IPostRepository>();
            _blogRepository = GetRequiredService<IBlogRepository>();
        }

        [Fact]
        public async Task Should_Get_List_Of_Comments()
        {
            var post = (await _postRepository.GetListAsync()).FirstOrDefault();
            await _commentRepository.InsertAsync(new Comment(Guid.NewGuid(), post.Id, null, "qweasd"));
            await _commentRepository.InsertAsync(new Comment(Guid.NewGuid(), post.Id, null, "qweasd"));

            var comments =
                await _commentAppService.GetHierarchicalListOfPostAsync(post.Id);

            comments.Count.ShouldBeGreaterThan(2);
        }

        [Fact]
        public async Task Should_Create_A_Comment()
        {
            var postId = (await _postRepository.GetListAsync()).First().Id;
            var content = "test content";

            var commentWithDetailsDto = await _commentAppService.CreateAsync(new CreateCommentDto()
                {PostId = postId,Text = content});

            UsingDbContext(context =>
            {
                var comment = context.Comments.FirstOrDefault(q => q.Id == commentWithDetailsDto.Id);
                comment.ShouldNotBeNull();
                comment.Text.ShouldBe(commentWithDetailsDto.Text);
            });
        }

        [Fact]
        public async Task Should_Update_A_Comment()
        {
            var newContent = "new content";

            var oldComment = (await _commentRepository.GetListAsync()).FirstOrDefault(); ;

            await _commentAppService.UpdateAsync(oldComment.Id, new UpdateCommentDto()
                {Text = newContent});

            UsingDbContext(context =>
            {
                var comment = context.Comments.FirstOrDefault(q => q.Id == oldComment.Id);
                comment.Text.ShouldBe(newContent);
            });
        }

        [Fact]
        public async Task Should_Delete_A_Comment()
        {
            var comment = (await _commentRepository.GetListAsync()).First();

            await _commentAppService.DeleteAsync(comment.Id);
        }
    }
}
