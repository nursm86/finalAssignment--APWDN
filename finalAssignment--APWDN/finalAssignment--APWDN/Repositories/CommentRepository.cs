using finalAssignment__APWDN.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace finalAssignment__APWDN.Repositories
{
    public class CommentRepository : Repository<Comment>
    {
        public bool UpdateComment(Comment com)
        {
            Comment oldCom = GetAll().Where<Comment>(x => x.PostId == com.PostId && x.CommentId == com.CommentId).FirstOrDefault();
            if (oldCom != null)
            {
                oldCom.Comment1 = com.Comment1;
                this.context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}