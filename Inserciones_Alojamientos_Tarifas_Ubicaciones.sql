---- Primero insertamos todas las ubicaciones
INSERT INTO [dbo].[Ubicaciones] ([Nombre], [Descripcion], [TipoUbicacion], [Activa], [FechaCreacion])
VALUES 
('Villeta', 'Sede recreativa con 8 habitaciones tipo alcoba, cada una con cama doble y camarote. Capacidad m�xima: 32 personas. Cuenta con nevera, televisor y terraza cubierta en cada habitaci�n.', 'Sede Recreativa', 1, GETDATE()),
('El Placer - Fusagasug�', 'Sede con 4 alojamientos numerados y 4 caba�as. Capacidad total: 34 personas. Los alojamientos tienen habitaciones con combinaciones de camas dobles y sencillas, mientras las caba�as incluyen sala de estar, cocineta equipada y terraza comedor.', 'Sede Recreativa', 1, GETDATE()),
('Gonzalo Morante - Chinchin�', 'Sede con alojamientos numerados y caba�as tipo A y B. Capacidad total: 30 personas. Algunos alojamientos tienen camas auxiliares adicionales.', 'Sede Recreativa', 1, GETDATE()),
('Tablones - Palmira', 'Sede con 4 alojamientos numerados, cada uno con combinaciones de camas dobles y camarotes. Capacidad total: 24 personas. Incluyen televisor, cocineta y sala de estar.', 'Sede Recreativa', 1, GETDATE()),
('Manguruma - Santa Fe de Antioquia', 'Sede con alojamientos tradicionales y nuevo bloque. Capacidad total: 46 personas. Los nuevos alojamientos tienen dos camas gemelas y camarote cada uno.', 'Sede Recreativa', 1, GETDATE()),
('Federman - Bogot�', 'Sede urbana con 4 habitaciones para alojamiento y amplios servicios recreativos como zona h�meda, gimnasio, salas de entretenimiento y actividades programadas.', 'Sede Urbana', 1, GETDATE()),
('Suramericana - Medell�n', 'Conjunto de apartamentos en Medell�n con 5 habitaciones distribuidas en diferentes configuraciones de camas sencillas y ba�os privados.', 'Apartamento', 1, GETDATE()),
('El Rodadero - Santa Marta', 'Conjunto de apartamentos en Santa Marta con diferentes capacidades (6-8 personas). Incluyen sala-comedor, cocina, ba�os y sitio de parqueo.', 'Apartamento', 1, GETDATE());

-- Verificamos los IDs asignados
SELECT UbicacionId, Nombre FROM [dbo].[Ubicaciones] ORDER BY UbicacionId DESC;


-- Alojamientos para Villeta (8 habitaciones similares)
INSERT INTO [dbo].[Alojamientos] (
    [Nombre], [Descripcion], [TipoAlojamiento], [CapacidadMaximaPersonas],
    [CamasDobles], [CamasSencillas], [Camarotes], [TieneBanoPrivado], 
    [TieneNevera], [TieneTelevisor], [TieneCocinetaEquipada], 
    [TieneSalaEstar], [TieneTerraza], [TarifaDiaOrdinario], [TarifaDiaEspecial],
    [Activo], [FechaCreacion], [FechaActualizacion], [UbicacionId]
)
VALUES
('Habitaci�n 1 Villeta', 'Alcoba con cama doble y camarote (capacidad 4 personas). Incluye nevera, televisor y terraza cubierta.', 'Habitaci�n', 4, 1, 0, 1, 1, 1, 1, 0, 0, 1, 70000, 90000, 1, GETDATE(), GETDATE(), 2),
('Habitaci�n 2 Villeta', 'Alcoba con cama doble y camarote (capacidad 4 personas). Incluye nevera, televisor y terraza cubierta.', 'Habitaci�n', 4, 1, 0, 1, 1, 1, 1, 0, 0, 1, 70000, 90000, 1, GETDATE(), GETDATE(), 2),
('Habitaci�n 3 Villeta', 'Alcoba con cama doble y camarote (capacidad 4 personas). Incluye nevera, televisor y terraza cubierta.', 'Habitaci�n', 4, 1, 0, 1, 1, 1, 1, 0, 0, 1, 70000, 90000, 1, GETDATE(), GETDATE(), 2),
('Habitaci�n 4 Villeta', 'Alcoba con cama doble y camarote (capacidad 4 personas). Incluye nevera, televisor y terraza cubierta.', 'Habitaci�n', 4, 1, 0, 1, 1, 1, 1, 0, 0, 1, 70000, 90000, 1, GETDATE(), GETDATE(), 2),
('Habitaci�n 5 Villeta', 'Alcoba con cama doble y camarote (capacidad 4 personas). Incluye nevera, televisor y terraza cubierta.', 'Habitaci�n', 4, 1, 0, 1, 1, 1, 1, 0, 0, 1, 70000, 90000, 1, GETDATE(), GETDATE(), 2),
('Habitaci�n 6 Villeta', 'Alcoba con cama doble y camarote (capacidad 4 personas). Incluye nevera, televisor y terraza cubierta.', 'Habitaci�n', 4, 1, 0, 1, 1, 1, 1, 0, 0, 1, 70000, 90000, 1, GETDATE(), GETDATE(), 2),
('Habitaci�n 7 Villeta', 'Alcoba con cama doble y camarote (capacidad 4 personas). Incluye nevera, televisor y terraza cubierta.', 'Habitaci�n', 4, 1, 0, 1, 1, 1, 1, 0, 0, 1, 70000, 90000, 1, GETDATE(), GETDATE(), 2),
('Habitaci�n 8 Villeta', 'Alcoba con cama doble y camarote (capacidad 4 personas). Incluye nevera, televisor y terraza cubierta.', 'Habitaci�n', 4, 1, 0, 1, 1, 1, 1, 0, 0, 1, 70000, 90000, 1, GETDATE(), GETDATE(), 2);

 --Alojamientos para Fusagasug�
