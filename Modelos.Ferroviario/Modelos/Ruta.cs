using System.ComponentModel.DataAnnotations;

namespace Modelos.Ferroviario.Modelos
{
    public class Ruta
    {
        [Key]
        public int Id { get; set; }
        public string CiudadInicio { get; set; }
        public string CiudadDestino { get; set; }
        public string Descripcion { get; set; }
        public double PrecioBase { get; set; }

        public List<Schedule>? Schedules { get; set; } // Lista de horarios asociados a esta ruta
    }
}
