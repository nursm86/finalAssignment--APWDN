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
        CommentRepository comRepo = new CommentRepository();
        LikeRepository likeRepo = new LikeRepository();

        [Route(""), BasicAuthentication]
        public IHttpActionResult Get()
        {
            return Ok(postRepo.GetAll());
        }

        [Route("{id}", Name = "GetPostById"), BasicAuthentication]
        public IHttpActionResult Get(int id)
        {
            Post post = postRepo.Get(id);
            if (post == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            return Ok(post);
        }

        [Route(""), BasicAuthentication]
        public IHttpActionResult Post(Post post)
        {
            postRepo.Insert(post);
            string uri = Url.Link("GetPostById", new { id = post.PostId });
            return Created(uri, post);
        }

        [Route("{id}"), BasicAuthentication]
        public IHttpActionResult Put([FromUri] int id, [FromBody] Post post)
        {
            post.PostId = id;
            if (postRepo.UpdatePost(post))
            {
                return Ok(post);
            }
            else
            {
                return StatusCode(HttpStatusCode.NoContent);
            }

        }

        [Route("{id}"), BasicAuthentication]
        public IHttpActionResult Delete(int id)
        {
            postRepo.DeletePost(id);
            return StatusCode(HttpStatusCode.NoContent);
        }

        [Route("{id}/comments"), BasicAuthentication]
        public IHttpActionResult GetComments(int id)
        {
            List<Comment> comments = comRepo.GetAll().Where<Comment>(x => x.PostId == id).ToList();
            if (comments.Count <= 0)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                return Ok(comments);
            }
        }

        [Route("{id}/comments/{cid}", Name = "GetCommentById"), BasicAuthentication]
        public IHttpActionResult GetComment(int id, int cid)
        {
            Comment com = comRepo.GetAll().Where<Comment>(x => x.CommentId == cid && x.PostId == id).FirstOrDefault();
            if (com == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                return Ok(com);
            }
        }

        [Route("{id}/comments"), BasicAuthentication]
        public IHttpActionResult PostComment(int id, Comment com)
        {
            com.PostId = id;
            comRepo.Insert(com);
            string uri = Url.Link("GetCommentById", new { id = com.PostId, cid = com.CommentId });
            return Created(uri, com);
        }

        [Route("{id}/comments/{cid}"), BasicAuthentication]
        public IHttpActionResult Put([FromUri] int id, [FromUri] int cid, [FromBody] Comment com)
        {
            com.CommentId = cid;
            com.PostId = id;
            if (comRepo.UpdateComment(com))
            {
                return Ok(com);
            }
            else
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
        }

        [Route("{id}/comments/{cid}"), BasicAuthentication]
        public IHttpActionResult DeleteComment(int id, int cid)
        {
            Comment comment = comRepo.GetAll().Where<Comment>(x => x.CommentId == cid && x.PostId == id).FirstOrDefault();
            if (comment == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            comRepo.Delete(cid);
            return StatusCode(HttpStatusCode.NoContent);
        }

        [Route("{id}/like"), BasicAuthentication]
        public IHttpActionResult GetLike(int id)
        {
            return Ok(likeRepo.GetAll().Where<Like>(x => x.postId == id).ToList().Count);
        }

        [Route("{id}/like"), BasicAuthentication]
        public IHttpActionResult PostLike(int id,Like like)
        {
            like.postId = id;
            likeRepo.Insert(like);
            return Ok(like);
        }

        [Route("{id}/like/{uid}"), BasicAuthentication]
        public IHttpActionResult DeleteLike(int id,int uid)
        {
            Like like = likeRepo.GetAll().Where<Like>(x => x.postId == id && x.userId == uid).FirstOrDefault();
            if (like == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            likeRepo.Delete(like.likeId);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
