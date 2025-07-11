---- Primero insertamos todas las ubicaciones
INSERT INTO [dbo].[Ubicaciones] ([Nombre], [Descripcion], [TipoUbicacion], [Activa], [FechaCreacion])
VALUES 
('Villeta', 'Sede recreativa con 8 habitaciones tipo alcoba, cada una con cama doble y camarote. Capacidad máxima: 32 personas. Cuenta con nevera, televisor y terraza cubierta en cada habitación.', 'Sede Recreativa', 1, GETDATE()),
('El Placer - Fusagasugá', 'Sede con 4 alojamientos numerados y 4 cabañas. Capacidad total: 34 personas. Los alojamientos tienen habitaciones con combinaciones de camas dobles y sencillas, mientras las cabañas incluyen sala de estar, cocineta equipada y terraza comedor.', 'Sede Recreativa', 1, GETDATE()),
('Gonzalo Morante - Chinchiná', 'Sede con alojamientos numerados y cabañas tipo A y B. Capacidad total: 30 personas. Algunos alojamientos tienen camas auxiliares adicionales.', 'Sede Recreativa', 1, GETDATE()),
('Tablones - Palmira', 'Sede con 4 alojamientos numerados, cada uno con combinaciones de camas dobles y camarotes. Capacidad total: 24 personas. Incluyen televisor, cocineta y sala de estar.', 'Sede Recreativa', 1, GETDATE()),
('Manguruma - Santa Fe de Antioquia', 'Sede con alojamientos tradicionales y nuevo bloque. Capacidad total: 46 personas. Los nuevos alojamientos tienen dos camas gemelas y camarote cada uno.', 'Sede Recreativa', 1, GETDATE()),
('Federman - Bogotá', 'Sede urbana con 4 habitaciones para alojamiento y amplios servicios recreativos como zona húmeda, gimnasio, salas de entretenimiento y actividades programadas.', 'Sede Urbana', 1, GETDATE()),
('Suramericana - Medellín', 'Conjunto de apartamentos en Medellín con 5 habitaciones distribuidas en diferentes configuraciones de camas sencillas y baños privados.', 'Apartamento', 1, GETDATE()),
('El Rodadero - Santa Marta', 'Conjunto de apartamentos en Santa Marta con diferentes capacidades (6-8 personas). Incluyen sala-comedor, cocina, baños y sitio de parqueo.', 'Apartamento', 1, GETDATE());

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
('Habitación 1 Villeta', 'Alcoba con cama doble y camarote (capacidad 4 personas). Incluye nevera, televisor y terraza cubierta.', 'Habitación', 4, 1, 0, 1, 1, 1, 1, 0, 0, 1, 70000, 90000, 1, GETDATE(), GETDATE(), 2),
('Habitación 2 Villeta', 'Alcoba con cama doble y camarote (capacidad 4 personas). Incluye nevera, televisor y terraza cubierta.', 'Habitación', 4, 1, 0, 1, 1, 1, 1, 0, 0, 1, 70000, 90000, 1, GETDATE(), GETDATE(), 2),
('Habitación 3 Villeta', 'Alcoba con cama doble y camarote (capacidad 4 personas). Incluye nevera, televisor y terraza cubierta.', 'Habitación', 4, 1, 0, 1, 1, 1, 1, 0, 0, 1, 70000, 90000, 1, GETDATE(), GETDATE(), 2),
('Habitación 4 Villeta', 'Alcoba con cama doble y camarote (capacidad 4 personas). Incluye nevera, televisor y terraza cubierta.', 'Habitación', 4, 1, 0, 1, 1, 1, 1, 0, 0, 1, 70000, 90000, 1, GETDATE(), GETDATE(), 2),
('Habitación 5 Villeta', 'Alcoba con cama doble y camarote (capacidad 4 personas). Incluye nevera, televisor y terraza cubierta.', 'Habitación', 4, 1, 0, 1, 1, 1, 1, 0, 0, 1, 70000, 90000, 1, GETDATE(), GETDATE(), 2),
('Habitación 6 Villeta', 'Alcoba con cama doble y camarote (capacidad 4 personas). Incluye nevera, televisor y terraza cubierta.', 'Habitación', 4, 1, 0, 1, 1, 1, 1, 0, 0, 1, 70000, 90000, 1, GETDATE(), GETDATE(), 2),
('Habitación 7 Villeta', 'Alcoba con cama doble y camarote (capacidad 4 personas). Incluye nevera, televisor y terraza cubierta.', 'Habitación', 4, 1, 0, 1, 1, 1, 1, 0, 0, 1, 70000, 90000, 1, GETDATE(), GETDATE(), 2),
('Habitación 8 Villeta', 'Alcoba con cama doble y camarote (capacidad 4 personas). Incluye nevera, televisor y terraza cubierta.', 'Habitación', 4, 1, 0, 1, 1, 1, 1, 0, 0, 1, 70000, 90000, 1, GETDATE(), GETDATE(), 2);

 --Alojamientos para Fusagasugá
