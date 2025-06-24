using System;
using System.Numerics;

namespace GravitySim
{
    public static class Gravity
    {
        public static Dictionary<(Matter, Matter), float> forceHash = [];


        public static bool AlreadyApplied(Matter matter1, Matter matter2, out float force)
        {
            bool hasForce = false;
            force = 0;
            
            if (forceHash.ContainsKey((matter1, matter2)))
            {
                force = forceHash[(matter1, matter2)];
                hasForce = true;
            }
            else if (forceHash.ContainsKey((matter2, matter1)))
            {
                force = forceHash[(matter2, matter1)];
                hasForce = true;
            }
            
            return hasForce;
        }

        public static Vector3 GetVectorFromForce(Matter matter, Vector3 destination, float force)
        {
            Vector3 direction =
            Vector3.Normalize(destination - matter.position);

            return direction * (force / matter.mass);
        }

        public static float DetermineGravityForce(Matter matter1, Matter matter2)
        {
            float distanceSqr = Vector3.DistanceSquared(matter1.position, matter2.position);
            float force = 0;
            float massDivisor = 1;
            if (distanceSqr > 1)
            {
                force = ((matter1.mass / massDivisor) * (matter2.mass / massDivisor)) / distanceSqr;
            }
             
            return force;
        }



    }
}


