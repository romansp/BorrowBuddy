using System;
using System.Linq;
using System.Threading.Tasks;
using BorrowBuddy.Data;
using BorrowBuddy.Domain;
using BorrowBuddy.Dto;
using BorrowBuddy.Services;
using Xunit;

namespace BorrowBuddy.Test.Services {

  public class ParticipantServiceTest : ServiceTestBase {
    [Fact]
    public async Task Get() {
      // Arrange
      var options = BuildContextOptions();
      var participant = new Participant();
      using(var context = new BorrowBuddyContext(options)) {
        context.Participants.Add(participant);
        context.SaveChanges();
      }

      using(var context = new BorrowBuddyContext(options)) {
        // Act
        var service = new ParticipantService(context);
        var result = await service.GetAsync(participant.Id);

        // Assert
        Assert.NotNull(result);
      }
    }

    [Fact]
    public async Task Get_NotExisting_ReturnsNull() {
      // Arrange
      var options = BuildContextOptions();

      using(var context = new BorrowBuddyContext(options)) {
        // Act
        var service = new ParticipantService(context);
        var result = await service.GetAsync(Guid.NewGuid());

        // Assert
        Assert.Null(result);
      }
    }

    [Fact]
    public void Add() {
      // Arrange
      var options = BuildContextOptions();

      using(var context = new BorrowBuddyContext(options)) {
        // Act
        var service = new ParticipantService(context);
        var participant = new ParticipantDto();
        service.AddAsync(participant);

        // Assert
        var result = context.Participants.FirstOrDefault();
        Assert.NotNull(result);
      }
    }
  }
}
