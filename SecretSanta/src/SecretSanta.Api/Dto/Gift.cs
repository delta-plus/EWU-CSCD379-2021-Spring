using System.Collections.Generic;

namespace SecretSanta.Api.Dto
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
                Receiver = User.ToDto(gift.Receiver)
            };
        }

        public static List<Gift?> ToDtos(List<Data.Gift?> gifts)
        {
            if (gifts is null) return null;
            List<Gift> dtoGifts = new();
            foreach (Data.Gift gift in gifts)
            {
               dtoGifts.Add(ToDto(gift)); 
            }

            return dtoGifts;
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
                Receiver = User.FromDto(gift.Receiver)
            };
        }

        public static List<Data.Gift?> FromDtos(List<Gift?> dtoGifts)
        {
            if (dtoGifts is null) return null;
            List<Data.Gift> gifts = new();
            foreach (Gift dtoGift in dtoGifts)
            {
               gifts.Add(FromDto(dtoGift)); 
            }

            return gifts;
        }
    }
}
