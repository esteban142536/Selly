using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiseTipoMovimiento : IServiseTipoMovimiento
    {
        IRepositoryTipoMovimiento rtm = new RepositoryTipoMovimiento();
        public int agregarTipoMovimiento(TipoMovimiento tm)
        {
           return rtm.agregarTipoMovimiento(tm);
        }

        public void modificarTipoMovimiento(TipoMovimiento tm)
        {
            throw new NotImplementedException();
        }
    }
}
