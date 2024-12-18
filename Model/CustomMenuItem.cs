namespace Model
{

    public class CustomMenuItem
    {
        public Func<Task<MyEditFile?>>? MenuActon { get; set; }

        public Func<object>? SimpleActon { get; set; }


        Func<int, int> square = x => x * x;
        public INamedActionPlugin? Plugin { get; set; }= null;
        public Type? MyType { get; set; }

        public string Title { get; set; } = "";
        public ActionParameter Parameter { get; set; } = new ActionParameter();

    }

}
