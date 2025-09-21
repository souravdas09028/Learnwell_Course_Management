using LearnWell.Infrastructure.Authentication;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LearnWell.Tests.Services
{
    public class AuthServiceTests
    {
        //[Fact]
        //public void GenerateToken_ShouldReturnValidJwt()
        //{
        //    // Arrange
        //    var jwtSettings = new JwtSettings
        //    {
        //        Key = "supersecretkey1234567890",
        //        Issuer = "LearnWell",
        //        Audience = "LearnWellUsers",
        //        ExpiryMinutes = 60
        //    };

        //    var options = Options.Create(jwtSettings);
        //    var service = new AuthService(options, null!); // null DbContext since not needed here

        //    // Act
        //    var token = service.GenerateToken("testuser", "Staff");

        //    // Assert
        //    Assert.False(string.IsNullOrEmpty(token));
        //    var handler = new JwtSecurityTokenHandler();
        //    var jwt = handler.ReadJwtToken(token);
        //    Assert.Equal("testuser", jwt.Claims.First(c => c.Type == ClaimTypes.Name).Value);
        //    Assert.Equal("Staff", jwt.Claims.First(c => c.Type == ClaimTypes.Role).Value);
        //}
    }
}
