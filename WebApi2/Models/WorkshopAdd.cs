namespace WorkshopWebApi.Models
{
    public class WorkshopAdd
    {
        public string NameWorkshop { get; set; } = string.Empty;

        public string GroupWorkshop { get; set; } = string.Empty;

        public int EquipmentId { get; set; }
    }
}
