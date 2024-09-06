using AspProjekat.Application;
using AspProjekat.DataAccess;
using AspProjekat.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspProjekat.Implementation
{
    public class BasicAuthorizationApplicationActorProvider : IApplicationActorProvider
    {
        private string _authorizationHeader;
        private AspContext _context;

        public BasicAuthorizationApplicationActorProvider(string authorizationHeader, AspContext context)
        {
            _authorizationHeader = authorizationHeader;
            _context = context;
        }

        public IApplicationActor GetActor()
        {
            //Primer header-a "Basic cGVyYTpsb3ppbmthMTIz"
            if (_authorizationHeader == null || !_authorizationHeader.Contains("Basic"))
            {
                return new UnauthorizedActor();
            }

            var base64Data = _authorizationHeader.Split(" ")[1];

            //Primer base64Data - cGVyYTpsb3ppbmthMTIz

            var bytes = Convert.FromBase64String(base64Data);

            var decodedCredentials = System.Text.Encoding.UTF8.GetString(bytes);

            if (decodedCredentials.Split(":").Length < 2)
            {
                throw new InvalidOperationException("Invalid Basic authorization header.");
            }

            string username = decodedCredentials.Split(":")[0];
            string password = decodedCredentials.Split(":")[1];

            User u = _context.Users.FirstOrDefault(x => x.Username == username && x.Password == password);

            if (u == null)
            {
                return new UnauthorizedActor();
            }

            var useCases = u.Role.UseCases.Select(r=>r.UseCaseId);

            return new Actor
            {
                Email = u.Email,
                FirstName = u.FirstName,
                Id = u.Id,
                LastName = u.LastName,
                Username = u.Username,
                RoleId = u.RoleId,
                AllowedUseCases = useCases
            };
        }
    }
}
