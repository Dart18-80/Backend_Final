using Backend_Final.Models;

namespace Backend_Final.RepositoryPattern.IRepository
{
    public interface IBicicletaRepository
    {
        ICollection<bicicleta> _GetBicicleta();
        bicicleta GetBicicleta(int idBicicleta);
        bool ExisteBicicleta(string nombre);
        bool ExisteBicicleta(int idBicicleta);
        bool CrearBicicleta(bicicleta Bicicleta);
        bool ActualizarBicicleta(bicicleta Bicicleta);
        bool BorrarBicicleta(bicicleta Bicicleta);
        bool Guardar();
    }
}
