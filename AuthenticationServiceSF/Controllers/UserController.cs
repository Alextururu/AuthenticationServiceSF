using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthenticationServiceSF.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private ILogger logger;
        private IMapper mapper;
        private IUserRepository userRepository;
        public UserController( ILogger _logger, IMapper _mapper, IUserRepository userRepository)
        {
            logger = _logger;
            mapper = _mapper;
            logger.WriteEvent("Сообщение о событии в прогремме");
            logger.WriteError("Сообщение об ошибке впрограмме");
            this.userRepository = userRepository;
        }
        [HttpGet]
        public User GetUser()
        {
            return new User()
            {
                Id = Guid.NewGuid(),
                FirstName = "Иван",
                LastName = "Иванов",
                Email = "123@ya.ru",
                Password = "123",
                Login = "ivanov"
            };
        }
        [Authorize(Roles = "Администратор")]
        [HttpGet]
        [Route("viewmodel")]
        public UserViewModel GetUserViewModel()
        {
            User user = new User()
            {
                Id = Guid.NewGuid(),
                FirstName = "Иван",
                LastName = "Иванов",
                Email = "ivan@gmail.com",
                Password = "11111122222qq",
                Login = "ivanov"
            };
            var userViewModel = mapper.Map<UserViewModel>(user);
            return userViewModel;
        }
        //[HttpGet]
        //[Route("getUsers")]
        //public IEnumerable<User> GetUsers()
        //{
        //    var allUsers = userRepository.GetAll();
        //    return allUsers;
        //}

        //[HttpGet]
        //[Route("getUserByLogin")]
        //public User GetUserByLogin(string login)
        //{
        //    return userRepository.GetByLogin(login);
        //}

        [HttpGet]
        [Route("authenticate")]
        public async Task<UserViewModel> Authenticate(string login, string password)
        {
            if (String.IsNullOrEmpty(login) ||    String.IsNullOrEmpty(password))
                throw new ArgumentNullException("Запрос не корректен");
            User user = userRepository.GetByLogin(login);
            if (user is null)
                throw new AuthenticationException("Пользователь на найден");
            if (user.Password != password)
                throw new AuthenticationException("Введенный пароль не корректен");

            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType,user.role.name)
            };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "AppCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            return mapper.Map<UserViewModel>(user);
        }
    }
}
