using Leopotam.Ecs;
using UnityEngine;

namespace Client {
    sealed class UnitArriveSystem : IEcsRunSystem {
        private EcsFilter<Unit, Arriving> _units;

        public void Run() {
            foreach (var i in _units) {
                ref var unit = ref _units.Get1(i);
                ref var arriving = ref _units.Get2(i);

                var desiredVel = arriving.Target - unit.Position;
                var dist = desiredVel.magnitude;

                if (dist < 5f) {
                    desiredVel = desiredVel.normalized * unit.MaxVelocity * (dist / 5f);
                } else {
                    desiredVel = desiredVel.normalized * unit.MaxVelocity;
                }
                
                unit.Steering += (desiredVel - unit.Velocity);
            }
        }
    }
}