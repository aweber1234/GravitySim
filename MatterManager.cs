using System;
using System.Data;
using System.Numerics;
using System.Security.Cryptography;
using Microsoft.VisualBasic;

namespace GravitySim
{
    public class MatterManager
    {
        List<Matter> matterObjects = [];
        Dictionary<VectorInt, List<Matter>> matterObjectsSpace = [];
        Dictionary<Matter, Matter> collisionHash = [];

        public static int ComputedSpaceWidth = 100;
        public static int ComputedSpaceHeight = 50;


        public List<Matter> GetNearObjects(Vector3 position, int radius)
        {
            List<Matter> nearObjects = [];



            for (int x = (int)position.X - radius; x <= (int)position.X + radius; x++)
            {
                for (int y = (int)position.Y - radius; y <= (int)position.Y + radius; y++)
                {
                    if (matterObjectsSpace.TryGetValue(new VectorInt(x, y, 0), out List<Matter>? contents))
                    {
                        nearObjects.AddRange(contents);
                    }
                }
            }
            return nearObjects;
        }

        /// <summary>
        /// Converts world position to manager array indexes. 
        /// </summary>
        /// <param name="position"></param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="isWithinComputedSpace"></param>
        public void GetIndexes(Vector3 position, out int X, out int Y, out bool isWithinComputedSpace)
        {
            X = (int)position.X + ComputedSpaceWidth / 2;
            Y = (int)position.Y + ComputedSpaceHeight / 2;
            if (X >= 0 && X < ComputedSpaceWidth && Y >= 0 && Y < ComputedSpaceHeight)
            {
                isWithinComputedSpace = true;
            }
            else { isWithinComputedSpace = false; }
        }

        public void Add(Matter matter)
        {
            matterObjects.Add(matter);

            if (matterObjectsSpace.ContainsKey(new VectorInt(matter.position)))
            {
                matterObjectsSpace[new VectorInt(matter.position)].Add(matter);
            }
            else { matterObjectsSpace.Add(new VectorInt(matter.position), new List<Matter> { matter }); }
        }


        private void UpdateGravity(Matter matter1)
        {
            foreach (Matter matter2 in matterObjects)
            {
                if (!Gravity.AlreadyApplied(matter1, matter2, out _) && matter1 != matter2)
                {
                    float force = Gravity.DetermineGravityForce(matter1, matter2);
                    if (force > 0)
                    {
                        matter1.velocity += Gravity.GetVectorFromForce(matter1, matter2.position, force);
                        matter2.velocity += Gravity.GetVectorFromForce(matter2, matter1.position, force);
                        Gravity.forceHash.Add((matter1, matter2), force);
                    }


                }

            }
        }

        public void UpdateMatter()
        {


            foreach (Matter matter in matterObjects)
            {


                //updates position
                Vector3 oldPosition = matter.position;

                matter.position += matter.velocity;

                //changes object occupying space if it has moved sufficiently
                if ((int)matter.position.X != (int)oldPosition.X || (int)matter.position.Y != (int)oldPosition.Y)
                {
                    matterObjectsSpace[new VectorInt(oldPosition)].Remove(matter);

                    if (matterObjectsSpace.TryGetValue(new VectorInt(matter.position), out List<Matter>? contents2))
                    {
                        contents2.Add(matter);
                    }
                    else { matterObjectsSpace.Add(new VectorInt(matter.position), new List<Matter>() { matter }); }
                }

                Collision.ColllisionProcess(matter);

                UpdateGravity(matter);
            }

            Gravity.forceHash.Clear();

        }



        public void DrawMatter()
        {
            foreach (var matter in matterObjects)
            {
                DrawSpace.DrawCharacter((int)matter.position.X, (int)matter.position.Y, (char)(matterObjectsSpace[new VectorInt(matter.position)].Count + '0'));
            }
        }

    }
}