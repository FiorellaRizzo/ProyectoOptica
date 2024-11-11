using ProyectoOptica.BD.Data.Entity;
using ProyectoOptica.BD.Data;


namespace ProyectoOptica.Server.Repositorio
{
    public interface IPersonaRepositorio : IRepositorio<Persona>
    {
        Task<Persona> FullGetById(int id);
        Task<List<Persona>> FullGetAll();
        Task FullInsert(Persona persona);
        Task FullUpdate(Persona persona);
    }
}
