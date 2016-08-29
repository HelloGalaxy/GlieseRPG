using UnityEngine;
using System.Collections;

namespace Game.Actor
{
    public class PlayerMove : MonoBehaviour
    {
        public float speed = 5f;
        public bool isMoving = false;

        private ActorState actorState = ActorState.Idle;
        private CharacterController controller;
        private PlayerDir dir;

        private float moveThreshold = 0.1f;

        public ActorState ActorState
        {
            get { return actorState; }
            set
            {
                actorState = value;
                if (actorState == ActorState.Moving)
                {
                    controller.SimpleMove(transform.forward * speed);
                    isMoving = true;
                }
                else
                    isMoving = false;
            }
        }

        private void Awake()
        {
            controller = GetComponent<CharacterController>();
            dir = GetComponent<PlayerDir>();
        }

        private void Update()
        {
            float distance = Vector3.Distance(dir.targetPosition, transform.position);
            ActorState = distance > moveThreshold ? ActorState.Moving : ActorState.Idle;
        }
    }
}
