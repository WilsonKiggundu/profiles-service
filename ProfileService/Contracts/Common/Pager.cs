namespace ProfileService.Contracts.Common
{
    public class Pager
    {
        public int PageSize { get; set; } = 10;
        public int PageNumber { get; set; } = 1;
    }
}