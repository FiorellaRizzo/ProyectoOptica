using ProyectoOptica.Shared.DTO;


namespace ProyectoOptica.Client.Autorizacion;

public interface ILoginService
    {
        Task Login(UserTokenDTO tokenDTO);
        Task Logout();
    }

