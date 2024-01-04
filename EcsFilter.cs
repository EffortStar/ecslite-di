using System;
using Leopotam.EcsLite;
using System.Runtime.CompilerServices;

namespace EffortStar.EcsLite.Di {
#region EcsFilter

  public struct EcsFilter<TInc> : IEcsInjectable
    where TInc : struct, IEcsInclude {

    EcsFilter? _filter;

    public EcsFilter Filter {
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      get => _filter ?? throw new InvalidOperationException($"Filter not injected");
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public EcsFilter.Enumerator GetEnumerator() => Filter.GetEnumerator();

    public void Inject(EcsWorld world) {
      TInc inc = default;
      _filter = inc.Fill(world).End();
    }

    public static implicit operator EcsFilter(EcsFilter<TInc> filter) => filter.Filter;
  }

  public struct EcsFilter<TInc, TExc> : IEcsInjectable
    where TInc : struct, IEcsInclude
    where TExc : struct, IEcsExclude {

    EcsFilter? _filter;

    public EcsFilter Filter {
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      get => _filter ?? throw new InvalidOperationException($"Filter not injected");
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public EcsFilter.Enumerator GetEnumerator() => Filter.GetEnumerator();

    public void Inject(EcsWorld world) {
      TInc inc = default;
      TExc exc = default;
      var mask = inc.Fill(world);
      _filter = exc.Fill(mask).End();
    }

    public static implicit operator EcsFilter(EcsFilter<TInc, TExc> filter) => filter.Filter;
  }

#endregion
#region Include

  public interface IEcsInclude {
    EcsWorld.Mask Fill(EcsWorld world);
  }

  public struct Inc<T1> : IEcsInclude
    where T1 : struct {
    public readonly EcsWorld.Mask Fill(EcsWorld world) => world
      .Filter<T1>();
  }

  public struct Inc<T1, T2> : IEcsInclude
    where T1 : struct
    where T2 : struct {
    public readonly EcsWorld.Mask Fill(EcsWorld world) => world
      .Filter<T1>()
      .Inc<T2>();
  }

  public struct Inc<T1, T2, T3> : IEcsInclude
    where T1 : struct
    where T2 : struct
    where T3 : struct {
    public readonly EcsWorld.Mask Fill(EcsWorld world) => world
      .Filter<T1>()
      .Inc<T2>()
      .Inc<T3>();
  }

  public struct Inc<T1, T2, T3, T4> : IEcsInclude
    where T1 : struct
    where T2 : struct
    where T3 : struct
    where T4 : struct {
    public readonly EcsWorld.Mask Fill(EcsWorld world) => world
      .Filter<T1>()
      .Inc<T2>()
      .Inc<T3>()
      .Inc<T4>();
  }

#endregion
#region Exclude

  public interface IEcsExclude {
    EcsWorld.Mask Fill(EcsWorld.Mask mask);
  }

  public struct Exc<T1> : IEcsExclude
    where T1 : struct {
    public readonly EcsWorld.Mask Fill(EcsWorld.Mask mask) => mask
      .Exc<T1>();
  }
  public struct Exc<T1, T2> : IEcsExclude
    where T1 : struct
    where T2 : struct {
    public readonly EcsWorld.Mask Fill(EcsWorld.Mask mask) => mask
      .Exc<T1>()
      .Exc<T2>();
  }

  public struct Exc<T1, T2, T3> : IEcsExclude
    where T1 : struct
    where T2 : struct
    where T3 : struct {
    public readonly EcsWorld.Mask Fill(EcsWorld.Mask mask) => mask
      .Exc<T1>()
      .Exc<T2>()
      .Exc<T3>();
  }

  public struct Exc<T1, T2, T3, T4> : IEcsExclude
    where T1 : struct
    where T2 : struct
    where T3 : struct
    where T4 : struct {
    public readonly EcsWorld.Mask Fill(EcsWorld.Mask mask) => mask
      .Exc<T1>()
      .Exc<T2>()
      .Exc<T3>()
      .Exc<T4>();
  }

#endregion
}
