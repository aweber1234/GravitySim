using Microsoft.VisualBasic;
using System;
using System.Data;
using System.Diagnostics.Metrics;
using System.Net.Http.Headers;
using System.Numerics;
using System.Security.Cryptography;
using static GravitySim.Gravity;

namespace GravitySim
{
    public class MatterManager
    {
        public List<Matter> matterObjects = [];
        public Dictionary<VectorInt, List<Matter>> matterObjectsSpace = [];
        public int[] inDrawArray = new int[DrawConsole.drawWidth * DrawConsole.drawHeight];


        public void Add(Matter matter)
        {
            matterObjects.Add(matter);
        }

        public void UpdateMatter()
        {
            inDrawArray = new int[DrawConsole.drawWidth * DrawConsole.drawHeight];
            int minDrawX = DrawConsole.cameraPosition.X;
            int maxDrawX = minDrawX + DrawConsole.drawWidth;
            int minDrawY = DrawConsole.cameraPosition.Y;
            int maxDrawY = minDrawY + DrawConsole.drawHeight;


            foreach (Matter matter1 in matterObjects)
            {
                matter1.position += matter1.velocity;

                //changes object occupying space if it has moved sufficiently
                if ((int)matter1.position.X > minDrawX && (int)matter1.position.X < maxDrawX &&
                    (int)matter1.position.Y > minDrawY && (int)matter1.position.Y < maxDrawY)
                {
                    int i = DrawConsole.ArrayIndex((int)matter1.position.X - minDrawX, (int)matter1.position.Y - minDrawY);
                    inDrawArray[i]++;
                }

            }



            foreach (Matter matter1 in matterObjects)
            {
                foreach (Matter matter2 in matterObjects)
                {
                    if (matter1 != matter2)
                    {
                        if (GameRoot.collisionOn)
                        { Collision.ColllisionProcess(matter1, matter2); }

                        UpdateGravity(matter1, matter2);
                    }
                }
            }
        }



        public void DrawMatter()
        {
            for (int i = 0; i < inDrawArray.Length; i++)
            {
                int count = inDrawArray[i];
                if (count > 0)
                {
                    DrawConsole.drawArray[i] = (char)(count + '0');
                }
            }            



        }

    }
}