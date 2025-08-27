using Api_FiesteDocs.Data;
using Api_FiesteDocs.Entities;
using Api_FiesteDocs.Models;
using Api_FiesteDocs.Services.Interfaces;

namespace Api_FiesteDocs.Services
{
    public class S_Ensayo:I_Ensayo
    {
        private ApplicationDbContext _context;
        public S_Ensayo(ApplicationDbContext context)
        {
            ApplicationDbContext _context = context;
        }

        public Request Crear(Ensayo ensayo)
        {
            if (ensayo == null)
            {
                return new Request { Message = "El ensayo no puede ser nulo", Success = false };
            }
            _context.Ensayos.Add(ensayo);
            _context.SaveChanges();
            return new Request { Message = "Ensayo creado exitosamente", Success = true };
        }

        public Request Editar(Ensayo ensayo)
        {
            var ensayoExistente = _context.Ensayos.Find(ensayo.IdEnsayo);
            if (ensayoExistente == null)
                return new Request { Message = "El ensayo no existe", Success = false };

            if (ensayo.Fecha != default(DateOnly))
                ensayoExistente.Fecha = ensayo.Fecha;

            if (ensayo.HoraInicio != default(TimeOnly))
                ensayoExistente.HoraInicio = ensayo.HoraInicio;

            if (ensayo.HoraFin != default(TimeOnly))
                ensayoExistente.HoraFin = ensayo.HoraFin;

            if (ensayo.IdGrupo > 0)
                ensayoExistente.IdGrupo = ensayo.IdGrupo;

            _context.SaveChanges();

            return new Request { Message = "Ensayo editado exitosamente", Success = true };
        }


        public Request Eliminar(int Id_Ensayo)
        {
            if (Id_Ensayo <= 0)
                return new Request { Message = "El Id del ensayo no es válido", Success = false };

            Ensayo seleccionado = _context.Ensayos.Find(Id_Ensayo);
            
            if (seleccionado == null)
                return new Request { Message = "El ensayo no existe", Success = false };

            _context.Ensayos.Remove(seleccionado);
            _context.SaveChanges();

            return new Request { Message = "Ensayo eliminado exitosamente", Success = true };
        }

        public List<Ensayo> Listar(int Id_Grupo)
        {
            List<Ensayo> lista = new List<Ensayo>();
            lista = _context.Ensayos.Where(e => e.IdGrupo == Id_Grupo).ToList();
            return lista;
        }



        public Ensayo ObtenerId(int Id_Ensayo)
        {
            Ensayo ensayo = new Ensayo();
            ensayo = _context.Ensayos.Find(Id_Ensayo);
            return ensayo;
        }
    }
}
