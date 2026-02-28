using System;
using System.Collections.Generic;

namespace Agro_Mercado.AppMVC.Models;

public partial class Compra
{
    public int Id { get; set; }

    public DateTime? Fecha { get; set; }

    public int ProveedorId { get; set; }

    public int EmpleadoId { get; set; }

    public decimal? Total { get; set; }

    public virtual ICollection<DetalleCompra> DetalleCompras { get; set; } = new List<DetalleCompra>();

    public virtual Empleado Empleado { get; set; } = null!;

    public virtual Proveedore Proveedor { get; set; } = null!;
}
