using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkshopClientApp.Data
{
    public class Workshop
    {

        [Key]
        public int Id { get; set; }

        public string Email { get; set; }

        public string NameWorkshop { get; set; }
        public string GroupWorkshop { get; set; }

        public string Password { get; set; }

    }
}
