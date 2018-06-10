using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using BorrowBuddy.Data;
using BorrowBuddy.Domain;
using BorrowBuddy.Dto;
using BorrowBuddy.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BorrowBuddy.Test.Services {

  public static class Extensions {
    public static DbContextOptions<BorrowBuddyContext> СontextOptions([CallerMemberName]string databaseName = "") {
      if(string.IsNullOrEmpty(databaseName)) {
        databaseName = Guid.NewGuid().ToString();
      }
      return new DbContextOptionsBuilder<BorrowBuddyContext>()
                      .UseInMemoryDatabase(databaseName: databaseName)
                      .UseLazyLoadingProxies()
                      .Options;
    }
  }

  public class CurrencyServiceTest {
    [Fact]
    public async Task GetCurrency() {
      // Arrange
      var options = Extensions.СontextOptions();
      using(var context = new BorrowBuddyContext(options)) {
        context.Currencies.Add(new Currency {
          Code = "code",
        });
        context.SaveChanges();
      }

      using(var context = new BorrowBuddyContext(options)) {
        // Act
        var service = new CurrencyService(context);
        var result = await service.GetAsync("code");

        // Assert
        Assert.NotNull(result);
      }
    }

    [Fact]
    public async Task GetCurrency_CodeIsNull_Throws() {
      // Arrange
      var options = Extensions.СontextOptions();

      using(var context = new BorrowBuddyContext(options)) {
        var service = new CurrencyService(context);

        // Act
        // Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => service.GetAsync(null));
      }
    }

    [Fact]
    public async Task GetCurrency_NotExisting_ReturnsNull() {
      // Arrange
      var options = Extensions.СontextOptions();

      using(var context = new BorrowBuddyContext(options)) {
        // Act
        var service = new CurrencyService(context);
        var result = await service.GetAsync("code");

        // Assert
        Assert.Null(result);
      }
    }

    [Fact]
    public void AddCurrency() {
      // Arrange
      var options = Extensions.СontextOptions();

      using(var context = new BorrowBuddyContext(options)) {
        // Act
        var service = new CurrencyService(context);
        service.AddAsync(new CurrencyDto {
          Code = "code"
        });

        // Assert
        var result = service.GetAsync("code");
        Assert.NotNull(result);
      }
    }
  }
}
