using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BankingApp.API.AuthorizationAttributes {
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class YearsRegisteredAttribute: TypeFilterAttribute {
        public YearsRegisteredAttribute(int years): base(typeof(YearsRegisteredFilter)) {
            Arguments = new object[] { years };
        }
    }

    public class YearsRegisteredFilter: IAuthorizationFilter {
        public int Years { get; set; }
        public YearsRegisteredFilter(int years) => Years = years;

        public void OnAuthorization(AuthorizationFilterContext context) {

            if (!context.HttpContext.User.Identity.IsAuthenticated)
                context.Result = new UnauthorizedResult();

            var registered = context.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "RegisteredDate").Value;
            var dateRegistered = DateTime.Parse(registered);

            if (DateTime.Now.Year - dateRegistered.Year < Years)
                context.Result = new UnauthorizedResult();
        }
    }
}
