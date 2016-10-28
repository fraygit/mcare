using mcare.API.Models;
using mcare.MongoData.Common;
using mcare.MongoData.Interface;
using mcare.MongoData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace mcare.API.Controllers
{
    public class UserController : ApiController
    {
        private readonly IUserRepository userRepository;
        private readonly IUserTokenRepository userTokenRepository;

        public UserController(IUserRepository userRepository, IUserTokenRepository userTokenRepository)
        {
            this.userRepository = userRepository;
            this.userTokenRepository = userTokenRepository;
        }

        /// <summary>
        /// Create user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpPut]
        public async Task<User> CreateUser(RequestCreateUser user)
        {
            var existingUser = await userRepository.GetUser(user.Email);
            if (existingUser == null)
            {
                try
                {
                    var createdUser = new User
                    {
                        Email = user.Email,
                        Password = user.Password,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        UserType = user.UserType
                    };
                    await userRepository.CreateSync(createdUser);
                    return createdUser;
                }
                catch (Exception ex)
                {
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                    {
                        Content = new StringContent("Error occurred on - CreateUser (UserController)"),
                        ReasonPhrase = ex.Message
                    });
                }
            }
            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent("Existing user"),
                ReasonPhrase = "User with the same email address already exist."
            });
        }

        /// <summary>
        /// User Login
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpPost]
        public async Task<ResponseLogin> Login(RequestLogin user)
        {
            var existingUser = await userRepository.GetUser(user.Email);
            if (existingUser != null)
            {
                if (existingUser.Password == Crypto.HashSha256(user.Password))
                {
                    string generatedToken = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                    var token = new UserToken
                    {
                        LastAccessed = DateTime.UtcNow,
                        Username = user.Email,
                        Token = generatedToken
                    };
                    await userTokenRepository.CreateSync(token);
                    return new ResponseLogin
                    {
                        UserDetails = existingUser,
                        UserToken = token,
                        HasLogon = existingUser.HasLogon,
                        UserType = existingUser.UserType
                    };
                }
            }
            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent("Invalid username or password."),
                ReasonPhrase = "Invalid username or password."
            });
        }
    }
}
