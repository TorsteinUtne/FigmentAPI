namespace PowerService.Data.Models
{
    public class ConfigurationItem: OrganizationModel
    {
        public string TableName { get; set; }
        public bool IsActivated { get; set; }
    }
}