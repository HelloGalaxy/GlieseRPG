using UnityEngine;
using System.Collections;

namespace Game.Actor
{
    public class Wolf : MonoBehaviour
    {
        public WolfState state = WolfState.Idle;

        public int hp = 100;
        public float miss_rate = 0.2f;
        public float redTime = 1f;

        private float attackTimer = 0f;
        private Color normalColor;


        public string animIdleKey;
        public string animWalkKey;
        public string animDeathKey;

        private string animNow;

        public float speed = 1;
        public float timeChangeState = 1f;
        private float timer = 0f;

        private Animation animPlayer;
        private CharacterController controller;
        private bool isAttack;

        private Renderer meRender;

        private void Awake()
        {
            animPlayer = GetComponent<Animation>();
            controller = GetComponent<CharacterController>();
            animNow = animIdleKey;
            meRender = GetComponentInChildren<Renderer>();
            normalColor = meRender.material.color;
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
                    if (timer >= timeChangeState)
                    {
                        timer = 0;
                        RandomState();
                    }
                    break;
            }

            if (Input.GetKeyDown(KeyCode.A))
                TakeDamage(20);
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


        public void TakeDamage(int attack)
        {
            float p = Random.Range(0f, 1f);

            if (p < miss_rate)
            {
                Debug.LogWarning("Oops, miss attack: " + attack);
            }
            else
            {
                this.hp -= attack;
                StartCoroutine(ShowBodyRed());
                if (hp < 0)
                {
                    state = WolfState.Death;
                    Destroy(this.gameObject, 2);
                }
            }
        }

        private IEnumerator ShowBodyRed()
        {
            meRender.material.color = Color.red;
            yield return new WaitForSeconds(1f);
            meRender.material.color = normalColor;
        }
    }
}