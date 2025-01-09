using DefaultPlugins;
using Extensions.Util;
using ICSharpCode.AvalonEdit;
using Model;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ViewModel
{
    public class HexView: LoadFile
    {
        private HexPlugin hex;
        public HexView(MainEditViewModel model, ProgressBar progressBar, TabControl tab): base(model, progressBar, tab)
        {
            hex = AllPlugins.InvokePlugin<HexPlugin>(PluginType.Hex);

        }
        



        public override async Task<MyEditFile?> Load()
        {
            var result = new MyEditFile();
            if (MyEditFiles.Current == null || MyEditFiles.Current.Text == null) throw new NullReferenceException();

            model.SetStatusText($"Hex View:{MyEditFiles.Current.Path}");

            var progress = new Progress<long>(value => progressBar.Value = value);
            var parameter = new ActionParameter(MyEditFiles.Current.Path, MyEditFiles.Current.Encoding);
            var hexOutput = await hex.Perform(parameter, progress);
            
            result.Path = MyEditFiles.Current.Path;
            result.Content = hexOutput;
    
            var items  = AddMyControlsForExisting(result.Path, "hex:");
            result.Text = items.Item1;
            result.Area = fileTypeLoader.CurrentArea;

            items.Item1.Text = hexOutput;
            result.Tab = items.Item2;
            MyEditFiles.Add(result);


            return result;
        }
    }
}
