using Api_FiesteDocs.Data;
using Api_FiesteDocs.Entities;
using Api_FiesteDocs.Functions;
using Api_FiesteDocs.Models;
using Api_FiesteDocs.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api_FiesteDocs.Services
{
    public class S_Partitura:I_Partitura
    {
        private readonly ApplicationDbContext _context;
        public S_Partitura(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Partitura>> Buscar(string Nombre)
        {
            List<Partitura> partituras = new List<Partitura>();
            partituras = await _context.Partituras.AsNoTracking()
                .Where(p => p.Nombre.Contains(Nombre.ToUpper()))
                .ToListAsync();
            return partituras;
        }

        public async Task<Request> Crear(Partitura partitura)
        {
            partitura.Carpeta = Archivos.NormalizarCarpeta(partitura.Carpeta);
            partitura.Nombre = partitura.Nombre.Trim().ToUpper();
            await _context.Partituras.AddAsync(partitura);
            await _context.SaveChangesAsync();
            return new Request { Message = "Partitura creada correctamente", Success = true };
        }

        public async Task<Request> Editar(Partitura partitura)
        {
            Partitura partituraExistente = await _context.Partituras.FindAsync(partitura.IdPartitura);
            if (partituraExistente == null)
                return new Request { Message = "La partitura no existe", Success = false };

            partituraExistente.IdSeccion = partitura.IdSeccion <= 0
                ? partituraExistente.IdSeccion
                : partitura.IdSeccion;

            partituraExistente.Archivo = string.IsNullOrEmpty(partitura.Archivo)
                ? partituraExistente.Archivo
                : partitura.Archivo;

            partituraExistente.Comentarios = string.IsNullOrEmpty(partitura.Comentarios)
                ? partituraExistente.Comentarios
                : partitura.Comentarios;

            partituraExistente.Nombre = string.IsNullOrEmpty(partitura.Nombre)
                ? partituraExistente.Nombre
                : partitura.Nombre.ToUpper();

            partituraExistente.Carpeta = string.IsNullOrEmpty(partitura.Carpeta)
                ? partituraExistente.Carpeta
                : partitura.Carpeta.Trim().ToUpper();

            partituraExistente.Tipo = string.IsNullOrEmpty(partitura.Tipo)
                ? partituraExistente.Tipo
                : partitura.Tipo.Trim().ToLower();

            _context.SaveChanges();
            return new Request { Message = "Partitura editada correctamente", Success = true };

        }

        public async Task<Request> Eliminar(int Id_Partitura)
        {
            if (Id_Partitura <= 0)
                return new Request { Message = "El Id de la partitura no es valido", Success = false };
            Partitura partitura = await _context.Partituras.FindAsync(Id_Partitura);
            if (partitura == null)
                return new Request { Message = "La partitura no existe", Success = false };
            _context.Partituras.Remove(partitura);
            _context.SaveChanges();
            return new Request { Message = "Partitura eliminada correctamente", Success = true };
        }

        public async Task<List<Partitura>> Listar()
        {
            List<Partitura> partituras = new List<Partitura>();
            partituras = await _context.Partituras.AsNoTracking().ToListAsync();
            return partituras;
        }

        public async Task<List<Partitura>> ListarIdGrupo(int Id_Grupo)
        {
            if (Id_Grupo <= 0)
                return new List<Partitura>();

            return await _context.Partituras.AsNoTracking()
                .Where(p => _context.Seccions
                    .Any(s => s.IdSeccion == p.IdSeccion && s.IdGrupo == Id_Grupo))
                .ToListAsync();
        }

        public async Task<List<InfoPartitura>> ListarInfo(int Id_Grupo)
        {
            if (Id_Grupo <= 0)
                return new List<InfoPartitura>();

            return await _context.Partituras.AsNoTracking()
                .Where(p => _context.Seccions.Any(s => s.IdSeccion == p.IdSeccion && s.IdGrupo == Id_Grupo))
                .Select(p => new InfoPartitura
                {
                    IdPartitura = p.IdPartitura,
                    IdSeccion = p.IdSeccion,
                    Nombre = p.Nombre,
                    Comentarios = p.Comentarios
                })
                .ToListAsync();
        }


        public async Task<List<Partitura>> ListarIdSeccion(int Id_Seccion)
        {
            List<Partitura> partituras = new List<Partitura>();
            partituras = await _context.Partituras.AsNoTracking()
                .Where(p => p.IdSeccion == Id_Seccion)
                .ToListAsync();
            return partituras;
        }

        public async Task<Partitura> Obtener(int Id_Partitura)
        {
            Partitura partitura = new Partitura();
            partitura = await _context.Partituras.FindAsync(Id_Partitura);
            return partitura;
        }
    }
}
