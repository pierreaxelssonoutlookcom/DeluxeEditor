using Model;
using System.Collections.Generic;
using System.Linq;
using Extensions.Util;
using System;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Editing;
using ICSharpCode.AvalonEdit.Document;
using System.IO;
using System.Collections.ObjectModel;
using Extensions;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;

namespace DefaultPlugins.DefaultPlugins.PluginHelpers
{
    public class FileTypeLoader
    {

        //public static List<FileTypeItem> AllFileTypes { get; set; }
        public static string CurrentPath { get; set; } = string.Empty;
        public TextEditor CurrentText { get; set; } = new TextEditor();
        public TextArea CurrentArea { get; set; } = new TextEditor().TextArea;
        public FileTypeItem? CurrentFileItem { get; set; } = new FileTypeItem();
        /*
        public static FileTypeItem? GetFileTypeItemByMenu(string menuTitle)
        {
            var result = AllFileTypes.FirstOrDefault(p => p.ToString() == menuTitle && menuTitle.StartsWith("As "));
            return result;
        }
        public static FileTypeItem GetFileTypeItemByFileType(FileType fileType)
        {
            var result = AllFileTypes.First(p => p.FileType == fileType);
            return result;
            */





        public void LoadCurrent(string path)
        {
            var manager = HighlightingManager.Instance;


            if (path.HasContent())
                CurrentFileItem = GetFileTypes().FirstOrDefault(p => path.EndsWith(p.FileExtension, StringComparison.OrdinalIgnoreCase));


            if (CurrentFileItem != null) CurrentFileItem.HilightDefinition = manager.GetDefinitionByExtension(new FileInfo(path).Extension);
            CurrentText = new TextEditor();

            CurrentArea = CurrentText.TextArea;
            CurrentArea.MinHeight = 500;
            CurrentArea.MinWidth = 1000;
            //CurrentText.SyntaxHighlighting = "C#";
            //CurrentDocument= CurrentText.Document;




            if (CurrentFileItem != null && CurrentFileItem.HilightDefinition != null)
                CurrentText.SyntaxHighlighting = CurrentFileItem.HilightDefinition;


            CurrentPath = path;
        }


        public List<FileTypeItem> GetFileTypes()
        {

            var names = Enum.GetNames(typeof(FileType));

            var result = names.Select(p => Enum.Parse<FileType>(p)).Select(p =>
            new FileTypeItem
            {
                FileExtension = WPFUtil.FileTypeToExtension(p),
                FileType = p
            }).ToList();
            return result;


        }




    }
}
