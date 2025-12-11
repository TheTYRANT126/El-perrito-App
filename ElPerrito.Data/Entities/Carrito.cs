using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ElPerrito.Data.Entities;

[Table("carrito")]
[Index("IdCliente", Name = "fk_cart_cliente")]
[MySqlCollation("utf8mb4_unicode_ci")]
public partial class Carrito
{
    [Key]
    [Column("id_carrito", TypeName = "int(11)")]
    public int IdCarrito { get; set; }

    [Column("id_cliente", TypeName = "int(11)")]
    public int IdCliente { get; set; }

    [Column("estado", TypeName = "enum('activo','cerrado')")]
    public string Estado { get; set; } = null!;

    [Column("fecha_creacion", TypeName = "datetime")]
    public DateTime FechaCreacion { get; set; }

    [InverseProperty("IdCarritoNavigation")]
    public virtual ICollection<DetalleCarrito> DetalleCarritos { get; set; } = new List<DetalleCarrito>();

    [ForeignKey("IdCliente")]
    [InverseProperty("Carritos")]
    public virtual Cliente IdClienteNavigation { get; set; } = null!;
}
