using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProfileService.Contracts.Blog.Post;
using ProfileService.Controllers.Common;
using ProfileService.Services.Interfaces;

namespace ProfileService.Controllers.Blog
{
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
    }
}