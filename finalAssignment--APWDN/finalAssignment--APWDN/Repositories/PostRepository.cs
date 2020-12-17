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
    }
}