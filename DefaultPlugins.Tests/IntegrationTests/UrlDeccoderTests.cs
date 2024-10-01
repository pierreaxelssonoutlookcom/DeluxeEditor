using DefaultPlugins;
using Model;
using Xunit;


namespace DeluxeEdit.DefaultPlugins.Tests.IntegrationTests
{
    public class UrlDecoderTests
    {
        [Fact]
        public async void UrlDecodeTest()
        {
            var plugin = AllPlugins.InvokePlugin(PluginType.UrlDecode) as UrlDecodePlugin;
            var expected = "Hej på dig";
            var actual = await plugin.Perform(
                new ActionParameter("Hej+p%c3%a5+dig"), null);
            Assert.Equal(expected, actual);
        }
        [Fact]
        public async void UrlDecodeTestSimple()
        {
            var plugin = AllPlugins.InvokePlugin(PluginType.UrlDecode) as UrlDecodePlugin;
            var expected = "Ninja";
            var actual = await plugin.Perform(
                new ActionParameter("Ninja"), null);
            Assert.Equal(expected, actual);
        }


    }
}