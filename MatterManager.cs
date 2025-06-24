using System;
using System.Data;
using System.Net.Http.Headers;
using System.Numerics;
using System.Security.Cryptography;
using Microsoft.VisualBasic;

namespace GravitySim
{
    public class MatterManager
    {
        public List<Matter> matterObjects = [];
        public Dictionary<VectorInt, List<Matter>> matterObjectsSpace = [];      
       
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

                if (GameRoot.collisionOn)
                { Collision.ColllisionProcess(matter); }
                

                UpdateGravity(matter);
            }

            Gravity.forceHash.Clear();
        }



        public void DrawMatter()
        {
            for (int x = DrawConsole.cameraPosition.X; x < DrawConsole.cameraPosition.X + DrawConsole.drawWidth; x++)
            {
                for (int y = DrawConsole.cameraPosition.Y; y < DrawConsole.cameraPosition.Y + DrawConsole.drawHeight; y++)
                {
                    if (matterObjectsSpace.TryGetValue(new VectorInt(x, y, 0), out List<Matter> items) && items.Any())
                    {
                        DrawConsole.DrawCharacter(x - DrawConsole.cameraPosition.X, y - DrawConsole.cameraPosition.Y, (char)(items.Count + '0'));
                    }
                }
            }
            

        }

    }
}