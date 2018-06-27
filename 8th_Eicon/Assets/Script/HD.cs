using UnityEngine;

namespace HD
{
    public static class HD
    {
        #region Color
        public static Color ChangeAlpha(this Color color, float alpha)
        {
            Color newColor = new Color(color.r, color.g, color.b, alpha);

            return newColor;
        }
        #endregion

        public static Quaternion ChangeY(this Quaternion rotation, float eulerY)
        {
            Quaternion newQuaternion = Quaternion.Euler(rotation.eulerAngles.x,
                                                        eulerY,
                                                        rotation.eulerAngles.z);

            return newQuaternion;
        }
    }
}