using Leopotam.Ecs;
using UnityEngine;

namespace Client {
    sealed class UnitSpawnSystem : IEcsRunSystem {
        private EcsFilter<UnitSpawnEvent> _events;
        private Config _config;
        private EcsWorld _world;
        private UnitService _unitService;

        public void Run() {
            foreach (var i in _events) {
                ref var e = ref _events.Get1(i);
                
                var entity = _world.NewEntity();
                entity.Get<Wandering>();
                ref var unit = ref entity.Get<Unit>();
                
                var view = e.View != null ? e.View : Object.Instantiate(_config.UnitPrefab, e.Pos, Quaternion.identity);

                unit.View = view;
                unit.InvMass = 1 / 2f;
                unit.MaxSeeAhead = _config.UnitMaxSeeAhead;
                unit.MaxSteering = 30f;
                unit.MaxVelocity = 8f;
                unit.Thirst = Random.Range(0f, 100f);
                unit.MaxAvoidance = 25f;
                unit.Position = view.Transform.position;
                unit.Id = _unitService.UnitCount;
                view.Entity = entity;

                _unitService.Units.Add(entity);
                _unitService.UnitCount++;

                _events.GetEntity(i).Destroy();
            }
        }
    }
}