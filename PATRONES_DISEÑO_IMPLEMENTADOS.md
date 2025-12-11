# Patrones de Diseño Implementados - El Perrito E-commerce

## Resumen Ejecutivo

Se han implementado **20 patrones de diseño** de manera profesional y funcional en la aplicación de escritorio C# para el sistema de e-commerce "El Perrito". Esta documentación detalla cada patrón implementado, su propósito, ubicación en el código y casos de uso.

---

## Arquitectura del Proyecto

```
Aplicacion/
├── ElPerrito.Core/           # Utilidades core, logging, configuración
├── ElPerrito.Domain/         # Modelos de dominio, builders, DTOs
├── ElPerrito.Data/           # Acceso a datos, repositorios, EF Core
├── ElPerrito.Business/       # Lógica de negocio, patrones
└── ElPerrito.WPF/           # Interfaz de usuario WPF
```

---

## Patrones Creacionales (5)

### 1. Singleton Pattern
**Ubicación:** `ElPerrito.Core/`

**Implementaciones:**
- `Logger.cs` - Sistema de logging thread-safe
- `ConfigurationManager.cs` - Gestor de configuración centralizado

**Propósito:** Garantizar una única instancia de clases críticas como el logger y la configuración.

**Uso:**
```csharp
var logger = Logger.Instance;
logger.LogInfo("Mensaje de log");

var config = ConfigurationManager.Instance;
string connectionString = config.GetConnectionString();
```

---

### 2. Factory Method Pattern
**Ubicación:** `ElPerrito.Core/Notifications/`

**Implementaciones:**
- `NotificationFactory.cs` - Crea diferentes tipos de notificaciones
- `EmailNotification.cs`, `SmsNotification.cs`, `PushNotification.cs`

**Propósito:** Crear objetos de notificación sin especificar su clase exacta.

**Uso:**
```csharp
INotification notificacion = NotificationFactory.CreateNotification(NotificationType.Email);
await notificacion.SendAsync("cliente@email.com", "Pedido confirmado", "Su pedido ha sido procesado");
```

---

### 3. Abstract Factory Pattern
**Ubicación:** `ElPerrito.Core/Reports/`

**Implementaciones:**
- `IReportFactory.cs` - Interfaz abstracta
- `PdfReportFactory.cs`, `ExcelReportFactory.cs`, `CsvReportFactory.cs` - Factories concretas
- Reportes para Ventas, Productos, Inventario, Clientes

**Propósito:** Crear familias de objetos relacionados (reportes en diferentes formatos).

**Uso:**
```csharp
IReportFactory factory = ReportFactoryProvider.GetFactory(ReportFormat.Pdf);
IReport salesReport = factory.CreateSalesReport();
byte[] pdfBytes = await salesReport.GenerateAsync(ventasData, "Reporte de Ventas");
```

---

### 4. Builder Pattern
**Ubicación:** `ElPerrito.Business/Builders/`

**Implementaciones:**
- `VentaBuilder.cs` - Constructor de ventas complejas
- `ProductoBuilder.cs` - Constructor de productos con inventario
- `BuilderDirector.cs` - Coordina construcciones predefinidas

**Propósito:** Construir objetos complejos paso a paso.

**Uso:**
```csharp
var ventaBuilder = new VentaBuilder();
var venta = ventaBuilder
    .SetCliente(clienteId)
    .SetDireccionEnvio("Calle 123")
    .AddDetalle(productoId, 2, 100.50m)
    .Build();
```

---

### 5. Prototype Pattern
**Ubicación:** `ElPerrito.Domain/`

**Implementaciones:**
- `IPrototype.cs` - Interfaz del patrón
- `ProductoDto.cs` - DTO clonable
- `ConfiguracionDto.cs` - Configuración clonable

**Propósito:** Clonar objetos existentes sin depender de sus clases concretas.

**Uso:**
```csharp
ProductoDto productoOriginal = ObtenerProducto();
ProductoDto productoClonado = productoOriginal.DeepClone();
productoClonado.Nombre = "Copia de " + productoOriginal.Nombre;
```

---

## Patrones Estructurales (7)

### 6. Adapter Pattern
**Ubicación:** `ElPerrito.Business/Patterns/Adapter/`

