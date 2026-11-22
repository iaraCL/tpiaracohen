namespace TPFINAL.Models
{
    public class HorarioClase
    {
        public int Id { get; set; }
        public int IdClase { get; set; }
        public DateTime FechaHora { get; set; }
        public int DuracionMinutos { get; set; }
        public int Cupo { get; set; }
        public int LugaresDisponibles { get; set; }
    }
}