INSERT INTO [dbo].[Alojamientos] (
    [Nombre], [Descripcion], [TipoAlojamiento], [CapacidadMaximaPersonas],
    [CamasDobles], [CamasSencillas], [Camarotes], [TieneBanoPrivado], 
    [TieneNevera], [TieneTelevisor], [TieneCocinetaEquipada], 
    [TieneSalaEstar], [TieneTerraza], [TarifaDiaOrdinario], [TarifaDiaEspecial],
    [Activo], [FechaCreacion], [FechaActualizacion], [UbicacionId]
)
VALUES
 --Alojamientos numerados
('Alojamiento 1 Fusagasug�', '2 habitaciones: 1 con cama doble y cama sencilla, otra con cama sencilla. Ba�o y televisor.', 'Alojamiento', 4, 1, 2, 0, 1, 0, 1, 0, 0, 0, 70000, 90000, 1, GETDATE(), GETDATE(), 3),
('Alojamiento 2 Fusagasug�', '2 habitaciones: 1 con cama doble, otra con 4 camas sencillas. Ba�o y televisor.', 'Alojamiento', 6, 1, 4, 0, 1, 0, 1, 0, 0, 0, 70000, 90000, 1, GETDATE(), GETDATE(), 3),
('Alojamiento 3 Fusagasug�', '1 habitaci�n con cama doble y 2 camas sencillas. Ba�o y televisor.', 'Alojamiento', 4, 1, 2, 0, 1, 0, 1, 0, 0, 0, 70000, 90000, 1, GETDATE(), GETDATE(), 3),
('Alojamiento 4 Fusagasug�', '2 habitaciones: 1 con cama doble y cama sencilla, otra con cama sencilla. Ba�o y televisor.', 'Alojamiento', 4, 1, 2, 0, 1, 0, 1, 0, 0, 0, 70000, 90000, 1, GETDATE(), GETDATE(), 3),

 --Caba�as (5-8)
('Caba�a 5 Fusagasug�', 'Sala de estar con sof� cama y TV, ba�o, habitaci�n con cama doble y cama sencilla, cocineta equipada, nevera y terraza comedor.', 'Caba�a', 4, 1, 1, 0, 1, 1, 1, 1, 1, 1, 90000, 120000, 1, GETDATE(), GETDATE(), 3),
('Caba�a 6 Fusagasug�', 'Sala de estar con sof� cama y TV, ba�o, habitaci�n con cama doble y cama sencilla, cocineta equipada, nevera y terraza comedor.', 'Caba�a', 4, 1, 1, 0, 1, 1, 1, 1, 1, 1, 90000, 120000, 1, GETDATE(), GETDATE(), 3),
('Caba�a 7 Fusagasug�', 'Sala de estar con sof� cama y TV, ba�o, habitaci�n con cama doble y cama sencilla, cocineta equipada, nevera y terraza comedor.', 'Caba�a', 4, 1, 1, 0, 1, 1, 1, 1, 1, 1, 90000, 120000, 1, GETDATE(), GETDATE(), 3),
('Caba�a 8 Fusagasug�', 'Sala de estar con sof� cama y TV, ba�o, habitaci�n con cama doble y cama sencilla, cocineta equipada, nevera y terraza comedor.', 'Caba�a', 4, 1, 1, 0, 1, 1, 1, 1, 1, 1, 90000, 120000, 1, GETDATE(), GETDATE(), 3);

 --Alojamientos para Chinchin�
