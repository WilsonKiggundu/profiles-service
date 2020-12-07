using AutoMapper;
using ProfileService.Contracts.Business;

namespace ProfileService.Contracts.Blog.Post
{
    public class PostMapper : Profile
    {
        public PostMapper()
        {
            CreateMap<Models.Posts.Post, GetPost>();
            CreateMap<NewPost, Models.Posts.Post>() .ReverseMap();
            CreateMap<UpdatePost, Models.Posts.Post>() .ReverseMap();
        }
    }
}