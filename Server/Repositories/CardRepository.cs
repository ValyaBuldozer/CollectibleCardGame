using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Models;

namespace Server.Repositories
{
    public class CardRepository
    {
        public List<Card> Collection { set; get; }

        public CardRepository()
        {
            Collection = new List<Card>
            {
                new Card(){Id=1,Name = "test1",Descritprion = "test1"},
                new Card(){Id=2,Name = "test2",Descritprion = "test2"}
            };
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}
