using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ElPerrito.Data.Entities;

[Table("categoria")]
[Index("Nombre", Name = "nombre", IsUnique = true)]
[MySqlCollation("utf8mb4_unicode_ci")]
public partial class Categorium
{
    [Key]
    [Column("id_categoria", TypeName = "int(11)")]
    public int IdCategoria { get; set; }

    [Column("nombre")]
    [StringLength(120)]
    public string Nombre { get; set; } = null!;

    [Column("descripcion")]
    [StringLength(255)]
    public string? Descripcion { get; set; }

    [Required]
    [Column("activa")]
    public bool? Activa { get; set; }

    [InverseProperty("IdCategoriaNavigation")]
    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
