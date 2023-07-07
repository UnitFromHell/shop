using System;
using System.Collections.Generic;

namespace shop.Models;

public partial class Order
{
    public int IdOrder { get; set; }

    public string OrderNumber { get; set; } = null!;

    public int ProductId { get; set; }

    public int UserId { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
