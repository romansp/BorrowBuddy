using BorrowBuddy.Models.Requests;
using BorrowBuddy.Models.Resources;
using Reinforced.Typings.Fluent;

namespace BorrowBuddy.Shared {
  public static class TypescriptGenerationConfiguration {
    public static void Configure(ConfigurationBuilder builder) {
      builder.Global(global => global.CamelCaseForMethods().CamelCaseForProperties().TabSymbol("  ").UseModules());

      builder.AddTypeSubsitutes();
      builder.AddExports(typeof(Participant));
      builder.AddExports(typeof(FlowPost));
    }
  }
}
