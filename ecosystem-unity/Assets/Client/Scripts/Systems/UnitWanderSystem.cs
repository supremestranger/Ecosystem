using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    sealed class UnitWanderSystem : IEcsRunSystem {
        private EcsFilter<Unit, Wandering> _units;
        private const float CircleDistance = 0.5f;
        private const float CircleSize = 2f;

        public void Run() {
            foreach (var i in _units) {
                ref var unit = ref _units.Get1(i);
                var steering = Wander(ref unit);
                unit.Steering += steering;
            }
        }

        private Vector2 Wander(ref Unit unit) {
            var circleCenter = unit.Velocity.normalized * CircleDistance;
            var displacement = new Vector2(0, 1) * CircleSize;

            SetAngle(ref displacement, unit.WanderAngle);

            unit.WanderAngle += Random.Range(-1f, 1f) * 120f;
            
            return circleCenter + displacement;
        }

        private void SetAngle(ref Vector2 displacement, float wanderAngle)
        {
            var len = displacement.magnitude;
            displacement.x = Mathf.Cos(wanderAngle) * len;
            displacement.y = Mathf.Sin(wanderAngle) * len;
        }
    }
}