INSERT INTO [dbo].[Alojamientos] (
    [Nombre], [Descripcion], [TipoAlojamiento], [CapacidadMaximaPersonas],
    [CamasDobles], [CamasSencillas], [Camarotes], [TieneBanoPrivado], 
    [TieneNevera], [TieneTelevisor], [TieneCocinetaEquipada], 
    [TieneSalaEstar], [TieneTerraza], [TarifaDiaOrdinario], [TarifaDiaEspecial],
    [Activo], [FechaCreacion], [FechaActualizacion], [UbicacionId]
)
VALUES
 --Alojamientos numerados
('Alojamiento 1 Fusagasugá', '2 habitaciones: 1 con cama doble y cama sencilla, otra con cama sencilla. Baño y televisor.', 'Alojamiento', 4, 1, 2, 0, 1, 0, 1, 0, 0, 0, 70000, 90000, 1, GETDATE(), GETDATE(), 3),
('Alojamiento 2 Fusagasugá', '2 habitaciones: 1 con cama doble, otra con 4 camas sencillas. Baño y televisor.', 'Alojamiento', 6, 1, 4, 0, 1, 0, 1, 0, 0, 0, 70000, 90000, 1, GETDATE(), GETDATE(), 3),
('Alojamiento 3 Fusagasugá', '1 habitación con cama doble y 2 camas sencillas. Baño y televisor.', 'Alojamiento', 4, 1, 2, 0, 1, 0, 1, 0, 0, 0, 70000, 90000, 1, GETDATE(), GETDATE(), 3),
('Alojamiento 4 Fusagasugá', '2 habitaciones: 1 con cama doble y cama sencilla, otra con cama sencilla. Baño y televisor.', 'Alojamiento', 4, 1, 2, 0, 1, 0, 1, 0, 0, 0, 70000, 90000, 1, GETDATE(), GETDATE(), 3),

 --Cabañas (5-8)
('Cabaña 5 Fusagasugá', 'Sala de estar con sofá cama y TV, baño, habitación con cama doble y cama sencilla, cocineta equipada, nevera y terraza comedor.', 'Cabaña', 4, 1, 1, 0, 1, 1, 1, 1, 1, 1, 90000, 120000, 1, GETDATE(), GETDATE(), 3),
('Cabaña 6 Fusagasugá', 'Sala de estar con sofá cama y TV, baño, habitación con cama doble y cama sencilla, cocineta equipada, nevera y terraza comedor.', 'Cabaña', 4, 1, 1, 0, 1, 1, 1, 1, 1, 1, 90000, 120000, 1, GETDATE(), GETDATE(), 3),
('Cabaña 7 Fusagasugá', 'Sala de estar con sofá cama y TV, baño, habitación con cama doble y cama sencilla, cocineta equipada, nevera y terraza comedor.', 'Cabaña', 4, 1, 1, 0, 1, 1, 1, 1, 1, 1, 90000, 120000, 1, GETDATE(), GETDATE(), 3),
('Cabaña 8 Fusagasugá', 'Sala de estar con sofá cama y TV, baño, habitación con cama doble y cama sencilla, cocineta equipada, nevera y terraza comedor.', 'Cabaña', 4, 1, 1, 0, 1, 1, 1, 1, 1, 1, 90000, 120000, 1, GETDATE(), GETDATE(), 3);

 --Alojamientos para Chinchiná