**Implementaciones:**
- `IPaymentGateway.cs` - Interfaz común
- `StripePaymentAdapter.cs` - Adapta Stripe a la interfaz común
- `StripePaymentService.cs` - Servicio externo con su propia API

**Propósito:** Adaptar interfaces incompatibles de servicios externos.

**Uso:**
```csharp
IPaymentGateway gateway = new StripePaymentAdapter();
var (success, txId) = await gateway.ProcessPaymentAsync(150.00m, "4242424242424242", "123");
```

---

### 7. Bridge Pattern
**Ubicación:** `ElPerrito.Business/Patterns/Bridge/`

**Implementaciones:**
- `IPaymentProcessor.cs` - Implementación
- `Payment.cs` - Abstracción
- `CreditCardProcessor.cs`, `PayPalProcessor.cs` - Implementaciones concretas
- `OnlinePayment.cs`, `RecurringPayment.cs` - Abstracciones refinadas

**Propósito:** Separar abstracción (tipo de pago) de implementación (procesador).

**Uso:**
```csharp
IPaymentProcessor processor = new PayPalProcessor();
Payment payment = new OnlinePayment(processor);
bool success = await payment.Pay(200.00m);
```

---

### 8. Composite Pattern
**Ubicación:** `ElPerrito.Business/Patterns/Composite/`

**Implementaciones:**
- `IMenuComponent.cs` - Componente base
- `CategoriaLeaf.cs` - Hoja (categoría sin hijos)
- `CategoriaComposite.cs` - Composite (categoría con subcategorías)

**Propósito:** Tratar objetos individuales y composiciones de manera uniforme.

**Uso:**
```csharp
var mascotas = new CategoriaComposite(1, "Mascotas");
var perros = new CategoriaComposite(2, "Perros");
perros.Agregar(new CategoriaLeaf(3, "Alimento Perros", "Comida para perros"));
perros.Agregar(new CategoriaLeaf(4, "Juguetes Perros", "Juguetes"));
mascotas.Agregar(perros);
mascotas.MostrarInformacion();
```

---

### 9. Decorator Pattern
**Ubicación:** `ElPerrito.Business/Patterns/Decorator/`

**Implementaciones:**
- `IProductoComponent.cs` - Componente base
- `ProductoDecorator.cs` - Decorador abstracto
- `DescuentoDecorator.cs` - Añade descuento porcentual
- `PromocionDecorator.cs` - Añade promoción con descuento fijo

**Propósito:** Añadir funcionalidades dinámicamente (descuentos, promociones).

**Uso:**
```csharp
IProductoComponent producto = new ProductoBase("Croquetas", 500.00m);
producto = new DescuentoDecorator(producto, 10); // 10% descuento
producto = new PromocionDecorator(producto, "Black Friday", 50.00m);
decimal precioFinal = producto.ObtenerPrecio(); // 400.00
```

---

### 10. Facade Pattern
**Ubicación:** `ElPerrito.Business/Patterns/Facade/`

**Implementaciones:**
- `VentaFacade.cs` - Simplifica proceso completo de venta

**Propósito:** Simplificar subsistemas complejos con una interfaz unificada.

**Uso:**
```csharp
var facade = new VentaFacade(ventaRepo, productoRepo, clienteRepo, stockSubject);
var items = new List<(int, int)> { (productoId1, 2), (productoId2, 1) };
var (success, message, idVenta) = await facade.CrearVentaCompletaAsync(
    clienteId,
    "Av. Principal 123",
    items
);
```

---

### 11. Proxy Pattern
**Ubicación:** `ElPerrito.Business/Patterns/Proxy/`

**Implementaciones:**
- `IProductoService.cs` - Interfaz del servicio
- `ProductoServiceReal.cs` - Servicio real
- `ProductoServiceProxy.cs` - Proxy con caché y logging

**Propósito:** Controlar acceso, añadir caché, lazy loading.

**Uso:**
```csharp
IProductoService service = new ProductoServiceProxy(
    new ProductoServiceReal(repository),
    memoryCache
);
var producto = await service.ObtenerProductoAsync(5); // Se cachea automáticamente
```

---

## Patrones de Comportamiento (8)

### 12. Command Pattern
**Ubicación:** `ElPerrito.Business/Patterns/Command/`

