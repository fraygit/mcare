using mcare.API.Models;
using mcare.MongoData.Interface;
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
    public class MaternityProfileController : ApiController
    {
        private readonly IMaternityRepository maternityRepository;
        private readonly IPatientProfileRepository patientProfileRepository;
        private readonly IUserTokenRepository userTokenRepository;
        private readonly IUserRepository userRepository;

        public MaternityProfileController(IMaternityRepository maternityRepository, IUserTokenRepository userTokenRepository, IUserRepository userRepository, IPatientProfileRepository patientProfileRepository)
        {
            this.maternityRepository = maternityRepository;
            this.patientProfileRepository = patientProfileRepository;
            this.userTokenRepository = userTokenRepository;
            this.userRepository = userRepository;
        }

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpPost]
        public async Task<ResponseMaternityProfile> Update(string token, ResponseMaternityProfile profile)
        {
            if (await userTokenRepository.IsTokenValid(token))
            {
                var userToken = await userTokenRepository.GetUserTokenDetailByToken(token);
                var user = await userRepository.GetUser(userToken.Username);
                if (user != null)
                {
                    var patientProfile = await patientProfileRepository.GetByUser(user.Email);
                    var maternity = await maternityRepository.GetCurrentByUser(user.Email);

                    user.FirstName = profile.User.FirstName;
                    user.LastName = profile.User.LastName;
                    patientProfile.Address = profile.PatientProfile.Address;
                    patientProfile.DateOfBirth = profile.PatientProfile.DateOfBirth;
                    patientProfile.HomeNumber = profile.PatientProfile.HomeNumber;
                    patientProfile.MobileNumber = profile.PatientProfile.MobileNumber;
                    patientProfile.NHI = profile.PatientProfile.NHI;
                    patientProfile.Gender = profile.PatientProfile.Gender;

                    maternity.LastPeriod = profile.Maternity.LastPeriod;

                    await maternityRepository.Update(maternity.Id.ToString(), maternity);
                    await patientProfileRepository.Update(patientProfile.Id.ToString(), patientProfile);
                    await userRepository.Update(user.Id.ToString(), user);

                    return new ResponseMaternityProfile
                    {
                        PatientProfile = patientProfile,
                        User = user,
                        Maternity = maternity
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
        /// Get patient profile by token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpGet]
        public async Task<ResponseMaternityProfile> Get(string token)
        {
            if (await userTokenRepository.IsTokenValid(token))
            {
                var userToken = await userTokenRepository.GetUserTokenDetailByToken(token);
                var user = await userRepository.GetUser(userToken.Username);
                if (user != null)
                {
                    var patientProfile = await patientProfileRepository.GetByUser(user.Email);
                    var maternity = await maternityRepository.GetCurrentByUser(user.Email);

                    return new ResponseMaternityProfile
                    {
                        PatientProfile = patientProfile,
                        User = user,
                        Maternity = maternity
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
        /// Get patient profile by Email
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpPut]
        public async Task<ResponseMaternityProfile> Get(string token, string email)
        {
            if (await userTokenRepository.IsTokenValid(token))
            {
                var userToken = await userTokenRepository.GetUserTokenDetailByToken(token);
                var user = await userRepository.GetUser(email);
                if (user != null)
                {
                    var patientProfile = await patientProfileRepository.GetByUser(email);
                    var maternity = await maternityRepository.GetCurrentByUser(email);

                    return new ResponseMaternityProfile
                    {
                        PatientProfile = patientProfile,
                        User = user,
                        Maternity = maternity
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
    }
}
