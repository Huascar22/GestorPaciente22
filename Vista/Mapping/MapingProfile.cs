using AutoMapper;
using Modelo.Entidades;
using Vista.ViewModels;

namespace Vista.Mapping
{
    public class MapingProfile: Profile
    {
        public MapingProfile()
        {
            CreateMap<Usuario, RegistroViewModel>().ReverseMap();
            CreateMap<Medico, MedicoViewModel>().ReverseMap();
            CreateMap<Prueba, PruebaViewModelFormulario>().ReverseMap();
            CreateMap<Paciente, PacienteViewModel>().ReverseMap();
            CreateMap<Cita, CitaViewModel>().ReverseMap();
        }
    }
}
