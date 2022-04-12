using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Project.Business.Interfaces
{
    public interface IUser
    {
        string Name { get; }
        Guid GetUserId();
        string getUserEmail();
        bool IsAuthenticated();
        bool isInRole(string role);
        IEnumerable<Claim> GetClaimsIdentity();
    }
}
