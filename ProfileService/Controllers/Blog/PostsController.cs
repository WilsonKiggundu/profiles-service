using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProfileService.Contracts.Blog.Article;
using ProfileService.Contracts.Blog.Post;
using ProfileService.Controllers.Common;
using ProfileService.Services.Interfaces;

namespace ProfileService.Controllers.Blog
{
    [Authorize]
    [Route("api/blog/posts")]
    public class PostsController : BaseController
    {
        private readonly IPostService _postService;
        private readonly ILogger<PostsController> _logger;

        public PostsController(IPostService postService, ILogger<PostsController> logger)
        {
            _postService = postService;
            _logger = logger;
        }

        /// <summary>
        /// SEARCH posts
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<SearchPostResponse> Get([FromQuery] SearchPostRequest request)
        {
            var claimsIdentity = HttpContext.User.Identity as ClaimsIdentity;
            var userId = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId != null) request.UserId = Guid.Parse(userId);
            
            return await _postService.SearchAsync(request);
        }

        /// <summary>
        /// CREATE a post
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<NewPost> Create(NewPost post)
        {
            try
            {
                return await _postService.InsertAsync(post);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        
        /// <summary>
        /// UPDATE a article
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<UpdatePost> Update(UpdatePost post)
        {
            try
            {
                await _postService.UpdateAsync(post);
                return post;
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
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _postService.DeleteAsync(id);
                return Ok(id);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}