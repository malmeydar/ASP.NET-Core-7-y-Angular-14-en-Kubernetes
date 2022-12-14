using NetKubernetes.Models;
namespace NetKubernetes.Data.Inmuebles;


public interface IInmuebleRepository
{
    bool SaveChanges();
    IEnumerable<Inmueble>GetAllInmuebles();

    Inmueble GetInmuebleByID(int Id);

    Task CreateInmueble(Inmueble inmueble);
    void DeleteInmueble(int Id);
    

}