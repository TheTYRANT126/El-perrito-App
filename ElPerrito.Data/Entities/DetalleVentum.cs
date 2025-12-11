using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ElPerrito.Data.Entities;

[Table("detalle_venta")]
[Index("IdProducto", Name = "fk_dventa_prod")]
[Index("IdVenta", Name = "fk_dventa_venta")]
[MySqlCollation("utf8mb4_unicode_ci")]
public partial class DetalleVentum
{
    [Key]
    [Column("id_detalle", TypeName = "int(11)")]
    public int IdDetalle { get; set; }

    [Column("id_venta", TypeName = "int(11)")]
    public int IdVenta { get; set; }

    [Column("id_producto", TypeName = "int(11)")]
    public int IdProducto { get; set; }

    [Column("cantidad", TypeName = "int(11)")]
    public int Cantidad { get; set; }

    [Column("precio_unitario")]
    [Precision(10, 2)]
    public decimal PrecioUnitario { get; set; }

    [ForeignKey("IdProducto")]
    [InverseProperty("DetalleVenta")]
    public virtual Producto IdProductoNavigation { get; set; } = null!;

    [ForeignKey("IdVenta")]
    [InverseProperty("DetalleVenta")]
    public virtual Ventum IdVentaNavigation { get; set; } = null!;
}
