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
        public float rotateSpeed = 2;

        private bool isRotating;

        void Awake()
        {
            player = GameObject.FindGameObjectWithTag(Tags.Player).transform;
            transform.LookAt(player.position);
            offsetPosition = transform.position - player.position;
        }

        void Update()
        {
            transform.position = offsetPosition + player.position;

            RotateView();
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


        void RotateView()
        {
            if (Input.GetMouseButtonDown(1))
            {
                isRotating = true;
            }

            if (Input.GetMouseButtonUp(1))
            {
                isRotating = false;
            }

            if (isRotating)
            {
                transform.RotateAround(player.position, player.up, rotateSpeed * Input.GetAxis("Mouse X"));

                Vector3 originalPos = transform.position;
                Quaternion originalRotation = transform.rotation;

                transform.RotateAround(player.position, transform.right, -rotateSpeed * Input.GetAxis("Mouse Y"));//影响的属性有两个 一个是position 一个是rotation
                float x = transform.eulerAngles.x;
                if (x < 10 || x > 80)
                {
                    transform.position = originalPos;
                    transform.rotation = originalRotation;
                }

                offsetPosition = transform.position - player.position;
            }
        }
    }
}