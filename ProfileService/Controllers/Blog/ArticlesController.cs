using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProfileService.Contracts.Blog.Article;
using ProfileService.Controllers.Common;
using ProfileService.Services.Interfaces;

namespace ProfileService.Controllers.Blog
{
    [Route("api/blog/articles")]
    public class ArticlesController : BaseController
    {
        private readonly IArticleService _articleService;
        private readonly ILogger<ArticlesController> _logger;

        public ArticlesController(IArticleService articleService, ILogger<ArticlesController> logger)
        {
            _articleService = articleService;
            _logger = logger;
        }

        /// <summary>
        /// SEARCH articles
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] SearchArticleRequest request)
        {
            try
            {
                var articles = await _articleService.SearchAsync(request);
                return Ok(articles);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        
        /// <summary>
        /// GET article by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<GetArticle> GetOne(Guid id)
        {
            return await _articleService.GetByIdAsync(id);
        }

        /// <summary>
        /// CREATE a article
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<NewArticle> Create(NewArticle article)
        {
            try
            {
                await _articleService.InsertAsync(article);
                return article;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        
        /// <summary>
        /// UPDATE a article
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<UpdateArticle> Update(UpdateArticle article)
        {
            try
            {
                await _articleService.UpdateAsync(article);
                return article;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        
        /// <summary>
        /// DELETE article
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _articleService.DeleteAsync(id);
                return Ok(id);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}