using Api_FiesteDocs.Data;
using Api_FiesteDocs.Entities;
using Api_FiesteDocs.Models;
using Api_FiesteDocs.Services.Interfaces;

namespace Api_FiesteDocs.Services
{
    public class S_Partitura:I_Partitura
    {
        private readonly ApplicationDbContext _context;
        public S_Partitura(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Partitura> Buscar(string Nombre)
        {
            List<Partitura> partituras = new List<Partitura>();
            partituras = _context.Partituras
                .Where(p => p.Nombre.Contains(Nombre.ToUpper()))
                .ToList();
            return partituras;
        }

        public Request Crear(Partitura partitura)
        {
            _context.Partituras.Add(partitura);
            _context.SaveChanges();
            return new Request { Message = "Partitura creada correctamente", Success = true };
        }

        public Request Editar(Partitura partitura)
        {
            Partitura partituraExistente = _context.Partituras.Find(partitura.IdPartitura);
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

            _context.SaveChanges();
            return new Request { Message = "Partitura editada correctamente", Success = true };

        }

        public Request Eliminar(int Id_Partitura)
        {
            if (Id_Partitura <= 0)
                return new Request { Message = "El Id de la partitura no es valido", Success = false };
            Partitura partitura = _context.Partituras.Find(Id_Partitura);
            if (partitura == null)
                return new Request { Message = "La partitura no existe", Success = false };
            _context.Partituras.Remove(partitura);
            _context.SaveChanges();
            return new Request { Message = "Partitura eliminada correctamente", Success = true };
        }

        public List<Partitura> Listar()
        {
            List<Partitura> partituras = new List<Partitura>();
            partituras = _context.Partituras.ToList();
            return partituras;
        }

        public List<Partitura> ListarIdGrupo(int Id_Grupo)
        {
            if (Id_Grupo <= 0)
                return new List<Partitura>();

            return _context.Partituras
                .Where(p => _context.Seccions
                    .Any(s => s.IdSeccion == p.IdSeccion && s.IdGrupo == Id_Grupo))
                .ToList();
        }

        public List<InfoPartitura> ListarInfo(int Id_Grupo)
        {
            if (Id_Grupo <= 0)
                return new List<InfoPartitura>();

            return _context.Partituras
                .Where(p => _context.Seccions.Any(s => s.IdSeccion == p.IdSeccion && s.IdGrupo == Id_Grupo))
                .Select(p => new InfoPartitura
                {
                    IdPartitura = p.IdPartitura,
                    IdSeccion = p.IdSeccion,
                    Nombre = p.Nombre,
                    Comentarios = p.Comentarios
                })
                .ToList();
        }


        public List<Partitura> ListarIdSeccion(int Id_Seccion)
        {
            List<Partitura> partituras = new List<Partitura>();
            partituras = _context.Partituras
                .Where(p => p.IdSeccion == Id_Seccion)
                .ToList();
            return partituras;
        }

        public Partitura Obtener(int Id_Partitura)
        {
            Partitura partitura = new Partitura();
            partitura = _context.Partituras.Find(Id_Partitura);
            return partitura;
        }
    }
}
