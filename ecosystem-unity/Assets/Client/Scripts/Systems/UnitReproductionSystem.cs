using Leopotam.Ecs;

namespace Client {
    sealed class UnitReproductionSystem : IEcsRunSystem {
        private EcsFilter<Reproduction>.Exclude<Reproducing> _reproductions;
        private TimeService _timeService;
        private EcsWorld _world;

        public void Run() {
            foreach (var i in _reproductions) {
                ref var reproduction = ref _reproductions.Get1(i);

                ref var unit1 = ref reproduction.First.Get<Unit>();
                ref var unit2 = ref reproduction.Second.Get<Unit>();
                

                if ((unit1.Position - unit2.Position).sqrMagnitude <= 0.01f) {
                    ref var reproducing = ref _reproductions.GetEntity(i).Get<Reproducing>();
                    reproduction.First = reproduction.First;
                    reproduction.Second = reproduction.Second;
                    reproducing.BornTime = _timeService.Time + 5f;
                } else {
                    reproduction.First.Get<Follow>().Target = reproduction.Second;
                    reproduction.Second.Get<Follow>().Target = reproduction.First;
                }
            }
        }

    }
}