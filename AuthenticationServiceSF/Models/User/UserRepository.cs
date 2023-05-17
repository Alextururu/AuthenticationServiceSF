using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AuthenticationServiceSF
{
    public class UserRepository : IUserRepository
    {
        private readonly List<User> _users = new List<User>();
        public UserRepository()
        {
            User user1 = new User()
            {
                Id = Guid.NewGuid(),
                Email = "ivanovivan@gmail.com",
                Login = "ivan",
                Password = "ivan123",
                FirstName = "Иван",
                LastName = "Иванов",
                role = new Role(1, "Пользователь")
            };
            User user2 = new User()
            {
                Id = Guid.NewGuid(),
                Email = "stepanovalex@gmail.com",
                Login = "alex",
                Password = "alex123",
                FirstName = "Алексей",
                LastName = "Степанов",
                role = new Role(2, "Администратор")
            };
            User user3 = new User()
            {
                Id = Guid.NewGuid(),
                Email = "fedormironov@gmail.com",
                Login = "fedor",
                Password = "fedor123",
                FirstName = "Федор",
                LastName = "Миронов",
                role = new Role(1, "Пользователь")
            };
            _users.Add(user1);
            _users.Add(user2);
            _users.Add(user3);
        }
        public IEnumerable<User> GetAll()
        {
            return _users;
        }

        public User GetByLogin(string login)
        {
            User user = _users.Where(x => x.Login == login).Select(x => x).FirstOrDefault();
            return user;
        }
    }
}
