using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace ProyectoOptica.Client.Autorizacion
{
    public class ProveedorAutenticacion : AuthenticationStateProvider
    {
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            //await Task.Delay(5000);
            var anonimo = new ClaimsIdentity();
            var usuarioNuevo = new ClaimsIdentity(
                new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "Fiorella Rizzo"),
                    new Claim(ClaimTypes.Role, "admin"),
                    new Claim(ClaimTypes.Role, "operador"),
                    new Claim("DNI", "40.254.895"),
                },
                authenticationType: "ok"
                );
            var usuarioJuan = new ClaimsIdentity(
                new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "Juan Perez"),
                    new Claim(ClaimTypes.Role, "operador"),
                    new Claim("DNI", "52.587.895"),
                },
                authenticationType: "ok"
                );
            return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(usuarioNuevo)));
        }
    }
}