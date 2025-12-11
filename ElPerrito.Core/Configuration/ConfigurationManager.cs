using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace ElPerrito.Core.Configuration
{
    /// <summary>
    /// Implementación del patrón Singleton para gestión de configuración
    /// Thread-safe usando Lazy initialization
    /// </summary>
    public sealed class ConfigurationManager
    {
        private static readonly Lazy<ConfigurationManager> _lazy =
            new Lazy<ConfigurationManager>(() => new ConfigurationManager());

        private readonly Dictionary<string, object> _settings;
        private readonly string _configFilePath;

        private ConfigurationManager()
        {
            _settings = new Dictionary<string, object>();
            _configFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json");
            LoadConfiguration();
        }

        public static ConfigurationManager Instance => _lazy.Value;

        private void LoadConfiguration()
        {
            try
            {
                if (File.Exists(_configFilePath))
                {
                    string json = File.ReadAllText(_configFilePath);
                    var config = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(json);

                    if (config != null)
                    {
                        foreach (var kvp in config)
                        {
                            _settings[kvp.Key] = kvp.Value;
                        }
                    }
                }
                else
                {
                    // Configuración por defecto
                    SetDefaultConfiguration();
                    SaveConfiguration();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar configuración: {ex.Message}");
                SetDefaultConfiguration();
            }
        }

        private void SetDefaultConfiguration()
        {
            _settings["ConnectionString"] = "server=127.0.0.1;database=elperrito;user=root;";
            _settings["ApplicationName"] = "El Perrito E-commerce";
            _settings["Version"] = "1.0.0";
            _settings["MaxImageSize"] = 5242880; // 5MB
            _settings["ItemsPerPage"] = 20;
            _settings["EnableLogging"] = true;
            _settings["EnableCaching"] = true;
            _settings["CacheDurationMinutes"] = 10;
            _settings["MinStockAlert"] = 5;
        }

        public T GetSetting<T>(string key, T defaultValue = default!)
        {
            if (_settings.TryGetValue(key, out var value))
            {
                try
                {
                    if (value is JsonElement jsonElement)
                    {
                        return JsonSerializer.Deserialize<T>(jsonElement.GetRawText()) ?? defaultValue;
                    }
                    return (T)Convert.ChangeType(value, typeof(T));
                }
                catch
                {
                    return defaultValue;
                }
            }
            return defaultValue;
        }

        public void SetSetting<T>(string key, T value)
        {
            _settings[key] = value!;
        }

        public void SaveConfiguration()
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                string json = JsonSerializer.Serialize(_settings, options);
                File.WriteAllText(_configFilePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar configuración: {ex.Message}");
            }
        }

        public string GetConnectionString()
        {
            return GetSetting("ConnectionString", "server=127.0.0.1;database=elperrito;user=root;");
        }

        public int GetItemsPerPage()
        {
            return GetSetting("ItemsPerPage", 20);
        }

        public bool IsLoggingEnabled()
        {
            return GetSetting("EnableLogging", true);
        }

        public bool IsCachingEnabled()
        {
            return GetSetting("EnableCaching", true);
        }

        public int GetCacheDurationMinutes()
        {
            return GetSetting("CacheDurationMinutes", 10);
        }

        public int GetMinStockAlert()
        {
            return GetSetting("MinStockAlert", 5);
        }

        public int GetMaxImageSize()
        {
            return GetSetting("MaxImageSize", 5242880);
        }
    }
}
