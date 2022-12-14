using Microsoft.AspNetCore.Identity;
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
        inmueble.FechaCreacion= DateTime.Now;
        inmueble.UsuarioID= Guid.Parse( usuario.Id);

    }
    public void DeleteInmueble(int Id)
    {

    }

   
}