INSERT INTO [dbo].[Alojamientos] (
    [Nombre], [Descripcion], [TipoAlojamiento], [CapacidadMaximaPersonas],
    [CamasDobles], [CamasSencillas], [Camarotes], [TieneBanoPrivado], 
    [TieneNevera], [TieneTelevisor], [TieneCocinetaEquipada], 
    [TieneSalaEstar], [TieneTerraza], [TarifaDiaOrdinario], [TarifaDiaEspecial],
    [Activo], [FechaCreacion], [FechaActualizacion], [UbicacionId]
)
VALUES
 --Alojamientos numerados
('Alojamiento 1 Chinchiná', '2 habitaciones: 1 con 2 camas sencillas + 2 auxiliares, otra con cama doble y cama sencilla. Cocineta, baño y televisor.', 'Alojamiento', 7, 1, 5, 0, 1, 1, 1, 1, 0, 0, 70000, 90000, 1, GETDATE(), GETDATE(), 4),
('Alojamiento 2 Chinchiná', '2 habitaciones: 1 con cama doble + auxiliar doble, otra con 2 camas sencillas + 2 auxiliares. Cocineta, baño y televisor.', 'Alojamiento', 8, 2, 4, 0, 1, 1, 1, 1, 0, 0, 70000, 90000, 1, GETDATE(), GETDATE(), 4),
('Alojamiento 4 Chinchiná', '1 habitación con cama doble y cama sencilla. Cocineta, baño y televisor.', 'Alojamiento', 3, 1, 1, 0, 1, 1, 1, 0, 0, 0, 70000, 90000, 1, GETDATE(), GETDATE(), 4),

 --Cabañas
('Cabaña A Chinchiná', 'Cocineta, dos baños, sala comedor, TV y 2 habitaciones: 1 con cama doble, otra con 2 camas sencillas + 2 auxiliares.', 'Cabaña', 8, 1, 4, 0, 1, 1, 1, 1, 1, 0, 90000, 120000, 1, GETDATE(), GETDATE(), 4),
('Cabaña B-5 Chinchiná', 'Cocineta, baño, sala con sofá, TV, habitación con cama doble y cama sencilla.', 'Cabaña', 4, 1, 1, 0, 1, 1, 1, 1, 1, 0, 90000, 120000, 1, GETDATE(), GETDATE(), 4),
('Cabaña B-6 Chinchiná', 'Cocineta, baño, sala con sofá, TV, habitación con cama doble y cama sencilla.', 'Cabaña', 4, 1, 1, 0, 1, 1, 1, 1, 1, 0, 90000, 120000, 1, GETDATE(), GETDATE(), 4);

 --Alojamientos para Palmira
