using ProyectoOptica.BD.Data.Entity;
using ProyectoOptica.BD.Data;


namespace ProyectoOptica.Server.Repositorio
{
    public interface ITDocumentoRepositorio : IRepositorio<TDocumento>
    {

        Task<TDocumento> SelectByCod(string cod);
    }
}
