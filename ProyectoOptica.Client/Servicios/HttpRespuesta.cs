using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProyectoOptica.Client.Servicios

{
    public class HttpRespuesta<T>
    {
        public T? Respuesta { get; }

        public bool Error { get; }

        public HttpResponseMessage HttpResponseMessage { get; set; }

        public HttpRespuesta(T? respuesta, bool error, HttpResponseMessage httpResponseMessage)
        {
            Respuesta = respuesta;
            Error = error;
            HttpResponseMessage = httpResponseMessage;
        }

        public async Task<string> ObtenerError()
        {
            if (!Error) return string.Empty;

            var contenido = await HttpResponseMessage.Content.ReadAsStringAsync();

            return HttpResponseMessage.StatusCode switch
            {
                System.Net.HttpStatusCode.BadRequest => contenido,
                System.Net.HttpStatusCode.Unauthorized => "Error, no está logueado",
                System.Net.HttpStatusCode.Forbidden => "Error, no tiene autorización a ejecutar este proceso",
                System.Net.HttpStatusCode.NotFound => "Error, dirección no encontrada",
                _ => contenido
            };
        }
    }

    
}
