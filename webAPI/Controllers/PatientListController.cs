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

        /// <summary>
        /// Register a new patient
        /// </summary>
        /// <param name="token"></param>
        /// <param name="register"></param>
        /// <returns></returns>
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpPut]
        public async Task<List<PatientList>> Add(string token, RequestRegisterPatient register)
        {
            if (await userTokenRepository.IsTokenValid(token))
            {
                var userToken = await userTokenRepository.GetUserTokenDetailByToken(token);
                var user = await userRepository.GetUser(userToken.Username);
                if (user != null)
                {
                    try
                    {
                        if (await patientProfileRepository.GetByUser(register.Email) == null)
                        {
                            await patientProfileRepository.CreateSync(new PatientProfile
                            {
                                Email = register.Email,
                                NHI = register.NHI
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
                        var patientNotRegisteredToLMC = false;
                        if (practitionerProfile.Patients != null)
                        {
                            patientNotRegisteredToLMC = practitionerProfile.Patients.Any(n => n.Email == register.Email);
                        }
                        if (!patientNotRegisteredToLMC)
                        {
                            if (practitionerProfile.Patients != null)
                            {
                                practitionerProfile.Patients.Add(new PatientList
                                {
                                    Email = register.Email,
                                    DateRegistered = DateTime.Now
                                });
                            }
                            else
                            {
                                practitionerProfile.Patients = new List<PatientList>();
                                practitionerProfile.Patients.Add(new PatientList
                                {
                                    Email = register.Email,
                                    DateRegistered = DateTime.Now
                                });
                            }
                            await practitionerProfileRepositry.Update(practitionerProfile.Id.ToString(), practitionerProfile);
                            return practitionerProfile.Patients;
                        }
                        throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest)
                        {
                            Content = new StringContent("Patient already exist."),
                            ReasonPhrase = "Please add a different one."
                        });
                    }
                    catch (Exception ex)
                    {
                        throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                        {
                            Content = new StringContent("Error occured." + ex.Message),
                            ReasonPhrase = "Internal Server error."
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


        /// <summary>
        /// Get practioner's patient lists
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpGet]
        public async Task<List<ResponseMaternityProfile>> Get(string token)
        {
            if (await userTokenRepository.IsTokenValid(token))
            {
                var userToken = await userTokenRepository.GetUserTokenDetailByToken(token);
                var user = await userRepository.GetUser(userToken.Username);
                if (user != null)
                {
                    try
                    {
                        var practitionerProfile = await practitionerProfileRepositry.GetByUser(user.Email);
                        var patientLists = new List<ResponseMaternityProfile>();
                        foreach (var patientEmail in practitionerProfile.Patients)
                        {
                            var maternity = await maternityRepository.GetCurrentByUser(patientEmail.Email);
                            if (maternity != null)
                            {
                                var patientProfile = await patientProfileRepository.GetByUser(patientEmail.Email);
                                var patientUserProfile = await userRepository.GetUser(patientEmail.Email);
                                patientLists.Add(new ResponseMaternityProfile
                                {
                                    Maternity = maternity,
                                    PatientProfile = patientProfile,
                                    User = patientUserProfile
                                });
                            }
                        }
                        return patientLists;
                    }
                    catch (Exception ex)
                    {
                        throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                        {
                            Content = new StringContent("Error occured." + ex.Message),
                            ReasonPhrase = "Internal Server error."
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
            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent("Invalid token"),
                ReasonPhrase = "Please login."
            });
        }

    }
}
