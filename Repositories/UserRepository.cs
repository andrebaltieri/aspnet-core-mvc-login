using DemoLogin.Models;

namespace DemoLogin.Repositories
{
    public static class UserRepository
    {
        public static UserModel Get(string email, string password)
        {
            if (email != "teste@teste.com" || password != "teste")
                return null;

            return new UserModel
            {
                Email = email,
                Password = password
            };
        }
    }
}