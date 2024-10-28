using DefaultPlugins;
using Model;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;


namespace ViewModel
{
    public partial class MainEditViewModel
    {
        private async  void OnEvent(object sender, CustomEventArgs e)
        {
            if (e.Type == EventType.EditFile)
                await LoadFile();
            else if (e.Type == EventType.NewFile)
                NewFile();


        }

        private void ProgressBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (!lastFileLength.HasValue) return;

            var percent = e.NewValue;
            progressText.Text = $"{percent}%%";
            
        }
        private void Text_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            var keyeddata = KeyDown();
            if (keyeddata == null) e.Handled = false;
            else
            {


                e.Handled = true;
            }
        }

        public async Task<MyEditFile?> KeyDown()
        {
            //done:cast enum from int
            MyEditFile? result = null;
            bool keysOkProceed = false;
            foreach (var plugin in relevantPlugins)
            {
                var matchCount = plugin.Configuration.KeyCommand.Keys
                    .Cast<System.Windows.Input.Key>()
                    .Count(p => System.Windows.Input.Keyboard.IsKeyDown(p));

                keysOkProceed = matchCount == plugin.Configuration.KeyCommand.Keys.Count && openPlugin.Configuration.KeyCommand.Keys.Count > 0;
                if (keysOkProceed)
                {
                    if (plugin is FileOpenPlugin)
                        result = await LoadFile();
                    else if (plugin is FileSavePlugin)
                        SaveFile();
                    else if (plugin is FileSaveAsPlugin  )
                        SaveAsFile();
                    else if (plugin is FileNewPlugin)
                        NewFile();

                }

            }
            return result;
        }




    }
}
