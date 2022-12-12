using Microsoft.AspNetCore.Identity;
using  Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NetKubernetes.Models;

namespace NetKubernetes.Data;

public class LoadDataBase
{
    public static async Task InsertarData(AppDbContext context,UserManager<Usuario> usuarioManager)
    {
        if(!usuarioManager.Users.Any())
        {
            var usuario= new Usuario
            {
                Nombre = "Miguel",
                Apellido="Almeyda Ramos",
                Telefono="991059515",
                Email="miguelangel3316@gmail.com",
                UserName="malmeydar"

            };
            await usuarioManager.CreateAsync(usuario,"MigCyn05$$");

        }
        if(!context.Inmuebles!.Any())
        {
            context.Inmuebles!.AddRange(
              new Inmueble{
                Nombre="Casa de Playa",
                Direccion="Urb Cooperativa Mz Jo",
                Precio= 45000M,
                FechaCreacion=DateTime.Now
              },
              new Inmueble{
                Nombre="Casa de Playa 2",
                Direccion="Urb Cooperativa Mz Ju",
                Precio= 50000M,
                FechaCreacion=DateTime.Now
              }  
            );
        }
        context.SaveChanges();
    }


}