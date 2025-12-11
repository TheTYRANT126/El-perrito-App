using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ElPerrito.Data.Entities;

[Table("venta")]
[Index("IdCliente", Name = "fk_venta_cliente")]
[Index("IdDireccionEnvio", Name = "fk_venta_direccion")]
[Index("EstadoEnvio", Name = "idx_venta_estado_envio")]
[MySqlCollation("utf8mb4_unicode_ci")]
public partial class Ventum
{
    [Key]
    [Column("id_venta", TypeName = "int(11)")]
    public int IdVenta { get; set; }

    [Column("id_cliente", TypeName = "int(11)")]
    public int IdCliente { get; set; }

    [Column("fecha", TypeName = "datetime")]
    public DateTime Fecha { get; set; }

    [Column("total")]
    [Precision(12, 2)]
    public decimal Total { get; set; }

    [Column("estado_pago", TypeName = "enum('pendiente','pagado','rechazado')")]
    public string EstadoPago { get; set; } = null!;

    [Column("estado_envio", TypeName = "enum('pendiente','preparacion','enviado','entregado','cancelado')")]
    public string EstadoEnvio { get; set; } = null!;

    [Column("direccion_envio")]
    [StringLength(255)]
    public string? DireccionEnvio { get; set; }

    [Column("id_direccion_envio", TypeName = "int(11)")]
    public int? IdDireccionEnvio { get; set; }

    [InverseProperty("IdVentaNavigation")]
    public virtual ICollection<DetalleVentum> DetalleVenta { get; set; } = new List<DetalleVentum>();

    [InverseProperty("IdVentaNavigation")]
    public virtual ICollection<Envio> Envios { get; set; } = new List<Envio>();

    [ForeignKey("IdCliente")]
    [InverseProperty("Venta")]
    public virtual Cliente IdClienteNavigation { get; set; } = null!;

    [InverseProperty("IdVentaNavigation")]
    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();
}
