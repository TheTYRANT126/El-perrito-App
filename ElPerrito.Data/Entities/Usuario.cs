using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ElPerrito.Data.Entities;

[Table("usuario")]
[Index("Email", Name = "email", IsUnique = true)]
[Index("Rol", Name = "idx_usuario_rol")]
[MySqlCollation("utf8mb4_unicode_ci")]
public partial class Usuario
{
    [Key]
    [Column("id_usuario", TypeName = "int(11)")]
    public int IdUsuario { get; set; }

    [Column("nombre")]
    [StringLength(120)]
    public string Nombre { get; set; } = null!;

    [Column("apellido")]
    [StringLength(80)]
    public string? Apellido { get; set; }

    [Column("email")]
    [StringLength(160)]
    public string Email { get; set; } = null!;

    [Column("password_hash")]
    [StringLength(255)]
    public string PasswordHash { get; set; } = null!;

    [Column("curp")]
    [StringLength(18)]
    public string? Curp { get; set; }

    [Column("fecha_nacimiento")]
    public DateOnly? FechaNacimiento { get; set; }

    [Column("direccion")]
    [StringLength(255)]
    public string? Direccion { get; set; }

    [Column("telefono")]
    [StringLength(40)]
    public string? Telefono { get; set; }

    [Column("fecha_registro", TypeName = "datetime")]
    public DateTime FechaRegistro { get; set; }

    [Column("fecha_ultima_modificacion", TypeName = "datetime")]
    public DateTime? FechaUltimaModificacion { get; set; }

    [Column("rol", TypeName = "enum('admin','operador')")]
    public string Rol { get; set; } = null!;

    [Required]
    [Column("activo")]
    public bool? Activo { get; set; }

    [InverseProperty("IdUsuarioModificadorNavigation")]
    public virtual ICollection<HistorialUsuario> HistorialUsuarioIdUsuarioModificadorNavigations { get; set; } = new List<HistorialUsuario>();

    [InverseProperty("IdUsuarioNavigation")]
    public virtual ICollection<HistorialUsuario> HistorialUsuarioIdUsuarioNavigations { get; set; } = new List<HistorialUsuario>();

    [InverseProperty("IdUsuarioCreadorNavigation")]
    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();

    [InverseProperty("IdUsuarioNavigation")]
    public virtual ICollection<RegistroActividad> RegistroActividads { get; set; } = new List<RegistroActividad>();
}
