using System;
using System.Collections.Generic;

namespace CookSite.Models;

public partial class Recipe
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Perperation { get; set; } = null!;

    public int CookTypeId { get; set; }

    public string? CreateDate { get; set; }

    public virtual CookType? CookType { get; set; } = null!;

    public virtual ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();

    public virtual ICollection<Medium> Media { get; set; } = new List<Medium>();
}
