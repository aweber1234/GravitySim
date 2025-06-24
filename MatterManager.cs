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
                

                Gravity.UpdateGravity(matter);
            }

            Gravity.forceAppliedHash.Clear();
        }



        public void DrawMatter()
        {
            for (int x = DrawConsole.cameraPosition.X; x < DrawConsole.cameraPosition.X + DrawConsole.drawWidth; x++)
            {
                for (int y = DrawConsole.cameraPosition.Y; y < DrawConsole.cameraPosition.Y + DrawConsole.drawHeight; y++)
                {
                    if (matterObjectsSpace.TryGetValue(new VectorInt(x, y, 0), out List<Matter>? items) && items.Any())
                    {
                        DrawConsole.DrawCharacter(x - DrawConsole.cameraPosition.X, y - DrawConsole.cameraPosition.Y, (char)(items.Count + '0'));
                    }
                }
            }
            

        }

    }
}