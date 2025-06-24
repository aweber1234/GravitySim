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

        public static void ColllisionProcess(Matter matter)
        {
            List<Matter> nearObjects = GameRoot.entityManager.GetNearObjects(matter.position, 1);
            //runs collision calculations
            if (nearObjects.Count > 1)
            {                    //updates collision for entities colliding
                foreach (Matter collidedMatter in nearObjects)
                {
                    if (matter != collidedMatter && Vector3.DistanceSquared(matter.position, collidedMatter.position) < 1)
                    {
                        Collision.CollisionMath(matter, collidedMatter, out Vector3 newVelocity1, out Vector3 newVelocity2);
                        matter.velocity = newVelocity1;
                        collidedMatter.velocity = newVelocity2;
                    }
                }
            }
        }





    }
}
