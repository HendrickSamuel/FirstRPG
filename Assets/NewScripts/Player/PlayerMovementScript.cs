using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerScripts
{
    public class PlayerMovementScript : MonoBehaviour
    {
        #region PLAYER SCRIPT
        [SerializeField] internal PlayerScript playerScript;
        #endregion

        internal void MovePlayer()
        {
            playerScript._anim.SetBool("isWalking", true);

            playerScript._anim.SetFloat("MoveX", playerScript._deplacement.x);
            playerScript._anim.SetFloat("MoveY", playerScript._deplacement.y);

            playerScript._hitPoint.transform.localPosition = playerScript._deplacement * playerScript._hitPointDistance;

            playerScript.SetState(PlayerState.walk);

            playerScript._deplacement.Normalize();

            if(!playerScript._isSprinting)
                playerScript._rb.MovePosition((Vector2)transform.position + playerScript._deplacement * playerScript._speed * Time.deltaTime);
            else
                playerScript._rb.MovePosition((Vector2)transform.position + playerScript._deplacement * playerScript._sprintSpeed * Time.deltaTime);
        }

        internal void IdlePlayer()
        {
            playerScript._anim.SetBool("isWalking", false);
            playerScript.SetState(PlayerState.idle);
        }

    }

    
}