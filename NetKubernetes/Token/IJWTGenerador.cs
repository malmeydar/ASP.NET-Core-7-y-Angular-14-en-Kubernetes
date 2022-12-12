using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NetKubernetes.Models;

namespace NetKubernetes.Token;

public interface IJWTGenerador
{
    string CrearToken(Usuario usuario);
   
}
