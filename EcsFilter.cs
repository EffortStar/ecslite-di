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
  
  public struct Inc<T1, T2, T3, T4, T5> : IEcsInclude
	  where T1 : struct
	  where T2 : struct
	  where T3 : struct
	  where T4 : struct
	  where T5 : struct {
	  public readonly EcsWorld.Mask Fill(EcsWorld world) => world
		  .Filter<T1>()
		  .Inc<T2>()
		  .Inc<T3>()
		  .Inc<T4>()
		  .Inc<T5>();
  }
  
  public struct Inc<T1, T2, T3, T4, T5, T6> : IEcsInclude
	  where T1 : struct
	  where T2 : struct
	  where T3 : struct
	  where T4 : struct
	  where T5 : struct
	  where T6 : struct {
	  public readonly EcsWorld.Mask Fill(EcsWorld world) => world
		  .Filter<T1>()
		  .Inc<T2>()
		  .Inc<T3>()
		  .Inc<T4>()
		  .Inc<T5>()
		  .Inc<T6>();
  }
  
  public struct Inc<T1, T2, T3, T4, T5, T6, T7> : IEcsInclude
	  where T1 : struct
	  where T2 : struct
	  where T3 : struct
	  where T4 : struct
	  where T5 : struct
	  where T6 : struct
	  where T7 : struct {
	  public readonly EcsWorld.Mask Fill(EcsWorld world) => world
		  .Filter<T1>()
		  .Inc<T2>()
		  .Inc<T3>()
		  .Inc<T4>()
		  .Inc<T5>()
		  .Inc<T6>()
		  .Inc<T7>();
  }
  
  public struct Inc<T1, T2, T3, T4, T5, T6, T7, T8> : IEcsInclude
	  where T1 : struct
	  where T2 : struct
	  where T3 : struct
	  where T4 : struct
	  where T5 : struct
	  where T6 : struct
	  where T7 : struct
	  where T8 : struct {
	  public readonly EcsWorld.Mask Fill(EcsWorld world) => world
		  .Filter<T1>()
		  .Inc<T2>()
		  .Inc<T3>()
		  .Inc<T4>()
		  .Inc<T5>()
		  .Inc<T6>()
		  .Inc<T7>()
		  .Inc<T8>();
  }
  
  public struct Inc<T1, T2, T3, T4, T5, T6, T7, T8, T9> : IEcsInclude
	  where T1 : struct
	  where T2 : struct
	  where T3 : struct
	  where T4 : struct
	  where T5 : struct
	  where T6 : struct
	  where T7 : struct
	  where T8 : struct
	  where T9 : struct {
	  public readonly EcsWorld.Mask Fill(EcsWorld world) => world
		  .Filter<T1>()
		  .Inc<T2>()
		  .Inc<T3>()
		  .Inc<T4>()
		  .Inc<T5>()
		  .Inc<T6>()
		  .Inc<T7>()
		  .Inc<T8>()
		  .Inc<T9>();
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
  
  public struct Exc<T1, T2, T3, T4, T5> : IEcsExclude
	  where T1 : struct
	  where T2 : struct
	  where T3 : struct
	  where T4 : struct
	  where T5 : struct {
	  public readonly EcsWorld.Mask Fill(EcsWorld.Mask mask) => mask
		  .Exc<T1>()
		  .Exc<T2>()
		  .Exc<T3>()
		  .Exc<T4>()
		  .Exc<T5>();
  }
  
  public struct Exc<T1, T2, T3, T4, T5, T6> : IEcsExclude
	  where T1 : struct
	  where T2 : struct
	  where T3 : struct
	  where T4 : struct
	  where T5 : struct
	  where T6 : struct {
	  public readonly EcsWorld.Mask Fill(EcsWorld.Mask mask) => mask
		  .Exc<T1>()
		  .Exc<T2>()
		  .Exc<T3>()
		  .Exc<T4>()
		  .Exc<T5>()
		  .Exc<T6>();
  }
  
  public struct Exc<T1, T2, T3, T4, T5, T6, T7> : IEcsExclude
	  where T1 : struct
	  where T2 : struct
	  where T3 : struct
	  where T4 : struct
	  where T5 : struct
	  where T6 : struct
	  where T7 : struct {
	  public readonly EcsWorld.Mask Fill(EcsWorld.Mask mask) => mask
		  .Exc<T1>()
		  .Exc<T2>()
		  .Exc<T3>()
		  .Exc<T4>()
		  .Exc<T5>()
		  .Exc<T6>()
		  .Exc<T7>();
  }
  
  public struct Exc<T1, T2, T3, T4, T5, T6, T7, T8> : IEcsExclude
	  where T1 : struct
	  where T2 : struct
	  where T3 : struct
	  where T4 : struct
	  where T5 : struct
	  where T6 : struct
	  where T7 : struct
	  where T8 : struct {
	  public readonly EcsWorld.Mask Fill(EcsWorld.Mask mask) => mask
		  .Exc<T1>()
		  .Exc<T2>()
		  .Exc<T3>()
		  .Exc<T4>()
		  .Exc<T5>()
		  .Exc<T6>()
		  .Exc<T7>()
		  .Exc<T8>();
  }
  
  public struct Exc<T1, T2, T3, T4, T5, T6, T7, T8, T9> : IEcsExclude
	  where T1 : struct
	  where T2 : struct
	  where T3 : struct
	  where T4 : struct
	  where T5 : struct
	  where T6 : struct
	  where T7 : struct
	  where T8 : struct
	  where T9 : struct {
	  public readonly EcsWorld.Mask Fill(EcsWorld.Mask mask) => mask
		  .Exc<T1>()
		  .Exc<T2>()
		  .Exc<T3>()
		  .Exc<T4>()
		  .Exc<T5>()
		  .Exc<T6>()
		  .Exc<T7>()
		  .Exc<T8>()
		  .Exc<T9>();
  }

#endregion
}
