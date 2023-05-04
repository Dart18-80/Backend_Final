using Backend_Final.Models;

namespace Backend_Final.RepositoryPattern.IRepository
{
    public interface IUsuarioRepository
    {
        usuario GetUsuario(int idUsuario);
        usuario GetUsuario(string userName);
        bool ExisteUsuario(string nombre);
        bool ExisteUsuario(int idUsuario);
        bool CrearUsuario(usuario Usuario);
        bool Guardar();
    }
}
