using EntityLayer.Concrete;
using System.ComponentModel.DataAnnotations;

public class MenuItemOption
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; }

    public string Color { get; set; }

    public MenuItem MenuItem { get; set; }
}