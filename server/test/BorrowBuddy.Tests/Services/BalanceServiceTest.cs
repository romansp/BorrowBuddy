using System.Threading.Tasks;
using BorrowBuddy.Data;
using BorrowBuddy.Domain;
using BorrowBuddy.Services;
using Xunit;

namespace BorrowBuddy.Test.Services {
  public class BalanceServiceTest : ServiceTestBase {
    [Fact]
    public async Task Balance_ShouldAddToEachOtherAsync() {
      // Arrange
      var options = BuildContextOptions();
      Participant from;
      Participant to;
      Currency currency;

      using(var context = new BorrowBuddyContext(options)) {
        currency = context.AddCurrency();
        from = context.AddParticipant();
        to = context.AddParticipant();
        context.AddFlow(from, to, currency, 50);
        context.AddFlow(from, to, currency, 100);
      }

      using(var context = new BorrowBuddyContext(options)) {
        var service = new BalanceService(context);
        // Act
        var balance = await service.BalanceAsync(from.Id, to.Id, currency.Code);

        // Assert
        Assert.Equal(150, balance);
      }
    }

    [Fact]
    public async Task Balance_ShouldCancelEachOtherAsync() {
      // Arrange
      var options = BuildContextOptions();
      Participant from;
      Participant to;
      Currency currency;

      using(var context = new BorrowBuddyContext(options)) {
        currency = context.AddCurrency();
        from = context.AddParticipant();
        to = context.AddParticipant();
        context.AddFlow(from, to, currency, 50);
      }

      using(var context = new BorrowBuddyContext(options)) {
        var service = new BalanceService(context);
        // Act
        var positive = await service.BalanceAsync(from.Id, to.Id, currency.Code);
        var negative = await service.BalanceAsync(to.Id, from.Id, currency.Code);

        // Assert
        Assert.Equal(50, positive);
        Assert.Equal(-50, negative);
      }
    }
  }
}
