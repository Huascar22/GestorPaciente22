using Modelo.Entidades;

namespace Vista.ViewModels
{
    public class PruebaViewModel
    {
        public PruebaViewModelFormulario? PruebaForomulario { get; set; }
        public List<Prueba> Pruebas { get; set; } = new();
    }
}


