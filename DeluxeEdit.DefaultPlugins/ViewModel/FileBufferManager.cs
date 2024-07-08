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
        private List<string> MemoryMuffer { get; set; }
        public FileBufferManager()
        {
            MemoryMuffer = new List<string>();
        }
        public async void ClearMemoryBuffer()
        {
            MemoryMuffer.Clear();
        }
        public void ScrollTo(double newValue)
        { }
        public async void ReadPortion(FileOpenPlugin plugin, MyEditFile file)
        {
            plugin.Parameter = new ActionParameter(file.Path);
            MemoryMuffer.AddRange(await plugin.Perform());
                
               
                ;




        }
         
        public async void WriteRPortion(List<string> data,FileSavePlugin plugin, MyEditFile file)
        {
            await plugin.Perform(new ActionParameter(file.Path, data));
         







        }




        public async void AppenBuffer(IEnumerable<string> data, MyEditFile file)
        {
            File.AppendAllLines(file.BufferPath, data);
        }




    }
}