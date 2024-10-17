# Instrucciones para Desplegar la API "WeatherForecast" Usando Docker

Este documento describe los pasos para construir y ejecutar la API de **WeatherForecast** usando Docker. Siga los pasos detallados a continuación para configurar todo correctamente en su máquina local.

## Prerrequisitos

Antes de comenzar, asegúrese de tener los siguientes elementos instalados en su sistema:

- [Docker](https://www.docker.com/get-started) (versión 20.x o posterior).
- Un editor de texto, como [Visual Studio Code](https://code.visualstudio.com/) o cualquier editor que prefieras.

## Instrucciones

### 1. Clonar el Repositorio

Primero, clone el repositorio que contiene el código fuente de la aplicación API.

```bash
git clone https://github.com/chavamm/weather-api.git
```
### 2. Navegar al Directorio del Proyecto

Una vez descargdo, acceda al directorio donde está el archivo Dockerfile. Este archivo es lo principal para crear la imagen de Docker.
```bash
cd weather-api
```
### 3. Crear la Imagen Docker
Ejecute el siguiente comando en la raíz del proyecto (donde está ubicado el archivo Dockerfile). Este comando construira una imagen personalizada de Docker llamada weatherdotnet8:

```bash
docker build -t weatherdotnet8:latest .
```
Este comando de Docker realiza lo siguiente:

- Lee el archivo Dockerfile.
- Descarga la imagen base de .NET SDK y compila la aplicación.
- Crea una nueva imagen personalizada con su API lista para ejecutarse.

### 4. Verificar la Imagen Creada
Después de que se ha construid la imagen, puede verificar que la imagen fue creada correctamente ejecutando:
```bash
docker images
```
En la salida de ese comando deberia de ver en lista un elemento con el nombre que le asigno `weatherdotnet8`

### 5. Ejecutar la Aplicación en un Contenedor
Una vez que la imagen ya se tiene construida, puede crear un contenedor y ejecutar la aplicación. Use el siguiente comando para ejecutar la API mapeando el puerto 8080 en el contenedor al puerto 5000 de tu máquina local:

```bash
docker run -p 5000:8080 --name weather-api-local weatherdotnet8:latest
```
Este comando realiza lo siguiente:

- Crea un contenedor basado en la imagen `weatherdotnet8:latest`.
- Mapea el puerto `8080` dentro del contenedor al puerto `5000` en tu máquina local, permitiendo que accedas a la API desde `http://localhost:5000`.
- Asigna el nombre `weather-api-local` al contenedor.

### 6. Verificar que la API Está Corriendo
Una vez que el contenedor esté en ejecución, puede acceder a la API desde su navegador o usando herramientas como curl o Postman.
Recomiendo Postman, y puede usar el archivo con definiciones de Postman adjunto en el mismo proyecto en Utils/WeatherAPI-deployed.postman_collection.json

Tendra disponible los endpoints:
- `http://localhost:5000/api/weatherforecast/{int:NumeroDeDias}`
- `http://localhost:5000/api/auth/login`


### Notas
Si desea cambiar el puerto en el que se accede a la API desde su máquina local, simplemente modifique el parámetro -p en el comando docker run. Por ejemplo, para exponer la API en el puerto 4000, usa -p 4000:8080.

# Documentacion para el API
# Endpoints de la API

La API expone los siguientes endpoints:

### 1. `POST /api/auth/login`

#### Descripción:
Este endpoint permite autenticar a un usuario y recibir un **JWT Token** que puede ser usado para autenticarse en otros endpoints protegidos.

#### Ejemplo de Solicitud:

**URL:** `http://localhost:5000/api/auth/login`

**Método:** `POST`

**Cuerpo del Request:**

```json
{
  "username": "user",
  "password": "mypassword"
}
```

#### Respuesta de ejemplo
```json
{
    "status": true,
    "message": "Successfully",
    "data": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIwIiwidW5pcXVlX25hbWUiOiJ1c2VyIiwibmJmIjoxNzI5MjA1NTI1LCJleHAiOjE3MjkyMTI3MjUsImlhdCI6MTcyOTIwNTUyNSwiaXNzIjoiV2VhdGhlckFQSSIsImF1ZCI6IldlYXRoZXJBUElVc2VycyJ9.z5YxNkOa1YKtw92DYAqnxO7V-rWarKM4tZfv85kEjnA",
    "errors": []
}
```
#### Notas:
El JWT Token devuelto debe ser utilizado para autenticar otras solicitudes, añadiéndolo en el header `Authorization` como `Bearer {token}`.

### 2. GET /api/weatherforecast/{NumeroDeDias}
#### Descripción:
Este endpoint devuelve un pronóstico del clima para los próximos {NumeroDeDias} días. Requiere autenticación con el JWT Token obtenido en el endpoint de login.

#### Ejemplo de Solicitud:
**URL:** `http://localhost:5000/api/weatherforecast/5`

**Método:** `GET`

**Autenticación:** Este endpoint está protegido, por lo que debe incluir el JWT Token en el header:
```bash
Authorization: Bearer {token}
```

#### Respuesta de ejemplo
```json
{
    "status": true,
    "message": "Successfully requested",
    "data": [
        {
            "date": "18-10-2024",
            "temperatureC": -17,
            "temperatureF": 2,
            "summary": "Chilly"
        },
        {
            "date": "19-10-2024",
            "temperatureC": -9,
            "temperatureF": 16,
            "summary": "Hot"
        },
        {
            "date": "20-10-2024",
            "temperatureC": 50,
            "temperatureF": 121,
            "summary": "Scorching"
        },
        {
            "date": "21-10-2024",
            "temperatureC": 7,
            "temperatureF": 44,
            "summary": "Warm"
        },
        {
            "date": "22-10-2024",
            "temperatureC": 45,
            "temperatureF": 112,
            "summary": "Hot"
        }
    ],
    "errors": []
}
```

#### Notas:
- Reemplace {NumeroDeDias} por el número de días que quiere obtener el pronóstico.
- El valor por defecto para DATE_FORMAT (usado para formatear la fecha) es "yyyy-MM-dd", pero puedes modificarlo utilizando la variable de entorno DATE_FORMAT del archivo Dockerfile antes de generar la imagen.
