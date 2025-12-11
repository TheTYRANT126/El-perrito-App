using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ElPerrito.Data.Entities;

[Table("pago")]
[Index("IdVenta", Name = "fk_pago_venta")]
[Index("Estado", Name = "idx_pago_estado")]
[MySqlCollation("utf8mb4_unicode_ci")]
public partial class Pago
{
    [Key]
    [Column("id_pago", TypeName = "int(11)")]
    public int IdPago { get; set; }

    [Column("id_venta", TypeName = "int(11)")]
    public int IdVenta { get; set; }

    [Column("metodo_pago", TypeName = "enum('tarjeta_credito','tarjeta_debito','transferencia','paypal','mercado_pago','oxxo','efectivo')")]
    public string MetodoPago { get; set; } = null!;

    [Column("monto")]
    [Precision(10, 2)]
    public decimal Monto { get; set; }

    /// <summary>
    /// ID de transacción o referencia bancaria
    /// </summary>
    [Column("referencia")]
    [StringLength(100)]
    public string? Referencia { get; set; }

    /// <summary>
    /// Últimos 4 dígitos de tarjeta
    /// </summary>
    [Column("ultimos_digitos")]
    [StringLength(4)]
    public string? UltimosDigitos { get; set; }

    [Column("estado", TypeName = "enum('pendiente','completado','rechazado','reembolsado','cancelado')")]
    public string Estado { get; set; } = null!;

    [Column("fecha_pago", TypeName = "datetime")]
    public DateTime? FechaPago { get; set; }

    [Column("fecha_creacion", TypeName = "datetime")]
    public DateTime FechaCreacion { get; set; }

    [Column("notas", TypeName = "text")]
    public string? Notas { get; set; }

    [ForeignKey("IdVenta")]
    [InverseProperty("Pagos")]
    public virtual Ventum IdVentaNavigation { get; set; } = null!;
}
