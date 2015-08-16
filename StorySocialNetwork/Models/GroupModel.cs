using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StorySocialNetwork.Models
{
    public class GroupModel
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool isSelected { get; set; }
        public int UsersCount { get; set; }
        public int StoriesCount { get; set; }
    }
}