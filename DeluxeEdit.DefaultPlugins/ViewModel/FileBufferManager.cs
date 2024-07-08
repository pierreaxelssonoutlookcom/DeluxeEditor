using DefaultPlugins;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace DeluxeEdit.DefaultPlugins.ViewModel
{

    public class FileBufferManager
    {
        private Stack<string> MyMuffer { get; set; }
        public FileBufferManager()
        {
            MyMuffer = new Stack<string>();
        }
        public async void ClearMemoryBuffer()
        {
            MyMuffer.Clear();

        }
        public void ScrollTo(double newValue)
        { 
        }
        public  string PopBuffer(FileOpenPlugin plugin, MyEditFile file)
        {
           var result= MyMuffer.Pop();
            return result;
        }
        public async void ReadBufeePush(FileOpenPlugin plugin, MyEditFile file)
        {
            plugin.Parameter = new ActionParameter(file.Path);

            (await plugin.Perform()).ToList().ForEach(p => MyMuffer.Push(p));




            ;




        }
         
        public async void WriteRPortion(List<string> data,FileSavePlugin plugin, MyEditFile file)
        {
            await plugin.Perform(new ActionParameter(file.Path, data));
         







        }




        public async void AppenBuffer(IEnumerable<string> data, MyEditFile file)
        {
            File.AppendAllLines(file.Path 
   , data);
        }
        


    }
}