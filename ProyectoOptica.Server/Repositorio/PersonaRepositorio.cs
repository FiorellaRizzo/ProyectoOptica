using ProyectoOptica.BD.Data.Entity;
using ProyectoOptica.BD.Data;
using Microsoft.EntityFrameworkCore;

namespace ProyectoOptica.Server.Repositorio
{
    public class PersonaRepositorio : Repositorio<Persona>, IPersonaRepositorio
    {
        private readonly Context context;

        public PersonaRepositorio(Context context) : base(context)
        {
            this.context = context;
        }

        public async Task<Persona> FullGetById(int id)
        {
            return await context.Personas
                .Include(p => p.TDocumento)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Persona>> FullGetAll()
        {
            return await context.Personas
                .Include(p => p.TDocumento)
                .ToListAsync();
        }

        public async Task FullInsert(Persona persona)
        {
            await context.Personas.AddAsync(persona);
            await context.SaveChangesAsync();
        }

        public async Task FullUpdate(Persona persona)
        {
            context.Personas.Update(persona);
            await context.SaveChangesAsync();
        }
    }
}
