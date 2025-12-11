# Actualización: Login con Modo Invitado

## Cambios Realizados

Se ha actualizado el sistema de login para permitir acceso en modo invitado y agregar el logo de la aplicación.

### 1. Flujo de Inicio Modificado

**Antes**: La aplicación iniciaba directamente en la ventana de login.

**Ahora**: La aplicación inicia en la ventana principal (tienda) en modo invitado, con opción de iniciar sesión.

### 2. Archivos Modificados

#### App.xaml
- `StartupUri` cambiado de `Views/LoginView.xaml` a `MainWindow.xaml`
- Ahora la aplicación inicia mostrando la tienda directamente

#### MainViewModel.cs
- **Nuevas propiedades**:
  - `IsAuthenticated`: Verifica si hay sesión activa
  - `LoginCommand`: Comando para mostrar ventana de login
- **Modificaciones**:
  - `LogoutCommand`: Ahora tiene validación `CanExecute` basada en `IsAuthenticated`
  - `ShowLogin()`: Nuevo método que abre LoginView como diálogo modal
  - `Logout()`: Ya no cierra/abre ventanas, solo actualiza el estado y vuelve a la tienda
  - Actualización automática de propiedades al login/logout usando `OnPropertyChanged()`
- **Comportamiento inicial**:
  - Si NO está autenticado: Muestra tienda con mensaje "Bienvenido a El Perrito - Explora nuestros productos"
  - Si está autenticado: Muestra vista según tipo de usuario (Productos para admin, Tienda para cliente)

#### LoginViewModel.cs
- **Modificación en método Login**:
  - Detecta si se abrió como diálogo modal (tiene Owner)
  - Si es modal: Establece `DialogResult = true` y cierra
  - Si no es modal: Abre MainWindow y cierra LoginView (comportamiento anterior)

#### MainWindow.xaml
- **Estructura del menú superior reorganizada**:
  - Grid con 3 columnas: Logo | Menú | Opciones Usuario

- **Logo agregado**:
  ```xaml
  <Image Source="Assets/logo.png" Height="30" Width="30"/>
  <TextBlock Text="El Perrito" FontSize="18" FontWeight="Bold"/>
  ```

- **Botón "Iniciar Sesión"**:
  - Visible solo cuando NO está autenticado
  - Ejecuta `LoginCommand`
  - Estilo moderno con fondo azul

- **Menú de usuario**:
  - Visible solo cuando está autenticado
  - Muestra nombre del usuario
  - Opción de "Cerrar Sesión"

- **Botón Salir**:
  - Siempre visible
  - Se pone rojo al hacer hover

#### LoginView.xaml
- **Logo agregado**:
  ```xaml
  <Image Source="../Assets/logo.png" Width="80" Height="80"/>
  ```
- Reemplaza el emoji 🐕 con el logo oficial
- Título "El Perrito" debajo del logo

#### ElPerrito.WPF.csproj
- Agregada configuración para copiar `Assets\logo.png` al directorio de salida

### 3. Nuevo Flujo de Usuario

#### Usuario Invitado (No Autenticado)
1. Aplicación inicia mostrando la TIENDA
2. Puede navegar por productos
3. Botón "🔑 Iniciar Sesión" visible en el menú superior
4. Al hacer clic, se abre ventana de login como diálogo modal
5. Después de autenticarse, vuelve a la ventana principal actualizada

#### Usuario Autenticado
1. Después del login, el menú se actualiza automáticamente
2. **Cliente**:
   - Ve "TIENDA" y "MI CARRITO"
   - Puede comprar productos
3. **Admin/Operador**:
   - Ve opciones administrativas (Productos, Ventas, Reportes, Configuración)
   - Inicia en vista de Productos
4. Menú superior muestra nombre del usuario
5. Opción "Cerrar Sesión" disponible
6. Al cerrar sesión, vuelve al modo invitado

### 4. Características del Modo Invitado

- ✅ Acceso inmediato a la tienda sin login
- ✅ Puede navegar y ver productos
- ✅ Interfaz limpia con botón prominente de "Iniciar Sesión"
- ✅ No se muestra opción de carrito (requiere autenticación)
- ✅ No se muestran opciones administrativas
- ✅ Mensaje de bienvenida genérico

### 5. Logo de la Aplicación

**Ubicación**: `Aplicacion/ElPerrito.WPF/Assets/logo.png`

**Copiado desde**: `src/assets/logo.png` (aplicación web)

**Usos**:
- MainWindow: Logo 30x30 en esquina superior izquierda
- LoginView: Logo 80x80 centrado sobre el formulario

### 6. Ventajas del Nuevo Sistema

1. **Mejor experiencia de usuario**: Acceso inmediato sin barreras
2. **Flexibilidad**: Los usuarios pueden explorar antes de registrarse
3. **Consistencia visual**: Logo oficial en toda la aplicación
4. **Transiciones suaves**: Login/Logout sin cerrar ventanas
5. **Feedback visual claro**: El menú se adapta automáticamente al estado de autenticación

### 7. Seguridad Mantenida

- ❌ No se puede acceder al carrito sin autenticación
- ❌ No se pueden ver opciones administrativas sin autenticación
- ✅ Validación de permisos en cada comando
- ✅ Sesión se limpia completamente al cerrar sesión
- ✅ Autenticación contra base de datos con BCrypt

## Flujo de Navegación

```
┌─────────────────────────────────────┐
│  Inicio de Aplicación               │
│  (Modo Invitado)                    │
└────────────┬────────────────────────┘
             │
             ▼
      ┌──────────────┐
      │   TIENDA     │ ◄──────────────┐
      │  (Invitado)  │                │
      └──────┬───────┘                │
             │                        │
             │ Clic "Iniciar Sesión"  │
             ▼                        │
      ┌──────────────┐                │
      │ LoginView    │                │
      │  (Diálogo)   │                │
      └──────┬───────┘                │
             │                        │
    ┌────────┴────────┐              │
    │                 │              │
    ▼                 ▼              │
┌────────┐      ┌──────────┐         │
│CLIENTE │      │  ADMIN   │         │
│        │      │ OPERADOR │         │
└───┬────┘      └────┬─────┘         │
    │                │               │
    │ Logout         │ Logout        │
    └────────────────┴───────────────┘
```

## Resumen

La aplicación ahora permite exploración sin autenticación, con una interfaz moderna que incluye el logo oficial y transiciones suaves entre estados autenticado/no autenticado.
