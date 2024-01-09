using Leopotam.Ecs;
using UnityEngine;

namespace Client {
    sealed class UnitCollisionAvoidanceSystem : IEcsRunSystem {
        private EcsFilter<Unit> _units;
        private EnvironmentService _environmentService;

        public void Run() {
            foreach (var i in _units) {
                ref var unit = ref _units.Get1(i);

                var dynamicLength = unit.Velocity.magnitude / unit.MaxVelocity;
                var ahead = unit.Position + unit.Velocity.normalized * unit.MaxSeeAhead;
                var ahead2 = unit.Position + unit.Velocity.normalized * unit.MaxSeeAhead * 0.5f;
                
                
                var (pos, ok, dist) = FindMostThreateningObstacle(unit.Position, ahead, ahead2);
                var avoidance = new Vector2(0, 0);

                if (ok) {
                    avoidance.x = ahead.x - pos.x;
                    avoidance.y = ahead.y - pos.y;
                    
                    avoidance.Normalize();
                    unit.Steering += avoidance * unit.MaxAvoidance;
                }
            }
        }

        private (Vector2, bool, float) FindMostThreateningObstacle(Vector2 unitPos, Vector2 ahead, Vector2 ahead2) {
            var ok = false;
            var min = float.MaxValue;
            Vector2 pos = default;
            foreach (var obs in _environmentService.Obstacles) {
                var distA = Vector2.Distance(ahead, obs.Pos);
                var distA1 = Vector2.Distance(ahead2, obs.Pos);
                var dist = Vector2.Distance(unitPos, obs.Pos);

                var collision = distA <= obs.Radius || dist <= obs.Radius || distA1 <= obs.Radius;

                if (collision && (dist < min)) {
                    ok = true;
                    min = dist;
                    pos = obs.Pos;
                }
            }

            return (pos, ok, 1 / min);
        }
    }
}