INSERT INTO [dbo].[Alojamientos] (
    [Nombre], [Descripcion], [TipoAlojamiento], [CapacidadMaximaPersonas],
    [CamasDobles], [CamasSencillas], [Camarotes], [TieneBanoPrivado], 
    [TieneNevera], [TieneTelevisor], [TieneCocinetaEquipada], 
    [TieneSalaEstar], [TieneTerraza], [TarifaDiaOrdinario], [TarifaDiaEspecial],
    [Activo], [FechaCreacion], [FechaActualizacion], [UbicacionId]
)
VALUES
('Alojamiento 1 Palmira', '1 habitación con cama doble y camarote. Televisor, baño, cocineta con nevera, comedor.', 'Alojamiento', 4, 1, 0, 1, 1, 1, 1, 1, 0, 1, 70000, 90000, 1, GETDATE(), GETDATE(), 5),
('Alojamiento 2 Palmira', '1 habitación con cama doble y camarote. Televisor, baño y cocineta con nevera, comedor.', 'Alojamiento', 4, 1, 0, 1, 1, 1, 1, 1, 0, 1, 70000, 90000, 1, GETDATE(), GETDATE(), 5),
('Alojamiento 3 Palmira', '2 habitaciones: 1 con cama doble y camarote, otra con 2 camarotes. Sala de estar con TV, baño y cocineta.', 'Alojamiento', 6, 1, 0, 3, 1, 1, 1, 1, 1, 0, 90000, 120000, 1, GETDATE(), GETDATE(), 5),
('Alojamiento 4 Palmira', '2 habitaciones: 1 con cama doble y camarote, otra con 2 camarotes. Sala de estar con TV, baño y cocineta.', 'Alojamiento', 6, 1, 0, 3, 1, 1, 1, 1, 1, 0, 90000, 120000, 1, GETDATE(), GETDATE(), 5);

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
('Alojamiento 1 Manguruma', '1 habitación con cama doble y camarote. Baño y terraza. Televisor.', 'Alojamiento', 4, 1, 0, 1, 1, 0, 1, 0, 0, 1, 70000, 90000, 1, GETDATE(), GETDATE(), 6),
('Alojamiento 2 Manguruma', '1 habitación con cama doble, camarote y sofá-cama. Baño y terraza. Televisor.', 'Alojamiento', 5, 1, 0, 1, 1, 0, 1, 0, 0, 1, 70000, 90000, 1, GETDATE(), GETDATE(), 6),
('Alojamiento 3 Manguruma', '1 habitación con cama doble, camarote y sofá-cama. Baño y terraza. Televisor.', 'Alojamiento', 5, 1, 0, 1, 1, 0, 1, 0, 0, 1, 70000, 90000, 1, GETDATE(), GETDATE(), 6),

 --Bloque nuevo (8 alojamientos)
('Alojamiento N1 Manguruma', '1 habitación con 2 camas gemelas y camarote; baño, terraza-comedor y cocina. Nevera y televisor.', 'Alojamiento', 5, 0, 2, 1, 1, 1, 1, 1, 1, 1, 70000, 90000, 1, GETDATE(), GETDATE(), 6),
('Alojamiento N2 Manguruma', '1 habitación con 2 camas gemelas y camarote; baño, terraza-comedor y cocina. Nevera y televisor.', 'Alojamiento', 5, 0, 2, 1, 1, 1, 1, 1, 1, 1, 70000, 90000, 1, GETDATE(), GETDATE(), 6),
('Alojamiento N3 Manguruma', '1 habitación con 2 camas gemelas y camarote; baño, terraza-comedor y cocina. Nevera y televisor.', 'Alojamiento', 5, 0, 2, 1, 1, 1, 1, 1, 1, 1, 70000, 90000, 1, GETDATE(), GETDATE(), 6),
('Alojamiento N4 Manguruma', '1 habitación con 2 camas gemelas y camarote; baño, terraza-comedor y cocina. Nevera y televisor.', 'Alojamiento', 5, 0, 2, 1, 1, 1, 1, 1, 1, 1, 70000, 90000, 1, GETDATE(), GETDATE(), 6),
('Alojamiento N5 Manguruma', '1 habitación con 2 camas gemelas y camarote; baño, terraza-comedor y cocina. Nevera y televisor.', 'Alojamiento', 5, 0, 2, 1, 1, 1, 1, 1, 1, 1, 70000, 90000, 1, GETDATE(), GETDATE(), 6),
('Alojamiento N6 Manguruma', '1 habitación con 2 camas gemelas y camarote; baño, terraza-comedor y cocina. Nevera y televisor.', 'Alojamiento', 5, 0, 2, 1, 1, 1, 1, 1, 1, 1, 70000, 90000, 1, GETDATE(), GETDATE(), 6),
('Alojamiento N7 Manguruma', '1 habitación con 2 camas gemelas y camarote; baño, terraza-comedor y cocina. Nevera y televisor.', 'Alojamiento', 5, 0, 2, 1, 1, 1, 1, 1, 1, 1, 70000, 90000, 1, GETDATE(), GETDATE(), 6),
('Alojamiento N8 Manguruma', '1 habitación con 2 camas gemelas y camarote; baño, terraza-comedor y cocina. Nevera y televisor.', 'Alojamiento', 5, 0, 2, 1, 1, 1, 1, 1, 1, 1, 70000, 90000, 1, GETDATE(), GETDATE(), 6);


 --Alojamientos para Bogotá
