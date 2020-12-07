using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProfileService.Contracts.Blog.Comment;
using ProfileService.Controllers.Common;
using ProfileService.Services.Interfaces;

namespace ProfileService.Controllers.Blog
{
    [Route("api/blog/comments")]
    public class CommentsController : BaseController
    {
        private readonly ICommentService _commentService;
        private readonly ILogger<CommentsController> _logger;

        public CommentsController(ICommentService commentService, ILogger<CommentsController> logger)
        {
            _commentService = commentService;
            _logger = logger;
        }

        /// <summary>
        /// SEARCH comments
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<GetComment> Get(Guid? postId = null, Guid? articleId = null)
        {
            return _commentService.GetAll(postId, articleId);
        }
        
        /// <summary>
        /// GET comment by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<GetComment> GetOne(Guid id)
        {
            return await _commentService.GetByIdAsync(id);
        }

        /// <summary>
        /// CREATE a comment
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<NewComment> Create(NewComment comment)
        {
            try
            {
                await _commentService.InsertAsync(comment);
                return comment;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        
        /// <summary>
        /// UPDATE a comment
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<UpdateComment> Update(UpdateComment comment)
        {
            try
            {
                _logger.LogCritical(JsonConvert.SerializeObject(comment));
                await _commentService.UpdateAsync(comment);
                return comment;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        
        /// <summary>
        /// DELETE comment
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _commentService.DeleteAsync(id);
                return Ok(id);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}