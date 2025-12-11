using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ElPerrito.Data.Entities;

[Table("inventario")]
[MySqlCollation("utf8mb4_unicode_ci")]
public partial class Inventario
{
    [Key]
    [Column("id_producto", TypeName = "int(11)")]
    public int IdProducto { get; set; }

    [Column("stock", TypeName = "int(11)")]
    public int Stock { get; set; }

    [Column("stock_minimo", TypeName = "int(11)")]
    public int StockMinimo { get; set; }

    [ForeignKey("IdProducto")]
    [InverseProperty("Inventario")]
    public virtual Producto IdProductoNavigation { get; set; } = null!;
}
