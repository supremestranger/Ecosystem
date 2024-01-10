using Leopotam.Ecs;

namespace Client {
    sealed class UnitFollowSystem : IEcsRunSystem {
        private EcsFilter<Unit, Follow> _units;
        
        public void Run() {
            foreach (var i in _units) {
                ref var follow = ref _units.Get2(i);

                ref var arriving = ref _units.GetEntity(i).Get<Arriving>();

                arriving.Target = follow.Target.Get<Unit>().Position;
            }
        }
    }
}