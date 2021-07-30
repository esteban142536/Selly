﻿using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
   public interface IServiceUsuario
    {
        usuario logIn(String email, String clave);

        void SignIn(usuario usuario);
        IEnumerable<usuario> listadoUsuario();
        void cambiarEstado(int id);

    }
}
