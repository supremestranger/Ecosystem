using UnityEngine;
using Leopotam.Ecs;

namespace Client {
    sealed class GoodUnitAiSystem : IEcsRunSystem {
        private EcsFilter<Unit>.Exclude<Busy> _units;
        private EnvironmentService _environmentService;
        private TimeService _timeService;
        private const float ReproductionInterval = 0f;
        
        public void Run() {
            foreach (var i in _units) {
                ref var unit = ref _units.Get1(i);
                if (unit.Thirst <= 20f) {
                    var (sea, rad) = GetClosestSea(unit.Position);

                    ref var thirsty = ref _units.GetEntity(i).Get<Thirsty>();
                    thirsty.TargetSeaPos = sea;
                    thirsty.TargetSeaRadiusSqr = rad * rad;
                    _units.GetEntity(i).Get<Busy>();
                }
                else {
                    if (_timeService.Time - unit.LastReproductionTime >= ReproductionInterval && unit.Age < 50) {
                        _units.GetEntity(i).Get<LookingForPartner>();

                    }
                    _units.GetEntity(i).Get<Wandering>();
                }
            }
        }

        private (Vector2, float) GetClosestSea(Vector2 unitPos) {
            var min = float.MaxValue;
            var pos = Vector2.zero;
            var rad = 0f;
            
            foreach (var sea in _environmentService.Seas) {
                var dist = (sea.Pos - unitPos).sqrMagnitude;
                if (dist < min) {
                    min = dist;
                    pos = sea.Pos;
                    rad = sea.Radius;
                }
            }
            
            return (pos + (unitPos - pos).normalized * rad, rad);
        }
    }
}