**Implementaciones:**
- `ICommand.cs` - Interfaz de comando
- `ActualizarStockCommand.cs` - Comando con deshacer
- `CommandInvoker.cs` - Ejecutor con historial

**Propósito:** Encapsular acciones como objetos, permitir deshacer/rehacer.

**Uso:**
```csharp
var command = new ActualizarStockCommand(repository, productoId, 100);
var invoker = new CommandInvoker();
invoker.ExecuteCommand(command);
// ... más tarde
invoker.UndoLastCommand(); // Revierte el cambio
```

---

### 13. Iterator Pattern
**Ubicación:** `ElPerrito.Business/Patterns/Iterator/`

**Implementaciones:**
- `IIterator.cs` - Interfaz del iterador
- `ProductoIterator.cs` - Itera todos los productos
- `ActiveProductoIterator.cs` - Itera solo productos activos

**Propósito:** Recorrer colecciones sin exponer su representación interna.

**Uso:**
```csharp
IIterator<Producto> iterator = new ActiveProductoIterator(productos);
while (iterator.HasNext())
{
    var producto = iterator.Next();
    Console.WriteLine(producto.Nombre);
}
```

---

### 14. Mediator Pattern
**Ubicación:** `ElPerrito.Business/Patterns/Mediator/`

**Implementaciones:**
- `IMediator.cs` - Interfaz del mediador
- `VentaMediator.cs` - Coordina componentes de venta
- `InventarioComponent.cs`, `PagoComponent.cs`, `NotificacionComponent.cs` - Componentes

**Propósito:** Centralizar comunicación entre objetos.

**Uso:**
```csharp
var mediator = new VentaMediator(inventario, pago, notificacion);
pago.ProcesarPago(ventaData); // Mediator coordina actualización de inventario y notificación
```

---

### 15. Memento Pattern
**Ubicación:** `ElPerrito.Business/Patterns/Memento/`

**Implementaciones:**
- `VentaMemento.cs` - Snapshot del estado
- `VentaOriginator.cs` - Crea y restaura mementos
- `VentaCaretaker.cs` - Gestiona historial de mementos

**Propósito:** Guardar y restaurar el estado de un objeto.

**Uso:**
```csharp
var originator = new VentaOriginator { IdVenta = 1, EstadoPago = "pagado" };
var caretaker = new VentaCaretaker();
caretaker.SaveState(originator.CreateMemento());

originator.EstadoPago = "rechazado";
var memento = caretaker.RestoreLastState();
originator.RestoreMemento(memento!); // Vuelve a "pagado"
```

---

### 16. Observer Pattern
**Ubicación:** `ElPerrito.Business/Patterns/Observer/`

**Implementaciones:**
- `IObserver.cs`, `ISubject.cs` - Interfaces del patrón
- `StockSubject.cs` - Sujeto que notifica cambios de stock
- `EmailStockObserver.cs` - Observador que envía emails

**Propósito:** Notificar a múltiples objetos cuando cambia el estado.

**Uso:**
```csharp
var stockSubject = new StockSubject();
stockSubject.Attach(new EmailStockObserver());
stockSubject.CheckStock(productoId, "Croquetas", 3, 5); // Notifica a observadores
```

---

### 17. State Pattern
**Ubicación:** `ElPerrito.Business/Patterns/State/`

**Implementaciones:**
- `IEstadoVenta.cs` - Interfaz de estado
- `VentaContext.cs` - Contexto que cambia de estado
- `EstadoPendiente.cs`, `EstadoPagado.cs`, etc. - Estados concretos

**Propósito:** Cambiar comportamiento según el estado interno.

**Uso:**
```csharp
var venta = new VentaContext(idVenta);
venta.ProcesarPago(); // Cambia a EstadoPagado
venta.PrepararEnvio(); // Cambia a EstadoEnPreparacion
venta.Enviar(); // Cambia a EstadoEnviado
```

---

### 18. Strategy Pattern
**Ubicación:** `ElPerrito.Business/Patterns/Strategy/`

**Implementaciones:**
- `IDescuentoStrategy.cs` - Interfaz de estrategia
- `DescuentoPorcentajeStrategy.cs` - Descuento porcentual
- `DescuentoFijoStrategy.cs` - Descuento fijo
- `DescuentoPorVolumenStrategy.cs` - Descuento por volumen

