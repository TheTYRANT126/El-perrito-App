using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ElPerrito.Data.Entities;

[Table("envio")]
[Index("IdDireccion", Name = "fk_envio_direccion")]
[Index("IdVenta", Name = "fk_envio_venta")]
[Index("EstadoEnvio", Name = "idx_envio_estado")]
[MySqlCollation("utf8mb4_unicode_ci")]
public partial class Envio
{
    [Key]
    [Column("id_envio", TypeName = "int(11)")]
    public int IdEnvio { get; set; }

    [Column("id_venta", TypeName = "int(11)")]
    public int IdVenta { get; set; }

    [Column("id_direccion", TypeName = "int(11)")]
    public int IdDireccion { get; set; }

    /// <summary>
    /// DHL, FedEx, Estafeta, etc.
    /// </summary>
    [Column("paqueteria")]
    [StringLength(50)]
    public string? Paqueteria { get; set; }

    [Column("numero_guia")]
    [StringLength(100)]
    public string? NumeroGuia { get; set; }

    [Column("costo_envio")]
    [Precision(10, 2)]
    public decimal CostoEnvio { get; set; }

    [Column("peso_kg")]
    [Precision(8, 2)]
    public decimal? PesoKg { get; set; }

    [Column("estado_envio", TypeName = "enum('pendiente','preparando','en_transito','en_reparto','entregado','devuelto','cancelado')")]
    public string EstadoEnvio { get; set; } = null!;

    [Column("fecha_envio", TypeName = "datetime")]
    public DateTime? FechaEnvio { get; set; }

    [Column("fecha_entrega_estimada")]
    public DateOnly? FechaEntregaEstimada { get; set; }

    [Column("fecha_entrega_real", TypeName = "datetime")]
    public DateTime? FechaEntregaReal { get; set; }

    [Column("notas", TypeName = "text")]
    public string? Notas { get; set; }

    [Column("receptor_nombre")]
    [StringLength(160)]
    public string? ReceptorNombre { get; set; }

    /// <summary>
    /// URL de imagen de firma
    /// </summary>
    [Column("receptor_firma")]
    [StringLength(255)]
    public string? ReceptorFirma { get; set; }

    [Column("fecha_creacion", TypeName = "datetime")]
    public DateTime FechaCreacion { get; set; }

    [ForeignKey("IdDireccion")]
    [InverseProperty("Envios")]
    public virtual Direccion IdDireccionNavigation { get; set; } = null!;

    [ForeignKey("IdVenta")]
    [InverseProperty("Envios")]
    public virtual Ventum IdVentaNavigation { get; set; } = null!;
}
