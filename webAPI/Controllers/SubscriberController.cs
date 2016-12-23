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
    public class SubscriberController : ApiController
    {
        private readonly ISubscribersRepository subscribersRepository;

        public SubscriberController(ISubscribersRepository subscribersRepository)
        {
            this.subscribersRepository = subscribersRepository;
        }

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpPut]
        public async Task<bool> Add(string email)
        {
            try
            {
                await subscribersRepository.CreateSync(new Subscribers
                {
                    Email = email,
                    DateAdded = DateTime.Now
                });
                return true;
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("Error adding subscriber."),
                    ReasonPhrase = ex.Message
                });
            }
        }
    }
}
