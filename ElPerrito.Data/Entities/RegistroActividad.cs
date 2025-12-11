using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ElPerrito.Data.Entities;

[Table("registro_actividad")]
[Index("IdUsuario", Name = "fk_registro_usuario")]
[Index("FechaAccion", Name = "idx_fecha_accion")]
[Index("TablaAfectada", Name = "idx_tabla_afectada")]
[MySqlCollation("utf8mb4_unicode_ci")]
public partial class RegistroActividad
{
    [Key]
    [Column("id_registro", TypeName = "int(11)")]
    public int IdRegistro { get; set; }

    [Column("id_usuario", TypeName = "int(11)")]
    public int IdUsuario { get; set; }

    [Column("tipo_accion")]
    [StringLength(50)]
    public string TipoAccion { get; set; } = null!;

    [Column("tabla_afectada")]
    [StringLength(50)]
    public string TablaAfectada { get; set; } = null!;

    [Column("id_registro_afectado", TypeName = "int(11)")]
    public int? IdRegistroAfectado { get; set; }

    [Column("descripcion", TypeName = "text")]
    public string Descripcion { get; set; } = null!;

    [Column("fecha_accion", TypeName = "datetime")]
    public DateTime FechaAccion { get; set; }

    [ForeignKey("IdUsuario")]
    [InverseProperty("RegistroActividads")]
    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
