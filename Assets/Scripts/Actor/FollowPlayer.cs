using UnityEngine;
using System.Collections;

namespace Game.Actor
{
    public class FollowPlayer : MonoBehaviour
    {
        private Transform player;
        private Vector3 offsetPosition;

        public float maxZoomDistance = 20;
        public float minZoomDistance = 8;
        public float zoomSpeed = 1;

        void Awake()
        {
            player = GameObject.FindGameObjectWithTag(Tags.Player).transform;
            offsetPosition = transform.position - player.position;
        }

        void Update()
        {
            transform.position = offsetPosition + player.position;

            ZoomInOut();
        }

        void ZoomInOut()
        {
            var delta = Input.GetAxis("Mouse ScrollWheel");
            var distance = offsetPosition.magnitude;
            distance += delta;
            distance = Mathf.Clamp(distance, minZoomDistance, maxZoomDistance);
            offsetPosition = offsetPosition.normalized * distance;
        }
    }
}