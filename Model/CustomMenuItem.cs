
using Model.Interface;
using System;

namespace Model
{

    public class CustomMenuItem
    {
        public INamedActionPlugin Plugin { get; set; }
        public Func<object,object>  MenuActon { get; set; }

        public Type MyType { get; set; }

        public string Title { get; set; }
        public ActionParameter Parameter { get; set; }




        public CustomMenuItem()
        {

        }
    }
}