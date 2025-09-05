using Api_FiesteDocs.Data;
using Api_FiesteDocs.Entities;
using Api_FiesteDocs.Models;
using Api_FiesteDocs.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api_FiesteDocs.Services
{
    public class S_Ensayo:I_Ensayo
    {
        private ApplicationDbContext _context;
        public S_Ensayo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Request> Crear(Ensayo ensayo)
        {
            if (ensayo == null)
            {
                return new Request { Message = "El ensayo no puede ser nulo", Success = false };
            }
            await _context.Ensayos.AddAsync(ensayo);
            await _context.SaveChangesAsync();
            return new Request { Message = "Ensayo creado exitosamente", Success = true };
        }

        public async Task<Request> Editar(Ensayo ensayo)
        {
            var ensayoExistente = await _context.Ensayos.FindAsync(ensayo.IdEnsayo);
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

            await _context.SaveChangesAsync();

            return new Request { Message = "Ensayo editado exitosamente", Success = true };
        }


        public async Task<Request> Eliminar(int Id_Ensayo)
        {
            if (Id_Ensayo <= 0)
                return new Request { Message = "El Id del ensayo no es válido", Success = false };

            Ensayo seleccionado = await _context.Ensayos.FindAsync(Id_Ensayo);
            
            if (seleccionado == null)
                return new Request { Message = "El ensayo no existe", Success = false };

            _context.Ensayos.Remove(seleccionado);
            await _context.SaveChangesAsync();

            return new Request { Message = "Ensayo eliminado exitosamente", Success = true };
        }

        public async Task<List<Ensayo>> Listar(int Id_Grupo)
        {
            List<Ensayo> lista = new List<Ensayo>();
            lista = await _context.Ensayos.AsNoTracking().Where(e => e.IdGrupo == Id_Grupo).ToListAsync();
            return lista;
        }



        public async Task<Ensayo> ObtenerId(int Id_Ensayo)
        {
            if (Id_Ensayo <= 0) return null;

            return await _context.Ensayos
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.IdEnsayo == Id_Ensayo);
        }
    }
}
