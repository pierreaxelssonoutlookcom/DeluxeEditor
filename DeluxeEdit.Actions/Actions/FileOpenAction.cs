using DeluxeEdit.Actions.Types;
using DeluxeEdit.Shared;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace DeluxeEdit.Actions.Actions
{
    public class FileOpenAction : NamedActionBase
    {
        private StreamReader reader;

        public bool CanReadMore()
        {
            var result = false;
            if (reader != null) result = reader.BaseStream.CanRead;
            return result;
        }
        public bool AsReaOnly { get; set; }
        public Encoding OpenEncoding { get; set; }
        public string Name { get; set; }
        public string Titel { get; set; }
        public List<char> HotKeys { get; set; }
        public int SortOrdder { get; set; }
        public string Parameter { get; set; }
        public string Result { get; set; }
        public Func<string, string> Action { get; set; }
        public FileOpenAction()
        {

        }

        public string Perform(string parameter)
        {
            var result = ReadPortion(parameter);
            return result;
        }
        public string ReadPortion(string parameter)
        {
            reader = reader ?? new StreamReader(new FileStream(Parameter, FileMode.Open));

            var buffy = new char[SystemConstants.FileBufferSize];
            reader.ReadBlock(buffy);
            var result = buffy.ToString();
            return result;
        }

    }


}
