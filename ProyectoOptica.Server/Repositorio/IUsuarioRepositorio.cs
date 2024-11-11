using ProyectoOptica.BD.Data.Entity;
using ProyectoOptica.BD.Data;


namespace ProyectoOptica.Server.Repositorio
{
    public interface IUsuarioRepositorio : IRepositorio<Usuario>
    {
        Task<List<Usuario>> FullGetAll();
        Task<Usuario> FullGetById(int id);
    }
}