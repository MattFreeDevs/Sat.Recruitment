
using Sat.Recruitment.Api.Entities;
using Sat.Recruitment.Api.Validations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Repositories
{
    public interface IUserRepository
    {
        Result CreateUser(User user);
        List<User> GetAll();
    }
}
