namespace Iris.Security.OAuth.Server.Dtos
{
    public class ScopeDto
    {
        public string Value { get; set; }
        public string Name { get; set; }
        public bool IsDefault { get; set; }
    }
}