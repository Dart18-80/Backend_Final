using Backend_Final.Data;
using Backend_Final.Models;
using Backend_Final.RepositoryPattern.IRepository;

namespace Backend_Final.RepositoryPattern
{
    public class BicicletaRepository : IBicicletaRepository
    {
        private readonly ApplicationDbContext _db;
        public BicicletaRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool ActualizarBicicleta(bicicleta Bicicleta)
        {
            _db.Bicicleta.Update(Bicicleta);
            return Guardar();
        }

        public bool BorrarBicicleta(bicicleta Bicicleta)
        {
            _db.Bicicleta.Remove(Bicicleta);
            return Guardar();
        }

        public bool CrearBicicleta(bicicleta Bicicleta)
        {
            _db.Bicicleta.Add(Bicicleta);
            return Guardar();
        }

        public bool ExisteBicicleta(string nombre)
        {
            bool valor = _db.Bicicleta.Any(c => c.Name.ToLower().Trim() == nombre.ToLower().Trim());
            return valor;
        }

        public bool ExisteBicicleta(int idBicicleta)
        {
            bool valor = _db.Bicicleta.Any(c => c.Id == idBicicleta);
            return valor;
        }

        public bicicleta GetBicicleta(int idBicicleta)
        {
            return _db.Bicicleta.FirstOrDefault(c => c.Id == idBicicleta);
        }

        public bool Guardar()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public ICollection<bicicleta> _GetBicicleta()
        {
            return _db.Bicicleta.OrderBy(c => c.Name).ToList();
        }
    }
}
