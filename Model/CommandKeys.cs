using System.Windows.Input;

namespace Model
{
    public class CommandKeys
    {
        public List<Key> Keys { get; set; }

        public CommandKeys() 
        {
            Keys = new List<Key>();
        }
        public override string ToString()
        {
           var result= String.Join("+", Keys.Select(p => p.ToString()));
            return result;
        }
    }
}
