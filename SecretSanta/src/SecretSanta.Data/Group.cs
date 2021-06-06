using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecretSanta.Data
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        // A many-to-many relationship is generated automatically as long as
        // each class contains a "collection navigation property", which is 
        // Microsoft's typically-overly-fancy way of saying "a List of the other object".
        // As far as I can tell, there's no need to use an intermediate class like "GroupUser".
        public List<User> Users { get; set; }
        public List<Assignment> Assignments { get; set; }
    }
}
