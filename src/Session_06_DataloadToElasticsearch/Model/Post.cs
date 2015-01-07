using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Session_06_DataloadToElasticsearch.Model
{
    public class Post
    {
        public int Id { get; set; }

        // 1: Querstion, 2: Answer
        public int PostTypeId { get; set; }

        public string PostType { get; set; }

        //only present if PostTypedId is 1 (Question)
        public int? AcceptedAnswerId { get; set; }

        //only present if PostTypeId is 2 (Answer)
        public int? ParentId { get; set; }

        public DateTime CreationDate { get; set; }

        public int Score { get; set; }

        public int ViewCount { get; set; }

        public string Body { get; set; }

        //present only if user has not been deleted
        public int? OwnerUserId { get; set; }

        public string OwnerDisplayName { get; set; }

        public int LastEditorUserId { get; set; }

        public string LastEditorDisplayName { get; set; }

        //the date and time of the most recent edit to the post
        public DateTime? LastEditDate { get; set; }

        //the date and time of the most recent activity on the post
        public DateTime? LastActivityDate { get; set; }

        public string Title { get; set; }

        public string Tags_String { get; set; }

        public string[] Tags { get; set; }

        public int AnswerCount { get; set; }

        public int CommentCount { get; set; }

        public int FavoriteCount { get; set; }

        //present only if the post is closed
        public DateTime? ClosedDate { get; set; }

        //present only if post is community wikied
        public DateTime? CommunityOwnedDate { get; set; }
    }
}
