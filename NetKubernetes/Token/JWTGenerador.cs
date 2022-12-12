using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using NetKubernetes.Models;

namespace NetKubernetes.Token;

public class JWTGenerador:IJWTGenerador
{
    public string CrearToken(Usuario usuario)
    {
        var claims=new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.NameId,usuario.UserName!),
            new Claim("userid",usuario.Id),
            new Claim("email",usuario.Email!)
        };
        var key=new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Mi palabra secreta"));
        var Credenciales= new SigningCredentials(key,SecurityAlgorithms.HmacSha512Signature);
        var TokenDescripcion= new SecurityTokenDescriptor{
            Subject=new ClaimsIdentity(claims),
            Expires=DateTime.Now.AddDays(30),
            SigningCredentials= Credenciales
        };
        var TokenHandler= new JwtSecurityTokenHandler();
        var token= TokenHandler.CreateToken(TokenDescripcion);
        return TokenHandler.WriteToken(token);
    }
}