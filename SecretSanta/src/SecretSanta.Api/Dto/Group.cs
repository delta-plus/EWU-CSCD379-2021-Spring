using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SecretSanta.Data;

namespace SecretSanta.Api.Dto
{
    public class Group
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public List<User> Users { get; } = new();
        public List<Assignment> Assignments { get; } = new();

        public static Group? ToDto(Data.Group? group, bool includeChildObjects = true)
        {
            if (group is null) return null;
            var rv = new Group
            {
                Id = group.Id,
                Name = group.Name
            };
            if (includeChildObjects)
            {
                foreach(Data.User? user in group.Users)
                {
                    if (User.ToDto(user) is { } dtoUser)
                    {
                        rv.Users.Add(dtoUser);
                    }
                }
                foreach(Data.Assignment? assignment in group.Assignments)
                {
                    if (Assignment.ToDto(assignment) is { } dtoAssignment)
                    {
                        rv.Assignments.Add(dtoAssignment);
                    }
                }
            }
            return rv;
        }

        public static Data.Group? FromDto(Group? group)
        {
            if (group is null) return null;
            return new Data.Group
            {
                Id = group.Id,
                Name = group.Name ?? "",
            };
        }
    }
}
