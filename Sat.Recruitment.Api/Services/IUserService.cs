using Sat.Recruitment.Api.Entities;
using Sat.Recruitment.Api.Validations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Services
{
    public interface IUserService
    {
        Result CreateUser(User user);
    }
}
