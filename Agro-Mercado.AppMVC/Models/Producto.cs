using System;
using System.Collections.Generic;

namespace Agro_Mercado.AppMVC.Models;

public partial class Producto
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public int CategoriaId { get; set; }

    public int UnidadMedidaId { get; set; }

    public decimal PrecioVenta { get; set; }

    public int? Stock { get; set; }

    public bool? Activo { get; set; }

    public virtual ICollection<AjusteInventario> AjusteInventarios { get; set; } = new List<AjusteInventario>();

    public virtual Categoria Categoria { get; set; } = null!;

    public virtual ICollection<DetalleCompra> DetalleCompras { get; set; } = new List<DetalleCompra>();

    public virtual ICollection<DetalleVentum> DetalleVenta { get; set; } = new List<DetalleVentum>();

    public virtual UnidadMedidum UnidadMedida { get; set; } = null!;
}
