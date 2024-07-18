using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Entity.DTOs.Categories;
using System.ComponentModel;

namespace Blog.Entity.DTOs.Articles
{
    public class ArticleAddDto
    {

        [DefaultValue("Default Article Title")]
        public string Title { get; set; }
             [DefaultValue("Default Article Content")]
        public string Content { get; set; }

        [DefaultValue("d23e4f79-9600-4b5e-b3e9-756cdcacd2b1")]
        public Guid CategoryId { get; set; }

        public IFormFile Photo { get; set; }

        public IList<CategoryDto> Categories { get; set; }
    }
}
