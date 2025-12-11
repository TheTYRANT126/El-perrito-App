using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ElPerrito.Data.Entities;

[Table("historial_usuario")]
[Index("IdUsuarioModificador", Name = "fk_historial_modificador")]
[Index("IdUsuario", Name = "fk_historial_usuario")]
[MySqlCollation("utf8mb4_unicode_ci")]
public partial class HistorialUsuario
{
    [Key]
    [Column("id_historial", TypeName = "int(11)")]
    public int IdHistorial { get; set; }

    [Column("id_usuario", TypeName = "int(11)")]
    public int IdUsuario { get; set; }

    [Column("campo_modificado")]
    [StringLength(100)]
    public string CampoModificado { get; set; } = null!;

    [Column("valor_anterior", TypeName = "text")]
    public string? ValorAnterior { get; set; }

    [Column("valor_nuevo", TypeName = "text")]
    public string? ValorNuevo { get; set; }

    [Column("fecha_cambio", TypeName = "datetime")]
    public DateTime FechaCambio { get; set; }

    [Column("id_usuario_modificador", TypeName = "int(11)")]
    public int IdUsuarioModificador { get; set; }

    [ForeignKey("IdUsuarioModificador")]
    [InverseProperty("HistorialUsuarioIdUsuarioModificadorNavigations")]
    public virtual Usuario IdUsuarioModificadorNavigation { get; set; } = null!;

    [ForeignKey("IdUsuario")]
    [InverseProperty("HistorialUsuarioIdUsuarioNavigations")]
    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
