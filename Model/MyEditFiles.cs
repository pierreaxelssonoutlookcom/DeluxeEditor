using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class MyEditFiles
    {
        public static MyEditFile Current { get; set; }

        public static List<MyEditFile> Files { get; set; } = new List<MyEditFile>();

    }
}
