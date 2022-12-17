using System.Net;
using Microsoft.AspNetCore.Identity;
using NetKubernetes.Middleware;
using NetKubernetes.Models;
using NetKubernetes.Token;

namespace NetKubernetes.Data.Inmuebles;


public class InmuebleRepository : IInmuebleRepository
{
    private readonly AppDbContext _contexto;
    private  readonly IUsuarioSesion _usuariosesion;
    private readonly UserManager<Usuario> _userManager;

    public InmuebleRepository(AppDbContext contexto,
                              IUsuarioSesion usuariosesion,
                              UserManager<Usuario> userManager )
    {
        _contexto=contexto;
        _usuariosesion=usuariosesion;
        _userManager=userManager;
    }
    public async Task CreateInmueble(Inmueble inmueble)
    {
        var usuario=await _userManager.FindByNameAsync(_usuariosesion.ObtenerUsuarioSesion());
        if(usuario is null)
        {
            throw new MiddlewareException(HttpStatusCode.Unauthorized,new {mensaje="El usuario no es valido para la creación del registro"});
        }
        if(inmueble is null)
        {
            throw new MiddlewareException(HttpStatusCode.BadRequest,new {mensaje="Los datos del inmueble son incorrectos"});
        }

        inmueble.FechaCreacion= DateTime.Now;
        inmueble.UsuarioID= Guid.Parse( usuario.Id);
        _contexto.Inmuebles!.Add(inmueble);
    }
    public void DeleteInmueble(int Id)
    {
        var inmueble=_contexto.Inmuebles!.FirstOrDefault(x=>x.Id==Id);
        _contexto.Inmuebles!.Remove(inmueble!);
    }
    public IEnumerable<Inmueble>GetAllInmuebles()
    {
        return _contexto.Inmuebles!.ToList();
    }
    public Inmueble GetInmuebleById(int Id)
    {
        return _contexto.Inmuebles!.FirstOrDefault(x=> x.Id==Id)!;
    }

    public Inmueble GetInmuebleByID(int Id)
    {
        throw new NotImplementedException();
    }

    public bool SaveChanges()
    {
        return (_contexto.SaveChanges()>=0);

    }

   
}