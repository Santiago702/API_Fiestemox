using Api_FiesteDocs.Data;
using Api_FiesteDocs.Entities;
using Api_FiesteDocs.Functions;
using Api_FiesteDocs.Models;
using Api_FiesteDocs.Services.Interfaces;

namespace Api_FiesteDocs.Services
{
    public class S_Seccion : I_Seccion
    {
        private readonly ApplicationDbContext _context; 
        public S_Seccion(ApplicationDbContext context)
        {
            _context = context;
        }

        public Request Crear(Seccion seccion)
        {
            if (seccion == null)
            {
                return new Request { Success = false, Message = "Seccion vacía" };
            }
            
            _context.Seccions.Add(Clases.Formatear(seccion));
            _context.SaveChanges();
            return new Request { Success = true, Message = "Sección creada exitosamente."};
        }

        public Request Editar(Seccion seccion)
        {
            var seccionExistente = _context.Seccions.Find(seccion.IdSeccion);
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

        public Request Eliminar(int Id_Seccion)
        {
            if (Id_Seccion <= 0)
                return new Request { Success = false, Message = "ID de sección inválido." };
            var seccion = _context.Seccions.Find(Id_Seccion);
            if (seccion == null)
                return new Request { Success = false, Message = "Sección no encontrada." };
            _context.Seccions.Remove(seccion);
            _context.SaveChanges();
            return new Request { Success = true, Message = "Sección eliminada exitosamente." };
        }

        public List<Seccion> Listar()
        {
            List<Seccion> secciones = _context.Seccions.ToList();
            return secciones;
        }

        public List<Seccion> ListarIdGrupo(int Id_Grupo)
        {
            List<Seccion> secciones = _context.Seccions.Where(s => s.IdGrupo == Id_Grupo).ToList();
            return secciones;
        }

        public Seccion Obtener(int Id_Seccion)
        {
            if (Id_Seccion <= 0)
                return null;
            Seccion seccion = _context.Seccions.Find(Id_Seccion);
            return seccion;

        }
    }
}
