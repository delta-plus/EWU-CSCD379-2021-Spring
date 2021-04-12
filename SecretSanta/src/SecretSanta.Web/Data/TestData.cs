using System.Collections.Generic;
using SecretSanta.Web.ViewModels;

namespace SecretSanta.Web.Data {
  public static class TestData {
    public static List<UserViewModel> Users = new List<UserViewModel>() {
      new UserViewModel {ID = 0, FirstName = "William", LastName = "Blake"},
      new UserViewModel {ID = 1, FirstName = "John", LastName = "von Neumann"},
      new UserViewModel {ID = 2, FirstName = "Elvis", LastName = "Presley"}
    };

    public static List<GroupViewModel> Groups = new List<GroupViewModel>() {
      new GroupViewModel {ID = 0, Name = "Work"},
      new GroupViewModel {ID = 1, Name = "Family"},
      new GroupViewModel {ID = 2, Name = "Friends"}
    };

    public static List<GiftViewModel> Gifts = new List<GiftViewModel>() {
      new GiftViewModel {
        ID = 0, 
        Title = "Bike", 
        Description = "Brand new bicycle.", 
        URL = "https://www.amazon.com/Roadmaster-Inches-Granite-Peak-Mountain/dp/B07DP6989B", 
        Priority = 1,
        UserID = 0
      },
      new GiftViewModel {
        ID = 1, 
        Title = "Xbox One", 
        Description = "An Xbox One X gaming console.", 
        URL = "https://www.amazon.com/Xbox-One-X-1TB-Console/dp/B074WPGYRF", 
        Priority = 1,
        UserID = 1
      },
      new GiftViewModel {
        ID = 2, 
        Title = "Violin", 
        Description = "A nice violin for the musically inclined.", 
        URL = "https://www.amazon.com/Mendini-92D-1-Piece-Shoulder-Strings/dp/B002026DLG", 
        Priority = 1,
        UserID = 2
      }
    };
  }
}
