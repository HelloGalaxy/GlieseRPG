using UnityEngine;
using System.Collections;

namespace Game.Actor
{
    public class Wolf : MonoBehaviour
    {
        public WolfState state = WolfState.Idle;

        public string animIdleKey;
        public string animWalkKey;
        public string animDeathKey;

        private string animNow;

        public float speed = 1;
        public float timeChangeState = 1f;
        private float timer = 0f;

        private Animation animPlayer;
        private CharacterController controller;

        private void Awake()
        {
            animPlayer = GetComponent<Animation>();
            controller = GetComponent<CharacterController>();
            animNow = animIdleKey;
        }

        private void Update()
        {
            switch (state)
            {
                case WolfState.Attack:
                    break;
                case WolfState.Death:
                    animPlayer.CrossFade(animDeathKey);
                    break;
                default:
                    animPlayer.CrossFade(animNow);
                    if (animNow == animWalkKey)
                        controller.SimpleMove(transform.forward * speed);
                    timer += Time.deltaTime;
                    if(timer >= timeChangeState)
                    {
                        timer = 0;
                        RandomState();
                    }
                    break;
            }
        }

        private void RandomState()
        {
            int value = Random.Range(0, 2);

            switch (value)
            {
                case 0:
                    animNow = animIdleKey;
                    break;
                case 1:
                case 2:
                    if (animNow != animWalkKey)
                        transform.Rotate(transform.up * Random.Range(0, 360));
                    animNow = animWalkKey;
                    break;
            }
        }

    }
}