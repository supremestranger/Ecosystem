using Leopotam.Ecs;
using UnityEngine;

namespace Client {
    sealed class UnitDrinkSystem : IEcsRunSystem {
        private EcsFilter<Unit, Thirsty> _thirstyUnits;
        private EcsFilter<Unit, Drinking> _drinkingUnits;
        private TimeService _timeService;

        public void Run() {
            foreach (var i in _thirstyUnits) {
                ref var unit = ref _thirstyUnits.Get1(i);
                ref var thirsty = ref _thirstyUnits.Get2(i);
                
                ref var a = ref _thirstyUnits.GetEntity(i).Get<Arriving>();
                a.Target = thirsty.TargetSeaPos;
                
                if ((unit.Position - thirsty.TargetSeaPos).sqrMagnitude <= 0.01f) {
                    _thirstyUnits.GetEntity(i).Get<Drinking>();
                }
            }

            foreach (var i in _drinkingUnits) {
                ref var unit = ref _drinkingUnits.Get1(i);
                unit.Velocity = Vector2.Lerp(unit.Velocity, Vector2.zero, 8f * _timeService.DeltaTime);

                unit.Thirst += 50f * _timeService.DeltaTime;
                unit.Thirst = Mathf.Clamp(unit.Thirst, 0f, 100f);
                if (unit.Thirst == 100f) {
                    _thirstyUnits.GetEntity(i).Del<Busy>();
                    _thirstyUnits.GetEntity(i).Del<Drinking>();
                    _thirstyUnits.GetEntity(i).Del<Thirsty>();
                }
            }
        }
    }
}