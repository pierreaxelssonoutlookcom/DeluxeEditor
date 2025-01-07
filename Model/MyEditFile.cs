using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Editing;
using System;
using System.Text;
using System.Windows.Controls;
namespace Model
{
  public class MyEditFile: IEquatable<MyEditFile>
    {
        public string BufferPath { get { return Path+".buff"; } }
        public Encoding? Encoding { get; set; }

        public string Path { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string Header { get; set; } = string.Empty;
        public bool  IsNewFile { get; set; }
        public TextEditor Text { get; set; } = new TextEditor();
        public TextArea? Area{ get; set; } = new TextEditor().TextArea;
        public TabControl? Tab { get; set; }
 

        public bool Equals(MyEditFile? other)
        {
            bool result = other != null ? other.Path == Path && other.Content == Content && other.Header == Header : false;
            return result;
            throw new NotImplementedException();
        }
    }



}
