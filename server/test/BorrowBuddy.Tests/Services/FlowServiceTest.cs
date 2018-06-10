using System;
using System.Linq;
using System.Threading.Tasks;
using BorrowBuddy.Data;
using BorrowBuddy.Domain;
using BorrowBuddy.Dto;
using BorrowBuddy.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BorrowBuddy.Test.Services {

  public class FlowServiceTest {
    [Fact]
    public async Task Add() {
      // Arrange
      var options = Extensions.СontextOptions();
      var currency = new Currency() {
        Code = "BYN"
      };
      var lender = new Participant();
      var lendee = new Participant();
      using(var context = new BorrowBuddyContext(options)) {
        context.Currencies.Add(currency);
        context.Participants.Add(lender);
        context.Participants.Add(lendee);
        context.SaveChanges();
      }

      using(var context = new BorrowBuddyContext(options)) {
        // Act
        var service = new FlowService(context);
        var dto = new FlowDto() {
          LendeeId = lendee.Id,
          LenderId = lender.Id,
          Amount = 100,
          Code = currency.Code
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
        Assert.Equal("BYN", flow.Amount.Currency.Code);
      }
    }
  }
}
