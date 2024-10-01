using System.Text;
using DefaultPlugins;
using Model;
using Xunit;
using System.IO;

namespace DeluxeEdit.DefaultPlugins.Tests.IntegrationTests
{
    public class FileSavePluginTests
    {
        public static string TempDir = "C:/temp";
        public static string TestFile = $"{TempDir}/testfile__{Guid.NewGuid()}.txt";
        public static string TestFile2 = $"{TempDir}/testfile2__{Guid.NewGuid()}.txt";

        [Fact]
        public async void FileSavePluginTest()
        {
            var plugin = AllPlugins.InvokePlugin(PluginType.FileSaveAs) as FileSavePlugin;

            var expected = "ninjaåäÖ\r\n";
            File.AppendAllLines(TestFile,new List<string> { "ninjaåäÖ" },  Encoding.UTF8);

            plugin.OpenEncoding=Encoding.UTF8;
            await plugin.Perform(

               new ActionParameter(TestFile, "ninjaåäÖ")
              , null );

            File.Copy(TestFile, TestFile2, true);



            var actual =File.ReadAllText(TestFile2, Encoding.UTF8);

            File.Delete(TestFile);
            File.Delete(TestFile2);
            Assert.Equal(expected , actual);

  
        }
        [Fact]
         public async void FileSavePluginTestSimple()
        {
            var plugin = AllPlugins.InvokePlugin(PluginType.FileSaveAs) as FileSavePlugin;

            var expected = "ninja" + Environment.NewLine;
            File.AppendAllLines(TestFile, new List<string> { "ninja" }, Encoding.UTF8);

            // if (File.Exists(TestFile2)) File.Delete(TestFile2);

            plugin.OpenEncoding = null;
            await  plugin.Perform(
               new ActionParameter(TestFile, "ninja"),null);
            File.Copy(TestFile, TestFile2, true);



            var actual = File.ReadAllText(TestFile2);
            File.Delete(TestFile);
            File.Delete(TestFile2);

            Assert.Equal(expected, actual);

        }




    }
}