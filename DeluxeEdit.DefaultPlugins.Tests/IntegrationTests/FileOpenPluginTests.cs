using System.Text;
using DefaultPlugins;
using Model;
using Xunit;

namespace DeluxeEdit.DefaultPlugins.Tests.IntegrationTests
{
    public class FileOpenPluginTests
     {
        public static string TempDir = "C:/temp";
        public static string TestFile = TempDir + "/testfile.txt";
        public static string TestFile2 = TempDir + "/testfile2.txt";

        [Fact]
        public async void FileOpenPluginTest()
        {
            var plugin = FileOpenPlugin.CastNative( AllPlugins.InvokePlugin(PluginType.FileOpen));

            var expected = "ninjaåäÖ";
            if (File.Exists(TestFile)) File.Delete(TestFile);
            File.WriteAllText(TestFile, "ninjaåäÖ", Encoding.UTF8);
            plugin.OpenEncoding=Encoding.UTF8;
            var actual = await plugin.Perform(
                new ActionParameter(TestFile));
            Assert.Equal(expected, actual);
        }
        [Fact]
        public async void FileOpenPluginTestSimple()
        {
            var plugin = AllPlugins.InvokePlugin(PluginType.FileOpen) as FileOpenPlugin;

            var expected = "ninjaåäö";
            if (File.Exists(TestFile2)) File.Delete(TestFile2);
            File.WriteAllText(TestFile2, "ninjaåäö");
            plugin.OpenEncoding = null;
            var actual = await plugin.Perform(
                new ActionParameter(TestFile2));
            Assert.Equal(expected, actual);
        }




    }
}