using System;
using System.Linq;
using System.Collections.Generic;
using SecretSanta.Data;

namespace SecretSanta.Business
{
    public class GroupRepository : IGroupRepository
    {
        public Group Create(Group item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

//            MockData.Groups[item.Id] = item;

            return item;
        }

        public Group? GetItem(int id)
        {
        /*
            if (MockData.Groups.TryGetValue(id, out Group? user))
            {
                return user;
            }
            return null;
        */
            using DbContext dbContext = new DbContext();

            return dbContext.Groups.Find(id);
        }

        public ICollection<Group> List()
        {
//            return MockData.Groups.Values;
            using DbContext dbContext = new DbContext();

            return dbContext.Groups.ToList();
        }

        public bool Remove(int id)
        {
//            return MockData.Groups.Remove(id);
            using DbContext dbContext = new DbContext();

            User user = dbContext.Users.Find(id);

            dbContext.Users.Remove(user);

            dbContext.SaveChangesAsync();

            return true;
        }

        public void Save(Group item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

//            MockData.Groups[item.Id] = item;
        }

        public AssignmentResult GenerateAssignments(int groupId)
        {
/*
            if (!MockData.Groups.TryGetValue(groupId, out Group? group))
            {
                return AssignmentResult.Error("Group not found");
            }

            Random random = new();
            var groupUsers = new List<User>(group.Users);

            if (groupUsers.Count < 3)
            {
                return AssignmentResult.Error($"Group {group.Name} must have at least three users");
            }

            var users = new List<User>();
            //Put the users in a random order
            while(groupUsers.Count > 0)
            {
                int index = random.Next(groupUsers.Count);
                users.Add(groupUsers[index]);
                groupUsers.RemoveAt(index);
            }

            //The assignments are created by linking the current user to the next user.
            group.Assignments.Clear();
            for(int i = 0; i < users.Count; i++)
            {
                int endIndex = (i + 1) % users.Count;
                group.Assignments.Add(new Assignment(users[i], users[endIndex]));
            }
            return AssignmentResult.Success();
*/
            using DbContext dbContext = new DbContext();

            Group group = dbContext.Groups.Find(groupId);

            if (group is null)
            {
                return AssignmentResult.Error("Group not found");
            }

            Random random = new();
            List<User> groupUsers = group.Users.ToList();

            if (groupUsers.Count < 3)
            {
                return AssignmentResult.Error($"Group {group.Name} must have at least three users");
            }

            var users = new List<User>();

            //Put the users in a random order
            while(groupUsers.Count > 0)
            {
                int index = random.Next(groupUsers.Count);
                users.Add(groupUsers[index]);
                groupUsers.RemoveAt(index);
            }

            //The assignments are created by linking the current user to the next user.
            group.Assignments.Clear();

            for(int i = 0; i < users.Count; i++)
            {
                int endIndex = (i + 1) % users.Count;
                group.GroupAssignment[0].Assignments.Add(new Assignment(users[i], users[endIndex]));
            }

            return AssignmentResult.Success();
        }
    }
}
