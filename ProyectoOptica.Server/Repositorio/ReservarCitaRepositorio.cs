using ProyectoOptica.BD.Data.Entity;
using ProyectoOptica.BD.Data;

namespace ProyectoOptica.Server.Repositorio
{
    public class ReservarCitaRepositorio : Repositorio<Cita> , IReservarCitaRepositorio
    {
        private readonly Context context;

        public ReservarCitaRepositorio(Context context) : base(context)
        {
            this.context = context;
        }

    }

}