INSERT INTO [dbo].[Alojamientos] (
    [Nombre], [Descripcion], [TipoAlojamiento], [CapacidadMaximaPersonas],
    [CamasDobles], [CamasSencillas], [Camarotes], [TieneBanoPrivado], 
    [TieneNevera], [TieneTelevisor], [TieneCocinetaEquipada], 
    [TieneSalaEstar], [TieneTerraza], [TarifaDiaOrdinario], [TarifaDiaEspecial],
    [Activo], [FechaCreacion], [FechaActualizacion], [UbicacionId]
)
VALUES
 --Alojamientos numerados
('Alojamiento 1 Chinchin�', '2 habitaciones: 1 con 2 camas sencillas + 2 auxiliares, otra con cama doble y cama sencilla. Cocineta, ba�o y televisor.', 'Alojamiento', 7, 1, 5, 0, 1, 1, 1, 1, 0, 0, 70000, 90000, 1, GETDATE(), GETDATE(), 4),
('Alojamiento 2 Chinchin�', '2 habitaciones: 1 con cama doble + auxiliar doble, otra con 2 camas sencillas + 2 auxiliares. Cocineta, ba�o y televisor.', 'Alojamiento', 8, 2, 4, 0, 1, 1, 1, 1, 0, 0, 70000, 90000, 1, GETDATE(), GETDATE(), 4),
('Alojamiento 4 Chinchin�', '1 habitaci�n con cama doble y cama sencilla. Cocineta, ba�o y televisor.', 'Alojamiento', 3, 1, 1, 0, 1, 1, 1, 0, 0, 0, 70000, 90000, 1, GETDATE(), GETDATE(), 4),

 --Caba�as
('Caba�a A Chinchin�', 'Cocineta, dos ba�os, sala comedor, TV y 2 habitaciones: 1 con cama doble, otra con 2 camas sencillas + 2 auxiliares.', 'Caba�a', 8, 1, 4, 0, 1, 1, 1, 1, 1, 0, 90000, 120000, 1, GETDATE(), GETDATE(), 4),
('Caba�a B-5 Chinchin�', 'Cocineta, ba�o, sala con sof�, TV, habitaci�n con cama doble y cama sencilla.', 'Caba�a', 4, 1, 1, 0, 1, 1, 1, 1, 1, 0, 90000, 120000, 1, GETDATE(), GETDATE(), 4),
('Caba�a B-6 Chinchin�', 'Cocineta, ba�o, sala con sof�, TV, habitaci�n con cama doble y cama sencilla.', 'Caba�a', 4, 1, 1, 0, 1, 1, 1, 1, 1, 0, 90000, 120000, 1, GETDATE(), GETDATE(), 4);

 --Alojamientos para Palmira
INSERT INTO [dbo].[Alojamientos] (
    [Nombre], [Descripcion], [TipoAlojamiento], [CapacidadMaximaPersonas],
    [CamasDobles], [CamasSencillas], [Camarotes], [TieneBanoPrivado], 
    [TieneNevera], [TieneTelevisor], [TieneCocinetaEquipada], 
    [TieneSalaEstar], [TieneTerraza], [TarifaDiaOrdinario], [TarifaDiaEspecial],
    [Activo], [FechaCreacion], [FechaActualizacion], [UbicacionId]
)
VALUES
('Alojamiento 1 Palmira', '1 habitaci�n con cama doble y camarote. Televisor, ba�o, cocineta con nevera, comedor.', 'Alojamiento', 4, 1, 0, 1, 1, 1, 1, 1, 0, 1, 70000, 90000, 1, GETDATE(), GETDATE(), 5),
('Alojamiento 2 Palmira', '1 habitaci�n con cama doble y camarote. Televisor, ba�o y cocineta con nevera, comedor.', 'Alojamiento', 4, 1, 0, 1, 1, 1, 1, 1, 0, 1, 70000, 90000, 1, GETDATE(), GETDATE(), 5),
('Alojamiento 3 Palmira', '2 habitaciones: 1 con cama doble y camarote, otra con 2 camarotes. Sala de estar con TV, ba�o y cocineta.', 'Alojamiento', 6, 1, 0, 3, 1, 1, 1, 1, 1, 0, 90000, 120000, 1, GETDATE(), GETDATE(), 5),
('Alojamiento 4 Palmira', '2 habitaciones: 1 con cama doble y camarote, otra con 2 camarotes. Sala de estar con TV, ba�o y cocineta.', 'Alojamiento', 6, 1, 0, 3, 1, 1, 1, 1, 1, 0, 90000, 120000, 1, GETDATE(), GETDATE(), 5);

 --Alojamientos para Santa Fe de Antioquia
