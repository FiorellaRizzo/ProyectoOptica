using ProyectoOptica.BD.Data.Entity;
using ProyectoOptica.BD.Data;
using ProyectoOptica.Server.Repositorio;


namespace ProyectoOptica.Server.Repositorio
{
    public class DisponibilidadRepositorio : Repositorio<Disponibilidad>, IDisponibilidadRepositorio
    {
        private readonly Context context;

        public DisponibilidadRepositorio(Context context) : base(context)
        {
            this.context = context;
        }
    }
}
