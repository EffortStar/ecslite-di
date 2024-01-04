using System;
using Leopotam.EcsLite;
using System.Collections.Generic;
using System.Reflection;

#if ECSLITE_EXTENDED
using Leopotam.EcsLite.ExtendedSystems;
#endif

namespace EffortStar.EcsLite.Di {
  public static class EcsLiteDi {
    public static IEcsSystems Inject(this IEcsSystems systems, params object[] injects) =>
      Inject(systems, (IReadOnlyList<object>) injects);

    public static IEcsSystems Inject(this IEcsSystems systems, IReadOnlyList<object> injects) {
      foreach (var system in systems.GetAllSystems()) {
#if ECSLITE_EXTENDED
        if (system is EcsGroupSystem groupSystem) {
          foreach (var nestedSystem in groupSystem.GetNestedSystems()) {
            Inject(nestedSystem, systems, injects);
          }
        } else {
          Inject(system, systems, injects);
        }
#endif
      }
      return systems;
    }

    public static void Inject(object target, IEcsSystems systems, IReadOnlyList<object> injects) {
      var fields = target.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
      foreach (var f in fields) {
        var attributes = f.GetCustomAttributes(typeof(DiAttribute), true);
        if (attributes.Length == 0) {
          continue;
        }
        var diAttribute = (DiAttribute) attributes[0];
        if (Attribute.IsDefined(f, typeof(DiAttribute))) {
          Inject(target, f, systems, diAttribute.WorldName, injects);
        }
      }
    }

    static object GetPool(EcsWorld world, Type type) {
      var method = typeof(EcsWorld).GetMethod(nameof(EcsWorld.GetPool));
      var genericMethod = method.MakeGenericMethod(type);
      return genericMethod.Invoke(world, null);
    }

    static void Inject(
      object target,
      FieldInfo f,
      IEcsSystems systems,
      string? worldName,
      IReadOnlyList<object> injects
    ) {
      if (systems == null) {
        throw new InvalidOperationException($"System is null");
      }
      // Inject world.
      if (f.FieldType == typeof(EcsWorld)) {
        f.SetValue(target, systems.GetWorld(worldName));
        return;
      }

      // Inject pool.
      if (
        f.FieldType.IsGenericType &&
        f.FieldType.GetGenericTypeDefinition() == typeof(EcsPool<>)
      ) {
        var componentType = f.FieldType.GetTypeInfo().GenericTypeArguments[0];
        var pool = GetPool(systems.GetWorld(worldName), componentType)
          ?? throw InjectError(f, $"Unable to get pool of type {componentType.Name}");
        f.SetValue(target, pool);
        return;
      }

      // Inject IEcsInjectable.
      if (typeof(IEcsInjectable).IsAssignableFrom(f.FieldType)) {
        var value = (IEcsInjectable) (f.GetValue(target) ?? Activator.CreateInstance(f.FieldType));
        value.Inject(systems.GetWorld(worldName));
        f.SetValue(target, value);
        return;
      }

      // Inject custom.
      for (var i = 0; i < injects.Count; i++) {
        if (f.FieldType.IsAssignableFrom(injects[i].GetType())) {
          f.SetValue(target, injects[i]);
          return;
        }
      }

      // Error if using on bare filter type.
      if (f.FieldType == typeof(EcsFilter)) {
        throw InjectError(f, $"Use EcsFitler<Inc> or EcsFilter<Inc, Exc> instead");
      }

      throw InjectError(f, "Unhandled type");
    }

    static Exception InjectError(FieldInfo field, string message) =>
      new InvalidOperationException($"Cannot inject {field.FieldType.Name} {field.Name}: {message}");
  }
}