INSERT INTO [dbo].[Alojamientos] (
    [Nombre], [Descripcion], [TipoAlojamiento], [CapacidadMaximaPersonas],
    [CamasDobles], [CamasSencillas], [Camarotes], [TieneBanoPrivado], 
    [TieneNevera], [TieneTelevisor], [TieneCocinetaEquipada], 
    [TieneSalaEstar], [TieneTerraza], [TarifaDiaOrdinario], [TarifaDiaEspecial],
    [Activo], [FechaCreacion], [FechaActualizacion], [UbicacionId]
)
VALUES
 --Alojamientos tradicionales
('Alojamiento 1 Manguruma', '1 habitaci�n con cama doble y camarote. Ba�o y terraza. Televisor.', 'Alojamiento', 4, 1, 0, 1, 1, 0, 1, 0, 0, 1, 70000, 90000, 1, GETDATE(), GETDATE(), 6),
('Alojamiento 2 Manguruma', '1 habitaci�n con cama doble, camarote y sof�-cama. Ba�o y terraza. Televisor.', 'Alojamiento', 5, 1, 0, 1, 1, 0, 1, 0, 0, 1, 70000, 90000, 1, GETDATE(), GETDATE(), 6),
('Alojamiento 3 Manguruma', '1 habitaci�n con cama doble, camarote y sof�-cama. Ba�o y terraza. Televisor.', 'Alojamiento', 5, 1, 0, 1, 1, 0, 1, 0, 0, 1, 70000, 90000, 1, GETDATE(), GETDATE(), 6),

 --Bloque nuevo (8 alojamientos)
('Alojamiento N1 Manguruma', '1 habitaci�n con 2 camas gemelas y camarote; ba�o, terraza-comedor y cocina. Nevera y televisor.', 'Alojamiento', 5, 0, 2, 1, 1, 1, 1, 1, 1, 1, 70000, 90000, 1, GETDATE(), GETDATE(), 6),
('Alojamiento N2 Manguruma', '1 habitaci�n con 2 camas gemelas y camarote; ba�o, terraza-comedor y cocina. Nevera y televisor.', 'Alojamiento', 5, 0, 2, 1, 1, 1, 1, 1, 1, 1, 70000, 90000, 1, GETDATE(), GETDATE(), 6),
('Alojamiento N3 Manguruma', '1 habitaci�n con 2 camas gemelas y camarote; ba�o, terraza-comedor y cocina. Nevera y televisor.', 'Alojamiento', 5, 0, 2, 1, 1, 1, 1, 1, 1, 1, 70000, 90000, 1, GETDATE(), GETDATE(), 6),
('Alojamiento N4 Manguruma', '1 habitaci�n con 2 camas gemelas y camarote; ba�o, terraza-comedor y cocina. Nevera y televisor.', 'Alojamiento', 5, 0, 2, 1, 1, 1, 1, 1, 1, 1, 70000, 90000, 1, GETDATE(), GETDATE(), 6),
('Alojamiento N5 Manguruma', '1 habitaci�n con 2 camas gemelas y camarote; ba�o, terraza-comedor y cocina. Nevera y televisor.', 'Alojamiento', 5, 0, 2, 1, 1, 1, 1, 1, 1, 1, 70000, 90000, 1, GETDATE(), GETDATE(), 6),
('Alojamiento N6 Manguruma', '1 habitaci�n con 2 camas gemelas y camarote; ba�o, terraza-comedor y cocina. Nevera y televisor.', 'Alojamiento', 5, 0, 2, 1, 1, 1, 1, 1, 1, 1, 70000, 90000, 1, GETDATE(), GETDATE(), 6),
('Alojamiento N7 Manguruma', '1 habitaci�n con 2 camas gemelas y camarote; ba�o, terraza-comedor y cocina. Nevera y televisor.', 'Alojamiento', 5, 0, 2, 1, 1, 1, 1, 1, 1, 1, 70000, 90000, 1, GETDATE(), GETDATE(), 6),
('Alojamiento N8 Manguruma', '1 habitaci�n con 2 camas gemelas y camarote; ba�o, terraza-comedor y cocina. Nevera y televisor.', 'Alojamiento', 5, 0, 2, 1, 1, 1, 1, 1, 1, 1, 70000, 90000, 1, GETDATE(), GETDATE(), 6);


 --Alojamientos para Bogot�
