namespace Model
{

    public class CustomMenuItem
    {
        public Func<Task<object>, object>? MenuActon { get; set; }

        public INamedActionPlugin? Plugin { get; set; }= null;
        public Type? MyType { get; set; }

        public string Title { get; set; } = "";
        public ActionParameter Parameter { get; set; } = new ActionParameter();
    }
}