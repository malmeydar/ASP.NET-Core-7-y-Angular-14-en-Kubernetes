using NetKubernetes.Models;
namespace NetKubernetes.Data.Inmuebles;


public interface IInmuebleRepository
{
    bool SaveChanges();
    Ienumerables<Inmueble>GetAllInmuebles();

    Inmueble GetInmuebleByID(int Id);

    void CreateInmueble(Inmueble inmueble);
    void DeleteInmueble(int Id);
    

}