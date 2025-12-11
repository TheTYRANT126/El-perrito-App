using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ElPerrito.Data.Entities;

[Table("cliente")]
[Index("Email", Name = "email", IsUnique = true)]
[MySqlCollation("utf8mb4_unicode_ci")]
public partial class Cliente
{
    [Key]
    [Column("id_cliente", TypeName = "int(11)")]
    public int IdCliente { get; set; }

    [Column("nombre")]
    [StringLength(80)]
    public string Nombre { get; set; } = null!;

    [Column("apellido")]
    [StringLength(80)]
    public string Apellido { get; set; } = null!;

    [Column("email")]
    [StringLength(160)]
    public string Email { get; set; } = null!;

    [Column("telefono")]
    [StringLength(40)]
    public string? Telefono { get; set; }

    [Column("direccion")]
    [StringLength(255)]
    public string? Direccion { get; set; }

    [Column("password_hash")]
    [StringLength(255)]
    public string PasswordHash { get; set; } = null!;

    [Column("fecha_registro", TypeName = "datetime")]
    public DateTime FechaRegistro { get; set; }

    [Column("estado", TypeName = "enum('activo','inactivo')")]
    public string Estado { get; set; } = null!;

    [InverseProperty("IdClienteNavigation")]
    public virtual ICollection<Carrito> Carritos { get; set; } = new List<Carrito>();

    [InverseProperty("IdClienteNavigation")]
    public virtual ICollection<DireccionEnvio> DireccionEnvios { get; set; } = new List<DireccionEnvio>();

    [InverseProperty("IdClienteNavigation")]
    public virtual ICollection<Direccion> Direccions { get; set; } = new List<Direccion>();

    [InverseProperty("IdClienteNavigation")]
    public virtual ICollection<Ventum> Venta { get; set; } = new List<Ventum>();
}
