# Guía de Instalación - Aurora.Backend.Clients

## Requisitos Previos

1. **.NET SDK 8.0** o superior
2. **PostgreSQL** (versión 12 o superior)
3. **Docker** (opcional, para despliegue en contenedores)
4. **IDE** recomendado: Visual Studio 2022, Visual Studio Code o JetBrains Rider

## Instalación Local

### 1. Clonar el Repositorio

```bash
git clone <url-del-repositorio>
cd Aurora.Backend.Clients
```

### 2. Configurar la Base de Datos

Edite el archivo `Aurora.Backend.Clients/appsettings.json` para configurar la conexión a la base de datos:

```json
"ConnectionStrings": {
  "DB": "Host=localhost;Port=5432;Database=Aurora;Username=su_usuario;Password=su_contraseña"
}
```

### 3. Restaurar Dependencias y Compilar

```bash
dotnet restore
dotnet build
```

### 4. Ejecutar la Aplicación

```bash
cd Aurora.Backend.Clients
dotnet run
```

La API estará disponible en:
- HTTP: http://localhost:5084/Clients
- HTTPS: https://localhost:7148/Clients

La documentación Swagger estará disponible en:
- HTTP: http://localhost:5084/Clients/swagger
- HTTPS: https://localhost:7148/Clients/swagger

## Despliegue con Docker

### 1. Construir la Imagen Docker

```bash
docker build -t aurora-backend-clients:latest .
```

### 2. Ejecutar el Contenedor

```bash
docker run -d -p 5084:80 -p 7148:443 --name aurora-clients aurora-backend-clients:latest
```

## Configuración de Entorno

### Variables de Entorno

La aplicación utiliza las siguientes variables de entorno:

- `ASPNETCORE_ENVIRONMENT`: Define el entorno de ejecución (Development, Staging, Production)
- `ConnectionStrings__DB`: Cadena de conexión a la base de datos (alternativa a la configuración en appsettings.json)

### Configuración de CORS

La API está configurada para permitir solicitudes desde cualquier origen. Para restringir los orígenes permitidos, modifique la configuración CORS en el archivo `Program.cs`:

```csharp
app.UseCors(builder => builder
    .WithOrigins("https://su-dominio.com")
    .AllowAnyHeader()
    .AllowAnyMethod()
);
```

## Verificación de la Instalación

Para verificar que la instalación se ha realizado correctamente, acceda al endpoint de verificación de salud:

```
GET /Clients/HealthCheck
```

Si la respuesta es un código de estado HTTP 200, la aplicación está funcionando correctamente.

## Solución de Problemas Comunes

### Error de Conexión a la Base de Datos

Verifique que:
- La cadena de conexión en `appsettings.json` es correcta
- El servidor PostgreSQL está en ejecución
- El usuario tiene permisos suficientes para acceder a la base de datos

### Error al Iniciar la Aplicación

- Verifique que los puertos 5084 y 7148 no estén siendo utilizados por otras aplicaciones
- Asegúrese de que .NET SDK 8.0 está instalado correctamente

## Soporte

Para obtener ayuda adicional, contacte a:
- Nombre: Example Contact
- URL: https://example.com/contact