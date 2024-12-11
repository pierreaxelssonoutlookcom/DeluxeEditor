using Model;
using System.Collections.Generic;
using System.Linq;
using Extensions;
using DefaultPlugins;
using Shared;
using System.Windows.Controls;
using Extensions.Util;
using System.Reflection.Metadata;
using System;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit;
using static System.Net.Mime.MediaTypeNames;

namespace ViewModel
{
    public class FileTypeLoader
    {
        private HighlightingManager man;
        public static List<FileTypeItem> AllFileTypes { get; set; }= new List<FileTypeItem>();
        public static string CurrentPath { get; set; }=String.Empty;


        public FileTypeLoader()
        {
            man = new HighlightingManager();

        }

        public void LoadCurrent(string path)
        {


            var text = new TextEditor();
            text.SyntaxHighlighting = man.GetDefinitionByExtension(path);
            CurrentPath = path; 
        }
        public void LoadFileTypes(string path)
        {
            LoadCurrent(path);


            var names = Enum.GetNames(typeof(FileType));
            
            AllFileTypes=names.Select(p => Enum.Parse<FileType>(p)).Select(p =>
            new FileTypeItem { 
                FileExtension = WPFUtil.FileTypeToExtension(p), 
                FileType = p, 
                Definition=man.GetDefinitionByExtension(WPFUtil.FileTypeToExtension(p)) }
             ).ToList();

            

        }




    }
}
