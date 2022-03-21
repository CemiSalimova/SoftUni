using FootballManager.ViewModels.UsersViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManager.Contracts
{
    public interface IUserService
    {
        (bool registered, string error) Register(RegisterViewModel model);

        string Login(LoginViewModel model);

    }
}
