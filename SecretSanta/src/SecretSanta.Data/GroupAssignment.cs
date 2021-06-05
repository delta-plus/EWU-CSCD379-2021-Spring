using System;
using System.Text;
using System.Collections.Generic;

namespace SecretSanta.Data
{
    public class GroupAssignment
    {
        public int GroupId { get; set; }
        public int AssignmentId { get; set; }
        public List<Assignment> Assignments = new();
    }
}
