using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProfileService.Data;
using ProfileService.Models.Posts;
using ProfileService.Repositories.Interfaces;

namespace ProfileService.Repositories.Implementations
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ProfileServiceContext _context;
        private readonly ILogger<CommentRepository> _logger;
        
        public CommentRepository(ProfileServiceContext context, ILogger<CommentRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IEnumerable<Comment> GetAll()
        {
            throw new NotImplementedException();
        }
        
        public IEnumerable<Comment> GetAll(Guid? postId, Guid? articleId)
        {
            return _context.Comments
                .Include(q => q.Author)
                .Where(q => q.ArticleId == articleId && q.PostId == postId)
                .ToList();
        }

        public async Task<Comment> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Comment>> GetByPostId(Guid postId)
        {
            return await _context.Comments.Where(q => q.PostId == postId).ToListAsync();
        }
        
        public async Task<IEnumerable<Comment>> GetByArticleId(Guid articleId)
        {
            return await _context.Comments.Where(q => q.ArticleId == articleId).ToListAsync();
        }

        public async Task InsertAsync(Comment entity)
        {
            await _context.Comments.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task InsertManyAsync(ICollection<Comment> entities)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(Comment entity)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}