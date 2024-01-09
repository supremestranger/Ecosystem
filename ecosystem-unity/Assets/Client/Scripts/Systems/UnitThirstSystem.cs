using Leopotam.Ecs;

namespace Client {
    sealed class UnitThirstSystem : IEcsRunSystem {
        private EcsFilter<Unit> _units;
        private TimeService _timeService;

        public void Run() {
            foreach (var i in _units) {
                ref var unit = ref _units.Get1(i);
                unit.Thirst -= 0.5f * _timeService.DeltaTime;
            }
        }
    }
}