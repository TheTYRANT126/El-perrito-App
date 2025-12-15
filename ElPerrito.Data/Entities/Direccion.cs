using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ElPerrito.Data.Entities;

[Table("direccion")]
[Index("IdCliente", Name = "fk_dir_cliente")]
[MySqlCollation("utf8mb4_unicode_ci")]
public partial class Direccion
{
    [Key]
    [Column("id_direccion", TypeName = "int(11)")]
    public int IdDireccion { get; set; }

    [Column("id_cliente", TypeName = "int(11)")]
    public int IdCliente { get; set; }

    [Column("alias")]
    [StringLength(50)]
    public string? Alias { get; set; }

    [Column("calle")]
    [StringLength(255)]
    public string Calle { get; set; } = null!;

    [Column("numero_exterior")]
    [StringLength(20)]
    public string NumeroExterior { get; set; } = null!;

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

    [Column("es_facturacion")]
    public bool EsFacturacion { get; set; }

    [Required]
    [Column("activa")]
    public bool? Activa { get; set; }

    [Column("fecha_creacion", TypeName = "datetime")]
    public DateTime FechaCreacion { get; set; }

    [InverseProperty("IdDireccionNavigation")]
    public virtual ICollection<Envio> Envios { get; set; } = new List<Envio>();

    [ForeignKey("IdCliente")]
    [InverseProperty("Direccions")]
    public virtual Cliente IdClienteNavigation { get; set; } = null!;
}
