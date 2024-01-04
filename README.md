# Leopotam ECSLite DI _(games.effortstar.ecslite-di)_

Dependency injection for LeoECS Lite.

## Install

1. Open Unity Package Manager,
2. click "+" and select "Add package from git URL...", and
3. use the following URL
   ```
   https://github.com/EffortStar/ecslite-di.git
   ```

## Usage

```cs
using Leopotam.EcsLite;
using EffortStar.EcsLite.Di;

class MyGame {
  EcsWorld _world;
  EcsSystems _systems;

  public MyGame(PlayerConfig playerConfig, EnemyConfig enemyConfig) {
    _world = new EcsWorld();
    _systems = new EcsSystems(_world);
    _systems
      .Add(new PlayerSystem())
      .Add(new WeaponSystem())
      .Add(new EnemySystem())
      .Inject(playerConfig, enemyConfig)
      .Init();
  }
}
```

```cs
using Leopotam.EcsLite;
using EffortStar.EcsLite.Di;

class EnemySystem : IEcsRunSystem {
  [Di] EcsWorld _world;
  [Di] EcsFilter<Inc<SpawnEnemy>> _spawnFilter;
  [Di] EcsFilter<Inc<Enemy>, Exc<IsDead>> _enemyFilter;
  [Di] EcsPool<SpawnEnemy> _spawnPool;
  [Di] EcsPool<Enemy> _enemyPool;
  [Di] EnemyConfig _config;

  public void Run(IEcsSystems systems) {
    foreach (var entity in _spawnFilter) {
      ref var spawn = ref _spawnPool.Get(entity);
      SpawnEnemy(spawn.Position);
      _world.DelEntity(entity);
    }

    foreach (var entity in _enemyFilter) {
      ref var enemy = ref _enemyPool.Get(entity);
      enemy.Position += enemy.Direction * _config.MoveSpeed;
    }
  }

  void SpawnEnemy() { /* ... */ }
}

```

## Contributing

[Contributions](https://github.com/EffortStar/ecslite-di/pulls) and [issues](https://github.com/EffortStar/ecslite-di/issues) welcome.

## License

See license in [LICENSE](LICENSE).