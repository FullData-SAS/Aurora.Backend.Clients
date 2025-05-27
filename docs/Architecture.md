# Arquitectura - Aurora.Backend.Clients

## Visión General

Aurora.Backend.Clients es un microservicio que forma parte del Sistema de Gestión Documental Electrónica de Archivo (SGDEA). Este servicio sigue una arquitectura de capas con separación clara de responsabilidades, implementando patrones de diseño como Repository, Unit of Work y Dependency Injection.

## Diagrama de Arquitectura

```
┌─────────────────────────────────┐
│           API Layer             │
│    (Aurora.Backend.Clients)     │
│                                 │
│  ┌─────────────────────────┐    │
│  │     Controllers         │    │
│  └─────────────────────────┘    │
└───────────────┬─────────────────┘
                │
                ▼
┌─────────────────────────────────┐
│        Service Layer            │
│(Aurora.Backend.Clients.Services)│
│                                 │
│  ┌─────────────────────────┐    │
│  │     Service Contracts   │    │
│  └─────────────────────────┘    │
│  ┌─────────────────────────┐    │
│  │     Implementations     │    │
│  └─────────────────────────┘    │
└───────────────┬─────────────────┘
                │
                ▼
┌─────────────────────────────────┐
│        Data Access Layer        │
│(Aurora.Backend.Clients.Services)│
│                                 │
│  ┌─────────────────────────┐    │
│  │     Repository Pattern  │    │
│  └─────────────────────────┘    │
│  ┌─────────────────────────┐    │
│  │     Unit of Work        │    │
│  └─────────────────────────┘    │
│  ┌─────────────────────────┐    │
│  │     Entity Framework    │    │
│  └─────────────────────────┘    │
└───────────────┬─────────────────┘
                │
                ▼
┌─────────────────────────────────┐
│         Database Layer          │
│                                 │
│  ┌─────────────────────────┐    │
│  │       PostgreSQL        │    │
│  └─────────────────────────┘    │
└─────────────────────────────────┘
```

## Componentes Principales

### 1. Capa de API (Aurora.Backend.Clients)

Esta capa expone los endpoints REST y maneja las solicitudes HTTP.

**Componentes clave:**
- **Controllers**: Implementan los endpoints de la API y manejan las solicitudes HTTP.
- **Program.cs**: Configura la aplicación, servicios, middleware y rutas.

### 2. Capa de Servicios (Aurora.Backend.Clients.Services)

Esta capa contiene la lógica de negocio y actúa como intermediaria entre la capa de API y la capa de acceso a datos.

**Componentes clave:**
- **Contracts**: Interfaces que definen los contratos de servicio.
- **Implements**: Implementaciones concretas de los contratos de servicio.
- **Models**: Modelos de datos utilizados para transferir información entre capas.
- **Enumerables**: Enumeraciones utilizadas en la aplicación.

### 3. Capa de Acceso a Datos (Aurora.Backend.Clients.Services)

Esta capa maneja el acceso a la base de datos y la persistencia de datos.

**Componentes clave:**
- **Repository Pattern**: Implementación del patrón Repository para abstraer el acceso a datos.
- **Unit of Work**: Implementación del patrón Unit of Work para gestionar transacciones.
- **Entity Framework**: ORM utilizado para mapear objetos a tablas de base de datos.
- **Persistence/Context**: Contexto de Entity Framework para interactuar con la base de datos.
- **Persistence/Entities**: Entidades que representan las tablas de la base de datos.

### 4. Capa de Base de Datos

PostgreSQL se utiliza como sistema de gestión de bases de datos relacional.

## Patrones de Diseño Implementados

### Repository Pattern
Proporciona una abstracción de la capa de acceso a datos, permitiendo un acceso centralizado a las entidades.

```csharp
public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(Guid id);
    Task<bool> AddAsync(T entity);
    Task<bool> UpdateAsync(T entity);
    Task<bool> DeleteAsync(Guid id);
}
```

### Unit of Work Pattern
Gestiona las transacciones de base de datos y garantiza la consistencia de los datos.

```csharp
public interface IUnitOfWork
{
    Task<bool> CommitAsync();
}
```

### Dependency Injection
Facilita la inyección de dependencias y mejora la testabilidad del código.

```csharp
// En Program.cs
builder.Services.AddServices();
builder.Services.AddDataBase(builder.Configuration);
```

## Flujo de Datos

1. El cliente envía una solicitud HTTP a un endpoint de la API.
2. El controlador correspondiente recibe la solicitud y la valida.
3. El controlador llama al servicio apropiado para procesar la solicitud.
4. El servicio utiliza el repositorio para acceder a los datos necesarios.
5. El repositorio interactúa con Entity Framework para realizar operaciones en la base de datos.
6. Los datos se devuelven a través de la cadena de llamadas hasta el controlador.
7. El controlador formatea la respuesta y la devuelve al cliente.

## Seguridad

La API implementa autenticación JWT Bearer para proteger los endpoints. Los clientes deben incluir un token JWT válido en el encabezado de autorización para acceder a los recursos protegidos.

## Escalabilidad

La arquitectura de microservicios permite escalar horizontalmente el servicio según sea necesario. La separación clara de responsabilidades facilita el mantenimiento y la evolución del sistema.

## Monitoreo y Diagnóstico

La aplicación incluye endpoints de verificación de salud para monitorear el estado del servicio y sus dependencias.

```csharp
app.UseHealthChecks($"/{appName}/HealthCheck", new HealthCheckOptions()
{
    ResultStatusCodes =
    {
        [HealthStatus.Healthy] = StatusCodes.Status200OK,
        [HealthStatus.Degraded] = StatusCodes.Status200OK,
        [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
    },
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
```