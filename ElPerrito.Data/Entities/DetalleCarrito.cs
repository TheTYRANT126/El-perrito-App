using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ElPerrito.Data.Entities;

[Table("detalle_carrito")]
[Index("IdCarrito", Name = "fk_dcart_cart")]
[Index("IdProducto", Name = "fk_dcart_prod")]
[MySqlCollation("utf8mb4_unicode_ci")]
public partial class DetalleCarrito
{
    [Key]
    [Column("id_item", TypeName = "int(11)")]
    public int IdItem { get; set; }

    [Column("id_carrito", TypeName = "int(11)")]
    public int IdCarrito { get; set; }

    [Column("id_producto", TypeName = "int(11)")]
    public int IdProducto { get; set; }

    [Column("cantidad", TypeName = "int(11)")]
    public int Cantidad { get; set; }

    [Column("precio_unitario")]
    [Precision(10, 2)]
    public decimal PrecioUnitario { get; set; }

    [ForeignKey("IdCarrito")]
    [InverseProperty("DetalleCarritos")]
    public virtual Carrito IdCarritoNavigation { get; set; } = null!;

    [ForeignKey("IdProducto")]
    [InverseProperty("DetalleCarritos")]
    public virtual Producto IdProductoNavigation { get; set; } = null!;
}
