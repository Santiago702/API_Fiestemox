using Api_FiesteDocs.Data;
using Api_FiesteDocs.Entities;
using Api_FiesteDocs.Functions;
using Api_FiesteDocs.Models;
using Api_FiesteDocs.Services.Interfaces;
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

        public Request Crear(Grupo grupo)
        {
            if (grupo == null)
                return new Request { Success = false, Message = "El objeto está vacío" };
            Grupo grupoF = F_Clases.Formatear(grupo);
            _context.Grupos.Add(grupoF);
            _context.SaveChanges();
            return new Request { Success = true, Message = "Creado Correctamente" };

        }

        public Request Editar(Grupo grupo)
        {
            var GrupoExistente = _context.Grupos.FirstOrDefault(g => g.IdGrupo == grupo.IdGrupo);
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

            _context.SaveChanges();
            return new Request { Success = true, Message = "Editado Correctamente" };
        }

        public Request Eliminar(int Id_Grupo)
        {
            var aEliminar = _context.Grupos.FirstOrDefault(g => g.IdGrupo == Id_Grupo);
            if (aEliminar == null)
                return new Request { Success = false, Message = "No se encontró Grupo" };

            _context.Grupos.Remove(aEliminar);
            _context.SaveChanges();
            return new Request { Success = true, Message = "Eliminado Correctamente" };
        }

        public List<Grupo> Listar()
        {
            List<Grupo> lista = new List<Grupo>();
            lista = _context.Grupos.ToList();
            return lista;
        }

        public Grupo ObtenerIdGrupo(int Id_Grupo)
        {
            Grupo grupo = new Grupo();
            grupo = _context.Grupos.FirstOrDefault( g => g.IdGrupo == Id_Grupo );
            
            return grupo;
        }

        public List<Grupo> ObtenerIdEstudiante(int Id_Estudiante)
        {
            return _context.Asignaciones
                .Where(a => a.IdEstudiante == Id_Estudiante)
                .Join(_context.Grupos,
                      a => a.IdGrupo,
                      g => g.IdGrupo,
                      (a, g) => g)
                .ToList();
        }

        public List<Grupo> ObtenerIdDirector(int Id_Director)
        {
            List<Grupo> lista = new List<Grupo>();
            lista = _context.Grupos.Where(g => g.IdUsuarioDirector == Id_Director).ToList();
            return lista;
        }
    }
}
