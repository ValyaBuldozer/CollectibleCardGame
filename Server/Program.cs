using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Database;
using Server.Models;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            AppDbContext context = new AppDbContext();
            context.Users.Add(new User() {Userame = "test", Password = "test"});
            context.Save();
            IEnumerable<User> users = context.Users;
            
        }
    }
}
