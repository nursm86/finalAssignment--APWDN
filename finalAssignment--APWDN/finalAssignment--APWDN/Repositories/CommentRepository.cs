using finalAssignment__APWDN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace finalAssignment__APWDN.Repositories
{
    public class CommentRepository : Repository<Comment>
    {
        public void UpdateComment(int id,Comment com)
        {
            Comment oldcom = Get(id);
            oldcom = com;
            this.context.SaveChanges();
        }
    }
}