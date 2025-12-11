using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ElPerrito.Data.Entities;

[Table("producto_imagen")]
[Index("IdProducto", Name = "fk_pimg_producto")]
[MySqlCollation("utf8mb4_unicode_ci")]
public partial class ProductoImagen
{
    [Key]
    [Column("id_imagen", TypeName = "int(11)")]
    public int IdImagen { get; set; }

    [Column("id_producto", TypeName = "int(11)")]
    public int IdProducto { get; set; }

    [Column("url")]
    [StringLength(255)]
    public string Url { get; set; } = null!;

    [Column("prioridad", TypeName = "int(11)")]
    public int Prioridad { get; set; }

    [ForeignKey("IdProducto")]
    [InverseProperty("ProductoImagens")]
    public virtual Producto IdProductoNavigation { get; set; } = null!;
}
