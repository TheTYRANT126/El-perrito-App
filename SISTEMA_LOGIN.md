# Sistema de Login - Aplicación C# WPF

## Resumen de Cambios

Se ha implementado un sistema de autenticación completo en la aplicación WPF de El Perrito que replica el sistema de login de la aplicación web.

## Archivos Creados

### 1. Models/CurrentSession.cs
- **Propósito**: Singleton que mantiene la sesión del usuario actual
- **Funcionalidades**:
  - Almacena información del usuario autenticado (ID, nombre, email, tipo)
  - Métodos `LoginAsCliente()` y `LoginAsUsuario()` para establecer la sesión
  - Método `Logout()` para cerrar sesión
  - Propiedades de conveniencia: `IsCliente`, `IsAdmin`, `IsOperador`, `IsAdminOrOperador`

### 2. Services/AuthService.cs
- **Propósito**: Servicio de autenticación contra la base de datos
- **Funcionalidades**:
  - `LoginAsync(email, password)`: Autentica usuarios contra las tablas `cliente` y `usuario`
  - Verifica contraseñas usando BCrypt (igual que el sistema web)
  - Valida que las cuentas estén activas
  - Establece la sesión en CurrentSession al autenticar exitosamente

### 3. Views/LoginView.xaml y LoginView.xaml.cs
- **Propósito**: Ventana de inicio de sesión
- **Características**:
  - Campos de email y contraseña
  - Validación de campos requeridos
  - Indicador de carga durante autenticación
  - Mensajes de error amigables
  - Diseño moderno y responsivo

### 4. ViewModels/LoginViewModel.cs
- **Propósito**: ViewModel para la vista de login
- **Funcionalidades**:
  - Manejo de credenciales (Email, Password)
  - Comando `LoginCommand` que ejecuta la autenticación
  - Validación y mensajes de error
  - Navegación automática a MainWindow al autenticar exitosamente

## Archivos Modificados

### 1. App.xaml
- Cambio en `StartupUri` de `MainWindow.xaml` a `Views/LoginView.xaml`
- Ahora la aplicación inicia mostrando el login en lugar de la ventana principal

### 2. ViewModels/MainViewModel.cs
- Agregadas propiedades de control de sesión:
  - `IsCliente`, `IsAdmin`, `IsOperador`, `IsAdminOrOperador`
  - `UserFullName`, `UserTypeLabel`
- Agregado comando `LogoutCommand` para cerrar sesión
- Modificados los comandos de navegación para incluir validación de permisos:
  - Opciones de admin solo disponibles para Admin/Operador
  - Carrito solo disponible para Clientes
- Vista inicial adaptativa según tipo de usuario:
  - Clientes ven la Tienda por defecto
  - Admins/Operadores ven Productos por defecto

### 3. MainWindow.xaml
- Menú superior actualizado con visibilidad condicional:
  - "MI CARRITO" solo visible para Clientes
  - Opciones de Admin (Productos, Ventas, Reportes, Configuración) solo visibles para Admin/Operador
- Agregado menú de usuario con nombre completo y opción "Cerrar Sesión"

### 4. ElPerrito.WPF.csproj
- Agregada referencia al proyecto `ElPerrito.Data` para acceso a la base de datos
- Agregado paquete NuGet `BCrypt.Net-Next` v4.0.3 para verificación de contraseñas

## Flujo de Autenticación

1. **Inicio de Aplicación**:
   - La aplicación inicia mostrando `LoginView`

2. **Login**:
   - Usuario ingresa email y contraseña
   - `LoginViewModel` valida campos y ejecuta `AuthService.LoginAsync()`
   - `AuthService` busca primero en tabla `cliente`, luego en tabla `usuario`
   - Verifica contraseña con BCrypt
   - Valida que la cuenta esté activa (estado='activo' o activo=1)

3. **Post-Autenticación**:
   - Si exitoso: establece sesión en `CurrentSession`, cierra `LoginView`, abre `MainWindow`
   - Si falla: muestra mensaje de error en `LoginView`

4. **Sesión Activa**:
   - `MainWindow` adapta su interfaz según el tipo de usuario
   - Clientes ven opciones de tienda y carrito
   - Admins/Operadores ven panel administrativo

5. **Cerrar Sesión**:
   - Usuario selecciona "Cerrar Sesión" del menú
   - `MainViewModel.Logout()` limpia la sesión y vuelve a `LoginView`

## Tipos de Usuario

### Cliente
- **Tabla**: `cliente`
- **Validación**: `estado = 'activo'`
- **Acceso**: Tienda, Carrito
- **Sesión**: ID cliente, nombre, apellido, email

### Admin/Operador
- **Tabla**: `usuario`
- **Validación**: `activo = 1`
- **Roles**: 'admin' o 'operador'
- **Acceso**: Productos, Ventas, Reportes, Configuración
- **Sesión**: ID usuario, nombre, apellido, email, rol

## Seguridad

- Las contraseñas se verifican usando **BCrypt** (mismo método que la aplicación web)
- No se almacenan contraseñas en texto plano
- La sesión se mantiene en memoria durante la ejecución de la aplicación
- Al cerrar sesión, toda la información se limpia de memoria

## Requisitos

- **.NET 8.0** o superior
- **BCrypt.Net-Next 4.0.3**
- **Base de datos MySQL/MariaDB** con las tablas `cliente` y `usuario`
- **Entity Framework Core** (a través de ElPerrito.Data)

## Notas

- El sistema de login es compatible con las credenciales de la aplicación web
- Usuarios pueden autenticarse con las mismas cuentas en web y aplicación de escritorio
- La interfaz se adapta automáticamente según el tipo de usuario autenticado
