using NetKubernetes.Models;

namespace NetKubernetes.Token;

public interface IJWTGenerador
{
    string CrearToken(Usuario usuario);
   
}
