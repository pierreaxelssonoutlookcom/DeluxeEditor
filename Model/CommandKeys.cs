using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class CommandKeys
    {
        public List<Key> KeyCommand { get; set; }

        public CommandKeys() 
        {
            KeyCommand = new List<Key>();
        }
        public string KeysToString()
       {
           var result= String.Join("+", KeyCommand.Select(p => p.ToString()));
            return result;
        }
    }
}
