using System;
using System.Collections.Generic;

namespace ZapatoDB.Models;

public partial class Zapato
{
    public int IdZapato { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public double? Precio { get; set; }

    public string? Imagen { get; set; }
}
