using ProyectoOptica.BD.Data.Entity;
using ProyectoOptica.BD.Data;


namespace ProyectoOptica.Server.Repositorio
{
    public class OptometristaRepositorio : Repositorio<Optometrista> , IOptometristaRepositorio
    {
        private readonly Context context;

        public OptometristaRepositorio(Context context) : base(context)
        {
            this.context = context;
        }
    }
}
