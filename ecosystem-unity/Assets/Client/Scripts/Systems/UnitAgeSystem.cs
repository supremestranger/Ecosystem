using Leopotam.Ecs;
using UnityEngine;

namespace Client {
    sealed class UnitAgeSystem : IEcsRunSystem {
        private EcsFilter<Unit> _units;
        private TimeService _timeService;
        private const float AgeDuration = 100f;

        public void Run() {
            foreach (var i in _units) {
                ref var unit = ref _units.Get1(i);
                unit.AgeTime += _timeService.DeltaTime;
                var ageAdd = (int) Mathf.Floor(unit.AgeTime / AgeDuration);
                unit.AgeTime %= AgeDuration;
                unit.Age += ageAdd;
            }
        }
    }
}