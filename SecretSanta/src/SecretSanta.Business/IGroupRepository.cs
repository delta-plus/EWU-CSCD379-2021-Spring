using System.Collections.Generic;
using SecretSanta.Data;

namespace SecretSanta.Business
{
    public interface IGroupRepository
    {
        ICollection<Group> List();
        Group? GetItem(int id);
        bool Remove(int id, int groupId);
        Group Create(Group item);
        void Save(Group item);
        AssignmentResult GenerateAssignments(int groupId);
    }

}
