using UnityEngine;

namespace Project.Runtime._Scripts.Gameplay.Player
{
    public class PlayerController : MonoBehaviour
    {
        
        [SerializeField] private Transform dollyCart;
        [SerializeField] private float lerpSpeed = 20f;
        
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update() {
            Transform t = transform;
            t.position = Vector3.Lerp(t.position, dollyCart.position, Time.deltaTime * lerpSpeed);
            t.rotation = Quaternion.Lerp(t.rotation, dollyCart.rotation, Time.deltaTime * lerpSpeed);
        }
    }
}
