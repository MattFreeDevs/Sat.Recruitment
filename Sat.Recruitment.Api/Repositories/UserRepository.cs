using Sat.Recruitment.Api.Entities;
using Sat.Recruitment.Api.Validations;
using System.Collections.Generic;

namespace Sat.Recruitment.Api.Repositories
{
    public class UserRepository : DataContext, IUserRepository
    {
        public List<User> data;
        public Result CreateUser(User user)
        {
            throw new System.NotImplementedException();
        }

        public List<User> GetAll()
        {
            data = Users();
            return data;
        }
    }
}
