using AutoMapper;
using ProfileService.Contracts.Blog.Post;

namespace ProfileService.Contracts.Blog.Comment
{
    public class CommentMapper : Profile
    {
        public CommentMapper()
        {
            CreateMap<Models.Posts.Comment, GetComment>();
            CreateMap<NewComment, Models.Posts.Comment>().ReverseMap();
            CreateMap<UpdateComment, Models.Posts.Comment>().ReverseMap();
        }
    }
}