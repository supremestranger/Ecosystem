using System;
using UnityEngine;

namespace Client {
    struct Circle : IEquatable<Circle> {
        public Vector2 Pos;
        public float Radius;

        public bool Equals(Circle other) {
            return Pos.Equals(other.Pos) && Radius.Equals(other.Radius);
        }

        public override bool Equals(object obj) {
            return obj is Circle other && Equals(other);
        }

        public override int GetHashCode() {
            return HashCode.Combine(Pos, Radius);
        }
        
        public static bool operator ==(Circle lhs, Circle rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(Circle lhs, Circle rhs)
        {
            return !lhs.Equals(rhs);
        }
    }
}