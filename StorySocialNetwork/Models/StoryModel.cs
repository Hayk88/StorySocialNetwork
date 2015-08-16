using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StorySocialNetwork.Models
{
    public class StoryModel
    {
        public int? id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string StoryContent { get; set; }
        public DateTime PostedOn { get; set; }
        public int UserID { get; set; }

        public List<GroupModel> Groups { get; set; }
    }
}