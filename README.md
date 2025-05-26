# Aurora.Backend.Clients - Documentación

## Descripción General
Aurora.Backend.Clients es un microservicio API REST que forma parte del Sistema de Gestión Documental Electrónica de Archivo (SGDEA). Este servicio gestiona la información de los clientes del sistema, permitiendo crear, consultar, actualizar y eliminar registros de clientes.

## Tecnologías Utilizadas
- ASP.NET Core 8.0
- Entity Framework Core
- PostgreSQL
- Swagger para documentación de API

## Estructura del Proyecto
El proyecto está organizado en dos componentes principales:

1. **Aurora.Backend.Clients**: Contiene los controladores API y la configuración de la aplicación.
2. **Aurora.Backend.Clients.Services**: Contiene la lógica de negocio, modelos, contratos y acceso a datos.

## Endpoints API

La API está disponible en las siguientes URLs:
- HTTP: http://localhost:5084/Clients
- HTTPS: https://localhost:7148/Clients

### Documentación Swagger
- http://localhost:5084/Clients/swagger
- https://localhost:7148/Clients/swagger

### Endpoints Disponibles

#### Clientes
- **GET /Clients/GetAll**: Obtiene todos los clientes registrados.
- **GET /Clients/Get?id={guid}**: Obtiene un cliente específico por su ID.
- **POST /Clients/Create**: Crea un nuevo cliente.
- **PUT /Clients/Update**: Actualiza la información de un cliente existente.
- **DELETE /Clients/Delete?id={guid}**: Elimina un cliente por su ID.

#### Monitoreo de Salud
- **GET /Clients/HealthCheck**: Verifica el estado de salud del servicio.

## Modelos de Datos

### ClientCreateModel
```csharp
public class ClientCreateModel
{
    public string Name { get; set; } = null!;
    public string TaxId { get; set; } = null!;
    public string? Location { get; set; }
    public string? CompanyType { get; set; }
    public bool? Active { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
```

### ClientUpdateModel
```csharp
public class ClientUpdateModel : ClientCreateModel
{
    public Guid Id { get; set; }
}
```

## Configuración

### Conexión a Base de Datos
La aplicación se conecta a una base de datos PostgreSQL. La cadena de conexión se configura en el archivo `appsettings.json`:

```json
"ConnectionStrings": {
  "DB": "Host=hosts.com;Port=1234;Database=DB;Username=user;Password=passwrod*"
}
```

## Seguridad
La API utiliza autenticación JWT Bearer para proteger los endpoints. Los clientes deben incluir un token JWT válido en el encabezado de autorización para acceder a los recursos protegidos.

## Despliegue
El proyecto incluye un archivo Dockerfile para facilitar el despliegue en contenedores Docker.

## Monitoreo y Salud
El servicio incluye un endpoint de verificación de salud en `/Clients/HealthCheck` que proporciona información sobre el estado del servicio.

## Contacto
Para más información, contacte a:
- Nombre: Example Contact
- URL: https://example.com/contact

## Licencia
- Nombre: Example License
- URL: https://example.com/license
