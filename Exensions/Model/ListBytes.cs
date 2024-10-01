using System;
using System.Collections.Generic;
using System.Text;

namespace Extensions.Model
{
    public class ListBytes
    {
        public List<string> Items {  get; set; }
        public long Bytes { get; set; }
        public ListBytes() 
        {
            Items = new List<string>();
        }
    }
}
