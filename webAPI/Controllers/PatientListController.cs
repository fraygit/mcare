using mcare.API.Models;
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
    public class PatientListController : ApiController
    {
        private readonly IUserTokenRepository userTokenRepository;
        private readonly IUserRepository userRepository;
        private readonly IPractitionerProfileRepository practitionerProfileRepositry;
        private readonly IPatientProfileRepository patientProfileRepository;
        private readonly IMaternityRepository maternityRepository;

        public PatientListController(IUserTokenRepository userTokenRepository, IUserRepository userRepository, IPractitionerProfileRepository practitionerProfileRepositry, IPatientProfileRepository patientProfileRepository, IMaternityRepository maternityRepository)
        {
            this.userRepository = userRepository;
            this.userTokenRepository = userTokenRepository;
            this.practitionerProfileRepositry = practitionerProfileRepositry;
            this.patientProfileRepository = patientProfileRepository;
            this.maternityRepository = maternityRepository;
        }

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpPost]
        public async Task<List<string>> Add(string token, RequestRegisterPatient register)
        {
            if (await userTokenRepository.IsTokenValid(token))
            {
                var userToken = await userTokenRepository.GetUserTokenDetailByToken(token);
                var user = await userRepository.GetUser(userToken.Username);
                if (user != null)
                {
                    if (await patientProfileRepository.GetByUser(register.Email) == null)
                    {
                        await patientProfileRepository.CreateSync(new PatientProfile
                        {
                             Email = register.Email                             
                        });
                    }
                    if (await maternityRepository.GetCurrentByUser(register.Email) == null)
                    {
                        await maternityRepository.CreateSync(new Maternity
                        {
                            Email = register.Email,
                            Status = "Active"
                        });
                    }
                    if (await userRepository.GetUser(register.Email) == null)
                    {
                        await userRepository.CreateSync(new User
                        {
                            Email = register.Email,
                            FirstName = register.FirstName,
                            LastName = register.LastName,
                            Password = Guid.NewGuid().ToString().Substring(0, 8)
                        });
                    }

                    var practitionerProfile = await practitionerProfileRepositry.GetByUser(user.Email);
                    if (!practitionerProfile.Patients.Contains(register.Email))
                    {
                        practitionerProfile.Patients.Add(register.Email);
                        await practitionerProfileRepositry.Update(practitionerProfile.Id.ToString(), practitionerProfile);
                        return practitionerProfile.Patients;
                    }
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest)
                    {
                        Content = new StringContent("Patient already exist."),
                        ReasonPhrase = "Please add a different one."
                    });
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
