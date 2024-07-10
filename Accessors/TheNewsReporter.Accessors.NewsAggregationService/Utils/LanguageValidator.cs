namespace TheNewsReporter.Accessors.NewsAggregationService.Utils
{
    public enum ValidLanguages
    {
        En,
        Es,
        Fr,
        De,
        It
    }
    public static class LanguageValidator
    {
        public static string ValidateLanguage(string language)
        {
            if (Enum.TryParse<ValidLanguages>(language, true, out _))
            {
                return language;
            }
            return "en";
        }
    }
}
