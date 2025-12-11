# El Perrito - Aplicación de Escritorio C#

Sistema de e-commerce de escritorio desarrollado en C# con .NET 8.0, implementando 20 patrones de diseño de manera profesional.

## Requisitos del Sistema

- .NET 8.0 SDK o superior
- Visual Studio 2022 (recomendado) o Visual Studio Code
- MySQL 5.7+ / MariaDB 10.3+
- Windows 10/11

## Estructura del Proyecto

```
Aplicacion/
├── ElPerrito.Core/              # Utilidades, Logging, Configuración, Notificaciones, Reportes
├── ElPerrito.Domain/            # Modelos, Builders, DTOs, Enums
├── ElPerrito.Data/              # Entity Framework, Repositorios, Context
├── ElPerrito.Business/          # Lógica de Negocio, Patrones de Diseño
└── ElPerrito.WPF/              # Interfaz de Usuario (WPF)
```

## Instalación y Configuración

### 1. Clonar el Repositorio

```bash
cd C:\xampp\htdocs\elperrito\Aplicacion
```

### 2. Restaurar Paquetes NuGet

```bash
dotnet restore
```

### 3. Configurar Base de Datos

Asegúrate de que MySQL/MariaDB esté corriendo y la base de datos `elperrito` esté importada:

```bash
C:\xampp\mysql\bin\mysql.exe -u root elperrito < C:\xampp\htdocs\elperrito\elperrito.sql
```

### 4. Configurar Cadena de Conexión

Edita el archivo `appsettings.json` que se generará en la primera ejecución, o configura directamente en:

**ElPerrito.Data/Context/ElPerritoContext.cs** (línea 54):

```csharp
optionsBuilder.UseMySql(
    "server=127.0.0.1;database=elperrito;user=root;password=TU_CONTRASEÑA",
    Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.32-mariadb")
);
```

### 5. Compilar el Proyecto

```bash
dotnet build
```

### 6. Ejecutar la Aplicación

```bash
cd ElPerrito.WPF
dotnet run
```

O desde Visual Studio: presiona F5 con el proyecto `ElPerrito.WPF` como proyecto de inicio.

## Patrones de Diseño Implementados

✅ **20 Patrones de Diseño** completamente implementados y documentados:

### Patrones Creacionales (5)
1. **Singleton** - Logger, ConfigurationManager
2. **Factory Method** - NotificationFactory
3. **Abstract Factory** - ReportFactory (PDF, Excel, CSV)
4. **Builder** - VentaBuilder, ProductoBuilder
5. **Prototype** - Clonación de DTOs

### Patrones Estructurales (7)
6. **Adapter** - StripePaymentAdapter
7. **Bridge** - Payment abstraction con diferentes procesadores
8. **Composite** - Categorías jerárquicas
9. **Decorator** - Descuentos y promociones dinámicas
10. **Facade** - VentaFacade para simplificar ventas
11. **Proxy** - ProductoServiceProxy con caché

### Patrones de Comportamiento (8)
12. **Command** - ActualizarStockCommand con deshacer
13. **Iterator** - ProductoIterator, ActiveProductoIterator
14. **Mediator** - VentaMediator para coordinar componentes
15. **Memento** - VentaMemento para guardar/restaurar estado
16. **Observer** - StockSubject para notificaciones de stock bajo
17. **State** - Estados de venta (Pendiente, Pagado, Enviado, etc.)
18. **Strategy** - Estrategias de descuento intercambiables
19. **Template Method** - ProcesadorOrdenBase
20. **Visitor** - ValidacionVisitor para validar entidades

Ver documentación completa en: **[PATRONES_DISEÑO_IMPLEMENTADOS.md](./PATRONES_DISEÑO_IMPLEMENTADOS.md)**

## Tecnologías Utilizadas

- **Framework:** .NET 8.0
- **Lenguaje:** C# 12
- **ORM:** Entity Framework Core 9.0 con Pomelo MySQL
- **UI:** WPF (Windows Presentation Foundation)
- **Caché:** Microsoft.Extensions.Caching.Memory
- **Seguridad:** BCrypt.Net-Next
- **Base de Datos:** MySQL 5.7+ / MariaDB 10.3+

## Características Principales

### Sistema de Logging
```csharp
var logger = Logger.Instance;
logger.LogInfo("Mensaje informativo");
logger.LogError("Error crítico", exception);
```

Los logs se guardan en: `Logs/log_YYYYMMDD.txt`

### Gestión de Configuración
```csharp
var config = ConfigurationManager.Instance;
string connString = config.GetConnectionString();
int itemsPerPage = config.GetItemsPerPage();
```

### Builder Pattern para Ventas
```csharp
var venta = new VentaBuilder()
    .SetCliente(clienteId)
    .SetDireccionEnvio("Calle 123")
    .AddDetalle(productoId, cantidad, precio)
    .Build();
```

### Notificaciones Multicanal
```csharp
var notificacion = NotificationFactory.CreateNotification(NotificationType.Email);
await notificacion.SendAsync("cliente@email.com", "Asunto", "Mensaje");
```

