using Backend_Final.Data;
using Backend_Final.Models;
using Backend_Final.RepositoryPattern.IRepository;

namespace Backend_Final.RepositoryPattern
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext _db;
        public UsuarioRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public bool CrearUsuario(usuario Usuario)
        {
            _db.Usuario.Add(Usuario);
            return Guardar();
        }

        public bool ExisteUsuario(string UserName)
        {
            bool valor = _db.Usuario.Any(c => c.UserName.ToLower().Trim() == UserName.ToLower().Trim());
            return valor;
        }

        public bool ExisteUsuario(int idUsuario)
        {
            bool valor = _db.Usuario.Any(c => c.Id == idUsuario);
            return valor;
        }

        public usuario GetUsuario(int idUsuario)
        {
            return _db.Usuario.FirstOrDefault(c => c.Id == idUsuario);
        }

        public usuario GetUsuario(string userName)
        {
            return _db.Usuario.FirstOrDefault(c => c.UserName == userName);
        }

        public bool Guardar()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }
    }
}
