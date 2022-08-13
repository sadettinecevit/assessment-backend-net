namespace Contacts.Application.Dto.Command
{
    public abstract class FilteredPageQuery
    {
        public int DataCount { get; set; } = 10;
        public int PageIndex { get; set; } = 1;
        public string Keyword { get; set; } = string.Empty;
    }
}
