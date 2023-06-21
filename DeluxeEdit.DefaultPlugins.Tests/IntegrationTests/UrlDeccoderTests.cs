using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeluxeEdit.DefaultPlugins;
using DeluxeEdit.Model;

namespace DeluxeEdit.DefaultPlugins.Tests.IntegrationTests
{
    public class UrlDecoderTests
    {
        [Fact]
        public void UrlDecodeTest()
        {
            var plugin = new UrlDecodePlugin();
            var expected = "Hej på dig";
            var actual = plugin.Perform(
                new ActionParameter("Hej+p%c3%a5+dig"));
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void UrlDecodeTestSimple()
        {
            var plugin = new UrlDecodePlugin();
            var expected = "Ninja";
            var actual = plugin.Perform(
                new ActionParameter("Ninja"));
            Assert.Equal(expected, actual);
        }


    }
}