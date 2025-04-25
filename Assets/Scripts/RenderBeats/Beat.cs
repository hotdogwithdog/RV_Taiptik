using System.Collections;
using UnityEngine;

namespace RenderBeats
{
    public class Beat : MonoBehaviour
    {
        private Vector3 _originPosition;
        private Vector3 _targetPosition;
        private float _timeToReach;
        private float _startTime;

        public void GoTarget(Vector3 originPosition, Vector3 targetPosition, float timeToReach)
        {
            _originPosition = originPosition;
            _targetPosition = targetPosition;
            _timeToReach = timeToReach;

            _startTime = Time.time;
            StartCoroutine(Move());
        }

        private IEnumerator Move()
        {
            float elapsedTime = 0;

            while (elapsedTime < _timeToReach)
            {
                transform.position = Vector3.Lerp(_originPosition, _targetPosition, elapsedTime / _timeToReach);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Logic after move
            Destroy(this.gameObject);
        }

        private void OnDestroy()
        {
            Debug.Log($"Object: {this} destroyed");
        }
    }
}

