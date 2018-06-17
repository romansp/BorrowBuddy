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
    public async Task Get_DontExist_ShouldReturnNull() {
      // Arrange
      var options = BuildContextOptions();

      using(var context = new BorrowBuddyContext(options)) {
        var service = new ParticipantService(context);
        // Act
        var result = await service.GetAsync(Guid.NewGuid());

        // Assert
        Assert.Null(result);
      }
    }

    [Fact]
    public async Task Get_Exists_ShouldReturnParticipant() {
      // Arrange
      var options = BuildContextOptions();

      Participant participant;
      using(var context = new BorrowBuddyContext(options)) {
        participant = context.AddParticipant();
        context.SaveChanges();
      }

      using(var context = new BorrowBuddyContext(options)) {
        var service = new ParticipantService(context);

        // Act
        var result = await service.GetAsync(participant.Id);

        // Assert
        Assert.NotNull(result);
      }
    }

    [Fact]
    public async Task Get_Exists_ShouldReturnParticipants() {
      // Arrange
      var options = BuildContextOptions();
      using(var context = new BorrowBuddyContext(options)) {
        context.AddParticipant();
        context.AddParticipant();
        context.SaveChanges();
      }

      using(var context = new BorrowBuddyContext(options)) {
        var service = new ParticipantService(context);

        // Act
        var result = await service.GetAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
      }
    }

    [Fact]
    public async Task Add_ShouldAddParticipant() {
      // Arrange
      var options = BuildContextOptions();

      using(var context = new BorrowBuddyContext(options)) {
        // Act
        var service = new ParticipantService(context);
        var dto = new ParticipantDto {
          FirstName = "FirstName",
          LastName = "LastName",
          MiddleName = "MiddleName"
        };
        await service.AddAsync(dto);
      }

      // Assert
      using(var context = new BorrowBuddyContext(options)) {
        var participant = context.Participants.FirstOrDefault();
        Assert.NotNull(participant);
        Assert.Equal("FirstName", participant.FirstName);
        Assert.Equal("LastName", participant.LastName);
        Assert.Equal("MiddleName", participant.MiddleName);
      }
    }

    [Fact]
    public async Task Update_ShouldUpdateParticipant() {
      // Arrange
      var options = BuildContextOptions();

      Participant participant;
      using(var context = new BorrowBuddyContext(options)) {
        participant = context.AddParticipant();
        context.SaveChanges();
      }

      // Act
      using(var context = new BorrowBuddyContext(options)) {
        var service = new ParticipantService(context);

        await service.UpdateAsync(participant.Id, new ParticipantDto {
          FirstName = "FirstName_Modified",
          LastName = "LastName_Modified",
          MiddleName = "MiddleName_Modified"
        });
      }

      // Assert
      using(var context = new BorrowBuddyContext(options)) {
        var result = context.Participants.FirstOrDefault();
        Assert.NotNull(result);
        Assert.Equal("FirstName_Modified", result.FirstName);
        Assert.Equal("LastName_Modified", result.LastName);
        Assert.Equal("MiddleName_Modified", result.MiddleName);
      }
    }

    [Fact]
    public async Task Delete_ShouldDeleteParticipant() {
      // Arrange
      var options = BuildContextOptions();

      Participant participant;
      using(var context = new BorrowBuddyContext(options)) {
        participant = context.AddParticipant();
      }

      using(var context = new BorrowBuddyContext(options)) {
        // Act
        var service = new ParticipantService(context);
        await service.DeleteAsync(participant.Id);
      }

      using(var context = new BorrowBuddyContext(options)) {
        // Assert
        var result = context.Participants.FirstOrDefault();
        Assert.Null(result);
      }
    }
  }
}
