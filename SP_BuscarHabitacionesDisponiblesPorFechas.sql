USE [FODUN_Reservas_DB]
GO
/****** Object:  StoredProcedure [dbo].[SP_BuscarHabitacionesDisponiblesPorFechas]    Script Date: 10/07/2025 6:33:56 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- SP_BuscarHabitacionesDisponiblesPorFechas
-- Este SP encuentra los alojamientos (habitaciones, apartamentos, cabañas) disponibles
-- en un rango de fechas dado.
ALTER PROCEDURE [dbo].[SP_BuscarHabitacionesDisponiblesPorFechas]
    @FechaLlegada DATE,
    @FechaSalida DATE
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

    -- Seleccionar alojamientos que NO estén ocupados en el rango de fechas
    SELECT
        A.AlojamientoId,
        A.Nombre AS NombreAlojamiento,
        A.Descripcion,
        A.TipoAlojamiento,
        U.Nombre AS NombreUbicacion,
        A.CapacidadMaximaPersonas, -- Usar el nombre de columna correcto del modelo
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
        A.TarifaDiaEspecial
    FROM
        Alojamientos A
    INNER JOIN
        Ubicaciones U ON A.UbicacionId = U.UbicacionId
    WHERE
        A.Activo = 1 -- Solo alojamientos activos
        AND NOT EXISTS (
            SELECT 1
            FROM Reservas R
            WHERE
                R.AlojamientoId = A.AlojamientoId
                -- Verifica si existe algún solapamiento de fechas
                -- La reserva existente comienza antes del final de la nueva y termina después del inicio de la nueva
                AND R.FechaInicio < @FechaSalida
                AND R.FechaFin > @FechaLlegada
        );
END;