INSERT INTO [dbo].[Alojamientos] (
    [Nombre], [Descripcion], [TipoAlojamiento], [CapacidadMaximaPersonas],
    [CamasDobles], [CamasSencillas], [Camarotes], [TieneBanoPrivado], 
    [TieneNevera], [TieneTelevisor], [TieneCocinetaEquipada], 
    [TieneSalaEstar], [TieneTerraza], [TarifaDiaOrdinario], [TarifaDiaEspecial],
    [Activo], [FechaCreacion], [FechaActualizacion], [UbicacionId]
)
VALUES
('Habitación 1 Bogotá', 'Habitación estándar con servicios premium de la sede urbana', 'Habitación', 2, 1, 0, 0, 1, 1, 1, 0, 0, 0, 80000, 100000, 1, GETDATE(), GETDATE(), 7),
('Habitación 2 Bogotá', 'Habitación estándar con servicios premium de la sede urbana', 'Habitación', 2, 1, 0, 0, 1, 1, 1, 0, 0, 0, 80000, 100000, 1, GETDATE(), GETDATE(), 7),
('Habitación 3 Bogotá', 'Habitación estándar con servicios premium de la sede urbana', 'Habitación', 2, 1, 0, 0, 1, 1, 1, 0, 0, 0, 80000, 100000, 1, GETDATE(), GETDATE(), 7),
('Habitación 4 Bogotá', 'Habitación estándar con servicios premium de la sede urbana', 'Habitación', 2, 1, 0, 0, 1, 1, 1, 0, 0, 0, 80000, 100000, 1, GETDATE(), GETDATE(), 7);

 --Alojamientos para Medellín
INSERT INTO [dbo].[Alojamientos] (
    [Nombre], [Descripcion], [TipoAlojamiento], [CapacidadMaximaPersonas],
    [CamasDobles], [CamasSencillas], [Camarotes], [TieneBanoPrivado], 
    [TieneNevera], [TieneTelevisor], [TieneCocinetaEquipada], 
    [TieneSalaEstar], [TieneTerraza], [TarifaDiaOrdinario], [TarifaDiaEspecial],
    [Activo], [FechaCreacion], [FechaActualizacion], [UbicacionId]
)
VALUES
('Habitación 1 Medellín', 'Habitación con 2 camas sencillas y baño privado', 'Habitación', 2, 0, 2, 0, 1, 0, 0, 0, 0, 0, 63000, 75000, 1, GETDATE(), GETDATE(), 8),
('Habitación 2 Medellín', 'Habitación con 2 camas sencillas', 'Habitación', 2, 0, 2, 0, 0, 0, 0, 0, 0, 0, 63000, 75000, 1, GETDATE(), GETDATE(), 8),
('Habitación 3 Medellín', 'Habitación con 2 camas sencillas', 'Habitación', 2, 0, 2, 0, 0, 0, 0, 0, 0, 0, 63000, 75000, 1, GETDATE(), GETDATE(), 8),
('Habitación 4 Medellín', 'Habitación con 2 camas sencillas', 'Habitación', 2, 0, 2, 0, 0, 0, 0, 0, 0, 0, 63000, 75000, 1, GETDATE(), GETDATE(), 8),
('Habitación 5 Medellín', 'Habitación con 1 cama sencilla y baño privado', 'Habitación', 1, 0, 1, 0, 1, 0, 0, 0, 0, 0, 63000, 75000, 1, GETDATE(), GETDATE(), 8);

 --Alojamientos para Santa Marta
