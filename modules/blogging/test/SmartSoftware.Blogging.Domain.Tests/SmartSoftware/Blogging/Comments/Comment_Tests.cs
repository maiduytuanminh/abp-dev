using System;
using System.Collections.Generic;
using System.Text;
using Shouldly;
using SmartSoftware.Blogging.Comments;
using Xunit;

namespace SmartSoftware.Blogging
{
    public class Comment_Tests
    {
        [Theory]
        [InlineData("aaa")]
        [InlineData("bbb")]
        public void SetText(string text)
        {
            var comment = new Comment(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), "good");
            comment.SetText(text);
            comment.Text.ShouldBe(text);
        }
    }
}
