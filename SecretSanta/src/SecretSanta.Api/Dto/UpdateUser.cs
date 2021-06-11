using System.Collections.Generic;

namespace SecretSanta.Api.Dto
{
    public class UpdateUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public List<Gift> Gifts { get; set; }
    }
}
