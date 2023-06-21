using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeluxeEdit.DefaultPlugins;
using DeluxeEdit.Model;

namespace DeluxeEdit.DefaultPlugins.Tests.IntegrationTests
{
    public class FileOpenPluginTests
     {
        public static string TempDir = "C:/temp";
        public static string TestFile = TempDir + "/testfile.txt";
        public static string TestFile2 = TempDir + "/testfile2.txt";

        [Fact]
        public void FileOpenPluginTest()
        {
            var expected = "ninjaåäö";
            if (File.Exists(TestFile)) File.Delete(TestFile);
            File.WriteAllText(TestFile, "ninjaåäö", Encoding.UTF8);
            var plugin = new FileOpenPlugin();
            plugin.OpenEncoding=Encoding.UTF8;
            var actual = plugin.Perform(
                new ActionParameter(TestFile));
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void FileOpenPluginTestSimple()
        {
            var expected = "ninjaåäö";
            if (File.Exists(TestFile2)) File.Delete(TestFile2);
            File.WriteAllText(TestFile2, "ninjaåäö");
            var plugin = new FileOpenPlugin();
            plugin.OpenEncoding = null;
            var actual = plugin.Perform(
                new ActionParameter(TestFile2));
            Assert.Equal(expected, actual);
        }




    }
}