using BorrowBuddy.Domain;
using Reinforced.Typings.Fluent;

namespace BorrowBuddy.Shared {
  public class TypescriptGenerationConfiguration {
    public static void Configure(ConfigurationBuilder builder) {
      builder.Global(global => global.CamelCaseForMethods().CamelCaseForProperties().TabSymbol("  ").UseModules());

      builder.AddTypeSubsitutes();
      builder.AddExports(typeof(Participant));
    }
  }
}
