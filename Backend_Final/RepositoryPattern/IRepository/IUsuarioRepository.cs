using Backend_Final.Models;

namespace Backend_Final.RepositoryPattern.IRepository
{
    public interface IUsuarioRepository
    {
        ICollection<usuario> _GetUsuario();
        usuario GetUsuario(int idUsuario);
        bool ExisteUsuario(string nombre);
        bool ExisteUsuario(int idUsuario);
        bool CrearUsuario(usuario Usuario);
        bool ActualizarUsuario(usuario Usuario);
        bool BorrarUsuario(usuario Usuario);
        bool Guardar();
    }
}
