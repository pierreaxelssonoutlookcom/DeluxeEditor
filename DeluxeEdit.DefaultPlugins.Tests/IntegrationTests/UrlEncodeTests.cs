using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeluxeEdit.DefaultPlugins;
using DeluxeEdit.Model;

namespace DeluxeEdit.DefaultPlugins.Tests.IntegrationTests
{
    public class UrlEncodeTests
    {
        [Fact]
        public void UrlEncodeTest()
        {
            var plugin = new UrlEncodePlugin();
            var expected = "Hej%20p%C3%A5%20dig";
            var actual = plugin.Perform(
                new ActionParameter("Hej på dig"));
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void UrlEncodeTestSimple()
        {
            var plugin = new UrlEncodePlugin();
            var expected = "Ninja";
            var actual = plugin.Perform(
                new ActionParameter("Ninja"));
            Assert.Equal(expected, actual);
        }

    }
}
    