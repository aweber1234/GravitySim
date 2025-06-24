using System;
using System.Numerics;

namespace GravitySim
{
    public class Matter
    {

        public readonly float mass;
        public Vector3 position;
        public Vector3 velocity;


        public Matter(float mass, Vector3 position, Vector3 velocity)
        {
            this.mass = mass;
            this.position = position;
            this.velocity = velocity;
        }

        public Matter(Matter matter)
        {
            this.mass = matter.mass;
            this.position = matter.position;
            this.velocity = matter.velocity;
        }


    }
}