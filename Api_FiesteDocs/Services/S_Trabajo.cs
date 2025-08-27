using Api_FiesteDocs.Data;
using Api_FiesteDocs.Entities;
using Api_FiesteDocs.Models;
using Api_FiesteDocs.Services.Interfaces;

namespace Api_FiesteDocs.Services
{
    public class S_Trabajo: I_Trabajo
    {
        private readonly ApplicationDbContext _context;
        public S_Trabajo(ApplicationDbContext context)
        {
            _context = context;
        }

        public Request Crear(Trabajo trabajo)
        {
            if (trabajo == null)
            {
                return new Request { Message = "El trabajo no puede ser nulo", Success = false };
            }
            _context.Trabajos.Add(trabajo);
            _context.SaveChanges();
            return new Request { Message = "Trabajo creado exitosamente", Success = true };
        }

        public Request Editar(Trabajo trabajo)
        {
            var trabajoExistente = _context.Trabajos.Find(trabajo.IdTrabajo);
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

            _context.SaveChanges();
            return new Request { Message = "Trabajo editado exitosamente", Success = true };
        }

        public Request Eliminar(int Id_Trabajo)
        {
            if (Id_Trabajo <= 0)
                return new Request { Message = "El Id del trabajo no es válido", Success = false };
            Trabajo seleccionado = _context.Trabajos.Find(Id_Trabajo);
            if (seleccionado == null)
                return new Request { Message = "El trabajo no existe", Success = false };
            _context.Trabajos.Remove(seleccionado);
            _context.SaveChanges();
            return new Request { Message = "Trabajo eliminado exitosamente", Success = true };
        }

        public List<Trabajo> Listar()
        {
            List<Trabajo> lista = new List<Trabajo>();
            lista = _context.Trabajos.ToList();
            return lista;
        }

        public List<InfoTrabajo> ListarIdEnsayo(int Id_Ensayo)
        {
            return _context.Trabajos
                .Where(trabajo => trabajo.IdEnsayo == Id_Ensayo) // se filtra en la BD
                .Select(trabajo => new InfoTrabajo
                {
                    IdTrabajo = trabajo.IdTrabajo,
                    Descripcion = trabajo.Descripcion,
                    Detalles = trabajo.Detalles,
                    Comentarios = trabajo.Comentarios,
                    IdEnsayo = trabajo.IdEnsayo,
                    IdSeccion = trabajo.IdSeccion
                })
                .ToList();
        }


        public List<InfoTrabajo> ListarInfo()
        {
            return _context.Trabajos
                .Select(trabajo => new InfoTrabajo
                {
                    IdTrabajo = trabajo.IdTrabajo,
                    Descripcion = trabajo.Descripcion,
                    Detalles = trabajo.Detalles,
                    Comentarios = trabajo.Comentarios,
                    IdEnsayo = trabajo.IdEnsayo,
                    IdSeccion = trabajo.IdSeccion
                })
                .ToList();
        }


        public Trabajo ObtenerId(int Id_Trabajo)
        {
            Trabajo trabajo = new Trabajo();
            trabajo = _context.Trabajos.Find(Id_Trabajo);
            return trabajo;
        }
    }
}
