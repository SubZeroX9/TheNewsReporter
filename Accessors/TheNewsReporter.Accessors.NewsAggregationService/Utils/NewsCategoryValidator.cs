namespace TheNewsReporter.Accessors.NewsAggregationService.Utils
{
    public enum ValidCategories
    {
        Business,
        Crime,
        Domestic,
        Education,
        Entertainment,
        Environment,
        Food,
        Health,
        Lifestyle,
        Other,
        Politics,
        Science,
        Sports,
        Technology,
        Top,
        Tourism,
        World
    }
    public static class NewsCategoryValidator
    {
        public static List<string> ValidateCategories(List<string> categories)
        {
            List<string> validCategories = new List<string>();
            if (categories == null)
            {
                return validCategories;
            }

            foreach (var category in categories)
            {
                if (Enum.TryParse<ValidCategories>(category, true, out _))
                {
                    validCategories.Add(category);
                }
            }
            return validCategories;
        }
    }
}
