USE FODUN_Reservas_DB;
GO

-- Eliminar el procedimiento si ya existe para evitar errores al crearlo nuevamente.
IF OBJECT_ID('SP_BuscarHabitacionesDisponiblesPorFechasYPersonas', 'P') IS NOT NULL
BEGIN
    DROP PROCEDURE SP_BuscarHabitacionesDisponiblesPorFechasYPersonas;
END;
GO

-- Crear el procedimiento almacenado con las correcciones necesarias
CREATE PROCEDURE SP_BuscarHabitacionesDisponiblesPorFechasYPersonas
    @FechaLlegada DATE,
    @FechaSalida DATE,
    @NumeroPersonas INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Validar que la fecha de salida sea posterior a la de llegada
    IF @FechaLlegada >= @FechaSalida
    BEGIN
        SELECT
            -1 AS Resultado,
            'La Fecha de Salida debe ser posterior a la Fecha de Llegada.' AS MensajeError;
        RETURN;
    END;

    -- Seleccionar alojamientos que cumplen con la capacidad y no estén ocupados
    SELECT
        A.AlojamientoId,
        A.Nombre, -- *** Corregido: Selecciona 'Nombre' directamente, sin alias ***
        A.Descripcion,
        A.TipoAlojamiento,
        A.CapacidadMaximaPersonas,
        A.CamasDobles,
        A.CamasSencillas,
        A.Camarotes,
        A.TieneBanoPrivado,
        A.TieneNevera,
        A.TieneTelevisor,
        A.TieneCocinetaEquipada,
        A.TieneSalaEstar,
        A.TieneTerraza,
        A.TarifaDiaOrdinario,
        A.TarifaDiaEspecial,
        A.Activo,
        A.FechaCreacion,
        A.FechaActualizacion,
        A.UbicacionId -- Asegúrate de incluir UbicacionId si es parte de tu modelo Alojamiento
    FROM
        Alojamientos A
    INNER JOIN
        Ubicaciones U ON A.UbicacionId = U.UbicacionId
    WHERE
        A.Activo = 1
        AND A.CapacidadMaximaPersonas >= @NumeroPersonas
        AND NOT EXISTS (
            SELECT 1
            FROM Reservas R
            WHERE
                R.AlojamientoId = A.AlojamientoId
                AND R.FechaInicio < @FechaSalida
                AND R.FechaFin > @FechaLlegada
        );
END;
GO