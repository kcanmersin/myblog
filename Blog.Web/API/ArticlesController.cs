using Microsoft.AspNetCore.Mvc;
using Blog.Service.Services.Abstractions;
using Blog.Entity.DTOs.Articles;

namespace Blog.Web.Controllers.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticleService _articleService;

        public ArticlesController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllArticles()
        {
            var articles = await _articleService.GetAllArticlesWithCategoryNonDeletedAsync();
            return Ok(articles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetArticle(Guid id)
        {
            var article = await _articleService.GetArticleWithCategoryNonDeletedAsync(id);
            if (article == null)
            {
                return NotFound();
            }
            return Ok(article);
        }

[HttpPost]
public async Task<IActionResult> CreateArticle([FromForm] ArticleAddDto articleDto)
{
    if (!ModelState.IsValid)
    {
        return BadRequest(ModelState);
    }

    var createdArticle = await _articleService.CreateArticleAsync(articleDto);

    return CreatedAtAction(nameof(GetArticle), new { id = createdArticle.Id }, createdArticle);
}

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateArticle(Guid id, [FromBody] ArticleUpdateDto articleDto)
        {
            if (id != articleDto.Id)
            {
                return BadRequest();
            }

            var result = await _articleService.UpdateArticleAsync(articleDto);
            if (result == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticle(Guid id)
        {
            var result = await _articleService.SafeDeleteArticleAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
