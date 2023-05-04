using DeluxeEdit.Actions.Types;
using DeluxeEdit.Shared;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;

namespace DeluxeEdit.Actions.Actions
{
    public class UrlDecodeAction : NamedActionBase
    {
        private StreamReader reader;

        public bool AsReaOnly { get; set; }

        public string Perform(string parameter)
        {
            var result = WebUtility.UrlDecode(parameter);
            return result;
        }

    }


}
