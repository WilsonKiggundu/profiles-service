using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProfileService.Contracts.Blog.Post;
using ProfileService.Controllers.Common;
using ProfileService.Models.Posts;
using ProfileService.Services.Interfaces;

namespace ProfileService.Controllers.Blog
{
    [Route("api/blog/posts/likes")]
    public class PostLikesController : BaseController
    {
        private readonly ILikesService _likesService;

        public PostLikesController(ILikesService likesService)
        {
            _likesService = likesService;
        }

        /// <summary>
        /// Get Post likes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<SearchLikeResponse> Get(Guid postId)
        {    
            return await _likesService.SearchAsync(new SearchLikeRequest
            {
                PostId = postId
            });
        }

        /// <summary>
        /// CREATE a post like
        /// </summary>
        /// <param name="like"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Like> Create(Like like)
        {
            try
            {
                return await _likesService.InsertAsync(like);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
    }
}