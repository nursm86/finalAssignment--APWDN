using CourierManagementSystem.Repositories;
using finalAssignment__APWDN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace finalAssignment__APWDN.Controllers
{
    public class PostController : ApiController
    {
        PostRepository postRepo = new PostRepository();
        public IHttpActionResult Get()
        {
            return Ok(postRepo.GetAll());
        }

        public IHttpActionResult Get(int id)
        {
            Post post = postRepo.Get(id);
            if(post == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            return Ok(postRepo.Get(id));
        }

        public IHttpActionResult Post(Post post)
        {
            postRepo.Insert(post);
            return Created("api/post/" + post.Id, post);
        }
        public IHttpActionResult Put([FromUri]int id, [FromBody]Post post)
        {
            if (postRepo.Get(id) == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            post.Id = id;
            postRepo.Update(post);
            return Ok(post);
        }
        public IHttpActionResult Delete(int id)
        {
            if (postRepo.Get(id) == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            postRepo.Delete(id);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
