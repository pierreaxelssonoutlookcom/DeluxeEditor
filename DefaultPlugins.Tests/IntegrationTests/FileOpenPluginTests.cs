using System.IO;
using System.Text;
using DefaultPlugins;
using Model;
using Xunit;
 
namespace DeluxeEdit.DefaultPlugins.Tests.IntegrationTests
{
    public class FileOpenPluginTests
     {
        public static string TempDir = "C:/temp";
        public static string TestFile = TempDir + "/testfile1.txt";
        public static string TestFile2 = TempDir + "/testfile2.txt";

        [Fact]
        public async Task FileOpenPluginTest()
        {
            var plugin = AllPlugins.InvokePlugin<FileOpenPlugin>(PluginType.FileOpen);

            var expected = "ninjaåäÖ";
            if (File.Exists(TestFile)) File.Delete(TestFile);
            File.WriteAllText(TestFile, "ninjaåäÖ", Encoding.UTF8);
            
            var actual = await plugin.Perform(
                     new ActionParameter(TestFile, Encoding.UTF8), new Progress<long>());
            Assert.Equal(expected, actual);
        }
        [Fact]
        public async Task FileOpenPluginTestSimple()
        {
            var plugin = AllPlugins.InvokePlugin<FileOpenPlugin>(PluginType.FileOpen);

            var expected = "ninjaåäö";
            if (File.Exists(TestFile2)) File.Delete(TestFile2);
            File.WriteAllText(TestFile2, "ninjaåäö");
            var actual = await plugin.Perform(
                new ActionParameter(TestFile2), new Progress<long>());
            Assert.Equal(expected, actual);
        }




    }
}