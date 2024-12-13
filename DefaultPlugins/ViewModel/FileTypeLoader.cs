using Model;
using System.Collections.Generic;
using System.Linq;
using Extensions.Util;
using System;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit;

namespace ViewModel
{
    public class FileTypeLoader
    {
        private HighlightingManager manager;
        public static List<FileTypeItem> AllFileTypes { get; set; }= LoadFileTypes();
        public static string CurrentPath { get; set; }=String.Empty;
        public TextEditor CurrentText { get; private set; }=new TextEditor();

        public FileTypeLoader()
        {
            manager = new HighlightingManager();

        }

        public void LoadCurrent(string path)
        {


            CurrentText = new TextEditor();
            CurrentText.SyntaxHighlighting = manager.GetDefinitionByExtension(path);
            CurrentPath = path; 
        }
        public static  List<FileTypeItem> LoadFileTypes()
        {
            var manager = new HighlightingManager();

            var names = Enum.GetNames(typeof(FileType));
            
            var result=names.Select(p => Enum.Parse<FileType>(p)).Select(p =>
            new FileTypeItem { 
                FileExtension = WPFUtil.FileTypeToExtension(p), 
                FileType = p, 
                Definition=manager.GetDefinitionByExtension(WPFUtil.FileTypeToExtension(p)) }
             ).ToList();
            return result;
            

        }




    }
}
