using System.ComponentModel.DataAnnotations;

namespace WorkshopWebApi.Data;

public class Equipment
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; }
}
