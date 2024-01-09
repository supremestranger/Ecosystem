using Leopotam.Ecs;
using UnityEngine;

namespace Client {
    sealed class UnitReproductionSystem : IEcsRunSystem {
        private EcsFilter<Unit> _units;
        private TimeService _timeService;
        private const float ReproductionInterval = 30f;
        
        public void Run() {
            if (_units.GetEntitiesCount() < 2) {
                return;
            }
            
            EcsEntity unitEntity1 = default;
            EcsEntity unitEntity2 = default;
            for (int k = 0; k < 2; k++) {
                foreach (var i in _units) {
                    ref var unit = ref _units.Get1(i);
                    ref var entity = ref _units.GetEntity(i);

                    if (_timeService.Time - unit.LastReproductionTime >= ReproductionInterval && unit.Age < 50) {
                        if (entity != unitEntity1 && unitEntity1 == default) {
                            unitEntity1 = entity;
                            unit.LastReproductionTime = _timeService.Time;
                            break;
                        }

                        if (entity != unitEntity2 && unitEntity2 == default) {
                            unitEntity2 = entity;
                            unit.LastReproductionTime = _timeService.Time;
                            break;
                        }
                    }
                }
            }

            if (unitEntity1 != default && unitEntity2 != default) {
                Debug.Log("Hi");
            }
        }

    }
}