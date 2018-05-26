using Reinforced.Typings.Ast.TypeNames;
using Reinforced.Typings.Fluent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BorrowBuddy.Shared {
  internal static class ConfigurationBuilderExtensions {

    private static readonly Dictionary<Type, RtTypeName> _typeSubstitutes = new Dictionary<Type, RtTypeName> {
      { typeof(DateTimeOffset), new RtSimpleTypeName("string") }
    };

    public static void AddExports(this ConfigurationBuilder builder, Type markerType) {
      var types = markerType.GetTypeInfo().Assembly.GetExportedTypes()
          .Where(type => type.Namespace == markerType.Namespace)
          .ToLookup(c => c.GetTypeInfo().IsEnum);
      var enums = types[true];
      var classes = types[false];

      builder.ExportAsEnums(enums, config => config.DontIncludeToNamespace().Const());
      builder.ExportAsInterfaces(classes, config => config.WithPublicFields().WithPublicMethods().WithPublicProperties().AutoI(false).DontIncludeToNamespace());
    }

    public static void AddTypeSubsitutes(this ConfigurationBuilder builder) {
      foreach (var typeSubstitute in _typeSubstitutes) {
        builder.Substitute(typeSubstitute.Key, typeSubstitute.Value);
      }
    }
  }
}
