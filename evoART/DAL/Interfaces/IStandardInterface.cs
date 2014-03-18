using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace evoART.DAL.Interfaces
{
    interface IStandardInterface
    {
        bool Save();

        bool SaveAsync();
    }
}
