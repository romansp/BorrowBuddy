using System;
using System.Linq;
using System.Threading.Tasks;
using BorrowBuddy.Data;
using BorrowBuddy.Domain;
using BorrowBuddy.Dto;
using BorrowBuddy.Services;
using Xunit;

namespace BorrowBuddy.Test.Services {
  public class FlowServiceTest : ServiceTestBase {
    [Fact]
    public async Task Get_DontExist_ShouldReturnNull() {
      // Arrange
      var options = BuildContextOptions();

      using(var context = new BorrowBuddyContext(options)) {
        var service = new FlowService(context);
        // Act
        var result = await service.GetAsync(Guid.NewGuid());

        // Assert
        Assert.Null(result);
      }
    }

    [Fact]
    public async Task Get_DontExist_ShouldReturnEmptyList() {
      // Arrange
      var options = BuildContextOptions();
      using(var context = new BorrowBuddyContext(options)) {
        var service = new FlowService(context);

        // Act
        var result = await service.GetAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
      }
    }

    [Fact]
    public async Task Get_Exists_ShouldReturnFlow() {
      // Arrange
      var options = BuildContextOptions();
      Flow flow;
      using(var context = new BorrowBuddyContext(options)) {
        flow = context.AddFlow();
        context.SaveChanges();
      }

      using(var context = new BorrowBuddyContext(options)) {
        var service = new FlowService(context);

        // Act
        var result = await service.GetAsync(flow.Id);

        // Assert
        Assert.NotNull(result);
      }
    }

    [Fact]
    public async Task Get_Exists_ShouldReturnFlows() {
      // Arrange
      var options = BuildContextOptions();
      using(var context = new BorrowBuddyContext(options)) {
        context.AddFlow();
        context.AddFlow();
        context.SaveChanges();
      }

      using(var context = new BorrowBuddyContext(options)) {
        var service = new FlowService(context);

        // Act
        var result = await service.GetAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
      }
    }

    [Fact]
    public async Task Add_ShouldAddFlow() {
      // Arrange
      var options = BuildContextOptions();
      Currency currency;
      Participant lender;
      Participant lendee;
      using(var context = new BorrowBuddyContext(options)) {
        currency = context.AddCurrency();
        lender = context.AddParticipant();
        lendee = context.AddParticipant();
        context.SaveChanges();
      }

      using(var context = new BorrowBuddyContext(options)) {
        // Act
        var service = new FlowService(context);
        var dto = new FlowDto {
          LendeeId = lendee.Id,
          LenderId = lender.Id,
          Amount = 100,
          CurrencyCode = currency.Code
        };
        await service.AddAsync(dto);
      }

      // Assert
      using(var context = new BorrowBuddyContext(options)) {
        var flow = context.Flows.FirstOrDefault();
        Assert.NotNull(flow);
        Assert.Equal(lender.Id, flow.Lender.Id);
        Assert.Equal(lendee.Id, flow.Lendee.Id);
        Assert.Equal(100, flow.Amount.Value);
        Assert.Equal(currency.Code, flow.Amount.Currency.Code);
      }
    }

    [Fact]
    public async Task Update_ShouldUpdateFlow() {
      // Arrange
      var options = BuildContextOptions();
      Flow flow;
      Participant newLender;
      Participant newLendee;
      using(var context = new BorrowBuddyContext(options)) {
        flow = context.AddFlow();
        newLender = context.AddParticipant();
        newLendee = context.AddParticipant();
        context.SaveChanges();
      }

      // Act
      using(var context = new BorrowBuddyContext(options)) {
        var service = new FlowService(context);

        await service.UpdateAsync(flow.Id, new FlowDto {
          Amount = 123,
          Comment = "newComment",
          LendeeId = newLendee.Id,
          LenderId = newLender.Id
        });
      }

      // Assert
      using(var context = new BorrowBuddyContext(options)) {
        var result = context.Flows.FirstOrDefault();
        Assert.NotNull(result);
        Assert.Equal(123, result.Amount.Value);
        Assert.Equal("newComment", result.Comment);
        Assert.Equal(newLendee.Id, result.Lendee.Id);
        Assert.Equal(newLender.Id, result.Lender.Id);
      }
    }

    [Fact]
    public async Task Delete_ShouldDeleteFlow() {
      // Arrange
      var options = BuildContextOptions();

      Flow flow;
      using(var context = new BorrowBuddyContext(options)) {
        flow = context.AddFlow();
      }

      using(var context = new BorrowBuddyContext(options)) {
        // Act
        var service = new FlowService(context);
        await service.DeleteAsync(flow.Id);
      }

      using(var context = new BorrowBuddyContext(options)) {
        // Assert
        var result = context.Flows.FirstOrDefault();
        Assert.Null(result);
      }
    }
  }
}
