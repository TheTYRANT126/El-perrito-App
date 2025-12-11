using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ElPerrito.Data.Entities;

[Table("direccion_envio")]
[Index("IdCliente", Name = "fk_direccion_cliente")]
[Index("IdCliente", "EsPredeterminada", Name = "idx_direccion_predeterminada")]
[MySqlCollation("utf8mb4_unicode_ci")]
public partial class DireccionEnvio
{
    [Key]
    [Column("id_direccion", TypeName = "int(11)")]
    public int IdDireccion { get; set; }

    [Column("id_cliente", TypeName = "int(11)")]
    public int IdCliente { get; set; }

    [Column("nombre_completo")]
    [StringLength(160)]
    public string NombreCompleto { get; set; } = null!;

    [Column("telefono")]
    [StringLength(40)]
    public string Telefono { get; set; } = null!;

    [Column("calle")]
    [StringLength(255)]
    public string Calle { get; set; } = null!;

    [Column("numero_exterior")]
    [StringLength(20)]
    public string? NumeroExterior { get; set; }

    [Column("numero_interior")]
    [StringLength(20)]
    public string? NumeroInterior { get; set; }

    [Column("colonia")]
    [StringLength(100)]
    public string Colonia { get; set; } = null!;

    [Column("ciudad")]
    [StringLength(100)]
    public string Ciudad { get; set; } = null!;

    [Column("estado")]
    [StringLength(100)]
    public string Estado { get; set; } = null!;

    [Column("codigo_postal")]
    [StringLength(10)]
    public string CodigoPostal { get; set; } = null!;

    [Column("referencias", TypeName = "text")]
    public string? Referencias { get; set; }

    [Column("es_predeterminada")]
    public bool EsPredeterminada { get; set; }

    [Column("fecha_creacion", TypeName = "datetime")]
    public DateTime FechaCreacion { get; set; }

    [Column("fecha_modificacion", TypeName = "datetime")]
    public DateTime? FechaModificacion { get; set; }

    [ForeignKey("IdCliente")]
    [InverseProperty("DireccionEnvios")]
    public virtual Cliente IdClienteNavigation { get; set; } = null!;
}
