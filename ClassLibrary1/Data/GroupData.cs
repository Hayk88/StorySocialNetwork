using ClassLibrary1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StortyBack.Data
{
    public class GroupData : MainData
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool isSelected { get; set; }
        public int UsersCount { get; set; }
        public int StoriesCount { get; set; }

        public List<GroupData> GetGroups(int? id)
        {
            var groups = db.Groups.ToList();
            List<Group> selected = new List<Group>();
            if (id != null)
            {
                selected = db.Groups.Where(u => u.Stories.Any(e => e.id == id.Value)).ToList();
            }
            List<GroupData> gms = new List<GroupData>();
            foreach (var item in groups)
            {
                bool isselected = selected.Where(s => s.id == item.id).ToList().Count > 0;
                GroupData gm = new GroupData()
                {
                    id = item.id,
                    Description = item.Description,
                    Name = item.Name,
                    isSelected = isselected
                };
                gms.Add(gm);
            }

            return gms;
        }

        public List<GroupData> GetGroupsInfo()
        {
            var groups = db.Groups.ToList();
            List<GroupData> gms = new List<GroupData>();

            foreach (var item in groups)
            {
                var usersCount = item.Stories.GroupBy(s => s.UsersID).Select(g => g.Count()).ToList().Count();
                GroupData gm = new GroupData()
                {
                    id = item.id,
                    Description = item.Description,
                    Name = item.Name,
                    StoriesCount = item.Stories.Count,
                    UsersCount = usersCount
                };
                gms.Add(gm);
            }

            return gms;
        }
    }
}
