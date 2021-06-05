using System;
using System.Collections.Generic;

namespace SecretSanta.Data
{
    public class Assignment
    {
        public int Id { get; set; }
        public User Giver { get; }
        public User Receiver { get; }
        public string GiverName { get; }
        public List<Group> Groups { get; set; }

        public Assignment(User giver, User receiver)
        {
            Giver = giver ?? throw new ArgumentNullException(nameof(giver));
            Receiver = receiver ?? throw new ArgumentNullException(nameof(receiver));
            GiverName = giver.FirstName + " " + giver.LastName;
        }
    }
}
