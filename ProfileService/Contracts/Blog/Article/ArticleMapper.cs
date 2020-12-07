using AutoMapper;

namespace ProfileService.Contracts.Blog.Article
{
    public class ArticleMapper : Profile
    {
        public ArticleMapper()
        {
            CreateMap<Models.Posts.Article, GetArticle>();
            CreateMap<NewArticle, Models.Posts.Article>().ReverseMap();
            CreateMap<UpdateArticle, Models.Posts.Article>().ReverseMap();
        }
    }
}