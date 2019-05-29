using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public static class PoseConverter
    {
        // Waardes zijn aangepast om coördinaten stelsel van VSMS te matchen met het coördinaten stelsel van Unity!!!
        public static UnityEngine.Vector3 ToUnityVector3(F1Tenth.Vector3 vector3)
        {
            UnityEngine.Vector3 _vector3 = new UnityEngine.Vector3
            {
                x = -vector3.Y,
                y = vector3.Z,
                z = vector3.X
            };

            return _vector3;
        }

        public static UnityEngine.Quaternion ToUnityQuaternion(F1Tenth.Quaternion quaternion)
        {
            UnityEngine.Quaternion _quaternion = new UnityEngine.Quaternion
            {
                w = quaternion.W,
                x = quaternion.Y,
                y = quaternion.Z,
                z = quaternion.X
            };
            return _quaternion;
        }


        // Deprecated and is not used anymore
        public static UnityEngine.Vector3 StringToVector3(string stringVector)
        {
            string sVector = stringVector;
            // Remove spaces
            if (sVector.Contains(" "))
            {
                sVector.Replace(" ", string.Empty);
            }
            // Remove the parentheses
            if (sVector.StartsWith("(") && sVector.EndsWith(")"))
            {
                sVector = sVector.Substring(1, sVector.Length - 2);
            }

            // split the items
            string[] sArray = sVector.Split(',');

            // store as a Vector3
            UnityEngine.Vector3 result = new UnityEngine.Vector3(
            float.Parse(sArray[0]),
            float.Parse(sArray[1]),
            float.Parse(sArray[2]));
            return result;
        }
    }
}
