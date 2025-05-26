# Documentación de la API Aurora.Backend.Clients

## Información General
- **Versión**: v1.0.0.1
- **Título**: Clients API
- **Descripción**: API REST para la gestión de clientes del Sistema de Gestión Documental Electrónica de Archivo (SGDEA)
- **Base URL**: 
  - HTTP: http://localhost:5084/Clients
  - HTTPS: https://localhost:7148/Clients

## Autenticación
La API utiliza autenticación JWT Bearer. Para acceder a los endpoints protegidos, incluya un token JWT válido en el encabezado de autorización:

```
Authorization: Bearer {token}
```

## Endpoints

### Clientes

#### Obtener todos los clientes
```
GET /Clients/GetAll
```

**Respuestas**:
- **200 OK**: Lista de clientes obtenida correctamente
  - Tipo: `Result<IEnumerable<ClientUpdateModel>>`
- **400 Bad Request**: Error en la solicitud
  - Tipo: `Result<object>`
- **401 Unauthorized**: No autorizado
  - Tipo: `Result<object>`
- **500 Internal Server Error**: Error interno del servidor
  - Tipo: `Result<object>`

#### Obtener un cliente por ID
```
GET /Clients/Get?id={guid}
```

**Parámetros**:
- **id** (query, requerido): ID del cliente (formato GUID)

**Respuestas**:
- **200 OK**: Cliente obtenido correctamente
  - Tipo: `Result<ClientUpdateModel>`
- **400 Bad Request**: Error en la solicitud
  - Tipo: `Result<object>`
- **401 Unauthorized**: No autorizado
  - Tipo: `Result<object>`
- **500 Internal Server Error**: Error interno del servidor
  - Tipo: `Result<object>`

#### Crear un nuevo cliente
```
POST /Clients/Create
```

**Cuerpo de la solicitud**:
```json
{
  "name": "string",
  "taxId": "string",
  "location": "string",
  "companyType": "string",
  "active": true,
  "createdAt": "2023-01-01T00:00:00Z",
  "updatedAt": "2023-01-01T00:00:00Z"
}
```

**Respuestas**:
- **200 OK**: Cliente creado correctamente
  - Tipo: `Result<bool>`
- **400 Bad Request**: Error en la solicitud
  - Tipo: `Result<object>`
- **401 Unauthorized**: No autorizado
  - Tipo: `Result<object>`
- **500 Internal Server Error**: Error interno del servidor
  - Tipo: `Result<object>`

#### Actualizar un cliente existente
```
PUT /Clients/Update
```

**Cuerpo de la solicitud**:
```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "name": "string",
  "taxId": "string",
  "location": "string",
  "companyType": "string",
  "active": true,
  "createdAt": "2023-01-01T00:00:00Z",
  "updatedAt": "2023-01-01T00:00:00Z"
}
```

**Respuestas**:
- **200 OK**: Cliente actualizado correctamente
  - Tipo: `Result<bool>`
- **400 Bad Request**: Error en la solicitud
  - Tipo: `Result<object>`
- **401 Unauthorized**: No autorizado
  - Tipo: `Result<object>`
- **500 Internal Server Error**: Error interno del servidor
  - Tipo: `Result<object>`

#### Eliminar un cliente
```
DELETE /Clients/Delete?id={guid}
```

**Parámetros**:
- **id** (query, requerido): ID del cliente (formato GUID)

**Respuestas**:
- **200 OK**: Cliente eliminado correctamente
  - Tipo: `Result<bool>`
- **400 Bad Request**: Error en la solicitud
  - Tipo: `Result<object>`
- **401 Unauthorized**: No autorizado
  - Tipo: `Result<object>`
- **500 Internal Server Error**: Error interno del servidor
  - Tipo: `Result<object>`

### Monitoreo de Salud

#### Verificar estado de salud
```
GET /Clients/HealthCheck
```

**Respuestas**:
- **200 OK**: Servicio en estado saludable o degradado
- **503 Service Unavailable**: Servicio no disponible

## Modelos de Datos

### ClientCreateModel
```json
{
  "name": "string",
  "taxId": "string",
  "location": "string",
  "companyType": "string",
  "active": true,
  "createdAt": "2023-01-01T00:00:00Z",
  "updatedAt": "2023-01-01T00:00:00Z"
}
```

### ClientUpdateModel
```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "name": "string",
  "taxId": "string",
  "location": "string",
  "companyType": "string",
  "active": true,
  "createdAt": "2023-01-01T00:00:00Z",
  "updatedAt": "2023-01-01T00:00:00Z"
}
```

### Result<T>
```json
{
  "status": "SUCCESS",
  "message": "string",
  "response": {}
}
```

## Códigos de Estado
- **SUCCESS**: Operación exitosa
- **ERROR**: Error en la operación

## Contacto
- **Nombre**: Example Contact
- **URL**: https://example.com/contact

## Licencia
- **Nombre**: Example License
- **URL**: https://example.com/license