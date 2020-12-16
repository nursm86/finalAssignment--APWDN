using finalAssignment__APWDN.Attributes;
using finalAssignment__APWDN.Models;
using finalAssignment__APWDN.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace finalAssignment__APWDN.Controllers
{
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        UserRepository userRepo = new UserRepository();

        [Route("login"), BasicAuthentication]
        public IHttpActionResult PostValidate(User user)
        {
            
            if (userRepo.Validate(user))
            {
                return StatusCode(HttpStatusCode.Accepted);
            }
            return StatusCode(HttpStatusCode.Unauthorized);
        }

        [Route("{id}", Name = "GetUserById"), BasicAuthentication]
        public IHttpActionResult Get(int id)
        {
            User user = userRepo.Get(id);
            if (user == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            return Ok(user);
        }

        [Route(""), BasicAuthentication]
        public IHttpActionResult Post(User user)
        {
            userRepo.Insert(user);
            string uri = Url.Link("GetUserById", new { id = user.UserId });
            return Created(uri, user);
        }

        [Route("{id}"), BasicAuthentication]
        public IHttpActionResult Put([FromUri] int id, [FromBody] User user)
        {
            User u = userRepo.Get(id);
            if (u == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            user.UserId = id;
            userRepo.Update(user);
            return Ok(user);
        }

        [Route("{id}"), BasicAuthentication]
        public IHttpActionResult Delete(int id)
        {
            if (userRepo.Get(id) == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            userRepo.Delete(id);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
