using mcare.MongoData.Model;
using mcare.MongoData.Service;
using System.Threading.Tasks;

namespace mcare.MongoData.Interface
{
    public interface IUserRepository : IEntityService<User>
    {
        Task<User> GetUser(string username);
    }
}
