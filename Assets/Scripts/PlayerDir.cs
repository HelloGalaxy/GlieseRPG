using UnityEngine;
using System.Collections;


namespace Game.Actor
{
    public class PlayerDir : MonoBehaviour
    {
        public GameObject effectClickPrefab;
        public Vector3 iconOffset = new Vector3(0f, 1f, 0f);

        private bool isMoving = false;

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;
                var isCollided = Physics.Raycast(ray, out hitInfo);
                if (isCollided && hitInfo.collider.tag == Tags.Ground)
                {
                    ShowClickEffect(hitInfo.point);
                    isMoving = true;
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                isMoving = false;
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;
                bool isCollider = Physics.Raycast(ray, out hitInfo);
                if (isCollider && hitInfo.collider.tag == Tags.Ground)
                {
                    LookAtTarget(hitInfo.point);
                }
            }

            if (isMoving)
            {
               
            }
        }

        void ShowClickEffect(Vector3 hitPoint)
        {
            hitPoint = hitPoint + iconOffset;
            var newGameObject = GameObject.Instantiate(effectClickPrefab, hitPoint, Quaternion.identity);
        }

        void LookAtTarget(Vector3 hitPoint)
        {
            //targetPosition = hitPoint;
            //targetPosition = new Vector3(targetPosition.x, transform.position.y, targetPosition.z);
            this.transform.LookAt(hitPoint);
        }
    }
}