using System.Collections.Generic;
using System.Linq;
using ElPerrito.Domain.Patterns;

namespace ElPerrito.Domain.Models
{
    /// <summary>
    /// DTO de Configuración que implementa Prototype
    /// Útil para plantillas de configuración
    /// </summary>
    public class ConfiguracionDto : IPrototype<ConfiguracionDto>
    {
        public string Nombre { get; set; } = string.Empty;
        public Dictionary<string, object> Parametros { get; set; } = new();

        public ConfiguracionDto Clone()
        {
            return (ConfiguracionDto)this.MemberwiseClone();
        }

        public ConfiguracionDto DeepClone()
        {
            return new ConfiguracionDto
            {
                Nombre = string.Copy(this.Nombre),
                Parametros = new Dictionary<string, object>(this.Parametros.Select(kvp =>
                    new KeyValuePair<string, object>(kvp.Key, kvp.Value)))
            };
        }
    }
}
