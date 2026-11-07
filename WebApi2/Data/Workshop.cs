using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace WorkshopWebApi.Data
{
    public class Workshop
    {
        [Key]
        public int Id { get; set; }
        
        public string NameWorkshop { get; set; } = string.Empty;

        public string GroupWorkshop { get; set; } = string.Empty;

        [ForeignKey(nameof(Equipment))]
        public int EquipmentId { get; set; }

        [ValidateNever]
        public Equipment Equipment { get; set; }
    }
}
