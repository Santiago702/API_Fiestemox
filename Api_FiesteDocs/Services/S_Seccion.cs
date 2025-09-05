using Api_FiesteDocs.Data;
using Api_FiesteDocs.Entities;
using Api_FiesteDocs.Functions;
using Api_FiesteDocs.Models;
using Api_FiesteDocs.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api_FiesteDocs.Services
{
    public class S_Seccion : I_Seccion
    {
        private readonly ApplicationDbContext _context; 
        public S_Seccion(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Request> Crear(Seccion seccion)
        {
            if (seccion == null)
            {
                return new Request { Success = false, Message = "Seccion vacía" };
            }
            
            await _context.Seccions.AddAsync(Clases.Formatear(seccion));
            await _context.SaveChangesAsync();
            return new Request { Success = true, Message = "Sección creada exitosamente."};
        }

        public async Task<Request> Editar(Seccion seccion)
        {
            var seccionExistente = await _context.Seccions.FindAsync(seccion.IdSeccion);
            if (seccionExistente == null)
                return new Request { Success = false, Message = "Sección no encontrada." };

            seccionExistente.Descripcion = string.IsNullOrEmpty(seccion.Descripcion) 
                ? seccionExistente.Descripcion 
                : seccion.Descripcion.ToUpper();

            seccionExistente.IdGrupo = (seccion.IdGrupo <= 0)
                ? seccionExistente.IdGrupo
                : seccion.IdGrupo;

            _context.SaveChanges();
            return new Request { Success = true, Message = "Editado Exitosamente" };
        }

        public async Task<Request> Eliminar(int Id_Seccion)
        {
            if (Id_Seccion <= 0)
                return new Request { Success = false, Message = "ID de sección inválido." };
            var seccion = _context.Seccions.Find(Id_Seccion);
            if (seccion == null)
                return new Request { Success = false, Message = "Sección no encontrada." };
            _context.Seccions.Remove(seccion);
            await _context.SaveChangesAsync();
            return new Request { Success = true, Message = "Sección eliminada exitosamente." };
        }

        public async Task<List<Seccion>> Listar()
        {
            List<Seccion> secciones = await _context.Seccions.AsNoTracking().ToListAsync();
            return secciones;
        }

        public async Task<List<Seccion>> ListarIdGrupo(int Id_Grupo)
        {
            List<Seccion> secciones = await _context.Seccions.AsNoTracking().Where(s => s.IdGrupo == Id_Grupo).ToListAsync();
            return secciones;
        }

        public async Task<Seccion> Obtener(int Id_Seccion)
        {
            if (Id_Seccion <= 0)
                return null;
            Seccion seccion = await _context.Seccions.FindAsync(Id_Seccion);
            return seccion;

        }
    }
}
