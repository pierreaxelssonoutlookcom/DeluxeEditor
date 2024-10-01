using DefaultPlugins.Model;
using DeluxeEdit.DefaultPlugins;
using DeluxeEdit.DefaultPlugins.ViewModel;
using Extensions;
using Model;
using Model.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;



namespace DefaultPlugins.ViewModel
{
    public partial class MainEditViewModel
    {
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
            var matchCount = openPlugin.Configuration.KeyCommand.Keys
                .Cast<System.Windows.Input.Key>()
                .Count(p => System.Windows.Input.Keyboard.IsKeyDown(p));

            keysOkProceed = matchCount == openPlugin.Configuration.KeyCommand.Keys.Count && openPlugin.Configuration.KeyCommand.Keys.Count > 0;
            if (keysOkProceed) result = await LoadFile();


            return result;
        }




    }
}
