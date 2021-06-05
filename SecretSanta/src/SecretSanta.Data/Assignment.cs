using System;
using System.Collections.Generic;

namespace SecretSanta.Data
{
    public class Assignment
    {
        // Entity Framework was freaking out because this class didn't have a property 
        // with a standard database type to use as a key. No problem. Just add an ID.
        public int Id { get; set; }
        public User Giver { get; }
        public User Receiver { get; }
        public string GiverName { get; }
        // Needed for the many-to-many relationship?
        // public List<Group> Groups { get; } = new();

        public Assignment(User giver, User receiver)
        {
            Giver = giver ?? throw new ArgumentNullException(nameof(giver));
            Receiver = receiver ?? throw new ArgumentNullException(nameof(receiver));
            GiverName = giver.FirstName + " " + giver.LastName;
        }
    }
}
