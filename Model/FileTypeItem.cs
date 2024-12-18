using ICSharpCode.AvalonEdit.Highlighting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class FileTypeItem
    {
        public IHighlightingDefinition? Definition;
        public FileType FileType { get; set; }


        public string FileExtension { get; set; } = String.Empty;

        public override string ToString()
        {
            return $"As {FileType} ({FileExtension}";
        }

    }

}
