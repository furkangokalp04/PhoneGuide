namespace PhoneGuide.Reports.Utilities.DbConfigurationSettings
{

    public class DatabaseSettings : IDatabaseSettings
    {
        public string ContactCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
