using AspProjekat.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspProjekat.Implementation.UseCases
{
    public abstract class EfUseCase
    {
        private readonly AspContext _context;

        protected EfUseCase(AspContext context)
        {
            _context = context;
        }

        protected AspContext Context => _context;
    }
}
