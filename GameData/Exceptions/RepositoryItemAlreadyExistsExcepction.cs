using System;

namespace GameData.Exceptions
{
    public class RepositoryItemAlreadyExistsExcepction : Exception
    {
        public RepositoryItemAlreadyExistsExcepction(string message) : base(message)
        {
        }

        public RepositoryItemAlreadyExistsExcepction()
        {
        }
    }
}