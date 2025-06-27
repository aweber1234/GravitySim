using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GravitySim
{
    public static class Collision
    {
        public static void CollisionMath(Matter matter1, Matter matter2, out Vector3 newVelocity1, out Vector3 newVelocity2)
        {
            Vector3 collisionPoint = matter2.position - matter1.position;
            Vector3 normal = Vector3.Normalize(collisionPoint);
            float vRel = Vector3.Dot(matter1.velocity - matter2.velocity, normal);
            double massTotal = GameRoot.matterMass + GameRoot.matterMass;

            newVelocity1 = matter1.velocity - (float)(((1 + GameRoot.collisionRebound) * GameRoot.matterMass) / massTotal) * vRel * normal;
            newVelocity2 = matter2.velocity + (float)(((1 + GameRoot.collisionRebound) * GameRoot.matterMass) / massTotal) * vRel * normal;

        }

        public static void ColllisionProcess(Matter matter, Matter matter2)
        {
            if (Vector3.DistanceSquared(matter.position, matter2.position) < 1)
            {
                Collision.CollisionMath(matter, matter2, out Vector3 newVelocity1, out Vector3 newVelocity2);
                matter.velocity = newVelocity1;
                matter2.velocity = newVelocity2;
            }
        }





    }
}
