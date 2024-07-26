using System;
using JetBrains.Annotations;

namespace EffortStar.EcsLite.Di {
  [AttributeUsage(AttributeTargets.Field), MeansImplicitUse(ImplicitUseKindFlags.Assign)]
  public class DiAttribute : Attribute {
    public string? WorldName;
  }
}
