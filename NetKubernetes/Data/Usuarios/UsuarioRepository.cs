using System.Net;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NetKubernetes.Dtos.UsuriosDtos;
using NetKubernetes.Middleware;
using NetKubernetes.Models;
using NetKubernetes.Token;

namespace NetKubernetes.Data.Usuarios;

public class UsuarioRepository : IUsuarioRepository
{
    private  readonly UserManager<Usuario>?_UserManager;
    private readonly SignInManager<Usuario>? _SignInManager;
    private readonly IJWTGenerador? _IJwtGenerador;
    private readonly AppDbContext? _contexto;
    private readonly IUsuarioSesion? _usuarioSesion;

    public UsuarioRepository(UserManager<Usuario> userManager,
                             SignInManager<Usuario> signInManager, 
                             IJWTGenerador jWTGenerador,
                             AppDbContext contexto ,
                             IUsuarioSesion usuarioSesion)
    {
        _UserManager=userManager;
        _SignInManager=signInManager;
        _IJwtGenerador= jWTGenerador;
        _contexto=contexto;
        _usuarioSesion=usuarioSesion;
        
    }

    private UsuarioResponseDto TransformerUserToUserDto(Usuario usuario)
    {
        return new UsuarioResponseDto
        {
            Id=usuario.Id,
            Nombre=usuario.Nombre,
            Apellido=usuario.Apellido,
            Telefono=usuario.Telefono,
            Email=usuario.Email,
            UserNAme=usuario.UserName,
            Token= _IJwtGenerador?.CrearToken(usuario)

        };
    }

    
    public async Task<UsuarioResponseDto> GetUsuario()
    {
        var usuario= await _UserManager!.FindByNameAsync(_usuarioSesion!.ObtenerUsuarioSesion());
        if (usuario is null)
        {
             throw new MiddlewareException(HttpStatusCode.Unauthorized,
                                            new {mensaje="El usuario del Token no existe en la base de datos"});
        }
        return TransformerUserToUserDto(usuario!);

    }

    public async Task<UsuarioResponseDto> Login(UsuarioLoginRequestDto request)
    {
       var usuario= await _UserManager!.FindByEmailAsync(request.Email!);
        if (usuario is null)
        {
             throw new MiddlewareException(HttpStatusCode.Unauthorized,
                                            new {mensaje="El Email del usuario no existe en la Base de datos"});
        }

       var resultado= await _SignInManager!.CheckPasswordSignInAsync(usuario!,request.Password!,false);
       if (resultado.Succeeded)
       {

            return TransformerUserToUserDto(usuario!);

       }
       throw new MiddlewareException(HttpStatusCode.Unauthorized,new {mensaje="Las credenciales son incorrectas"});
    }

    public async Task<UsuarioResponseDto> RegistroUsuario(UsuarioRegistroRequestDto request)
    {
        var existeEmail=await _contexto!.Users.Where(x => x.Email==request.Email).AnyAsync();

        if (existeEmail)
        {
            throw new MiddlewareException(HttpStatusCode.BadRequest,new{mensaje="El Email del usuario ya existe en la base de datos"} );
        }
        
        var existeUserName=await _contexto!.Users.Where(x => x.UserName==request.UserName).AnyAsync();

        if (existeUserName)
        {
            throw new MiddlewareException(HttpStatusCode.BadRequest,new{mensaje="El UserName del usuario ya existe en la base de datos"} );
        }        
        var usuario= new Usuario{
            Nombre=request.Nombre,
            Apellido=request.Apellido,
            Telefono=request.Telefono,
            Email= request.Email,
            UserName=request.UserName,
            
        };
        var resultado= await _UserManager!.CreateAsync(usuario!,request.Password!);
        if(resultado.Succeeded)
        {
               return TransformerUserToUserDto(usuario);
        }
        throw new Exception("No se pudo registrar el usuario");
     

    }
}