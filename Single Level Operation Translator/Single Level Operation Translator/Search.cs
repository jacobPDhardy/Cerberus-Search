namespace Single_Level_Operation_Translator
{
    public class Search
    {
        public string SearchString { get; private set; }

        public bool Not { get; private set; }

        public Search(string searchString, bool not = false)
        {
            SearchString = searchString;
            Not = not;
        }
    }
}
