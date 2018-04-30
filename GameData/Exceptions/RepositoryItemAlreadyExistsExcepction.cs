using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameData.Exceptions
{
    public class RepositoryItemAlreadyExistsExcepction : Exception
    {
        public RepositoryItemAlreadyExistsExcepction(string message) : base(message) { }

        public RepositoryItemAlreadyExistsExcepction() : base() { }
    }
}
