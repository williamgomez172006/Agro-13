using System;
using System.Collections.Generic;

namespace Agro_Mercado.AppMVC.Models;

public partial class UnidadMedidum
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Abreviatura { get; set; } = null!;

    public string Tipo { get; set; } = null!;

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
