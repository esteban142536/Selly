
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Web.Security
{
    //Especifica que el acceso a un controlador o método de acción está restringido
    //a los usuarios que cumplen con el requisito de autorización.
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        //Roles permitidos
        private readonly int[] allowedroles;
        public CustomAuthorizeAttribute(params int[] tipoUsuario)
        {
            //roles Obtiene los roles de usuario autorizados
            //para acceder al controlador o al método de acción.
            this.allowedroles = tipoUsuario;
        }

        //Verificaciones de autorización personalizadas
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool authorize = false;
            var oUsuario = (usuario)httpContext.Session["Usuario"];

            if (oUsuario != null)
            {
                foreach (var TipoUsuario in allowedroles)
                {
                    if (TipoUsuario == oUsuario.idTipoUsuario)
                        return true;
                }
            }
            return authorize;
        }

        //Procesa solicitudes HTTP que fallan en la autorización.
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            // Si hubo un error redireccione a el siguiente Controller y View
            filterContext.Result = new RedirectToRouteResult(
               new RouteValueDictionary
               {
                    { "controller", "Usuario" },
                    { "action", "sinPermisos" }
               });
        }
    }
}
