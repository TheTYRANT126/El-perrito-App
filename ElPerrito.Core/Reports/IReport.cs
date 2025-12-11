using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElPerrito.Core.Reports
{
    /// <summary>
    /// Interfaz para reportes
    /// </summary>
    public interface IReport
    {
        Task<byte[]> GenerateAsync<T>(List<T> data, string title);
        string GetFileExtension();
        string GetMimeType();
    }
}
