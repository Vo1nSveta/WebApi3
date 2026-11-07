using System.ComponentModel.DataAnnotations;

namespace WebApp1_T1.Data
{
    public class Workshop
    {
        [Key]
        public int Id { get; set; }
        
        public string workshop { get; set; } = string.Empty;

        public string equipment { get; set; } = string.Empty;
    }
}
