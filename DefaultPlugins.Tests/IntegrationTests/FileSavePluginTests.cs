using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DefaultPlugins;
using DefaultPlugins.Misc;
using Model;

namespace DeluxeEdit.DefaultPlugins.Tests.IntegrationTests
{
    public class FileSavePluginTests
    {
        public static string TempDir = "C:/temp";
        public static string TestFile = TempDir + "/testsave.txt";
        public static string TestFile2 = TempDir + "/testsave.txt";

        [Fact]
        public void FileSavePluginTest()
        {
            var plugin = AllPlugins.InvokePlugin(PluginType.FileSave) as FileSavePlugin;

            var expected = "ninjaåäÖ";
            if (File.Exists(TestFile)) File.Delete(TestFile);
            File.WriteAllText(TestFile, "ninjaåäÖ", Encoding.UTF8);

            plugin.OpenEncoding=Encoding.UTF8;
             plugin.Perform(
                new ActionParameter(TestFile, "ninjaåäÖ"));
            
            
            var actual=File.ReadAllText(TestFile, Encoding.UTF8);
            Assert.Equal(expected, actual);
        }
        [Fact]
         public void FileSavePluginTestSimple()
        {
            var plugin = AllPlugins.InvokePlugin(PluginType.FileSave) as FileSavePlugin;

            var expected = "ninja";
            if (File.Exists(TestFile2)) File.Delete(TestFile2);
            File.WriteAllText(TestFile2, "ninja");
            plugin.OpenEncoding = null;
            plugin.Perform(
                new ActionParameter(TestFile2, "ninja"));
            var actual = File.ReadAllText(TestFile);

            Assert.Equal(expected, actual);
        }




    }
}