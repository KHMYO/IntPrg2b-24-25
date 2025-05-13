using System;
using System.Collections.Generic;

namespace CookSite.Models;

public partial class Ingredient
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Amount { get; set; } = null!;

    public int AmountUnitId { get; set; }

    public int RecipeId { get; set; }

    public virtual Unit? AmountUnit { get; set; } = null!;

    public virtual Recipe? Recipe { get; set; } = null!;
}
