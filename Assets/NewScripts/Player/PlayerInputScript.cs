using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerScripts
{
    public class PlayerInputScript : MonoBehaviour
    {
        #region PLAYER SCRIPT
        [SerializeField] internal PlayerScript playerScript;
        #endregion

        public void GetInput()
        {
            playerScript._deplacement = Vector2.zero;

            playerScript._deplacement.x = Input.GetAxisRaw("Horizontal");
            playerScript._deplacement.y = Input.GetAxisRaw("Vertical");

            if((playerScript._action = Input.GetKeyDown(KeyCode.Space)) && playerScript._nextIneractTime)
            {
                    playerScript._nextIneractTime = false;
                    StartCoroutine(CoNextInput());    
            }

            playerScript._isSprinting = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);


            /*
            if (Input.GetButtonDown("attack") && currentState != PlayerState.attack && currentState != PlayerState.stagger)
                StartCoroutine(AttackCo());
            else
            if (currentState == PlayerState.walk || currentState == PlayerState.idle)
                UpdateAnimationAndMove();
            */
        }

        IEnumerator CoNextInput()
        {
            yield return new WaitForSeconds(playerScript._minInteractTime);
            if (playerScript._waitForInput)
                playerScript._waitForInput = false;
            playerScript._nextIneractTime = true;
        }
    }
}
