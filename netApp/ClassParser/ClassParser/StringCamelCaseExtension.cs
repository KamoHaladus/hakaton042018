namespace ClassParser
{
    public static class StringCamelCaseExtension
    {
        public static string ToCamelCase(this string stringValue)
        {
            return stringValue.Substring(0, 1).ToLower() + stringValue.Substring(1);
        }
    }
}
