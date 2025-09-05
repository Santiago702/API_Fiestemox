using Api_FiesteDocs.Data;
using Api_FiesteDocs.Entities;
using Api_FiesteDocs.Models;
using Api_FiesteDocs.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api_FiesteDocs.Services
{
    public class S_Trabajo: I_Trabajo
    {
        private readonly ApplicationDbContext _context;
        public S_Trabajo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Request> Crear(Trabajo trabajo)
        {
            if (trabajo == null)
            {
                return new Request { Message = "El trabajo no puede ser nulo", Success = false };
            }
            await _context.Trabajos.AddAsync(trabajo);
            await _context.SaveChangesAsync();
            return new Request { Message = "Trabajo creado exitosamente", Success = true };
        }

        public async Task<Request> Editar(Trabajo trabajo)
        {
            var trabajoExistente = await _context.Trabajos.FindAsync(trabajo.IdTrabajo);
            if (trabajoExistente == null)
                return new Request { Message = "El trabajo no existe", Success = false };
            // Actualizar solo los campos que no son nulos o tienen valores predeterminados
            trabajoExistente.Descripcion = string.IsNullOrEmpty(trabajo.Descripcion) 
                ? trabajoExistente.Descripcion 
                : trabajo.Descripcion;

            trabajoExistente.IdEnsayo = trabajo.IdEnsayo <= 0 
                ? trabajoExistente.IdEnsayo
                : trabajo.IdEnsayo;

            trabajoExistente.IdSeccion = trabajo.IdSeccion <= 0
                ? trabajoExistente.IdSeccion
                : trabajo.IdSeccion;

            trabajoExistente.Detalles = string.IsNullOrEmpty(trabajo.Detalles)
                ? trabajoExistente.Detalles
                : trabajo.Detalles;

            trabajoExistente.Evidencia = string.IsNullOrEmpty(trabajo.Evidencia)
                ? trabajoExistente.Evidencia
                : trabajo.Evidencia;

            trabajoExistente.Foto = string.IsNullOrEmpty(trabajo.Foto)
                ? trabajoExistente.Foto
                : trabajo.Foto;

            trabajoExistente.Comentarios = string.IsNullOrEmpty(trabajo.Comentarios)
                ? trabajoExistente.Comentarios
                : trabajo.Comentarios;

            await _context.SaveChangesAsync();
            return new Request { Message = "Trabajo editado exitosamente", Success = true };
        }

        public async Task<Request> Eliminar(int Id_Trabajo)
        {
            if (Id_Trabajo <= 0)
                return new Request { Message = "El Id del trabajo no es válido", Success = false };
            Trabajo seleccionado = await _context.Trabajos.FindAsync(Id_Trabajo);
            if (seleccionado == null)
                return new Request { Message = "El trabajo no existe", Success = false };
            _context.Trabajos.Remove(seleccionado);
            await _context.SaveChangesAsync();
            return new Request { Message = "Trabajo eliminado exitosamente", Success = true };
        }

        public async Task<List<Trabajo>> Listar()
        {
            List<Trabajo> lista = new List<Trabajo>();
            lista = await _context.Trabajos.AsNoTracking().ToListAsync();
            return lista;
        }

        public async Task<List<InfoTrabajo>> ListarIdEnsayo(int Id_Ensayo)
        {
            return await _context.Trabajos.AsNoTracking()
                .Where(trabajo => trabajo.IdEnsayo == Id_Ensayo) 
                .Select(trabajo => new InfoTrabajo
                {
                    IdTrabajo = trabajo.IdTrabajo,
                    Descripcion = trabajo.Descripcion,
                    Detalles = trabajo.Detalles,
                    Comentarios = trabajo.Comentarios,
                    IdEnsayo = trabajo.IdEnsayo,
                    IdSeccion = trabajo.IdSeccion
                })
                .ToListAsync();
        }


        public async Task<List<InfoTrabajo>> ListarInfo()
        {
            return await _context.Trabajos.AsNoTracking()
                .Select(trabajo => new InfoTrabajo
                {
                    IdTrabajo = trabajo.IdTrabajo,
                    Descripcion = trabajo.Descripcion,
                    Detalles = trabajo.Detalles,
                    Comentarios = trabajo.Comentarios,
                    IdEnsayo = trabajo.IdEnsayo,
                    IdSeccion = trabajo.IdSeccion
                })
                .ToListAsync();
        }


        public async Task<Trabajo> ObtenerId(int Id_Trabajo)
        {
            Trabajo trabajo = new Trabajo();
            trabajo = await _context.Trabajos.FindAsync(Id_Trabajo);
            return trabajo;
        }
    }
}
