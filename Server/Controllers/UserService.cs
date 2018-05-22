using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using BaseNetworkArchitecture.Server;
using Server.Controllers.Repository;
using Server.Exceptions;
using Server.Models;

namespace Server.Controllers
{
    public class UserService
    {
        private readonly UserReposController _userReposController;
        private readonly ICollection<IClientConnection> _clients;

        public UserService(UserReposController userReposController,IServer server,
            ConnectedClientsRepositoryController clientsRepositoryController)
        {
            _userReposController = userReposController;
            _clients = server.Clients;
        }

        public string RegisterUser(string username, string password)
        {
            if(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                throw new UserServiceException("Username or password was empty");

            if(_userReposController.GetEnumerable.FirstOrDefault(u=>u.Username == username)
                !=null)
                throw new UserServiceException("User is already exists");

            var user = new User() { Username = username, Password = password, UserInfo = new UserInfo()};
            _userReposController.Add(user);

            return user.Username;
        }

        public User LogIn(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                throw new NullReferenceException();

            if(_clients.FirstOrDefault(c=>c.IdentificatorTocken == username)
                 !=null)
                throw new UserServiceException("User is already in system");

            var user = _userReposController.GetEnumerable.FirstOrDefault(
                u => u.Username == username);

            if(user == null)
                throw new UserServiceException("No such user is exists");

            if(user.Password != password)
                throw new UserServiceException("Incorrect password");

            return user;
        }

        public UserInfo GetUserInfo(string username)
        {
            return _userReposController.GetEnumerable.FirstOrDefault(u => u.Username == username)?.UserInfo;
        }

        public void SetUserInfo(string username, UserInfo userInfo)
        {
            var user = _userReposController.GetEnumerable.FirstOrDefault(u => u.Username == username);

            if(user == null)
                throw new Exception("No user found");

            user.UserInfo = userInfo;
            _userReposController.Edit(user);
        }
    }
}