INSERT INTO [dbo].[Alojamientos] (
    [Nombre], [Descripcion], [TipoAlojamiento], [CapacidadMaximaPersonas],
    [CamasDobles], [CamasSencillas], [Camarotes], [TieneBanoPrivado], 
    [TieneNevera], [TieneTelevisor], [TieneCocinetaEquipada], 
    [TieneSalaEstar], [TieneTerraza], [TarifaDiaOrdinario], [TarifaDiaEspecial],
    [Activo], [FechaCreacion], [FechaActualizacion], [UbicacionId]
)
VALUES
('Habitaci�n 1 Bogot�', 'Habitaci�n est�ndar con servicios premium de la sede urbana', 'Habitaci�n', 2, 1, 0, 0, 1, 1, 1, 0, 0, 0, 80000, 100000, 1, GETDATE(), GETDATE(), 7),
('Habitaci�n 2 Bogot�', 'Habitaci�n est�ndar con servicios premium de la sede urbana', 'Habitaci�n', 2, 1, 0, 0, 1, 1, 1, 0, 0, 0, 80000, 100000, 1, GETDATE(), GETDATE(), 7),
('Habitaci�n 3 Bogot�', 'Habitaci�n est�ndar con servicios premium de la sede urbana', 'Habitaci�n', 2, 1, 0, 0, 1, 1, 1, 0, 0, 0, 80000, 100000, 1, GETDATE(), GETDATE(), 7),
('Habitaci�n 4 Bogot�', 'Habitaci�n est�ndar con servicios premium de la sede urbana', 'Habitaci�n', 2, 1, 0, 0, 1, 1, 1, 0, 0, 0, 80000, 100000, 1, GETDATE(), GETDATE(), 7);

 --Alojamientos para Medell�n
INSERT INTO [dbo].[Alojamientos] (
    [Nombre], [Descripcion], [TipoAlojamiento], [CapacidadMaximaPersonas],
    [CamasDobles], [CamasSencillas], [Camarotes], [TieneBanoPrivado], 
    [TieneNevera], [TieneTelevisor], [TieneCocinetaEquipada], 
    [TieneSalaEstar], [TieneTerraza], [TarifaDiaOrdinario], [TarifaDiaEspecial],
    [Activo], [FechaCreacion], [FechaActualizacion], [UbicacionId]
)
VALUES
('Habitaci�n 1 Medell�n', 'Habitaci�n con 2 camas sencillas y ba�o privado', 'Habitaci�n', 2, 0, 2, 0, 1, 0, 0, 0, 0, 0, 63000, 75000, 1, GETDATE(), GETDATE(), 8),
('Habitaci�n 2 Medell�n', 'Habitaci�n con 2 camas sencillas', 'Habitaci�n', 2, 0, 2, 0, 0, 0, 0, 0, 0, 0, 63000, 75000, 1, GETDATE(), GETDATE(), 8),
('Habitaci�n 3 Medell�n', 'Habitaci�n con 2 camas sencillas', 'Habitaci�n', 2, 0, 2, 0, 0, 0, 0, 0, 0, 0, 63000, 75000, 1, GETDATE(), GETDATE(), 8),
('Habitaci�n 4 Medell�n', 'Habitaci�n con 2 camas sencillas', 'Habitaci�n', 2, 0, 2, 0, 0, 0, 0, 0, 0, 0, 63000, 75000, 1, GETDATE(), GETDATE(), 8),
('Habitaci�n 5 Medell�n', 'Habitaci�n con 1 cama sencilla y ba�o privado', 'Habitaci�n', 1, 0, 1, 0, 1, 0, 0, 0, 0, 0, 63000, 75000, 1, GETDATE(), GETDATE(), 8);

 --Alojamientos para Santa Marta
