using Api_FiesteDocs.Data;
using Api_FiesteDocs.Entities;
using Api_FiesteDocs.Functions;
using Api_FiesteDocs.Models;
using Api_FiesteDocs.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace Api_FiesteDocs.Services
{
    public class S_Estudiante : I_Estudiante
    {
        private ApplicationDbContext _context;
        public S_Estudiante(ApplicationDbContext context)
        {
            _context = context;
        }

        public Request Crear(InfoEstudiante estudiante)
        {
            if (estudiante == null || estudiante.Usuario == null || estudiante.Estudiante == null)
                return new Request { Success = false, Message = "Datos incompletos" };

            
            var correo = estudiante.Usuario.Correo?.Trim().ToLower();
            if (string.IsNullOrWhiteSpace(correo))
                return new Request { Success = false, Message = "Correo inválido" };

            
            bool existeUsuario = _context.Usuarios.Any(u => u.Correo.ToLower() == correo);
            if (existeUsuario)
                return new Request { Success = false, Message = "Correo ya existente" };

            
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                
                var nuevoUsuario = new Usuario
                {
                    Nombre = estudiante.Usuario.Nombre?.ToUpper(),
                    Ciudad = estudiante.Usuario.Ciudad?.ToUpper(),
                    Correo = correo,
                    Contrasena = BCrypt.Net.BCrypt.HashPassword(estudiante.Usuario.Contrasena ?? string.Empty),
                    Foto = estudiante.Usuario.Foto,
                    Estado = estudiante.Usuario.Estado,
                    IdRol = estudiante.Usuario.IdRol
                };

                _context.Usuarios.Add(nuevoUsuario);
                _context.SaveChanges(); 

                
                var nuevoEstudiante = new Estudiante
                {
                    Documento = estudiante.Estudiante.Documento?.Trim(),
                    TipoDocumento = estudiante.Estudiante.TipoDocumento?.Trim().ToUpperInvariant(),
                    IdInstrumento = estudiante.Estudiante.IdInstrumento, 
                    IdGrupo = estudiante.Estudiante.IdGrupo, 
                    IdUsuario = nuevoUsuario.IdUsuario
                };

                _context.Estudiantes.Add(nuevoEstudiante);
                _context.SaveChanges();

                transaction.Commit();
                return new Request { Success = true, Message = "Creado Correctamente" };
            }
            catch (DbUpdateException dbEx)
            {
                
                transaction.Rollback();
                
                return new Request { Success = false, Message = "Error al crear (DB): " + dbEx.Message };
            }
            catch (Exception ex)
            {
                 transaction.Rollback();
                
                return new Request { Success = false, Message = "Error interno: " + ex.Message };
            }
        }


        public Request Editar(Estudiante estudiante)
        {
            var dataEst = _context.Estudiantes.FirstOrDefault(e => e.IdEstudiante == estudiante.IdEstudiante);

            if (dataEst == null)
                return new Request { Success = false, Message = "No se encontró estudiante" };

            dataEst.Documento = string.IsNullOrEmpty(estudiante.Documento)
                ? dataEst.Documento
                : estudiante.Documento;

            dataEst.TipoDocumento = string.IsNullOrEmpty(estudiante.TipoDocumento)
                ? dataEst.TipoDocumento 
                : estudiante.TipoDocumento;

            dataEst.IdGrupo = (estudiante.IdGrupo <= 0 || !estudiante.IdGrupo.HasValue)
                ? dataEst.IdGrupo
                : estudiante.IdGrupo;

            dataEst.IdInstrumento = (estudiante.IdInstrumento <= 0 || !estudiante.IdInstrumento.HasValue)
                ? dataEst.IdInstrumento
                : estudiante.IdInstrumento;

            dataEst.IdUsuario = (estudiante.IdUsuario <= 0 || !estudiante.IdUsuario.HasValue)
                ? dataEst.IdUsuario
                : estudiante.IdUsuario;
            
            _context.SaveChanges();

            return new Request { Success = true , Message = "Editado Correctamente"};
            
        }

        public Request Eliminar(int Id_Estudiante)
        {
            if (Id_Estudiante <= 0)
                return new Request { Success = false, Message = "Id inválido" };

            var estudiante = _context.Estudiantes.FirstOrDefault(e => e.IdEstudiante == Id_Estudiante);

            if (estudiante == null)
                return new Request { Success = false, Message = "No se encontró un estudiante asociado" };

            using var transaccion = _context.Database.BeginTransaction();

            try
            {
                int? IdUsuario = estudiante.IdUsuario;

                
                _context.Estudiantes.Remove(estudiante);

                
                if (IdUsuario.HasValue && IdUsuario.Value > 0)
                {
                    var usuario = _context.Usuarios.FirstOrDefault(u => u.IdUsuario == IdUsuario.Value);
                    if (usuario != null)
                        _context.Usuarios.Remove(usuario);
                }

                _context.SaveChanges();
                transaccion.Commit();

                return new Request { Success = true, Message = "Estudiante eliminado" + (IdUsuario.HasValue ? " junto con su Usuario" : "") };
            }
            catch (DbUpdateException dbEx)
            {
                transaccion.Rollback();
                return new Request { Success = false, Message = "Error al eliminar (DB): " + dbEx.Message };
            }
            catch (Exception ex)
            {
                transaccion.Rollback();
                return new Request { Success = false, Message = "Error interno: " + ex.Message };
            }
        }


        public List<InfoEstudiante> Listar(int Id_Grupo)
        {
            return (from e in _context.Estudiantes
                    join u in _context.Usuarios on e.IdUsuario equals u.IdUsuario
                    where e.IdGrupo == Id_Grupo
                    select new InfoEstudiante
                    {
                        Estudiante = e,
                        Usuario = u
                    }).ToList();
        }


        public InfoEstudiante ObtenerId(int Id_Estudiante)
        {
            return (from e in _context.Estudiantes
                    join u in _context.Usuarios on e.IdUsuario equals u.IdUsuario
                    where e.IdEstudiante == Id_Estudiante
                    select new InfoEstudiante
                    {
                        Estudiante = e,
                        Usuario = u
                    }).FirstOrDefault();
        }

    }
}
