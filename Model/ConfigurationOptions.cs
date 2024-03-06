namespace Model
{
    public class ConfigurationOptions
    {

        public CommandKeys KeyCommand { get; set; }
        public string ShowInMenu { get; set; } = "";
        public string ShowInMenuItem { get; set; } = "";

        public int? SortOrder { get; set; }
        public ConfigurationOptions()
        {
            KeyCommand = new CommandKeys();
        }
     }
}
