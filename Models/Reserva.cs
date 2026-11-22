namespace TPFINAL.Models
{
    public class Reserva
    {
        public int Id { get; set; }
        public int IdHorarioClase { get; set; }
        public int IdUsuario { get; set; }
        public DateTime FechaReserva { get; set; }
        public bool Cancelado { get; set; }
        public DateTime? FechaCancelacion { get; set; }
        public string NombreClase { get; set; }
        public DateTime FechaHora { get; set; }
        public int DuracionMinutos { get; set; }
        public string Profesor { get; set; }

    }
}
