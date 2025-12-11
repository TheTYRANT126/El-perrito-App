using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ElPerrito.Data.Entities;

[Table("producto")]
[Index("IdCategoria", Name = "fk_prod_cat")]
[Index("Activo", Name = "idx_producto_activo")]
[Index("IdUsuarioCreador", Name = "idx_producto_creador")]
[MySqlCollation("utf8mb4_unicode_ci")]
public partial class Producto
{
    [Key]
    [Column("id_producto", TypeName = "int(11)")]
    public int IdProducto { get; set; }

    [Column("id_categoria", TypeName = "int(11)")]
    public int IdCategoria { get; set; }

    [Column("nombre")]
    [StringLength(180)]
    public string Nombre { get; set; } = null!;

    [Column("descripcion", TypeName = "text")]
    public string? Descripcion { get; set; }

    [Column("precio_venta")]
    [Precision(10, 2)]
    public decimal PrecioVenta { get; set; }

    [Column("imagen")]
    [StringLength(255)]
    public string? Imagen { get; set; }

    [Column("caducidad")]
    public DateOnly? Caducidad { get; set; }

    [Column("id_usuario_creador", TypeName = "int(11)")]
    public int? IdUsuarioCreador { get; set; }

    [Column("fecha_creacion", TypeName = "datetime")]
    public DateTime FechaCreacion { get; set; }

    [Column("fecha_modificacion", TypeName = "datetime")]
    public DateTime? FechaModificacion { get; set; }

    [Column("es_medicamento")]
    public bool EsMedicamento { get; set; }

    [Required]
    [Column("activo")]
    public bool? Activo { get; set; }

    [InverseProperty("IdProductoNavigation")]
    public virtual ICollection<DetalleCarrito> DetalleCarritos { get; set; } = new List<DetalleCarrito>();

    [InverseProperty("IdProductoNavigation")]
    public virtual ICollection<DetalleVentum> DetalleVenta { get; set; } = new List<DetalleVentum>();

    [ForeignKey("IdCategoria")]
    [InverseProperty("Productos")]
    public virtual Categorium IdCategoriaNavigation { get; set; } = null!;

    [ForeignKey("IdUsuarioCreador")]
    [InverseProperty("Productos")]
    public virtual Usuario? IdUsuarioCreadorNavigation { get; set; }

    [InverseProperty("IdProductoNavigation")]
    public virtual Inventario? Inventario { get; set; }

    [InverseProperty("IdProductoNavigation")]
    public virtual ICollection<ProductoImagen> ProductoImagens { get; set; } = new List<ProductoImagen>();
}
