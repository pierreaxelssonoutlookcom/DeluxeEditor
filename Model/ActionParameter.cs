namespace Model
 {
    public class ActionParameter
    {
        public string Parameter { get; set; } = "";
        public string InData { get; set; } = "";
        public ActionParameter()
        {
            Parameter = "";
        }
        public ActionParameter(string parameter,string indata="")
        {
            Parameter = parameter;
            InData = indata;
        }
    }
}
