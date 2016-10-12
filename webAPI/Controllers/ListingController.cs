using mcare.API.Custom;
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
    public class ListingController : ApiController
    {
        private readonly IListingRepository listingRepository;

        public ListingController(IListingRepository listingRepository)
        {
            this.listingRepository = listingRepository;
        }

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpGet]
        public async Task<List<Listing>> GetAll()
        {
            return await listingRepository.GetAllCurrentActive();
        }

        [TokenAuthentication]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpPut]
        public async Task<Listing> Create(Listing listing)
        {
            if (!string.IsNullOrEmpty(listing.Title))
            {
                try
                {
                    await listingRepository.CreateSync(listing);
                    return listing;
                }
                catch (Exception ex)
                {
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                    {
                        Content = new StringContent("Error occured"),
                        ReasonPhrase = ex.Message
                    });
                }
            }
            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent("Invalid listing"),
                ReasonPhrase = "Please check required fields"
            });

        }
    }
}
