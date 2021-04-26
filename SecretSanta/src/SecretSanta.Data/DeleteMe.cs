using System.Collections.Generic;

namespace SecretSanta.Data {
  public static class DeleteMe {
    public static List<User> Users { get; } = new() {
      new User() {Id = 1, FirstName = "Joe", LastName = "Grassl"},
      new User() {Id = 2, FirstName = "Joe", LastName = "DiMaggio"},
      new User() {Id = 3, FirstName = "Joe", LastName = "Louis"},
      new User() {Id = 4, FirstName = "Joe", LastName = "Biden"}
    };
  }
}
