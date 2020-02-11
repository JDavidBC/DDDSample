using System.Collections.Generic;

namespace Source.WebApi.Application.Authentication
{
    public interface IUserIdentity
        {
            //string UserId { get; set; }
            //string Username { get; set; }
            //string Name { get; set; }
            //string Surname { get; set; }
            //string Email { get; set; }
            //string AuthorizationToken { get; set; }
        }
        public class UserIdentity //: IUserIdentity
        {
            public string Username { get; set; }
            public string Name { get; set; }
            public string Surname { get; set; }
            public string Email { get; set; }
            public List<string> Roles { get; set; }
    
            public bool IsValid()
            {
                return !string.IsNullOrEmpty(Username) && !string.IsNullOrWhiteSpace(Username);
            }
        }

}