using System.Threading.Tasks;
using AccountService.Models;
using AccountService.Models.RequestModels;

namespace AccountService.Services.Contracts
{
    public interface IUserDataService
    {
        Task<User> GetUser(int userId);
        Task<User> GetUserByUsername(string username);


        Task<bool> CheckUserPassword(string username, string inputPassword);


        Task<User> Create(User newUser, string password);

        Task<User> UpdateUser(UpdateUserRequestModel request);

        Task<bool> UpdateUserPassword(UpdateUserPasswordRequestModel request);
    }
}
