using Leopotam.Ecs;
using UnityEngine;

namespace Client {
    sealed class InitSystem : IEcsInitSystem {
        private EcsWorld _world;
        private EnvironmentService _environmentService;
        private Config _config;
        
        public void Init() {
            // заранее помещенные на сцене юниты
            foreach (var view in Object.FindObjectsOfType<UnitView>()) {
                ref var e = ref _world.NewEntity().Get<UnitSpawnEvent>();
                e.View = view;
            }

            foreach (var obs in Object.FindObjectsOfType<ObstacleView>()) {
                // небольшое увеличение радиуса, чтобы избегание работало лучше
                _environmentService.Obstacles.Add(new Circle {Pos = obs.Transform.position, Radius = obs.Transform.localScale.x + 0.25f}); 
            }
            
            foreach (var sea in Object.FindObjectsOfType<SeaView>()) {
                _environmentService.Seas.Add(new Circle {Pos = sea.Transform.position, Radius = sea.Transform.localScale.x});
            }
        }
    }
}