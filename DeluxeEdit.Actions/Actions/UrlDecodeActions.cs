using DeluxeEdit.Actions.Types;
using DeluxeEdit.Actions.Types.Interfaces;
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
    public class UrlDecodeAction : INamedAction
    {
               public string Name { get; set; }
        public string Titel { get; set; }
        public List<Char> ShortCutCommand { get; set; }
        public string Parameter { get; set; }
        public string Result { get; set; }

        public Func<string, string> Action { get; set; }

 


        public string Perform(string parameter)
        {
            var result = WebUtility.UrlDecode(parameter);
            return result;
        }

    }


}
