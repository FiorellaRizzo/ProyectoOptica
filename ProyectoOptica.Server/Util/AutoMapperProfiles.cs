﻿using AutoMapper;
using ProyectoOptica.Shared.DTO;
using ProyectoOptica.BD.Data.Entity;



namespace ProyectoOptica.Server.Util
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<CrearCitaDTO, Cita>();

            // Mapper de Cita a ListaDeCitasDTO
            CreateMap<ListaDeCitasDTO, Cita>();


            CreateMap<CitaDTO, Cita>();


            CreateMap<DisponibilidadDTO, Disponibilidad>();
        }
    }
}