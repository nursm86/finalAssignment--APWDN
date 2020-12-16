using finalAssignment__APWDN.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace finalAssignment__APWDN.Attributes
{
    public class BasicAuthenticationAttribute:AuthorizationFilterAttribute
    {
        UserRepository userRepo = new UserRepository();
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            base.OnAuthorization(actionContext);
            if(actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            else
            {
                string encodedString = actionContext.Request.Headers.Authorization.Parameter;

                string userName = userRepo.Validate(encodedString);

                if (userName != null)
                {
                    Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(userName),null);
                }
                else
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                }
            }
        }
    }
}