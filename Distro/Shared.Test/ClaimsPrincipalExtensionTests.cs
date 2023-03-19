using gitViwe.Shared;
using gitViwe.Shared.Extension;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Xunit;

namespace Shared.Test;

public class ClaimsPrincipalExtensionTests
{
    [Fact]
    public void GetEmail_Should_Return_Email_If_Present_In_Claims()
    {
        // Arrange
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, "test@test.com")
        };
        var identity = new ClaimsIdentity(claims);
        var claimsPrincipal = new ClaimsPrincipal(identity);

        // Act
        var result = claimsPrincipal.GetEmail();

        // Assert
        Assert.Equal("test@test.com", result);
    }

    [Fact]
    public void GetTokenID_Should_Return_TokenID_If_Present_In_Claims()
    {
        // Arrange
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Jti, "12345")
        };
        var identity = new ClaimsIdentity(claims);
        var claimsPrincipal = new ClaimsPrincipal(identity);

        // Act
        var result = claimsPrincipal.GetTokenID();

        // Assert
        Assert.Equal("12345", result);
    }

    [Fact]
    public void GetUserId_Should_Return_NameIdentifier_If_Present_In_Claims()
    {
        // Arrange
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, "12345")
        };
        var identity = new ClaimsIdentity(claims);
        var claimsPrincipal = new ClaimsPrincipal(identity);

        // Act
        var result = claimsPrincipal.GetUserId();

        // Assert
        Assert.Equal("12345", result);
    }

    [Fact]
    public void GetUsername_Should_Return_Name_If_Present_In_Claims()
    {
        // Arrange
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, "test user")
        };
        var identity = new ClaimsIdentity(claims);
        var claimsPrincipal = new ClaimsPrincipal(identity);

        // Act
        var result = claimsPrincipal.GetUsername();

        // Assert
        Assert.Equal("test user", result);
    }

    [Fact]
    public void HasExpiredClaims_Should_Return_True_If_Exp_Claim_Is_Expired()
    {
        // Arrange
        var expiryDate = DateTime.UtcNow.AddMinutes(-6);
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Exp, Conversion.DateTimeToUnixTimeStamp(expiryDate).ToString())
        };
        var identity = new ClaimsIdentity(claims);
        var claimsPrincipal = new ClaimsPrincipal(identity);

        // Act
        var result = claimsPrincipal.HasExpiredClaims();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void HasExpiredClaims_Should_Return_False_If_Exp_Claim_Is_Not_Expired()
    {
        // Arrange
        var expiryDate = DateTime.UtcNow.AddMinutes(5);
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Exp, Conversion.DateTimeToUnixTimeStamp(expiryDate).ToString())
        };
        var identity = new ClaimsIdentity(claims);
        var claimsPrincipal = new ClaimsPrincipal(identity);

        // Act
        var result = claimsPrincipal.HasExpiredClaims();

        // Assert
        Assert.False(result);
    }
}

