
using Model.Interface;
using System;
using System.Collections.Generic;

namespace Model
{

    public class CustomMenuItem
    {
        public INamedActionPlugin Plugin { get; set; }
        public Type MyType { get; set; }

        public string Title { get; set; }
        public Func<ActionParameter, string> MenuAction { get; set; }
        public ActionParameter Parameter { get; set; }




        public CustomMenuItem()
        {

        }
    }
}