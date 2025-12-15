# Patrones de diseño aplicados


- **Singleton** → `ElPerrito.Core/Logging/Logger.cs` centraliza el logging thread-safe y `ElPerrito.Core/Configuration/ConfigurationManager.cs` mantiene la configuración de la app en una única instancia cargada desde `appsettings.json`.
- **Factory Method** → `ElPerrito.Core/Notifications/NotificationFactory.cs` expone `CreateNotification` para devolver la implementación de `INotification` (email, sms, push) según el tipo solicitado, encapsulando la lógica de construcción.
- **Abstract Factory** → `ElPerrito.Core/Reports/IReportFactory.cs` junto a `PdfReportFactory.cs`, `ExcelReportFactory.cs`, `CsvReportFactory.cs` generan familias completas de reportes (`IReport`) y `ReportFactoryProvider` selecciona la factoría según el formato.
- **Builder** → En `ElPerrito.Business/Builders` los builders de venta y producto (`VentaBuilder`, `ProductoBuilder`) permiten armar entidades paso a paso; `BuilderDirector` define recetas y es reutilizado, por ejemplo, desde la fachada de ventas.
- **Prototype** → `ElPerrito.Domain/Patterns/IPrototype.cs` implementado por `ProductoDto` y `ConfiguracionDto` para clonar superficial o profundamente DTOs evitando mutaciones compartidas.
- **Adapter** → `ElPerrito.Business/Patterns/Adapter/StripePaymentAdapter.cs` adapta `StripePaymentService` al contrato interno `IPaymentGateway`, convirtiendo montos y tokens para que el resto de la app no dependa de Stripe.
- **Bridge** → En `ElPerrito.Business/Patterns/Bridge` la jerarquía `Payment` (online/recurrente) se desacopla de los procesadores (`IPaymentProcessor`, `PayPalProcessor`, `CreditCardProcessor`), permitiendo combinar formas de pago con distintos providers.
- **Composite** → `ElPerrito.Business/Patterns/Composite` modela el árbol de categorías (`CategoriaComposite` con hijos `IMenuComponent` y hojas `CategoriaLeaf`) para recorrer y mostrar jerarquías de menús/productos.
- **Decorator** → `ElPerrito.Business/Patterns/Decorator` envuelve un `IProductoComponent` con capas como `DescuentoDecorator` o `PromocionDecorator` para añadir promociones sin tocar la clase base.
- **Facade** → `ElPerrito.Business/Patterns/Facade/VentaFacade.cs` orquesta la creación de ventas: valida cliente/productos, usa el `VentaBuilder`, persiste vía repositorios y dispara alertas de stock a través del sujeto de observadores.
- **Proxy** → `ElPerrito.Business/Patterns/Proxy/ProductoServiceProxy.cs` cachea llamadas de lectura de productos antes de delegar en `ProductoServiceReal`/repositorio, reduciendo hits a base de datos y registrando cache hits/misses.
- **Command** → `ElPerrito.Business/Patterns/Command` encapsula operaciones como `ActualizarStockCommand` con soporte de undo, y `CommandInvoker` mantiene el historial de comandos ejecutados.
- **Iterator** → `ElPerrito.Business/Patterns/Iterator/ProductoIterator.cs` y `ActiveProductoIterator` recorren listas de productos (todos o solo activos) sin exponer la colección subyacente.
- **Mediator** → `ElPerrito.Business/Patterns/Mediator/VentaMediator.cs` coordina componentes (inventario, pago, notificación) recibiendo eventos y delegando acciones sin acoplarlos entre sí.
- **Memento** → En `ElPerrito.Business/Patterns/Memento` el `VentaOriginator` crea `VentaMemento` con el estado de la venta, y `VentaCaretaker` guarda/restaura snapshots para deshacer cambios.
- **Observer** → `ElPerrito.Business/Patterns/Observer` implementa un sujeto de stock (`StockSubject`) que notifica observadores como `EmailStockObserver` cuando el inventario cae por debajo del mínimo.
- **State** → `ElPerrito.Business/Patterns/State` modela el ciclo de vida de la venta con estados (`EstadoPendiente`, `EstadoPagado`, etc.) que controlan transiciones válidas a través de `VentaContext`.
- **Strategy** → `ElPerrito.Business/Patterns/Strategy` define políticas de descuento intercambiables (`DescuentoPorcentajeStrategy`, `DescuentoFijoStrategy`, `DescuentoPorVolumenStrategy`) para calcular rebajas según el contexto.
- **Template Method** → `ElPerrito.Business/Patterns/TemplateMethod/ProcesadorOrdenBase.cs` fija la plantilla de pasos para procesar una orden y delega detalles a `ProcesadorOrdenEstandar`; `ElPerrito.Data/Repositories/Implementation/BaseRepository.cs` usa ganchos `Before*/After*` para especializar operaciones CRUD manteniendo el algoritmo general.
- **Visitor** → `ElPerrito.Business/Patterns/Visitor/ValidacionVisitor.cs` recorre entidades (`Producto`, `Cliente`, `Ventum`) aplicando reglas de validación agregadas en `Errores`, separando la lógica de validación del modelo.
