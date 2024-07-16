using DefaultPlugins;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Documents;
using Shared;

namespace DeluxeEdit.DefaultPlugins.ViewModel
{

    public class FileBufferManager
    {
        private List<string> MemoryBuffer { get; set; }
        public FileBufferManager()
        {
            MemoryBuffer = new List<string>();
        }
        public async void ClearMemoryBuffer()
        {
            MemoryBuffer.Clear();

        }
        public async void ReadToBufee(FileOpenPlugin plugin, MyEditFile file)
        { 
            plugin.Parameter = new ActionParameter(file.Path);
            MemoryBuffer.AddRange((await plugin.Perform()).ToList());
            var excess = MemoryBuffer.Where(x => x != null && MemoryBuffer.Count > SystemConstants.ReadBufferSizeLines)
               .Select(x => x).ToList();
                
                
                
                
                
            excess.ForEach(x => { MemoryBuffer.Remove(x); });
            
            
        }
         
        public async void WriteRPortion(List<string> data,FileSavePlugin plugin, MyEditFile file)
        {
            await plugin.Perform(new ActionParameter(file.Path, data));
         







        }




        public async void AppenBuffer(IEnumerable<string> data, MyEditFile file)
        {
            File.AppendAllLines(file.Path, data);
        }
        


    }
}