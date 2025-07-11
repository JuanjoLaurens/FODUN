# Sistema de Gestión de Reservas de Alojamientos FODUN

## Descripción del Proyecto

Aplicación web desarrollada con ASP.NET Core MVC para la gestión de reservas de alojamientos en las sedes y apartamentos de FODUN. Permite a los usuarios consultar disponibilidad, calcular tarifas y realizar/gestionar sus reservas. Utiliza una arquitectura por capas, Entity Framework Core para datos y .NET Identity para seguridad.

## Tecnologías Utilizadas

* **Backend:** .NET 8, C#, ASP.NET Core MVC, Entity Framework Core, .NET Identity.
* **Base de Datos:** Microsoft SQL Server.
* **Frontend:** HTML, CSS (Bootstrap), JavaScript (jQuery).

## Requisitos del Sistema

* .NET SDK  8.0.x
* Microsoft SQL Server 
* Visual Studio 2022

## Configuración y Ejecución

Sigue estos pasos para configurar y ejecutar el proyecto:

1.  **Clonar el Repositorio:**
    ```bash
    git clone [https://github.com/JuanjoLaurens/FODUN.git](https://github.com/JuanjoLaurens/FODUN.git)
    cd FODUN.Reservas
    ```

2.  **Configurar Base de Datos:**
    * Abre `appsettings.json` y actualiza la `DefaultConnection` para apuntar a la instancia de SQL Server.
    * Aplica las migraciones para crear la base de datos:
        ```bash
        dotnet ef database update
        ```
    * Ejecuta el script `Inserciones_Alojamientos_Tarifas_Ubicaciones.sql` directamente en la base de datos `FODUN_Reservas_DB` para cargar los datos iniciales.

3.  **Restaurar Dependencias:**
    ```bash
    dotnet restore
    ```

4.  **Ejecutar la Aplicación:**
    Desde Visual Studio (F5) o la terminal:
    ```bash
    dotnet run
    ```

## Funcionalidades Implementadas

* **Autenticación y Autorización:** Registro, Login, Recuperación de contraseña, Roles (Administrador, Usuario).
* **Gestión de Reservas:**
    * Búsqueda de alojamientos disponibles por fechas y personas.
    * Cálculo de tarifas según diversos criterios.
    * Creación, confirmación y gestión de reservas.
    * **Envío de correo de confirmación de reserva.**
* **Gestión de Catálogos (CRUD - Administrador):** Ubicaciones, Alojamientos.
* **Base de Datos:** Uso de SQL Server y procedimientos almacenados para cálculos y búsquedas optimizadas.

## Credenciales de Prueba

Para probar la aplicación:

* **Administrador:**
    * **Email:** `admin@fodun.com`
    * **Contraseña:** `Adm1n_2025*`


## Video de Demostración

**[Video Funcional Administracion Alojamientos](https://youtu.be/mlGc_QFi1bk?si=MJxFu_9kKvSwIdTj)**

---
**Desarrollado por:** Juan Jose Laurens Gomez