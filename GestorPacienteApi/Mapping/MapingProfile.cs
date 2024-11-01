using AutoMapper;
using GestorPacienteApi.DTOs.CitasDtos;
using GestorPacienteApi.DTOs.ExamenDto;
using GestorPacienteApi.DTOs.MedicosDto;
using GestorPacienteApi.DTOs.PacientesDtos;
using GestorPacienteApi.DTOs.PruebasDto;
using GestorPacienteApi.DTOs.UsuariosDtos;
using Modelo.Entidades;


namespace Vista.Mapping
{
    public class MapingProfile: Profile
    {
        public MapingProfile()
        {
            CreateMap<Usuario, PostUsuarioDto>().ReverseMap();
            CreateMap<Usuario, GetUsuarioDto>().ReverseMap();

            CreateMap<Medico, PostMedicoDto>().ReverseMap();
            CreateMap<Medico, GetMedicoDto>().ReverseMap();
            CreateMap<Medico, PatchMedicoDto>().ReverseMap();

            CreateMap<Cita, PostCitaDto>().ReverseMap();
            CreateMap<Cita, GetCitaDto>().ReverseMap();

            CreateMap<Prueba, GetPruebaDto>().ReverseMap();
            CreateMap<Prueba, PostPruebaDto>().ReverseMap();
            CreateMap<Paciente, PostPacienteDto>().ReverseMap();

            CreateMap<ExamenMedico, GetExamenDto>().ReverseMap();
            CreateMap<ExamenMedico, PostExamenDto>().ReverseMap();

            CreateMap<Paciente, GetPacienteDto>().ReverseMap();
            CreateMap<Paciente, PostPacienteDto>().ReverseMap();
        }
    }
}
