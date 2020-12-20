using finalAssignment__APWDN.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace finalAssignment__APWDN.Repositories
{
    public class PostRepository : Repository<Post>
    {
        LikeRepository likeRepo = new LikeRepository();
        CommentRepository comRepo = new CommentRepository();
        public bool UpdatePost(Post post)
        {
            Post oldPost = Get(post.PostId);
            if(oldPost != null)
            {
                oldPost.PostDescription = post.PostDescription;
                oldPost.Image = post.Image;
                this.context.SaveChanges();
                return true;
            }
            return false;
        }

        public void DeletePost(int id)
        {
            List<Like> likes = likeRepo.GetAll().Where<Like>(x => x.postId == id).ToList();
            List<Comment> comments = comRepo.GetAll().Where<Comment>(x => x.PostId == id).ToList();

            foreach(var item in comments)
            {
                comRepo.Delete(item.CommentId);
            }

            foreach (var item in likes)
            {
                likeRepo.Delete(item.likeId);
            }

            this.Delete(id);
        }
    }
}