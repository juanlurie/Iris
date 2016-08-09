namespace Iris.Security.OAuth.Server.Controllers
{
    public class EditedScope
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string OldValue { get; set; }
    }

    public class NewScope
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}