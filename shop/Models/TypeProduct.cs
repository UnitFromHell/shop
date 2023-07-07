using System;
using System.Collections.Generic;

namespace shop.Models;

public partial class TypeProduct
{
    public int IdTypeProduct { get; set; }

    public string NameType { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
