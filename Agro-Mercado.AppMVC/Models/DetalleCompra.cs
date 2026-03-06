using System;
using System.Collections.Generic;

namespace Agro_Mercado.AppMVC.Models;

public partial class DetalleCompra
{
    public int Id { get; set; }

    public int CompraId { get; set; }

    public int ProductoId { get; set; }

    public int Cantidad { get; set; }

    public decimal Precio { get; set; }

    public decimal? SubTotal { get; set; }

    public virtual Compra Compra { get; set; } = null!;

    public virtual Producto Producto { get; set; } = null!;
}
