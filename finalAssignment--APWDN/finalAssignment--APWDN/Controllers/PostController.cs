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
    [RoutePrefix("api/post")]
    public class PostController : ApiController
    {
        PostRepository postRepo = new PostRepository();
        [Route(""),BasicAuthentication]
        public IHttpActionResult Get()
        {
            return Ok(postRepo.GetAll());
        }
        [Route("{id}",Name ="GetPostById"),BasicAuthentication]
        public IHttpActionResult Get(int id)
        {
            Post post = postRepo.Get(id);
            if(post == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            return Ok(postRepo.Get(id));
        }
        [Route("")]
        public IHttpActionResult Post(Post post)
        {
            postRepo.Insert(post);
            string uri = Url.Link("GetPostById", new { id = post.Id });
            return Created(uri, post);
        }
        [Route("{id}")]
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
        [Route("{id}")]
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
