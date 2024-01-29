using Extensions.Model;
using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Extensions
{
    public static class OtherExtensions

    {


        public static PluginFile ParseNugetFileName(this string path)
        {
            //DefaultPlugins.0.0.1.nupkg
            var result = new PluginFile();
            result.LocalPath = path;

            var index = path.IndexOfDigit();
            var lastIndex = path.LastIndexOfDigit();
            result.Name = Path.GetFileNameWithoutExtension(new FileInfo(path).Name);
            if (index.HasValue && index.Value > -1)
            {
                result.Version = Version.Parse(path.SubstringPos(index.Value, lastIndex.Value));
                result.Name= Path.GetFileNameWithoutExtension( path.SubstringPos(0, index.Value-2));
                ;
            };                                                                                                         

            return result;
        }        
        /// <summary>
        /// Also returns bytes writtten
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="maxLines"></param>
        /// <returns></returns>
        public static ListBytes ReadLinesMax(this StreamReader reader, int maxLines)
        {
            long oldPos = reader.BaseStream.Position;

            var result = new ListBytes();
            int i = 0;
            string line = String.Empty;


            while ((line = reader.ReadLine()) != null && i < maxLines)
            {
                //were we manualy have to locate the NUL char 
                int index = line.IndexOf('\0');
                var resultLine = new String(line.ToCharArray(), 0, index);
                result.Items.Add(resultLine);
                i++;
            }
            result.Bytes=reader.BaseStream.Position - oldPos;
            return result;
        }

        /// <summary>
        /// Returns bytes written, removes lines written
        /// </summaryR
        /// S<param name="writer"></param>
        /// <param name="buffer"></param>
        /// <param name="maxLines"></param>
        /// <returns></returns>
        public static long WriteLinesMax(this StreamWriter writer, List<string> buffer, int maxLines)
        {
            long oldPos=writer.BaseStream.Position;
            var removals = new List<string>();

            foreach (var item in buffer.Take(maxLines).Select(p=>p.ToString()))
            {
                writer.WriteLine(item);
                removals.Add(item);
            }
                         
            removals.ForEach(p => buffer.Remove(p));
            
            return writer.BaseStream.Position - oldPos;
            

        }


    }
}
