using UnityEngine;
using System.Collections;

namespace Game.Actor
{
    public class PlayerAnimation : MonoBehaviour
    {
        public string prefix = "";
        private PlayerMove move;

        private void Awake()
        {
            move = GetComponent<PlayerMove>();
        }

        private void LateUpdate()
        {
            switch (move.ActorState)
            {
                case ActorState.Idle:
                    PlayAnim("Idle");
                    break;
                case ActorState.Walk:
                    PlayAnim("Run");
                    break;
            }
        }

        private void PlayAnim(string animName)
        {
            GetComponent<Animation>().CrossFade(prefix + animName);
        }
    }
}
