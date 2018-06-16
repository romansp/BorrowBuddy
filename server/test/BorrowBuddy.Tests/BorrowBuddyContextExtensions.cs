using BorrowBuddy.Data;
using BorrowBuddy.Domain;

namespace BorrowBuddy.Test {
  internal static class ContextExtensions {
    
    public static Currency AddCurrency(this BorrowBuddyContext context) {
      var currency = new Currency() {
        Code = "BYN"
      };
      context.Currencies.Add(currency);
      context.SaveChanges();
      return currency;
    }

    public static Participant AddParticipant(this BorrowBuddyContext context) {
      var participant = new Participant();
      context.Participants.Add(participant);
      context.SaveChanges();
      return participant;
    }
    
    public static Flow Addlow(this BorrowBuddyContext context, Participant from, Participant to, Currency currency, int amount) {
      var flow = new Flow() {
        Lendee = to,
        Lender = from,
        Amount = new Money {
          Currency = currency,
          Value = amount
        },
      };
      var f = context.Flows.Add(flow);
      context.SaveChanges();
      return flow;
    }
  }
}
