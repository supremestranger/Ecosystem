using Leopotam.Ecs;
using UnityEngine;

namespace Client {
    sealed class UnitBornSystem : IEcsRunSystem {
        private EcsFilter<Reproduction, Reproducing> _reproductions;
        private TimeService _timeService;
        private EcsWorld _world;
        
        public void Run() {
            foreach (var i in _reproductions) {
                ref var reproduction = ref _reproductions.Get1(i);
                ref var reproducing = ref _reproductions.Get2(i);
                
                if (_timeService.Time >= reproducing.BornTime) {
                    ref var unit1 = ref reproduction.First.Get<Unit>();
                    ref var unit2 = ref reproduction.Second.Get<Unit>();
                    ref var spawnEvent = ref _world.NewEntity().Get<UnitSpawnEvent>();
                    var pos1 = unit1.Position;
                    var pos2 = unit2.Position;
                    spawnEvent.Pos = (pos1 + pos2) * 0.5f;

                    reproduction.First.Del<Follow>();
                    reproduction.Second.Del<Follow>();
                    reproduction.First.Del<Busy>();
                    reproduction.Second.Del<Busy>();
                    reproduction.First.Del<Reproducing>();
                    reproduction.Second.Del<Reproducing>();

                    unit1.LastReproductionTime = _timeService.Time + 10f;
                    unit2.LastReproductionTime = _timeService.Time + 10f;
                    
                    _reproductions.GetEntity(i).Destroy();
                }
            }
        }
    }
}