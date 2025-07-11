// Services/IReservaService.cs
using FODUN.Reservas.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FODUN.Reservas.Services
{
    public interface IReservaService
    {
        Task<IEnumerable<Alojamiento>> BuscarAlojamientosDisponibles(DateTime fechaLlegada, DateTime fechaSalida);
        Task<IEnumerable<Alojamiento>> BuscarAlojamientosDisponiblesYPersonas(DateTime fechaLlegada, DateTime fechaSalida, int numeroPersonas);
        Task<(decimal valorTotal, string mensajeError)> CalcularTarifaTotalReserva(int alojamientoId, DateTime fechaLlegada, DateTime fechaSalida, int numeroPersonas, int numeroUnidadesReservadas, bool incluyeLavanderia, int numeroAcompanantesVisitaDia);
        Task<Reserva> CrearReserva(Reserva reserva);
        Task<Reserva> GetReservaById(int id);
        Task<IEnumerable<Reserva>> GetReservasByUserId(int userId);
        Task<IEnumerable<Reserva>> GetAllReservas();
        Task<bool> ActualizarReserva(Reserva reserva);
        Task<bool> EliminarReserva(int id);
        Task<bool> ExisteConflictoReserva(int alojamientoId, DateTime fechaInicio, DateTime fechaFin, int? reservaIdToExclude = null);

        // Métodos CRUD para Ubicaciones
        Task<IEnumerable<Ubicacion>> GetAllUbicaciones();
        Task<Ubicacion> GetUbicacionById(int id);
        Task<Ubicacion> CreateUbicacion(Ubicacion ubicacion);
        Task<bool> UpdateUbicacion(Ubicacion ubicacion);
        Task<bool> DeleteUbicacion(int id);

        // Métodos CRUD para Alojamientos
        Task<IEnumerable<Alojamiento>> GetAllAlojamientos();
        Task<Alojamiento> GetAlojamientoById(int id);
        Task<Alojamiento> CreateAlojamiento(Alojamiento alojamiento);
        Task<bool> UpdateAlojamiento(Alojamiento alojamiento);
        Task<bool> DeleteAlojamiento(int id);

        // Métodos CRUD para Tarifas 
        Task<IEnumerable<Tarifa>> GetAllTarifas();
        Task<Tarifa> GetTarifaById(int id);
        Task<Tarifa> CreateTarifa(Tarifa tarifa);
        Task<bool> UpdateTarifa(Tarifa tarifa);
        Task<bool> DeleteTarifa(int id);
    }
}