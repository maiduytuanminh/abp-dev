using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using SmartSoftware.Auditing;
using SmartSoftware.Domain.Entities;
using SmartSoftware.Blogging.Tagging;
using SmartSoftware.Blogging.Users;

namespace SmartSoftware.Blogging.Posts
{
    [Serializable]
    public class PostCacheItem : ICreationAuditedObject
    {
        public Guid Id { get; set; }
        
        public Guid BlogId { get; set; }

        public string Title { get; set; }

        public string CoverImage { get; set; }

        public string Url { get; set; }

        public string Content { get; set; }

        public string Description { get; set; }

        public int ReadCount { get; set; }

        public int CommentCount { get; set; }

        public List<Tag> Tags { get; set; }
        
        public Guid? CreatorId { get; set; }

        public DateTime CreationTime { get; set; }
    }
}