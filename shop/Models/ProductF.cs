using Microsoft.AspNetCore.Mvc.Rendering;
using Nest;
using System;
using System.Collections.Generic;

namespace shop.Models;

public partial class ProductF
{
  public IEnumerable<Product> Products { get; set; }
  //public IEnumerable<Manufacture> Manufactures { get; set; }
  public SelectList CompanyName { get; set; }

  
}
