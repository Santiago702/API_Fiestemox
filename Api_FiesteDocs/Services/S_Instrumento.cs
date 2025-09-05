using Api_FiesteDocs.Data;
using Api_FiesteDocs.Entities;
using Api_FiesteDocs.Models;
using Api_FiesteDocs.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api_FiesteDocs.Services
    
{
    public class S_Instrumento : I_Instrumento
    {
        private readonly ApplicationDbContext _context;
        public S_Instrumento(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Request> Crear(Instrumento instrumento)
        {
            await _context.Instrumentos.AddAsync(instrumento);
            await _context.SaveChangesAsync();
            return new Request {Success = true, Message = "Instrumento creado exitosamente."};
        }

        public async Task<Request> Editar(Instrumento instrumento)
        {
            if(instrumento == null || instrumento.IdInstrumento <= 0)
                return new Request { Success = false, Message = "Instrumento no válido." };

            Instrumento ExisteInstrumento = await _context.Instrumentos.FindAsync(instrumento.IdInstrumento);

            if (ExisteInstrumento == null)
                return new Request { Success = false, Message = "Instrumento no encontrado." };

            ExisteInstrumento.Nombre = string.IsNullOrEmpty(instrumento.Nombre) 
                ? ExisteInstrumento.Nombre 
                : instrumento.Nombre.ToUpper();

            ExisteInstrumento.IdSeccion = (instrumento.IdSeccion <= 0) 
                ? ExisteInstrumento.IdSeccion 
                : instrumento.IdSeccion;
            await _context.SaveChangesAsync();
            return new Request { Success = true, Message = "Instrumento editado exitosamente." };
        }

        public async Task<Request> Eliminar(int Id_Instrumento)
        {
            if(Id_Instrumento <= 0)
                return new Request { Success = false, Message = "ID de instrumento no válido." };
            Instrumento ExisteInstrumento = _context.Instrumentos.Find(Id_Instrumento);
            if (ExisteInstrumento == null)
                return new Request { Success = false, Message = "Instrumento no encontrado." };
            _context.Instrumentos.Remove(ExisteInstrumento);
            await _context.SaveChangesAsync();
            return new Request { Success = true, Message = "Instrumento eliminado exitosamente." };
        }

        public async Task<List<Instrumento>> Listar()
        {
            List<Instrumento> instrumentos = await _context.Instrumentos.AsNoTracking().ToListAsync();
            return instrumentos;
        }

        public async Task<List<Instrumento>> ListarIdGrupo(int Id_Grupo)
        {
            var instrumentos = await _context.Seccions.AsNoTracking()
                .Where(s => s.IdGrupo == Id_Grupo)               
                .SelectMany(s => _context.Instrumentos           
                    .Where(i => i.IdSeccion == s.IdSeccion))
                .ToListAsync();

            return instrumentos;
        }


        public async Task<List<Instrumento>> ListarIdSeccion(int Id_Seccion)
        {
            List<Instrumento> instrumentos = await _context.Instrumentos.AsNoTracking()
                .Where(i => i.IdSeccion == Id_Seccion)
                .ToListAsync();
            return instrumentos;
        }

        public async Task<Instrumento> Obtener(int Id_Instrumento)
        {
            Instrumento instrumento = await _context.Instrumentos.AsNoTracking()
                .FirstOrDefaultAsync(i => i.IdInstrumento == Id_Instrumento);
            return instrumento;
        }
    }
}
