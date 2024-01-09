using UnityEngine;

namespace Client
{
    struct Unit {
        public UnitView View;
        public int Id;
        public Vector2 Velocity;
        public float WanderAngle;
        public float InvMass;
        public Vector2 Steering;
        public float MaxSeeAhead;
        public Vector2 Position;
        public float MaxSteering;
        public float MaxVelocity;
        public float MaxAvoidance;
        public float Thirst;
        public int Age;
        public float AgeTime;
        public float LastReproductionTime;
    }
}