**Propósito:** Intercambiar algoritmos dinámicamente.

**Uso:**
```csharp
IDescuentoStrategy estrategia = new DescuentoPorcentajeStrategy(15);
decimal descuento = estrategia.CalcularDescuento(1000.00m); // 150.00
```

---

### 19. Template Method Pattern
**Ubicación:** `ElPerrito.Business/Patterns/TemplateMethod/`

**Implementaciones:**
- `ProcesadorOrdenBase.cs` - Define esqueleto del algoritmo
- `ProcesadorOrdenEstandar.cs` - Implementación concreta

**Propósito:** Definir esqueleto de algoritmo, delegando pasos a subclases.

**Uso:**
```csharp
ProcesadorOrdenBase procesador = new ProcesadorOrdenEstandar();
procesador.ProcesarOrden(idVenta); // Ejecuta todos los pasos en orden
```

---

### 20. Visitor Pattern
**Ubicación:** `ElPerrito.Business/Patterns/Visitor/`

**Implementaciones:**
- `IVisitor.cs`, `IVisitable.cs` - Interfaces del patrón
- `ValidacionVisitor.cs` - Visitante que valida entidades

**Propósito:** Separar algoritmos de las estructuras sobre las que operan.

**Uso:**
```csharp
var visitor = new ValidacionVisitor();
visitor.Visit(producto);
visitor.Visit(cliente);
if (visitor.Errores.Count > 0)
{
    // Manejar errores de validación
}
```

---

## Integración de Patrones

### Ejemplo de Uso Combinado

```csharp
// 1. Singleton: Obtener configuración y logger
var logger = Logger.Instance;
var config = ConfigurationManager.Instance;

// 2. Builder: Construir una venta
var ventaBuilder = new VentaBuilder();

// 3. Strategy: Aplicar descuento
IDescuentoStrategy descuento = new DescuentoPorcentajeStrategy(10);

// 4. Observer: Configurar notificaciones de stock
var stockSubject = new StockSubject();
stockSubject.Attach(new EmailStockObserver());

// 5. Facade: Crear venta completa
var facade = new VentaFacade(ventaRepo, productoRepo, clienteRepo, stockSubject);
var resultado = await facade.CrearVentaCompletaAsync(clienteId, direccion, items);

// 6. State: Procesar estados de la venta
var ventaContext = new VentaContext(resultado.idVenta.Value);
ventaContext.ProcesarPago();
ventaContext.PrepararEnvio();

// 7. Factory Method: Enviar notificación
var notificacion = NotificationFactory.CreateNotification(NotificationType.Email);
await notificacion.SendAsync(email, "Pedido confirmado", mensaje);

// 8. Abstract Factory: Generar reporte
var reportFactory = ReportFactoryProvider.GetFactory(ReportFormat.Pdf);
var reporte = reportFactory.CreateSalesReport();
await reporte.GenerateAsync(datos, "Ventas del Mes");
```

---

## Beneficios de la Arquitectura

✅ **Mantenibilidad:** Código organizado y fácil de mantener
✅ **Extensibilidad:** Fácil agregar nuevas funcionalidades
✅ **Testabilidad:** Cada patrón es testeable independientemente
✅ **Reutilización:** Componentes reutilizables en diferentes contextos
✅ **Desacoplamiento:** Baja dependencia entre módulos
✅ **SOLID Principles:** Todos los patrones siguen principios SOLID

---

## Próximos Pasos

1. ✅ Todos los patrones implementados
2. 🔄 Integrar patrones en servicios de negocio
3. 🔄 Crear interfaz WPF con MVVM
4. 📝 Pruebas unitarias para cada patrón
5. 📊 Diagramas UML de cada patrón

---

## Conclusión

Se han implementado exitosamente **20 patrones de diseño** en el proyecto El Perrito, proporcionando una base sólida, escalable y mantenible para el sistema de e-commerce. Cada patrón resuelve problemas específicos y juntos crean una arquitectura robusta y profesional.

**Fecha de implementación:** Diciembre 2025
**Versión:** 1.0.0
**Framework:** .NET 8.0
**Lenguaje:** C# 12
