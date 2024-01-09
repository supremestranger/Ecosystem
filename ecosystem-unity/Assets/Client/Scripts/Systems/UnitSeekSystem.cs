using Leopotam.Ecs;
using UnityEngine;

namespace Client {
    sealed class UnitSeekSystem : IEcsRunSystem {
        private EcsFilter<Unit, Seeking> _units;

        public void Run() {
            foreach (var i in _units) {
                ref var unit = ref _units.Get1(i);
                ref var seek = ref _units.Get2(i);
                
                var desiredVel = (seek.Target - unit.Position).normalized * unit.MaxVelocity;
                unit.Steering += desiredVel - unit.Velocity;
            }
        }
    }
}