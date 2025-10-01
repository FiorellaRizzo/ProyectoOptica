using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using ProyectoOptica.BD.Data;
using ProyectoOptica.BD.Data.Entity;
using ProyectoOptica.Server.Repositorio;
using ProyectoOptica.Shared.DTO;

public class Repositorio<E> : IRepositorio<E>
        where E : class, IEntityBase
{
    private readonly Context context;

    public Repositorio(Context context)
    {
        this.context = context;
    }

    public async Task<bool> Existe(int id)
    {
        return await context.Set<E>().AnyAsync(x => x.Id == id);
    }

    public async Task<List<E>> Select()
    {
        return await context.Set<E>().AsNoTracking().ToListAsync();
    }

    public async Task<E?> SelectById(int id)
    {
        return await context.Set<E>()
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<int> Insert(E entidad)
    {
        await context.Set<E>().AddAsync(entidad);
        await context.SaveChangesAsync();
        return entidad.Id;
    }

    public async Task<bool> Update(int id, E entidad)
    {
        if (id != entidad.Id) return false;

        var existente = await context.Set<E>()
                                     .AsNoTracking()
                                     .FirstOrDefaultAsync(x => x.Id == id);
        if (existente is null) return false;

        context.Set<E>().Update(entidad);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Borrar(int id)
    {
        var existente = await context.Set<E>().FirstOrDefaultAsync(x => x.Id == id);
        if (existente is null) return false;

        context.Set<E>().Remove(existente);
        await context.SaveChangesAsync();
        return true;
    }
}

