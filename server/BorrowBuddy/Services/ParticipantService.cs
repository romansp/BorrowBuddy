using System;
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

    public Task<Participant> GetAsync(Guid id) {
      return _context.Participants.FirstOrDefaultAsync(c => c.Id == id);
    }

    public Task AddAsync(ParticipantDto dto) {
      _context.Participants.Add(new Participant {
        FirstName = dto.FirstName,
        LastName = dto.LastName,
        MiddleName = dto.MiddleName
      });

      return _context.SaveChangesAsync();
    }
  }
}
