using System;
using System.Collections.Generic;
using System.Text;
using Shouldly;
using SmartSoftware.Blogging.Tagging;
using Xunit;

namespace SmartSoftware.Blogging
{
    public class Tag_Tests
    {
        [Theory]
        [InlineData("aaa")]
        [InlineData("bbb")]
        public void SetName(string name)
        {
            var tag = new Tag(Guid.NewGuid(), Guid.NewGuid(), "ss", 0, "ss tag");
            tag.SetName(name);
            tag.Name.ShouldBe(name);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void IncreaseUsageCount(int number)
        {
            var tag = new Tag(Guid.NewGuid(), Guid.NewGuid(), "ss", 0, "ss tag");
            tag.IncreaseUsageCount(number);
            tag.UsageCount.ShouldBe(number);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void DecreaseUsageCount(int number)
        {
            var tag = new Tag(Guid.NewGuid(), Guid.NewGuid(), "ss",  10, "ss tag");
            tag.DecreaseUsageCount(number);
            tag.UsageCount.ShouldBe(10 - number);
        }

        [Fact]
        public void DecreaseUsageCount_Should_Greater_ThanOrEqual_Zero()
        {
            var tag = new Tag(Guid.NewGuid(), Guid.NewGuid(), "ss", 10, "ss tag");
            tag.DecreaseUsageCount(100);
            tag.UsageCount.ShouldBe(0);
        }

        [Theory]
        [InlineData("aaa")]
        [InlineData("bbb")]
        public void SetDescription(string description)
        {
            var tag = new Tag(Guid.NewGuid(), Guid.NewGuid(), "ss", 0, "ss tag");
            tag.SetDescription(description);
            tag.Description.ShouldBe(description);
        }


    }
}
