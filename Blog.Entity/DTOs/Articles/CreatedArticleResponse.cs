using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Entity.DTOs.Articles
{
    public class CreatedArticleResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        
    }
}