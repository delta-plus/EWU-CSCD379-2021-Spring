using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecretSanta.Data
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";

        [NotMapped]
        public List<User> Users
        {
            get
            {
                return GroupUser.SelectMany(item => item.Users).ToList();
            }

        }

        [NotMapped]
        public List<Assignment> Assignments
        {
            get
            {
                return GroupAssignment.SelectMany(item => item.Assignments).ToList();
            }

        }

        public List<GroupUser> GroupUser = new();
        public List<GroupAssignment> GroupAssignment = new();
    }
}
