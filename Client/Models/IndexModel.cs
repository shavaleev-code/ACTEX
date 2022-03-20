namespace Client.Models
{
    public class IndexModel
    {
        public SearchModel Search { get; set; }
        public List<string> Types { get; set; }

        public IndexModel()
        {
            Search = new SearchModel();          
        }
    }
}
