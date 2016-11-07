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
    public class MaternityController : ApiController
    {
        private readonly IMaternityRepository maternityRepository;
        private readonly IPatientProfileRepository patientProfileRepository;
        private readonly IUserTokenRepository userTokenRepository;
        private readonly IUserRepository userRepository;

        public MaternityController(IMaternityRepository maternityRepository, IUserTokenRepository userTokenRepository, IUserRepository userRepository, IPatientProfileRepository patientProfileRepository)
        {
            this.maternityRepository = maternityRepository;
            this.patientProfileRepository = patientProfileRepository;
            this.userTokenRepository = userTokenRepository;
            this.userRepository = userRepository;
        }

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpPut]
        public async Task<bool> Add(string token, Maternity maternity)
        { 
            if (await userTokenRepository.IsTokenValid(token))
            {
                var userToken = await userTokenRepository.GetUserTokenDetailByToken(token);
                var user = await userRepository.GetUser(userToken.Username);
                if (user != null)
                {
                    var updateMaternity = await maternityRepository.Get(maternity.Id.ToString());

                    return true;
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
