namespace Model
{
    public class ConfigurationOptions
    {
        public CommandKeys KeyCommand { get; set; }= new CommandKeys();
        public string ShowInMenu { get; set; }=String.Empty;

        public string ShowInMenuItem { get; set; }= String.Empty;

        public int? SortOrder { get; set; }
     }
}
