using Backend_Final.Models;
using Backend_Final.Models.Dtos;
using AutoMapper;
using System.Diagnostics.Metrics;

namespace Backend_Final.ParcialMappers
{
    public class ParcialMapper : Profile
    {
        public ParcialMapper()
        {
            CreateMap<usuario, UsuarioDTO>().ReverseMap();
            CreateMap<bicicleta, BicicletaDTO>().ReverseMap();
        }
    }
}
