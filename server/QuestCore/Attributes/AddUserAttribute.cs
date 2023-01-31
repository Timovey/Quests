namespace QuestCore.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AddUserAttribute : Attribute
    {
        public AddUserAttribute(HttpContext context)
        {

        }
    }
}
