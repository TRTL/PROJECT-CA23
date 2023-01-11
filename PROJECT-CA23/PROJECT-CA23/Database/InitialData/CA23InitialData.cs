using PROJECT_CA23.Models;
using PROJECT_CA23.Models.Enums;
using PROJECT_CA23.Services;
using PROJECT_CA23.Services.IServices;

namespace PROJECT_CA23.Database.InitialData
{
    public class CA23InitialData
    {


        public static readonly User[] userInidialData = new User[]
        {

            new User()
            {
                UserId = 1,
                Username = "admin",
                FirstName = "Jonas",
                LastName = "Jonaitis",
                Role = ERole.admin,
                //PasswordHash= adminPasswordHash,
            }
        };


    }
    
}
