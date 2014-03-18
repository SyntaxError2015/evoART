using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace evoART.DAL.Interfaces
{
    public interface ISessions : IStandardInterface, IDisposable
    {
        void OpenSession(int userId);

        void CloseSession(int userId);
    }
}
