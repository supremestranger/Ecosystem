using Leopotam.Ecs;
using UnityEngine;

namespace Client {
    sealed class UnitMoveSystem : IEcsRunSystem {
        private EcsFilter<Unit> _units;
        private TimeService _timeService;
        
        public void Run() {
            foreach (var i in _units) {
                ref var unit = ref _units.Get1(i);
                
                unit.Steering = Vector2.ClampMagnitude(unit.Steering, unit.MaxSteering);

                unit.Velocity = Vector2.ClampMagnitude(unit.Velocity + unit.Steering * _timeService.DeltaTime, unit.MaxVelocity);

                unit.Position += unit.Velocity * _timeService.DeltaTime;
                unit.View.SetPos(unit.Position);
                unit.Steering = default;
            }
        }
    }
}