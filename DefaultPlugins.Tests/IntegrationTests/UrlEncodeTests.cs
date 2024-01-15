using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using DefaultPlugins;
using DefaultPlugins.Misc;
using Model;

namespace DeluxeEdit.DefaultPlugins.Tests.IntegrationTests
{
    public class UrlEncodeTests
    {
        [Fact]
        public void UrlEncodeTest()
        {
            var plugin = AllPlugins.InvokePlugin(PluginType.UrlEncode) as UrlEncodePlugin;
            var expected = "Hej+p%c3%a5+dig";
            var actual = plugin.Perform(
                new ActionParameter("Hej på dig"));
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void UrlEncodeTestSimple()
        {
            var plugin = AllPlugins.InvokePlugin(PluginType.UrlEncode) as UrlEncodePlugin;
            var expected = "Ninja";
            var actual = plugin.Perform(
                new ActionParameter("Ninja"));
            Assert.Equal(expected, actual);
        }

    }
}
    