using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using ProyectoOptica.BD.Data;
using ProyectoOptica.BD.Data.Entity;
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
        var existe = await context.Set<E>()
                         .AnyAsync(x => x.Id == id);
        return existe;
    }


    public async Task<List<E>> Select()
    {
        return await context.Set<E>().ToListAsync();
    }

    //Es como get por Id
    public async Task<E> SelectById(int id)
    {
        E? CitaId = await context.Set<E>()
            .AsNoTracking()
            .FirstOrDefaultAsync(
            x => x.Id == id);
        return CitaId;
    }

    public async Task<int> Insert(E entidad)
    {
        try
        {
            await context.Set<E>().AddAsync(entidad);
            await context.SaveChangesAsync();
            return entidad.Id;
        }
        catch (Exception err)
        {
            throw err;
        }
    }


    public async Task<bool> update(int id, E entidad)
    {

        if (id != entidad.Id)
        {
            return false;
        }


        var citaExistente = await SelectById(id);

        if (citaExistente == null)
        {
            return false;
        }

        try
        {
            context.Set<E>().Update(entidad);
            await context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    public async Task<bool> Borrar(int id)
    {
        var CitaExistente = await SelectById(id);

        if (CitaExistente == null)
        {
            return false;
        }

        var existe = await Existe(id);
        if (!existe)
        {
            return false;
        }

        context.Set<E>().Remove(CitaExistente);
        await context.SaveChangesAsync();
        return true;
    }

}