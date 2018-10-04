using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System.Linq;

namespace Scripts.Refactor
{
    public class Sort
    {

        public static SortedList<float, float> sorto(List<float> wyniki, float[] angles)
        {

            SortedList<float, float> lista1 = new SortedList<float, float>();

            int i = 0;

            foreach (float a in wyniki)
            {
                lista1.Add(angles[i], a);
                i++;
            }

            return lista1;
        }

    }
}
