using UnityEngine;

namespace Counters
{
    public class StoveCounterVisual : MonoBehaviour
    {
        [SerializeField] private GameObject stoveOnGameObject;
        [SerializeField] private GameObject particlesGameObject;

        public void On()
        {
            stoveOnGameObject.SetActive(true);
            particlesGameObject.SetActive(true);
        }

        public void Off()
        {
            stoveOnGameObject.SetActive(false);
            particlesGameObject.SetActive(false);
        }
    }
}
