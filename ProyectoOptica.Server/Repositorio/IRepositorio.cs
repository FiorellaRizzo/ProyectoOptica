
using ProyectoOptica.BD.Data;

namespace ProyectoOptica.Server.Repositorio
{
    public interface IRepositorio<E> where E : class, IEntityBase
    {
        Task<bool> Existe(int id);
        Task<List<E>> Select();
        Task<E?> SelectById(int id);
        Task<int> Insert(E entidad);
        Task<bool> Update(int id, E entidad);
        Task<bool> Borrar(int id);
    }

}