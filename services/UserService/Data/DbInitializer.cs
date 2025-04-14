using UserService.Models;

namespace UserService.Data
{
    public static class DbInitializer
    {
        public static void Seed(AppDbContext context)
        {
            if (!context.Users.Any())
            {
                var users = new List<User>
                {
                    new User
                    {
                        Id = Guid.NewGuid(),
                        FullName = "Test User One",
                        Email = "user1@test.com",
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword("Test@123")
                    },
                    new User
                    {
                        Id = Guid.NewGuid(),
                        FullName = "Test User Two",
                        Email = "user2@test.com",
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword("Test@123")
                    },
                };

                context.Users.AddRange(users);
                context.SaveChanges();
            }
        }
    }
}
