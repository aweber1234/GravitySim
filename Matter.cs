using System;
using System.Numerics;

namespace GravitySim
{
    public class Matter
    {
        private readonly float mass;
        public Vector3 position;
        public Vector3 velocity;
        public int id;

        public Matter(float mass, Vector3 position, Vector3 velocity, int id)
        {
            this.mass = mass;
            this.position = position;
            this.velocity = velocity;
            this.id = id;
        }

        public Matter(Matter matter)
        {
            this.mass = matter.mass;
            this.position = matter.position;
            this.velocity = matter.velocity;
        }


    }
}