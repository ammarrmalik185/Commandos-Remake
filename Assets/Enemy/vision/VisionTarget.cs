using System.Collections.Generic;
using UnityEngine;

namespace Enemy.vision
{
    /// <summary>
    /// Put this script on your main character to make it detectable by enemies
    /// </summary>

    public class VisionTarget : MonoBehaviour
    {
        public bool visible = true;

        private static readonly List<VisionTarget> target_list = new List<VisionTarget>();

        private void Awake()
        {
            target_list.Add(this);
        }

        private void OnDestroy()
        {
            target_list.Remove(this);
        }

        public bool CanBeSeen()
        {
            return visible;
        }

        public static List<VisionTarget> GetAll()
        {
            return target_list;
        }
    }

}