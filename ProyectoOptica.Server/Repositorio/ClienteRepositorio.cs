using ProyectoOptica.BD.Data.Entity;
using ProyectoOptica.BD.Data;


namespace ProyectoOptica.Server.Repositorio
{
    public class ClienteRepositorio : Repositorio<Cliente>, IClienteRepositorio
    {
        private readonly Context context;

        public ClienteRepositorio(Context context) : base(context)
        {
            this.context = context;
        }
    }
}
