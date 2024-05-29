using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using SmartSoftware.Blogging.Tagging;

namespace SmartSoftware.Blogging.Posts
{
    [Serializable]
    public class PostEto
    {
        public Guid Id { get; set; }

        public Guid BlogId { get; set; }

        [NotNull]
        public string Url { get; set; }

        [NotNull]
        public string CoverImage { get; set; }

        [NotNull]
        public string Title { get; set; }

        [CanBeNull]
        public string Content { get; set; }

        public int ReadCount { get; set; }
    }
}