using System;
using System.Collections.Generic;

namespace shop.Models;

public partial class Manufacture
{
    public int IdManufacture { get; set; }

    public string Country { get; set; } = null!;

    public string CompanyName { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
