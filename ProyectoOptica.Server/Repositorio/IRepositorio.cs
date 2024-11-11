using ProyectoOptica.BD.Data;

public interface IRepositorio<E> where E : class, IEntityBase
{
    Task<bool> Borrar(int id);
    Task<bool> Existe(int id);
    Task<int> Insert(E entidad);
    Task<List<E>> Select();
    Task<E> SelectById(int id);
    Task<bool> update(int id, E entidad);
}