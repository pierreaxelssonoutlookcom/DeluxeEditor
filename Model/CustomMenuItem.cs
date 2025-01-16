using System.Windows.Controls;

namespace Model
{

    public class CustomMenuItem: MenuItem
    { 
        public Func<Task<MyEditFile?>>? MenuActon { get; set; }

        public FileType? FileType  { get; set; }
       
        public INamedActionPlugin? Plugin { get; set; }= null;
        public Type? MyType { get; set; }

        public string Title { get; set; } = string.Empty;
        public ActionParameter Parameter { get; set; } = new ActionParameter();
        public CheckBox? CheckBox { get; set; }
        public CustomMenuItem(): base()
        {
            
        }
        public CustomMenuItem(MenuItem copy): base()
        {
            var  header = copy.Header;
            if (header != null)
            {
                string? headerString  = header.ToString();
                if (headerString != null)
                    Title = headerString; 
            }
            foreach (var item in copy.Items) 
                Items.Add(item);    


            

        }


    }

}
