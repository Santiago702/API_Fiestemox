using Api_FiesteDocs.Data;
using Api_FiesteDocs.Entities;
using Api_FiesteDocs.Functions;
using Api_FiesteDocs.Models;
using Api_FiesteDocs.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace Api_FiesteDocs.Services
{
    public class S_Grupo : I_Grupo
    {
        private ApplicationDbContext _context;

        public S_Grupo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Request> Crear(Grupo grupo)
        {
            if (grupo == null)
                return new Request { Success = false, Message = "El objeto está vacío" };
            Grupo grupoF = Clases.Formatear(grupo);
            await _context.Grupos.AddAsync(grupoF);
            await _context.SaveChangesAsync();
            return new Request { Success = true, Message = "Creado Correctamente" };

        }

        public async Task<Request> Editar(Grupo grupo)
        {
            var GrupoExistente = await _context.Grupos.FirstOrDefaultAsync(g => g.IdGrupo == grupo.IdGrupo);
            if (GrupoExistente == null)
                return new Request { Success = false, Message = "Grupo no Encontrado" };

            
            GrupoExistente.IdUsuarioDirector = (grupo.IdUsuarioDirector <= 0)
                ? GrupoExistente.IdUsuarioDirector
                : grupo.IdUsuarioDirector;
            GrupoExistente.Nombre = string.IsNullOrEmpty(grupo.Nombre)
                ? GrupoExistente.Nombre
                : grupo.Nombre.ToUpper();
            GrupoExistente.Ciudad = string.IsNullOrEmpty(grupo.Ciudad)
                ? GrupoExistente.Ciudad
                : grupo.Ciudad.ToUpper();
            GrupoExistente.Codigo = string.IsNullOrEmpty(grupo.Codigo)
                ? GrupoExistente.Codigo
                : grupo.Codigo.ToUpper();

            await _context.SaveChangesAsync();
            return new Request { Success = true, Message = "Editado Correctamente" };
        }

        public async Task<Request> Eliminar(int Id_Grupo)
        {
            var aEliminar = await _context.Grupos.FirstOrDefaultAsync(g => g.IdGrupo == Id_Grupo);
            if (aEliminar == null)
                return new Request { Success = false, Message = "No se encontró Grupo" };
            
            _context.Grupos.Remove(aEliminar);
            await _context.SaveChangesAsync();
            return new Request { Success = true, Message = "Eliminado Correctamente" };
        }

        public async Task<List<Grupo>> Listar()
        {
            List<Grupo> lista = new List<Grupo>();
            lista = await _context.Grupos.AsNoTracking().ToListAsync();
            return lista;
        }

        public async Task<Grupo> ObtenerIdGrupo(int Id_Grupo)
        {
            Grupo grupo = new Grupo();
            grupo = await _context.Grupos.AsNoTracking().FirstOrDefaultAsync(g => g.IdGrupo == Id_Grupo);
            
            return grupo;
        }

        public async Task<List<Grupo>> ObtenerIdEstudiante(int Id_Estudiante)
        {
            return await _context.Asignaciones
                .Where(a => a.IdEstudiante == Id_Estudiante)
                .Join(_context.Grupos,
                      a => a.IdGrupo,
                      g => g.IdGrupo,
                      (a, g) => g)
                .ToListAsync();
        }

        public async Task<List<Grupo>> ObtenerIdDirector(int Id_Director)
        {
            List<Grupo> lista = new List<Grupo>();
            lista = await _context.Grupos.AsNoTracking().Where(g => g.IdUsuarioDirector == Id_Director).ToListAsync();
            return lista;
        }
    }
}
