using System;
using System.Numerics;
using System.Xml.Linq;

namespace GravitySim
{
    public static class Gravity
    {
        public static HashSet<GravityApplied> forceAppliedHash = [];

        public struct GravityApplied
        {
            int hash;


            public GravityApplied(int id1, int id2)
            {
                hash = HashCode.Combine(id1, id2);
            }

            public override int GetHashCode() => hash;
            public bool Equals(GravityApplied other) => hash == other.hash;
            public override bool Equals(object? obj) => obj is GravityApplied other && Equals(other);

        }



        public static Vector3 GetVectorFromForce(Matter matter, Vector3 destination, float force)
        {
            Vector3 direction =
            Vector3.Normalize(destination - matter.position);

            return direction * (force / GameRoot.matterMass);
        }

        public static float DetermineGravityForce(Matter matter1, Matter matter2)
        {
            float distanceSqr = Vector3.DistanceSquared(matter1.position, matter2.position);
            float force = 0;
            if (distanceSqr > 1)
            {
                force = (GameRoot.matterMass * GameRoot.matterMass) / distanceSqr;
            }
             
            return force;
        }


        public static void UpdateGravity(Matter matter1)
        {
            foreach (Matter matter2 in GameRoot.entityManager.matterObjects)
            {
                if (!forceAppliedHash.Contains(new GravityApplied(matter2.id, matter1.id)) && matter1 != matter2)
                {
                    float force = Gravity.DetermineGravityForce(matter1, matter2);
                    if (force > 0)
                    {
                        matter1.velocity += GetVectorFromForce(matter1, matter2.position, force);
                        matter2.velocity += GetVectorFromForce(matter2, matter1.position, force);
                        forceAppliedHash.Add(new GravityApplied(matter1.id, matter2.id));
                    }
                }

            }
        }
    }


   
}


