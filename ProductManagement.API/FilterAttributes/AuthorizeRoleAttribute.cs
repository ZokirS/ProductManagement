using Microsoft.AspNetCore.Mvc;
using ProductManagement.API.AuthorizationFilters;
using ProductManagement.DAL.Helpers;

namespace ProductManagement.API.FilterAttributes
{
    public class AuthorizeRoleAttribute : TypeFilterAttribute
    {
        public AuthorizeRoleAttribute(params UserRole[] roles) : base(typeof(AuthorizeRoleFilter))
        {
            Arguments = [roles.Select(t => t.ToString()).ToArray()];
        }
    }
}
