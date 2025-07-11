using FODUN.Reservas.Data;
using FODUN.Reservas.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FODUN.Reservas.Services
{
    public class ReservaService : IReservaService
    {
        private readonly ApplicationDbContext _context;

        public ReservaService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Métodos para Reservas


        public async Task<IEnumerable<Alojamiento>> BuscarAlojamientosDisponibles(DateTime fechaLlegada, DateTime fechaSalida)
        {
            
            var parametros = new[]
            {
                new SqlParameter("@FechaLlegada", fechaLlegada),
                new SqlParameter("@FechaSalida", fechaSalida)
            };

            
            return await _context.Alojamientos
                .FromSqlRaw("EXEC SP_BuscarHabitacionesDisponiblesPorFechas @FechaLlegada, @FechaSalida", parametros)
                .Include(a => a.Ubicacion)
                .ToListAsync();
        }

        public async Task<IEnumerable<Alojamiento>> BuscarAlojamientosDisponiblesYPersonas(DateTime fechaLlegada, DateTime fechaSalida, int numeroPersonas)
        {
            var parametros = new[]
            {
                new SqlParameter("@FechaLlegada", fechaLlegada),
                new SqlParameter("@FechaSalida", fechaSalida),
                new SqlParameter("@NumeroPersonas", numeroPersonas)
            };

            var alojamientosDelSP = await _context.Alojamientos
                .FromSqlRaw("EXEC SP_BuscarHabitacionesDisponiblesPorFechasYPersonas @FechaLlegada, @FechaSalida, @NumeroPersonas", parametros) // Nombre del SP corregido aquí
                .ToListAsync();

            return alojamientosDelSP;
        }


        public async Task<(decimal valorTotal, string mensajeError)> CalcularTarifaTotalReserva(int alojamientoId, DateTime fechaLlegada, DateTime fechaSalida, int numeroPersonas, int numeroUnidadesReservadas, bool incluyeLavanderia, int numeroAcompanantesVisitaDia)
        {
            try
            {
                var alojamiento = await _context.Alojamientos
                                                .Include(a => a.Tarifas) 
                                                .FirstOrDefaultAsync(a => a.AlojamientoId == alojamientoId);

                if (alojamiento == null)
                {
                    return (0, "Alojamiento no encontrado.");
                }

                if (fechaLlegada >= fechaSalida)
                {
                    return (0, "La fecha de llegada debe ser anterior a la fecha de salida.");
                }

                if (numeroPersonas <= 0)
                {
                    return (0, "El número de personas debe ser al menos 1.");
                }

                if (numeroUnidadesReservadas <= 0)
                {
                    return (0, "El número de unidades a reservar debe ser al menos 1.");
                }

                decimal totalCalculado = 0;
                TimeSpan duracionReserva = fechaSalida - fechaLlegada;

                for (DateTime diaActual = fechaLlegada; diaActual < fechaSalida; diaActual = diaActual.AddDays(1))
                {
                    decimal tarifaDiariaBase = 0;
                    Tarifa? tarifaAplicable = null;

                    // Primero, determinar si es fin de semana (sábado o domingo)
                    bool isFinDeSemana = (diaActual.DayOfWeek == DayOfWeek.Saturday || diaActual.DayOfWeek == DayOfWeek.Sunday);

                    if (isFinDeSemana)
                    {
                        tarifaAplicable = alojamiento.Tarifas.FirstOrDefault(t => t.TipoTemporada == "Especial" || t.TipoTemporada == "FinDeSemana");
                    }

                    if (tarifaAplicable == null)
                    {
                        tarifaAplicable = alojamiento.Tarifas.FirstOrDefault(t => t.TipoTemporada == "Ordinaria" || t.TipoTemporada == "Semana");
                    }

                    // 3. Asignar la tarifa diaria base
                    if (tarifaAplicable != null)
                    {
                        tarifaDiariaBase = tarifaAplicable.ValorBase;
                    }
                    else
                    {
                        if (isFinDeSemana)
                        {
                            tarifaDiariaBase = (decimal)alojamiento.TarifaDiaEspecial;
                        }
                        else
                        {
                            tarifaDiariaBase = (decimal)alojamiento.TarifaDiaOrdinario;
                        }
                    }

                    // Calcular costo base por día por número de unidades
                    decimal costoDia = tarifaDiariaBase * numeroUnidadesReservadas;

                    // Calcular costo adicional por personas si excede la capacidad base de la tarifa
                    if (tarifaAplicable != null && numeroPersonas > tarifaAplicable.MinPersonas && tarifaAplicable.ValorPorPersonaAdicional.HasValue)
                    {
                        int personasAdicionales = numeroPersonas - tarifaAplicable.MinPersonas;
                        costoDia += personasAdicionales * tarifaAplicable.ValorPorPersonaAdicional.Value * numeroUnidadesReservadas;
                    }
                    // Si el número de personas excede la capacidad máxima del Alojamiento
                    else if (numeroPersonas > alojamiento.CapacidadMaximaPersonas)
                    {
                    }

                    totalCalculado += costoDia;
                }

                // Añadir costo de lavandería si aplica 
                if (incluyeLavanderia)
                {
                    totalCalculado += 50000; 
                }

                // Añadir costo por acompañantes de visita día 
                if (numeroAcompanantesVisitaDia > 0)
                {
                    totalCalculado += numeroAcompanantesVisitaDia * 10000; 
                }

                // Redondear el valor total a un número entero
                return (Math.Round(totalCalculado), string.Empty);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al calcular la tarifa: {ex.Message}");
                return (0, $"Ocurrió un error al calcular la tarifa: {ex.Message}");
            }
        }

        public async Task<Reserva> CrearReserva(Reserva reserva)
        {
            // Validar que las fechas sean lógicas
            if (reserva.FechaInicio >= reserva.FechaFin)
            {
                throw new ArgumentException("La fecha de inicio debe ser anterior a la fecha de fin.");
            }

            // Validar disponibilidad antes de crear
            if (await ExisteConflictoReserva(reserva.AlojamientoId, reserva.FechaInicio, reserva.FechaFin))
            {
                throw new InvalidOperationException("El alojamiento no está disponible para las fechas seleccionadas.");
            }

            _context.Reservas.Add(reserva);
            await _context.SaveChangesAsync();
            return reserva;
        }

        public async Task<Reserva> GetReservaById(int id)
        {
            return await _context.Reservas
                                 .Include(r => r.ApplicationUser)
                                 .Include(r => r.Alojamiento)
                                     .ThenInclude(a => a.Ubicacion) 
                                 .FirstOrDefaultAsync(r => r.ReservaId == id);
        }

        public async Task<IEnumerable<Reserva>> GetReservasByUserId(int userId)
        {
            return await _context.Reservas
                                 .Include(r => r.Alojamiento)
                                     .ThenInclude(a => a.Ubicacion)
                                 .Where(r => r.ApplicationUserId == userId)
                                 .ToListAsync();
        }


        public async Task<IEnumerable<Reserva>> GetAllReservas()
        {
            var connection = _context.Database.GetDbConnection();
            return await _context.Reservas
                                 .Include(r => r.Alojamiento)
                                 .Include(r => r.ApplicationUser)
                                 .ToListAsync();
        }

        public async Task<bool> ActualizarReserva(Reserva reserva)
        {
            // Validar que las fechas sean lógicas
            if (reserva.FechaInicio >= reserva.FechaFin)
            {
                throw new ArgumentException("La fecha de inicio debe ser anterior a la fecha de fin.");
            }

            // Validar disponibilidad excluyendo la reserva que se está editando
            if (await ExisteConflictoReserva(reserva.AlojamientoId, reserva.FechaInicio, reserva.FechaFin, reserva.ReservaId))
            {
                throw new InvalidOperationException("El alojamiento no está disponible para las fechas seleccionadas.");
            }

            _context.Entry(reserva).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Reservas.Any(e => e.ReservaId == reserva.ReservaId))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<bool> EliminarReserva(int id)
        {
            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva == null)
            {
                return false;
            }
            _context.Reservas.Remove(reserva);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExisteConflictoReserva(int alojamientoId, DateTime fechaInicio, DateTime fechaFin, int? reservaIdToExclude = null)
        {
            var conflictos = await _context.Reservas
                .Where(r => r.AlojamientoId == alojamientoId &&
                            r.EstadoReserva != "Cancelada" && // Excluir reservas canceladas
                            ((fechaInicio < r.FechaFin && fechaFin > r.FechaInicio))) // Hay superposición de fechas
                .ToListAsync();

            if (reservaIdToExclude.HasValue)
            {
                conflictos = conflictos.Where(r => r.ReservaId != reservaIdToExclude.Value).ToList();
            }

            return conflictos.Any();
        }


        // Métodos CRUD para Ubicaciones
        public async Task<IEnumerable<Ubicacion>> GetAllUbicaciones()
        {
            return await _context.Ubicaciones.ToListAsync();
        }

        public async Task<Ubicacion> GetUbicacionById(int id)
        {
            return await _context.Ubicaciones.FindAsync(id);
        }

        public async Task<Ubicacion> CreateUbicacion(Ubicacion ubicacion)
        {
            _context.Ubicaciones.Add(ubicacion);
            await _context.SaveChangesAsync();
            return ubicacion;
        }

        public async Task<bool> UpdateUbicacion(Ubicacion ubicacion)
        {
            _context.Entry(ubicacion).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Ubicaciones.Any(e => e.UbicacionId == ubicacion.UbicacionId))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<bool> DeleteUbicacion(int id)
        {
            var ubicacion = await _context.Ubicaciones.FindAsync(id);
            if (ubicacion == null)
            {
                return false;
            }
            _context.Ubicaciones.Remove(ubicacion);
            await _context.SaveChangesAsync();
            return true;
        }

        // Métodos CRUD para Alojamientos
        public async Task<IEnumerable<Alojamiento>> GetAllAlojamientos()
        {
            return await _context.Alojamientos.Include(a => a.Ubicacion).ToListAsync();
        }

        public async Task<Alojamiento> GetAlojamientoById(int id)
        {
            // Incluye Ubicacion y Tarifas para que estén disponibles al calcular la tarifa
            return await _context.Alojamientos
                                 .Include(a => a.Ubicacion)
                                 .Include(a => a.Tarifas)
                                 .FirstOrDefaultAsync(a => a.AlojamientoId == id);
        }


        public async Task<Alojamiento> CreateAlojamiento(Alojamiento alojamiento)
        {
            alojamiento.FechaCreacion = DateTime.Now;
            alojamiento.FechaActualizacion = DateTime.Now;
            _context.Alojamientos.Add(alojamiento);
            await _context.SaveChangesAsync();
            return alojamiento;
        }

        public async Task<bool> UpdateAlojamiento(Alojamiento alojamiento)
        {
            alojamiento.FechaActualizacion = DateTime.Now;
            _context.Entry(alojamiento).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Alojamientos.Any(e => e.AlojamientoId == alojamiento.AlojamientoId))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<bool> DeleteAlojamiento(int id)
        {
            var alojamiento = await _context.Alojamientos.FindAsync(id);
            if (alojamiento == null)
            {
                return false;
            }
            _context.Alojamientos.Remove(alojamiento);
            await _context.SaveChangesAsync();
            return true;
        }

        // Métodos CRUD para Tarifas
        public async Task<IEnumerable<Tarifa>> GetAllTarifas()
        {
            return await _context.Tarifas.Include(t => t.Alojamiento).ToListAsync();
        }

        public async Task<Tarifa> GetTarifaById(int id)
        {
            return await _context.Tarifas.Include(t => t.Alojamiento).FirstOrDefaultAsync(t => t.TarifaId == id);
        }

        public async Task<Tarifa> CreateTarifa(Tarifa tarifa)
        {
            tarifa.FechaCreacion = DateTime.Now;
            tarifa.FechaActualizacion = DateTime.Now;
            _context.Tarifas.Add(tarifa);
            await _context.SaveChangesAsync();
            return tarifa;
        }

        public async Task<bool> UpdateTarifa(Tarifa tarifa)
        {
            tarifa.FechaActualizacion = DateTime.Now;
            _context.Entry(tarifa).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Tarifas.Any(e => e.TarifaId == tarifa.TarifaId))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<bool> DeleteTarifa(int id)
        {
            var tarifa = await _context.Tarifas.FindAsync(id);
            if (tarifa == null)
            {
                return false;
            }
            _context.Tarifas.Remove(tarifa);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}