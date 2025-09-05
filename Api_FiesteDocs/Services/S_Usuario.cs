using Api_FiesteDocs.Data;
using Api_FiesteDocs.Models;
using Api_FiesteDocs.Entities;
using Api_FiesteDocs.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api_FiesteDocs.Services
{
    public class S_Usuario : I_Usuario
    {
        private readonly ApplicationDbContext _context;
        public S_Usuario(ApplicationDbContext context) 
        { 
            _context = context;
        }

        public async Task<Request> Crear(Usuario usuario)
        {
            bool existeUsuario = await _context.Usuarios.AnyAsync(u => u.Correo == usuario.Correo);
            if (existeUsuario)
                return new Request { Success = false, Message = "Correo ya Existente" };

            usuario.Nombre = usuario.Nombre.ToUpper();
            usuario.Ciudad = usuario.Ciudad.ToUpper();
            usuario.Estado = false;
            usuario.Contrasena = BCrypt.Net.BCrypt.HashPassword(usuario.Contrasena);
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();
            return new Request { Success = true, Message = "Creado Correctamente" };

            

        }

        public  async Task<Request> Editar(Usuario usuario)
        {
            
            var usuarioExistente =  await _context.Usuarios.FirstOrDefaultAsync(u => u.IdUsuario == usuario.IdUsuario);

            if (usuarioExistente != null)
            {
                
                usuarioExistente.Nombre = string.IsNullOrWhiteSpace(usuario.Nombre)
                    ? usuarioExistente.Nombre
                    : usuario.Nombre.ToUpper();

                usuarioExistente.Correo = string.IsNullOrWhiteSpace(usuario.Correo)
                    ? usuarioExistente.Correo
                    : usuario.Correo;

                usuarioExistente.Contrasena = string.IsNullOrWhiteSpace(usuario.Contrasena)
                    ? usuarioExistente.Contrasena
                    : BCrypt.Net.BCrypt.HashPassword(usuario.Contrasena);

                usuarioExistente.Foto = string.IsNullOrWhiteSpace(usuario.Foto)
                    ? usuarioExistente.Foto
                    : usuario.Foto;

                usuarioExistente.Estado = (usuario.Estado)
                    ? true 
                    : false;

                usuarioExistente.Ciudad = string.IsNullOrWhiteSpace(usuario.Ciudad)
                    ? usuarioExistente.Ciudad
                    : usuario.Ciudad.ToUpper();

                if(usuarioExistente.IdRol != 0)
                {
                    usuarioExistente.IdRol = usuarioExistente.IdRol;
                }
                else
                {
                    usuarioExistente.IdRol = usuario.IdRol;
                }

                await _context.SaveChangesAsync();
                return new Request { Success = true, Message = "Editado Correctamente" };
            }

            return new Request { Success = false, Message = "Usuario no encontrado" };
        }



        public async Task<Request> Eliminar(int id_Usuario)
        {
            if (id_Usuario != 0)
            {
                var usuarioSeleccionado =  _context.Usuarios.FirstOrDefault(u => u.IdUsuario == id_Usuario);

                if (usuarioSeleccionado != null)
                {
                    _context.Usuarios.Remove(usuarioSeleccionado);
                     await _context.SaveChangesAsync();
                    return new Request { Success = true, Message = "Usuario Eliminado" };
                }

                return new Request { Success = false, Message = "Usuario no encontrado" };
            }

            return new Request { Success = false, Message = "Id inválido" };
        }


        public async Task<List<Usuario>> Listar()
        {
            List<Usuario> usuarios = new List<Usuario>();

            usuarios =  await _context.Usuarios.AsNoTracking().ToListAsync();
            return usuarios;
        }


        public async Task<Usuario> ObtenerCorreo(string correo)
        {
            var usuario =  await _context.Usuarios.AsNoTracking().FirstOrDefaultAsync(u => u.Correo == correo);
            return usuario;
        }

        public async Task<Usuario> ObtenerId(int id_Usuario)
        {
            var usuario =  await _context.Usuarios.AsNoTracking().FirstOrDefaultAsync(u => u.IdUsuario == id_Usuario);
            return usuario;
        }
    }
}
