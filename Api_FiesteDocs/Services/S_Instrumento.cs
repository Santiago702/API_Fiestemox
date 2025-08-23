using Api_FiesteDocs.Data;
using Api_FiesteDocs.Entities;
using Api_FiesteDocs.Models;
using Api_FiesteDocs.Services.Interfaces;

namespace Api_FiesteDocs.Services
    
{
    public class S_Instrumento : I_Instrumento
    {
        private readonly ApplicationDbContext _context;
        public S_Instrumento(ApplicationDbContext context)
        {
            _context = context;
        }

        public Request Crear(Instrumento instrumento)
        {
            _context.Instrumentos.Add(instrumento);
            _context.SaveChanges();
            return new Request {Success = true, Message = "Instrumento creado exitosamente."};
        }

        public Request Editar(Instrumento instrumento)
        {
            if(instrumento == null || instrumento.IdInstrumento <= 0)
                return new Request { Success = false, Message = "Instrumento no válido." };

            Instrumento ExisteInstrumento = _context.Instrumentos.Find(instrumento.IdInstrumento);

            if (ExisteInstrumento == null)
                return new Request { Success = false, Message = "Instrumento no encontrado." };

            ExisteInstrumento.Nombre = string.IsNullOrEmpty(instrumento.Nombre) 
                ? ExisteInstrumento.Nombre 
                : instrumento.Nombre.ToUpper();

            ExisteInstrumento.IdSeccion = (instrumento.IdSeccion <= 0) 
                ? ExisteInstrumento.IdSeccion 
                : instrumento.IdSeccion;
            _context.SaveChanges();
            return new Request { Success = true, Message = "Instrumento editado exitosamente." };
        }

        public Request Eliminar(int Id_Instrumento)
        {
            if(Id_Instrumento <= 0)
                return new Request { Success = false, Message = "ID de instrumento no válido." };
            Instrumento ExisteInstrumento = _context.Instrumentos.Find(Id_Instrumento);
            if (ExisteInstrumento == null)
                return new Request { Success = false, Message = "Instrumento no encontrado." };
            _context.Instrumentos.Remove(ExisteInstrumento);
            _context.SaveChanges();
            return new Request { Success = true, Message = "Instrumento eliminado exitosamente." };
        }

        public List<Instrumento> Listar()
        {
            List<Instrumento> instrumentos = _context.Instrumentos.ToList();
            return instrumentos;
        }

        public List<Instrumento> ListarIdGrupo(int Id_Grupo)
        {
            var instrumentos = _context.Seccions
                .Where(s => s.IdGrupo == Id_Grupo)               
                .SelectMany(s => _context.Instrumentos           
                    .Where(i => i.IdSeccion == s.IdSeccion))
                .ToList();

            return instrumentos;
        }


        public List<Instrumento> ListarIdSeccion(int Id_Seccion)
        {
            List<Instrumento> instrumentos = _context.Instrumentos
                .Where(i => i.IdSeccion == Id_Seccion)
                .ToList();
            return instrumentos;
        }

        public Instrumento Obtener(int Id_Instrumento)
        {
            Instrumento instrumento = _context.Instrumentos
                .FirstOrDefault(i => i.IdInstrumento == Id_Instrumento);
            return instrumento;
        }
    }
}
