using System;
using BorrowBuddy.Data;
using BorrowBuddy.Domain;

namespace BorrowBuddy.Test {
  internal static class ContextExtensions {
    public static Currency AddCurrency(this BorrowBuddyContext context, string code = "") {
      if (string.IsNullOrEmpty(code)) {
        code = Guid.NewGuid().ToString();
      }
      var currency = new Currency {
        Code = code
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

    public static Flow AddFlow(this BorrowBuddyContext context, Participant from, Participant to, Currency currency, long amount) {
      var flow = new Flow {
        Lendee = to,
        Lender = from,
        Amount = new Money {
          Currency = currency,
          Value = amount
        }
      };
      context.Flows.Add(flow);
      context.SaveChanges();
      return flow;
    }

    public static Flow AddFlow(this BorrowBuddyContext context, long amount = 0) {
      var currency = context.AddCurrency();
      var from = context.AddParticipant();
      var to = context.AddParticipant();
      return context.AddFlow(from, to, currency, amount);
    }
  }
}
