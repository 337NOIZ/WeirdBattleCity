
using UnityEngine;

namespace FadeScreen
{
    public class PrimaryFadeScreen : MonoBehaviour
    {
        public static PrimaryFadeScreen instance { get; private set; }

        [Space] public FadeScreen fadeScreen = null;

        private void Awake()
        {
            instance = this;
        }
    }
}