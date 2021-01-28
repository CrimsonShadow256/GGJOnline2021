using UnityEngine;

namespace GGJ_Online
{
    [RequireComponent(typeof(Collider))]
    public class Collectable : MonoBehaviour
    {
        const int playerLayer = 8;

        [Header("Must mark as trigger to Function")]
        [SerializeField] float value;
        [SerializeField] GameObject collectionEffect;

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.layer == playerLayer)
            {
                ScoreManager.AddScore(value);

                if (collectionEffect)
                {
                    GameObject.Instantiate(collectionEffect, transform.position, transform.rotation);
                }

                GameObject.Destroy(this.gameObject);
            }
        }
    }
}
