using System;
using System.Windows.Controls;
namespace Model
{
  public class MyEditFile
    {
        public string BufferPath { get { return Path+".buff"; } }
        public string Path { get; set; } = "";
        public string Content { get; set; } = "";
        public string Header { get; set; } = "";
        public bool  IsNewFile { get; set; } 
        public TextBox Text { get; set; }
        public TabControl? Tab { get; set; }
    }



}
