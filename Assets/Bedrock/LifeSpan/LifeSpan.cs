using UnityEngine;
using System.Collections;

namespace Bedrock.LifeSpan
{
    public class LifeSpan : MonoBehaviour
    {
        [SerializeField, Tooltip("How many seconds this GameObject lives before dying, 0 = forever.")]
        private float initialLifeSpan = 0;

        private IEnumerator destroyAfterSecondsCoroutine = null;

        private void Start()
        {
            SetLifeSpan(initialLifeSpan);
        }

        /// <summary>
        /// Set the life span of this GameObject. When it expires, the GameObject will be destroyed.
        /// </summary>
        /// <param name="lifeSpan">The life span, in seconds, before this GameObject is destroyed. If 0, the timer is cleared and the GameObject will not be destroyed.</param>
        public void SetLifeSpan(float lifeSpan)
        {
            if (destroyAfterSecondsCoroutine != null)
            {
                StopCoroutine(destroyAfterSecondsCoroutine);
            }

            if (lifeSpan > 0f)
            {
                destroyAfterSecondsCoroutine = DestroyAfterSeconds(lifeSpan);

                StartCoroutine(destroyAfterSecondsCoroutine);
            }
        }

        private IEnumerator DestroyAfterSeconds(float seconds)
        {
            yield return new WaitForSeconds(seconds);

            Destroy(gameObject);
        }
    }
}