using System.Windows.Controls;

namespace Model
{

    public class CustomMenuItem: MenuItem
    { 
        public Func<Task<MyEditFile?>>? MenuActon { get; set; }

        public FileType? FileType  { get; set; }
       
        public INamedActionPlugin? Plugin { get; set; }= null;
        public Type? MyType { get; set; }

        public string Title { get; set; } = "";
        public ActionParameter Parameter { get; set; } = new ActionParameter();

        public CustomMenuItem(): base()
        {
            
        }
    }

}