INSERT INTO [dbo].[Alojamientos] (
    [Nombre], [Descripcion], [TipoAlojamiento], [CapacidadMaximaPersonas],
    [CamasDobles], [CamasSencillas], [Camarotes], [TieneBanoPrivado], 
    [TieneNevera], [TieneTelevisor], [TieneCocinetaEquipada], 
    [TieneSalaEstar], [TieneTerraza], [TarifaDiaOrdinario], [TarifaDiaEspecial],
    [Activo], [FechaCreacion], [FechaActualizacion], [UbicacionId]
)
VALUES
('Apartamento 202 Santa Marta', 'Tiene sala comedor, cocina, 2 ba�os, tres habitaciones y sitio para parqueo. Capacidad m�xima: 8 personas.', 'Apartamento', 8, 2, 4, 0, 1, 1, 1, 1, 1, 1, 103000, 143000, 1, GETDATE(), GETDATE(), 9),
('Apartamento 301 Santa Marta', 'Tiene sala comedor, cocina, 1 ba�o, dos habitaciones y sitio para parqueo. Capacidad m�xima: 6 personas.', 'Apartamento', 6, 1, 4, 0, 1, 1, 1, 1, 1, 0, 89000, 124000, 1, GETDATE(), GETDATE(), 9),
('Apartamento 401 Santa Marta', 'Tiene sala comedor, cocina, 1 ba�o, dos habitaciones y sitio para parqueo. Capacidad m�xima: 6 personas.', 'Apartamento', 6, 1, 4, 0, 1, 1, 1, 1, 1, 0, 89000, 124000, 1, GETDATE(), GETDATE(), 9);

 --Tarifas generales para sedes recreativas
INSERT INTO [dbo].[Tarifas] (
    [TipoRegla], [AlojamientoId], [UbicacionId], [TipoTemporada], 
    [ValorBase], [MinPersonas], [MaxPersonas], [ValorPorPersonaAdicional],
    [AplicaLunesJueves], [Activo], [FechaCreacion]
)
VALUES
 --Tarifa general para habitaciones individuales (1-4 personas)
('General', NULL, 2, 'Baja', 70000, 1, 4, 16000, 0, 1, GETDATE()),
('General', NULL, 2, 'Alta', 90000, 1, 4, 16000, 0, 1, GETDATE()),
('Especial', NULL, 2, 'Baja', 27000, 1, 4, 11000, 1, 1, GETDATE()),

 --Tarifa general para caba�as (1-4 personas)
('General', NULL, 3, 'Baja', 90000, 1, 4, 16000, 0, 1, GETDATE()),
('General', NULL, 3, 'Alta', 120000, 1, 4, 16000, 0, 1, GETDATE()),
('Especial', NULL, 3, 'Baja', 37000, 1, 4, 11000, 1, 1, GETDATE()),

 --Tarifa para apartamentos en Medell�n
('Espec�fico', NULL, 8, 'Baja', 63000, 1, 1, 0, 0, 1, GETDATE()),
('Espec�fico', NULL, 8, 'Baja', 75000, 2, 2, 0, 0, 1, GETDATE()),

 --Tarifa para apartamentos en Santa Marta
('Espec�fico', NULL, 9, 'Baja', 89000, 1, 6, 0, 0, 1, GETDATE()),
('Espec�fico', NULL, 9, 'Alta', 124000, 1, 6, 0, 0, 1, GETDATE()),
('Espec�fico', NULL, 9, 'Baja', 103000, 1, 8, 0, 0, 1, GETDATE()),
('Espec�fico', NULL, 9, 'Alta', 143000, 1, 8, 0, 0, 1, GETDATE()),

 --Tarifa para servicio de lavander�a
('Servicio', NULL, NULL, 'General', 18000, 1, 1, 0, 0, 1, GETDATE());

 --Tarifas espec�ficas por alojamiento 
INSERT INTO [dbo].[Tarifas] (
    [TipoRegla], [AlojamientoId], [TipoTemporada], 
    [ValorBase], [MinPersonas], [MaxPersonas], [ValorPorPersonaAdicional],
    [AplicaLunesJueves], [Activo], [FechaCreacion]
)
SELECT 
    'Espec�fico', 
    AlojamientoId,
    CASE WHEN Nombre LIKE '%Caba�a%' THEN 'Alta' ELSE 'Baja' END,
    CASE 
        WHEN Nombre LIKE '%Caba�a%' THEN 120000
        WHEN Nombre LIKE '%Habitaci�n%' THEN 70000
        ELSE 90000
    END,
    1,
    CapacidadMaximaPersonas,
    16000,
    0,
    1,
    GETDATE()
FROM [dbo].[Alojamientos]
WHERE UbicacionId IN (2, 3, 4, 5, 6);