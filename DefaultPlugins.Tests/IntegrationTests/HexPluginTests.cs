using System.IO;
using System.Text;
using DefaultPlugins;
using Model;
using Xunit;

namespace DeluxeEdit.DefaultPlugins.Tests.IntegrationTests
{
    public class HexPluginTests
    {
        public static string TempDir = "C:/temp";
        public static string TestFile = TempDir + "/testfile.txt";
        public static string TestFile2 = TempDir + "/testfile2.txt";

        [Fact]
        public async void FileOpenPluginTest()
        {
            var plugin = AllPlugins.InvokePlugin<HexPlugin>(PluginType.Hex);

            var expected = "ninjaåäÖ";
            if (File.Exists(TestFile)) File.Delete(TestFile);
            File.WriteAllText(TestFile, "ninjaåäÖ", Encoding.UTF8);
            plugin.OpenEncoding = Encoding.UTF8;
            var actual = await plugin.Perform(
                new ActionParameter(TestFile), null);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async void FileOpenPluginTestSimple()
        {
            var plugin = AllPlugins.InvokePlugin<HexPlugin>(PluginType.Hex);

            var expected = "ninjaåäö";
            if (File.Exists(TestFile2)) File.Delete(TestFile2);
            File.WriteAllText(TestFile2, "ninjaåäö");
            plugin.OpenEncoding = null;
            var actual = await plugin.Perform(
                new ActionParameter(TestFile2), null);
            Assert.Equal(expected, actual);
        }




    }
}