### Reportes en Múltiples Formatos
```csharp
var factory = ReportFactoryProvider.GetFactory(ReportFormat.Pdf);
var reporte = factory.CreateSalesReport();
byte[] pdfBytes = await reporte.GenerateAsync(datos, "Reporte de Ventas");
```

### Descuentos con Strategy Pattern
```csharp
IDescuentoStrategy descuento = new DescuentoPorcentajeStrategy(15);
decimal montoDescuento = descuento.CalcularDescuento(1000.00m);
```

### Observer para Stock Bajo
```csharp
var stockSubject = new StockSubject();
stockSubject.Attach(new EmailStockObserver());
stockSubject.CheckStock(productoId, nombreProducto, stockActual, stockMinimo);
```

## Arquitectura y Principios

### Clean Architecture
- **Separación de capas:** Core, Domain, Data, Business, Presentation
- **Inversión de dependencias:** Uso de interfaces y abstracciones
- **Inyección de dependencias:** Preparado para DI container

### SOLID Principles
- ✅ Single Responsibility Principle
- ✅ Open/Closed Principle
- ✅ Liskov Substitution Principle
- ✅ Interface Segregation Principle
- ✅ Dependency Inversion Principle

### Repository Pattern
Todos los accesos a datos utilizan el patrón Repository:
```csharp
public interface IRepository<T>
{
    Task<T?> GetByIdAsync(int id);
    Task<List<T>> GetAllAsync();
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);
}
```

### Unit of Work Pattern
Gestión transaccional coordinada:
```csharp
public interface IUnitOfWork
{
    IProductoRepository Productos { get; }
    IClienteRepository Clientes { get; }
    IVentaRepository Ventas { get; }
    Task<int> SaveChangesAsync();
}
```

## Ejemplos de Uso

### Crear una Venta Completa (usando Facade)
```csharp
var facade = new VentaFacade(ventaRepo, productoRepo, clienteRepo, stockSubject);

var items = new List<(int idProducto, int cantidad)>
{
    (1, 2), // Producto 1, cantidad 2
    (5, 1)  // Producto 5, cantidad 1
};

var (success, message, idVenta) = await facade.CrearVentaCompletaAsync(
    idCliente: 1,
    direccionEnvio: "Av. Principal 123, Col. Centro",
    items: items
);

if (success)
{
    Console.WriteLine($"Venta creada: ID {idVenta}");
}
```

### Procesar Estados de Venta (State Pattern)
```csharp
var ventaContext = new VentaContext(idVenta);

// Transiciones de estado
ventaContext.ProcesarPago();      // Pendiente -> Pagado
ventaContext.PrepararEnvio();     // Pagado -> En Preparación
ventaContext.Enviar();            // En Preparación -> Enviado
ventaContext.Entregar();          // Enviado -> Entregado

Console.WriteLine($"Estado actual: {ventaContext.ObtenerEstado()}");
```

### Aplicar Descuentos (Decorator Pattern)
```csharp
IProductoComponent producto = new ProductoBase("Croquetas Premium", 500.00m);

// Aplicar múltiples descuentos
producto = new DescuentoDecorator(producto, 10);  // 10% descuento
producto = new PromocionDecorator(producto, "Black Friday", 50.00m);

Console.WriteLine($"Precio final: ${producto.ObtenerPrecio()}");
Console.WriteLine($"Descripción: {producto.ObtenerDescripcion()}");
```

### Deshacer Operaciones (Command Pattern)
```csharp
var command = new ActualizarStockCommand(repository, productoId, 100);
var invoker = new CommandInvoker();

invoker.ExecuteCommand(command);  // Actualiza stock a 100

// ... después si hubo un error
invoker.UndoLastCommand();        // Revierte al valor anterior
```

## Solución de Problemas

### Error: "No se puede conectar a la base de datos"
- Verifica que MySQL/MariaDB esté corriendo
- Confirma la cadena de conexión en `ElPerritoContext.cs`
- Asegúrate de que la base de datos `elperrito` existe

### Error: "The type initializer for 'Logger' threw an exception"
- Verifica que la carpeta `Logs/` tenga permisos de escritura
- En Windows normalmente no hay problema, pero verifica permisos

### Error de compilación en ElPerrito.Business
- Asegúrate de que todos los proyectos estén referenciados correctamente
- Ejecuta `dotnet restore` nuevamente
- Limpia y recompila: `dotnet clean && dotnet build`

## Testing

Para ejecutar las pruebas unitarias (cuando se implementen):

```bash
dotnet test
```

## Contribución

Este proyecto fue desarrollado como parte del sistema El Perrito E-commerce.

### Estructura de Commits
- `feat:` Nueva funcionalidad
- `fix:` Corrección de bugs
- `refactor:` Refactorización de código
- `docs:` Documentación
- `test:` Pruebas

## Licencia

Proyecto académico - El Perrito E-commerce

## Contacto y Soporte

Para preguntas o soporte:
- Revisar documentación en `PATRONES_DISEÑO_IMPLEMENTADOS.md`
- Consultar comentarios en el código fuente
- Revisar CLAUDE.md en la raíz del proyecto

---

**Versión:** 1.0.0
**Fecha:** Diciembre 2025
**Framework:** .NET 8.0
# El-perrito-App
