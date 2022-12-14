using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using NetKubernetes.Models;

namespace NetKubernetes.Token;


public class UsuarioSesion : IUsuarioSesion
{
    private readonly IHttpContextAccessor _IHttpContextAccessor;

    public UsuarioSesion(IHttpContextAccessor httpContextAccessor)
    {
        _IHttpContextAccessor=httpContextAccessor;
    }

    public string ObtenerUsuarioSesion()
    {
        var userName= _IHttpContextAccessor.HttpContext!.User?.Claims?.FirstOrDefault(x=>x.Type==ClaimTypes.NameIdentifier)?.Value;
        return userName;
        
    }
}