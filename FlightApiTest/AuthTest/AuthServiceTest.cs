using Infrastructure;
using Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NSubstitute;

namespace FlightApiTest.AuthTest;

public class AuthServiceTest
{
    private DbContextOptions<AirContext> GetInMemoryDbContextOptions()
    {
        return new DbContextOptionsBuilder<AirContext>()
            .UseInMemoryDatabase(databaseName: "InMemoryDb")
            .Options;
    }
    
    
    [Fact]
    public async Task Register_with_valid_role_creates_user()
    {
        // Arrange
        var options = GetInMemoryDbContextOptions();
        var config = Substitute.For<IConfiguration>();

        // Seed the database with a role
        using (var context = new AirContext(options))
        {
            context.Roles.Add(new Role { ID = 1, Code = "moderator" });
            context.SaveChanges();
        }

        // Act
        using (var context = new AirContext(options))
        {
            var userService = new AuthService(context, config);
            await userService.Register("testuser", "password123", "moderator");
        }

        // Assert
        using (var context = new AirContext(options))
        {
            var user = await context.Users.SingleOrDefaultAsync(u => u.Username == "testuser");
            Assert.NotNull(user);
            Assert.Equal("testuser", user.Username);
            Assert.True(BCrypt.Net.BCrypt.Verify("password123", user.Password));
            Assert.Equal(1, user.RoleId);
        }
    }

    [Fact]
    public async Task Register_with_invalid_role_throws_exception()
    {
        // Arrange
        var options = GetInMemoryDbContextOptions();
        var config = Substitute.For<IConfiguration>();

        // Act & Assert
        using (var context = new AirContext(options))
        {
            var userService = new AuthService(context,config);
            var exception = await Assert.ThrowsAsync<Exception>(() => userService.Register("testuser", "password123", "admin"));
            Assert.Equal("Role not found", exception.Message);
        }
    }
}