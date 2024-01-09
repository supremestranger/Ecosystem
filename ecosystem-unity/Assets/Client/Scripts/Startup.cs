using Leopotam.Ecs;
using UnityEngine;

namespace Client {
    sealed class Startup : MonoBehaviour {
        EcsWorld _world;
        EcsSystems _systems;
        [SerializeField] private Ui _ui;

        void Start() {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);
            
            var cs = new CameraService {
                Camera = Camera.main,
                CurSize = 10f
            };
            
            var ts = new TimeService();
            
            var config = new Config {
                MinCameraSize = 1f,
                MaxCameraSize = 35f,
                CameraZoomSpeed = 4f,
                UnitMaxSeeAhead = 1f
            };

            Application.targetFrameRate = 60;
            
            var es = new EnvironmentService();
            var us = new UnitService();

#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(_world);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_systems);
#endif
            _systems
                .Add(new InitSystem())
                .Add(new UnitSpawnSystem())
                .Add(new TimeSystem())
                .Add(new NextUnitFindSystem())
                .Add(new CameraControlSystem())
                .Add(new UnitAgeSystem())
                .OneFrame<Wandering>()
                .OneFrame<Seeking>()
                .OneFrame<Arriving>()
                .Add(new GoodUnitAiSystem())
                .Add(new UnitWanderSystem())
                .Add(new UnitSeekSystem())
                .Add(new UnitArriveSystem())
                .Add(new UnitCollisionAvoidanceSystem())
                .Add(new UnitMoveSystem()) // применение стиринга
                .Add(new UnitReproductionSystem())
                .Add(new DayNightSystem())
                .Add(new TimeControlSystem())
                .Add(new UnitThirstSystem())
                .Add(new UnitDrinkSystem())

                .Inject(cs)
                .Inject(ts)
                .Inject(config)
                .Inject(es)
                .Inject(us)
                .Inject(_ui)
                .Init();
        }

        void Update() {
            _systems?.Run();
        }

        void OnDestroy() {
            if (_systems != null) {
                _systems.Destroy();
                _systems = null;
                _world.Destroy();
                _world = null;
            }
        }
    }
}