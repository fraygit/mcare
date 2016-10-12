using mcare.MongoData.Model;
using mcare.MongoData.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mcare.MongoData.Interface
{
    public interface IAdminUserTokenRepository : IEntityService<AdminUserToken>
    {
        Task<AdminUserToken> GetUserToken(string username);
        Task<bool> IsTokenValid(string userToken);
    }
}
