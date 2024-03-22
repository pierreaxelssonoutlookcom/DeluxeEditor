using System;

namespace Model
{
  public class ContentPath: IEquatable<ContentPath>
    {
        public string Path { get; set; }
        public string Content { get; set; }
        public string Header { get; set; }


        public bool Equals(ContentPath other)
        {
            bool result = other != null ? other.Path == Path && other.Content == Content && other.Header == Header : false;
            return result;
        }
    }
}
