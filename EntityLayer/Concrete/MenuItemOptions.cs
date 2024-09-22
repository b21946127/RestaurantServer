public class MenuItemOption
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } // Name of the ingredient or option

    public bool IsAllergen { get; set; } // Flag to identify allergens

    public bool IsOptional { get; set; } // Indicates if this ingredient is optional or customizable

    public bool IsQuantityVariable { get; set; } // Can the quantity vary?

    public int? FixedQuantity { get; set; } // If quantity is fixed, set the value here

    public MenuItem MenuItem { get; set; } // Foreign key to MenuItem
}