using System;
using System.Linq;
using System.Threading.Tasks;
using BorrowBuddy.Data;
using BorrowBuddy.Domain;
using BorrowBuddy.Dto;
using BorrowBuddy.Services;
using Xunit;

namespace BorrowBuddy.Test.Services {
  public class CurrencyServiceTest : ServiceTestBase {
    [Fact]
    public async Task Get_CodeIsNull_ShouldThrow() {
      // Arrange
      var options = BuildContextOptions();

      using(var context = new BorrowBuddyContext(options)) {
        var service = new CurrencyService(context);

        // Act
        // Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => service.GetAsync(null));
      }
    }

    [Fact]
    public async Task Get_DontExist_ShouldReturnNull() {
      // Arrange
      var options = BuildContextOptions();

      using(var context = new BorrowBuddyContext(options)) {
        var service = new CurrencyService(context);
        // Act
        var result = await service.GetAsync("code");

        // Assert
        Assert.Null(result);
      }
    }

    [Fact]
    public async Task Get_Exists_ShouldReturnCurrency() {
      // Arrange
      var options = BuildContextOptions();
      using(var context = new BorrowBuddyContext(options)) {
        context.AddCurrency("code");
        context.SaveChanges();
      }

      using(var context = new BorrowBuddyContext(options)) {
        var service = new CurrencyService(context);

        // Act
        var result = await service.GetAsync("code");

        // Assert
        Assert.NotNull(result);
      }
    }

    [Fact]
    public async Task Get_DontExist_ShouldReturnEmptyList() {
      // Arrange
      var options = BuildContextOptions();

      using(var context = new BorrowBuddyContext(options)) {
        var service = new CurrencyService(context);

        // Act
        var result = await service.GetAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
      }
    }

    [Fact]
    public async Task Get_Exists_ShouldReturnCurrencies() {
      // Arrange
      var options = BuildContextOptions();
      using(var context = new BorrowBuddyContext(options)) {
        context.AddCurrency();
        context.AddCurrency();
        context.SaveChanges();
      }

      using(var context = new BorrowBuddyContext(options)) {
        var service = new CurrencyService(context);

        // Act
        var result = await service.GetAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
      }
    }

    [Fact]
    public async Task Add_ShouldAddCurrency() {
      var options = BuildContextOptions();

      // Arrange
      // Act
      using(var context = new BorrowBuddyContext(options)) {
        var service = new CurrencyService(context);

        await service.AddAsync(new CurrencyDto {
          Code = "code"
        });
      }

      // Assert
      using(var context = new BorrowBuddyContext(options)) {
        // Assert
        var result = context.Currencies.FirstOrDefault();
        Assert.NotNull(result);
        Assert.Equal("code", result.Code);
      }
    }

    [Fact]
    public async Task Update_ShouldUpdateCurrency() {
      var options = BuildContextOptions();

      // Arrange
      Currency currency;
      using(var context = new BorrowBuddyContext(options)) {
        currency = context.AddCurrency();
      }

      // Act
      using(var context = new BorrowBuddyContext(options)) {
        var service = new CurrencyService(context);
        await service.UpdateAsync(currency.Code, new CurrencyDto {
          Scale = 123,
          Symbol = "symbol123"
        });
      }

      using(var context = new BorrowBuddyContext(options)) {
        // Assert
        var result = context.Currencies.FirstOrDefault();
        Assert.NotNull(result);
        Assert.Equal(currency.Code, result.Code);
        Assert.Equal(123, result.Scale);
        Assert.Equal("symbol123", result.Symbol);
      }
    }

    [Fact]
    public async Task Delete_ShouldDeleteCurrency() {
      // Arrange
      var options = BuildContextOptions();

      Currency currency;
      using(var context = new BorrowBuddyContext(options)) {
        currency = context.AddCurrency();
      }

      using(var context = new BorrowBuddyContext(options)) {
        // Act
        var service = new CurrencyService(context);
        await service.DeleteAsync(currency.Code);
      }

      using(var context = new BorrowBuddyContext(options)) {
        // Assert
        var result = context.Currencies.FirstOrDefault();
        Assert.Null(result);
      }
    }
  }
}
