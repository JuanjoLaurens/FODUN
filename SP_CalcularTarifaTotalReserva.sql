-- SP_CalcularTarifaTotalReserva
-- Calcula el valor total de una reserva considerando tarifas base, personas adicionales,
-- temporadas (días ordinarios/especiales), lavandería y visitas día.
CREATE PROCEDURE SP_CalcularTarifaTotalReserva
    @AlojamientoID INT,
    @FechaLlegada DATE,
    @FechaSalida DATE,
    @NumeroPersonas INT,
    @NumeroUnidadesReservadas INT, -- Cuántas de estas unidades del mismo AlojamientoID se reservan
    @IncluyeLavanderia BIT = 0,
    @NumeroAcompañantesVisitaDia INT = 0, -- Nuevo parámetro para visitas día
    @ValorTotal DECIMAL(18, 2) OUTPUT,
    @MensajeError NVARCHAR(MAX) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @TarifaBaseOrdinaria DECIMAL(18, 2);
    DECLARE @TarifaBaseEspecial DECIMAL(18, 2);
    DECLARE @CapacidadMaximaAlojamiento INT;
    DECLARE @NombreUbicacion NVARCHAR(100);
    DECLARE @TipoUbicacion VARCHAR(50);
    DECLARE @TipoAlojamiento VARCHAR(50);

    SET @ValorTotal = 0;
    SET @MensajeError = NULL;

    -- Validar fechas
    IF @FechaLlegada >= @FechaSalida
    BEGIN
        SET @MensajeError = 'La Fecha de Salida debe ser posterior a la Fecha de Llegada.';
        RETURN;
    END;

    -- Obtener detalles del alojamiento
    SELECT
        @TarifaBaseOrdinaria = A.TarifaDiaOrdinario,
        @TarifaBaseEspecial = A.TarifaDiaEspecial,
        @CapacidadMaximaAlojamiento = A.CapacidadMaximaPersonas, -- Nombre de columna correcto
        @NombreUbicacion = U.Nombre,
        @TipoUbicacion = U.TipoUbicacion,
        @TipoAlojamiento = A.TipoAlojamiento
    FROM
        Alojamientos A
    INNER JOIN
        Ubicaciones U ON A.UbicacionId = U.UbicacionId
    WHERE
        A.AlojamientoId = @AlojamientoID;

    IF @TarifaBaseOrdinaria IS NULL
    BEGIN
        SET @MensajeError = 'Alojamiento no encontrado o tarifas no definidas.';
        RETURN;
    END;

    -- Calcular número de noches ordinarias y especiales
    DECLARE @TotalNoches INT = DATEDIFF(DAY, @FechaLlegada, @FechaSalida);
    DECLARE @NochesOrdinarias INT = 0;
    DECLARE @NochesEspeciales INT = 0;
    DECLARE @CurrentDate DATE = @FechaLlegada;

    WHILE @CurrentDate < @FechaSalida
    BEGIN
        -- Suponiendo que los días especiales son Sábados y Domingos
        IF DATENAME(dw, @CurrentDate) IN ('Saturday', 'Sunday') OR
           -- Puedes añadir aquí fechas festivas específicas si tienes una tabla de festivos
           EXISTS (SELECT 1 FROM FechasEspeciales WHERE Fecha = @CurrentDate) -- Asume una tabla FechasEspeciales
        BEGIN
            SET @NochesEspeciales = @NochesEspeciales + 1;
        END
        ELSE
        BEGIN
            SET @NochesOrdinarias = @NochesOrdinarias + 1;
        END;
        SET @CurrentDate = DATEADD(DAY, 1, @CurrentDate);
    END;

    -- Cálculo de la tarifa base por noches y unidades
    SET @ValorTotal = (@NochesOrdinarias * @TarifaBaseOrdinaria + @NochesEspeciales * @TarifaBaseEspecial) * @NumeroUnidadesReservadas;

    -- Si el Alojamiento es una "habitación" o "alojamiento" en una sede recreativa que cobra por persona adicional
    -- Ajuste: La lógica para Medellín "también aplica por persona" debería estar mejor definida,
    -- lo he incluido como ejemplo, pero idealmente se usaría una configuración de tarifas por Ubicacion/TipoAlojamiento.
    IF @TipoUbicacion = 'Sede Recreativa' OR @NombreUbicacion = 'Medellín' -- Para Medellín también aplica por persona
    BEGIN
        -- Multiplica la capacidad máxima del alojamiento por el número de unidades reservadas
        IF @NumeroPersonas > @CapacidadMaximaAlojamiento * @NumeroUnidadesReservadas
        BEGIN
             SET @ValorTotal = @ValorTotal + ((@NumeroPersonas - (@CapacidadMaximaAlojamiento * @NumeroUnidadesReservadas)) * 16000.00 * @TotalNoches);
        END
    END;


    -- Servicio de Lavandería (solo para El Rodadero, Santa Marta)
    IF @IncluyeLavanderia = 1 AND @NombreUbicacion = 'Santa Marta' AND @TipoUbicacion = 'Apartamento'
    BEGIN
        SET @ValorTotal = @ValorTotal + 18000.00; -- Es una tarifa fija, no por noche
    END;

    -- Visita Día – Acompañantes (Villeta, El Placer, Manguruma, Gonzalo Morante, y Tablones)
    -- Cada Acompañante (A partir del 5° y hasta máx. 10) $5.500
    IF @NumeroAcompañantesVisitaDia > 0 AND @TotalNoches > 0 AND -- Solo aplicar si hay acompañantes y reserva de al menos 1 noche
       @NombreUbicacion IN ('Villeta', 'El Placer', 'Manguruma', 'Gonzalo Morante', 'Tablones')
    BEGIN
        DECLARE @CostoAcompanante DECIMAL(18, 2) = 5500.00;
        -- Asumiendo que el cargo es por acompañante por día de la reserva
        SET @ValorTotal = @ValorTotal + (@NumeroAcompañantesVisitaDia * @CostoAcompanante * @TotalNoches);
    END;

END;
GO