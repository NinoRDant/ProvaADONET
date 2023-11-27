using Prova_ADO.NET.Models;

namespace Prova_ADO.NET.Interfaces
{
    public interface IPersonaRepository
    {
        List<Persona> GetPersone();
        Persona GetPersonaById(long id);
        bool InserisciPersona(Persona persona);
        bool ModificaPersona(long id, Persona persona);
        bool EliminaPersona(long id);
    }
}
