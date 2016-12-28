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
    public class AppointmentController : ApiController
    {
        private readonly IUserTokenRepository userTokenRepository;
        private readonly IUserRepository userRepository;
        private readonly IAppointmentRepository appointmentRepository;

        public AppointmentController(IAppointmentRepository appointmentRepository, IUserTokenRepository userTokenRepository, IUserRepository userRepository)
        {
            this.userRepository = userRepository;
            this.userTokenRepository = userTokenRepository;
            this.appointmentRepository = appointmentRepository;
        }

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpGet]
        public async Task<List<Appointment>> Get(string token)
        {
            if (await userTokenRepository.IsTokenValid(token))
            {
                var userToken = await userTokenRepository.GetUserTokenDetailByToken(token);
                var user = await userRepository.GetUser(userToken.Username);
                if (user != null)
                {
                    try
                    {
                        var appointments = await appointmentRepository.GetByUser(user.Email);
                        return appointments;
                    }
                    catch (Exception ex)
                    {
                        throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                        {
                            Content = new StringContent("Error executing. " + ex.Message),
                            ReasonPhrase = "Call your administrator."
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

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpPut]
        public async Task<Appointment> Add(string token, Appointment appointment)
        {
            if (await userTokenRepository.IsTokenValid(token))
            {
                var userToken = await userTokenRepository.GetUserTokenDetailByToken(token);
                var user = await userRepository.GetUser(userToken.Username);
                if (user != null)
                {
                    try{
                        var newAppointment = new Appointment
                        {
                            User = user.Email,
                            DateFrom = appointment.DateFrom,
                            DateTo = appointment.DateTo,
                            Details = appointment.Details,
                            Title = appointment.Title,
                            Attendees = appointment.Attendees
                        };
                        await appointmentRepository.CreateSync(newAppointment);
                        return newAppointment;
                    }
                    catch(Exception ex){
                        throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                        {
                            Content = new StringContent("Error executing. " + ex.Message),
                            ReasonPhrase = "Call your administrator."
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
