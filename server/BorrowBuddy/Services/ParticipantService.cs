using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BorrowBuddy.Data;
using BorrowBuddy.Domain;
using BorrowBuddy.Dto;
using Microsoft.EntityFrameworkCore;

namespace BorrowBuddy.Services {
  public class ParticipantService {
    private readonly BorrowBuddyContext _context;

    public ParticipantService(BorrowBuddyContext context) {
      _context = context;
    }

    public Task<List<Participant>> GetAsync() {
      return _context.Participants.ToListAsync();
    }

    public Task<Participant> GetAsync(Guid id) {
      return _context.Participants.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Participant> AddAsync(ParticipantDto dto) {
      var participant = new Participant {
        FirstName = dto.FirstName,
        LastName = dto.LastName,
        MiddleName = dto.MiddleName
      };
      _context.Participants.Add(participant);

      await _context.SaveChangesAsync();
      return participant;
    }

    public async Task<Participant> UpdateAsync(Guid id, ParticipantDto dto) {
      var participant = await GetAsync(id);
      participant.FirstName = dto.FirstName;
      participant.MiddleName = dto.MiddleName;
      participant.LastName = dto.LastName;

      await _context.SaveChangesAsync();
      return participant;
    }

    public async Task DeleteAsync(Guid id) {
      var participant = await GetAsync(id);
      _context.Participants.Remove(participant);
      await _context.SaveChangesAsync();
    }
  }
}
