using Model;
using System.Collections.Generic;
using System.Linq;
using Extensions.Util;
using System;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Editing;
using ICSharpCode.AvalonEdit.Document;

namespace ViewModel
{
    public class FileTypeLoader
    {
        private HighlightingManager manager;
        public static List<FileTypeItem> AllFileTypes { get; set; }= LoadFileTypes();
        public static string CurrentPath { get; set; }=String.Empty;
        public TextEditor CurrentText { get; set; } = new TextEditor();
        public TextArea CurrentArea { get; set; } = new TextEditor().TextArea;  

        public FileTypeLoader()
        {
            manager = new HighlightingManager();
        }
        public static FileTypeItem? GetFileTypeItemByMenu(string menuTitle)
        {
            var result = AllFileTypes.FirstOrDefault(p => p.ToString() == menuTitle && menuTitle.StartsWith("As "));
            return result;
        }
        public static FileTypeItem GetFileTypeItemByFileType(FileType fileType)
        {
            var result = AllFileTypes.First(p => p.FileType == fileType);
            return result;
        }






        public void LoadCurrent(string path)
        {


            CurrentText = new TextEditor();
            CurrentArea = CurrentText.TextArea;
            CurrentArea.MinHeight = 500;
            CurrentArea.MinWidth = 1000;

            //CurrentDocument= CurrentText.Document;
            CurrentText.IsReadOnly = false;
           
 
            var suggestededDefinition = manager.GetDefinitionByExtension(path);
            if (suggestededDefinition == null)
            {
                var mapped = WPFUtil.MapExtensionToMain(path);
                if (mapped != null)
                    suggestededDefinition=manager.GetDefinitionByExtension("dummy"+mapped);

            }
            


            if (suggestededDefinition != null) 
                CurrentText.SyntaxHighlighting = suggestededDefinition;

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
