using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class MyEditFiles
    {
        public static MyEditFile? Current { get; set; }
        public static void Add(MyEditFile item )
        {

            Files.Add(item);
            
            if (Files.Count == 1)
                Current= Files[0];

        }
        public static List<MyEditFile> Files { get; set; } = new List<MyEditFile>();


    }
}
