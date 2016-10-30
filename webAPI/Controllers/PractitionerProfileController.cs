using mcare.API.Models;
using mcare.MongoData.Interface;
using mcare.MongoData.Model;
using MongoDB.Bson;
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
    public class PractitionerProfileController : ApiController
    {
        private readonly IPractitionerProfileRepository practitionerProfileRepository;
        private readonly IUserTokenRepository userTokenRepository;
        private readonly IUserRepository userRepository;

        public PractitionerProfileController(IPractitionerProfileRepository practitionerProfileRepository, IUserTokenRepository userTokenRepository, IUserRepository userRepository)
        {
            this.practitionerProfileRepository = practitionerProfileRepository;
            this.userTokenRepository = userTokenRepository;
            this.userRepository = userRepository;
        }

        /// <summary>
        /// Create new practitioner profile
        /// </summary>
        /// <param name="token"></param>
        /// <param name="practitionerProfile"></param>
        /// <returns></returns>
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpPut]
        public async Task<PractitionerProfile> Get(string token, PractitionerProfile practitionerProfile)
        {
            if (await userTokenRepository.IsTokenValid(token))
            {
                var userToken = await userTokenRepository.GetUserTokenDetailByToken(token);
                var user = await userRepository.GetUser(userToken.Username);
                if (user != null)
                {
                    var newPractitionerProfile = new PractitionerProfile
                    {
                        Email = user.Email,
                        DateOfBirth = practitionerProfile.DateOfBirth,
                        Ethnicity = practitionerProfile.Ethnicity,
                        Gender = practitionerProfile.Gender,
                        RegistrationNumber = practitionerProfile.RegistrationNumber
                    };

                    await practitionerProfileRepository.CreateSync(newPractitionerProfile);
                    return newPractitionerProfile;
                }
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("Unable to find user."),
                    ReasonPhrase = "Please login."
                });
            }
            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent("Invalid token"),
                ReasonPhrase = "Please login."
            });

        }

        /// <summary>
        /// Get practitioner profile
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpGet]
        public async Task<ResponseUserAndPractitionerProfile> Get(string token)
        {
            if (await userTokenRepository.IsTokenValid(token))
            {
                var userToken = await userTokenRepository.GetUserTokenDetailByToken(token);
                var user = await userRepository.GetUser(userToken.Username);
                if (user != null)
                {
                    var practitionerProfile = await practitionerProfileRepository.GetByUser(user.Email);
                    return new ResponseUserAndPractitionerProfile
                    {
                        PractitionerProfile = practitionerProfile,
                        User = user
                    };
                }
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("Unable to find user."),
                    ReasonPhrase = "Please login."
                });
            }
            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent("Invalid token"),
                ReasonPhrase = "Please login."
            });
        }


        /// <summary>
        /// Update Practitioner Profile
        /// </summary>
        /// <param name="token"></param>
        /// <param name="practitionerProfile"></param>
        /// <returns></returns>
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpPost]
        public async Task<RequestUpdateUserAndPractitionerProfile> Update(string token, RequestUpdateUserAndPractitionerProfile userAndPractitionerProfile)
        {
            if (await userTokenRepository.IsTokenValid(token))
            {
                var userToken = await userTokenRepository.GetUserTokenDetailByToken(token);
                var user = await userRepository.GetUser(userToken.Username);
                if (user != null)
                {
                    try
                    {
                        var updatePractitionerProfile = new PractitionerProfile
                        {
                            Email = user.Email,
                            DateOfBirth = userAndPractitionerProfile.PractitionerProfile.DateOfBirth,
                            Ethnicity = userAndPractitionerProfile.PractitionerProfile.Ethnicity,
                            Gender = userAndPractitionerProfile.PractitionerProfile.Gender,
                            RegistrationNumber = userAndPractitionerProfile.PractitionerProfile.RegistrationNumber,
                            Address = userAndPractitionerProfile.PractitionerProfile.Address,
                            PractitionerType = userAndPractitionerProfile.PractitionerProfile.PractitionerType
                        };
                        await practitionerProfileRepository.Update(userAndPractitionerProfile.PractitionerProfileId, updatePractitionerProfile);

                        var updateUser = new User
                        {
                            FirstName = userAndPractitionerProfile.User.FirstName,
                            LastName = userAndPractitionerProfile.User.LastName,
                            Email = userAndPractitionerProfile.User.Email,
                            Password = userAndPractitionerProfile.User.Password
                        };
                        await userRepository.Update(userAndPractitionerProfile.UserId, updateUser);
                        return userAndPractitionerProfile;
                    }
                    catch (Exception ex)
                    {
                        throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                        {
                            Content = new StringContent(ex.Message),
                            ReasonPhrase = "Please contact your administrator."
                        });
                    }
                }
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("Unable to find user."),
                    ReasonPhrase = "Please login."
                });
            }
            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent("Invalid token"),
                ReasonPhrase = "Please login."
            });

        }
    }
}
