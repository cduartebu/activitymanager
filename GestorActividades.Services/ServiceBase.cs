using GestorActividades.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorActividades.Services
{
    public class ServiceBase
    {
        private IUnitOfWorkFactory myUnitOfWorkFactory;

        public IUnitOfWorkFactory UnitOfWorkFactory
        {
            get
            {
                return myUnitOfWorkFactory ?? (myUnitOfWorkFactory = new ActivityManagerUnitOfWorkFactory());
            }
            set
            {
                myUnitOfWorkFactory = value;
            }
        }
        
    }
}
