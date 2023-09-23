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

