namespace SecretSanta.Data
{
    public class Gift
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string? Description { get; set; } = "";
        public string? Url { get; set; } = "";
        public int Priority { get; set; }
        public User Receiver { get; set; }

        public static Gift? ToDto(Data.Gift? gift)
        {
            if (gift is null) return null;
            return new Gift
            {
                Id = gift.Id,
                Title = gift.Title,
                Description = gift.Description,
                Url = gift.Url,
                Priority = gift.Priority,
                Receiver = gift.Receiver
            };
        }

        public static Data.Gift? FromDto(Gift? gift)
        {
            if (gift is null) return null;
            return new Data.Gift
            {
                Id = gift.Id,
                Title = gift.Title,
                Description = gift.Description,
                Url = gift.Url,
                Priority = gift.Priority,
                Receiver = gift.Receiver
            };
        }
    }
}
