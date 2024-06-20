using DefaultPlugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeluxeEdit.DefaultPlugins.ViewModel
{
    public class TempFileManager
    {

        public async Task<List<string>> ReadPortion(FileOpenPlugin plugin)
        {
            var result = await plugin.Perform();
            return result.ToList();





        }
    } 
}
