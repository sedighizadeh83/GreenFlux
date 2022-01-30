using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenFlux.GlobalErrorHandling.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException()
        : base("Entity is not found in the database.")
        {
        }
    }
}
