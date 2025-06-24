using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GravitySim
{
    public static class IDtool
    {
        static int currentID = 0;





        public static int GetID()
        {
            int idReturn = currentID;

            currentID++;

            return idReturn;
        }
    }
}
