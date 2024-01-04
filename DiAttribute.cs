using System;

namespace EffortStar.EcsLite.Di {
  [AttributeUsage(AttributeTargets.Field)]
  public class DiAttribute : Attribute {
    public string? WorldName;
  }
}