INSERT INTO [dbo].[Alojamientos] (
    [Nombre], [Descripcion], [TipoAlojamiento], [CapacidadMaximaPersonas],
    [CamasDobles], [CamasSencillas], [Camarotes], [TieneBanoPrivado], 
    [TieneNevera], [TieneTelevisor], [TieneCocinetaEquipada], 
    [TieneSalaEstar], [TieneTerraza], [TarifaDiaOrdinario], [TarifaDiaEspecial],
    [Activo], [FechaCreacion], [FechaActualizacion], [UbicacionId]
)
VALUES
('Apartamento 202 Santa Marta', 'Tiene sala comedor, cocina, 2 baños, tres habitaciones y sitio para parqueo. Capacidad máxima: 8 personas.', 'Apartamento', 8, 2, 4, 0, 1, 1, 1, 1, 1, 1, 103000, 143000, 1, GETDATE(), GETDATE(), 9),
('Apartamento 301 Santa Marta', 'Tiene sala comedor, cocina, 1 baño, dos habitaciones y sitio para parqueo. Capacidad máxima: 6 personas.', 'Apartamento', 6, 1, 4, 0, 1, 1, 1, 1, 1, 0, 89000, 124000, 1, GETDATE(), GETDATE(), 9),
('Apartamento 401 Santa Marta', 'Tiene sala comedor, cocina, 1 baño, dos habitaciones y sitio para parqueo. Capacidad máxima: 6 personas.', 'Apartamento', 6, 1, 4, 0, 1, 1, 1, 1, 1, 0, 89000, 124000, 1, GETDATE(), GETDATE(), 9);

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

 --Tarifa general para cabañas (1-4 personas)
('General', NULL, 3, 'Baja', 90000, 1, 4, 16000, 0, 1, GETDATE()),
('General', NULL, 3, 'Alta', 120000, 1, 4, 16000, 0, 1, GETDATE()),
('Especial', NULL, 3, 'Baja', 37000, 1, 4, 11000, 1, 1, GETDATE()),

 --Tarifa para apartamentos en Medellín
('Específico', NULL, 8, 'Baja', 63000, 1, 1, 0, 0, 1, GETDATE()),
('Específico', NULL, 8, 'Baja', 75000, 2, 2, 0, 0, 1, GETDATE()),

 --Tarifa para apartamentos en Santa Marta
('Específico', NULL, 9, 'Baja', 89000, 1, 6, 0, 0, 1, GETDATE()),
('Específico', NULL, 9, 'Alta', 124000, 1, 6, 0, 0, 1, GETDATE()),
('Específico', NULL, 9, 'Baja', 103000, 1, 8, 0, 0, 1, GETDATE()),
('Específico', NULL, 9, 'Alta', 143000, 1, 8, 0, 0, 1, GETDATE()),

 --Tarifa para servicio de lavandería
('Servicio', NULL, NULL, 'General', 18000, 1, 1, 0, 0, 1, GETDATE());

 --Tarifas específicas por alojamiento 
INSERT INTO [dbo].[Tarifas] (
    [TipoRegla], [AlojamientoId], [TipoTemporada], 
    [ValorBase], [MinPersonas], [MaxPersonas], [ValorPorPersonaAdicional],
    [AplicaLunesJueves], [Activo], [FechaCreacion]
)
SELECT 
    'Específico', 
    AlojamientoId,
    CASE WHEN Nombre LIKE '%Cabaña%' THEN 'Alta' ELSE 'Baja' END,
    CASE 
        WHEN Nombre LIKE '%Cabaña%' THEN 120000
        WHEN Nombre LIKE '%Habitación%' THEN 70000
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