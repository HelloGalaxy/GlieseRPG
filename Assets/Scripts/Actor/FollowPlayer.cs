using UnityEngine;
using System.Collections;

namespace Game.Actor
{
    public class FollowPlayer : MonoBehaviour
    {
        private Transform player;
        private Vector3 offsetPosition;

        void Awake()
        {
            player  = GameObject.FindGameObjectWithTag(Tags.Player).transform;
            offsetPosition = transform.position - player.position;
        }

        void Update()
        {
            transform.position = offsetPosition + player.position;
        }
    }
}