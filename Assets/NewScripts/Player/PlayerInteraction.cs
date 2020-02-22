using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerScripts
{
    public class PlayerInteraction : MonoBehaviour
    {
        [SerializeField] PlayerScript playerScript;
        public GameObject receivedItem;
        public SpriteRenderer render;

        private void Start()
        {
            render = receivedItem.GetComponent<SpriteRenderer>();
            receivedItem.SetActive(false);
        }

        internal void GiveItem(Item item)
        {
            ShowItem(item);
            playerScript._ui.ShowMessage(item.itemDescription);
            playerScript._waitForInput = true;

            playerScript.SetState(PlayerState.interact);
            if (item.isKey)
            {
                playerScript._inventory.numberOfKeys++;
            }
            else
            {
                playerScript._inventory.currentItem = item;
                // ajouter l'item dans une liste
            }
        }
        
        internal void ClearItem()
        {
            playerScript._anim.SetBool("receiveItem", false);
            receivedItem.SetActive(false);
            playerScript._ui.Clean();
            playerScript.SetState(PlayerState.idle);
            playerScript._inventory.currentItem = null;
        }

        private void ShowItem(Item item)
        {
            playerScript._anim.SetBool("receiveItem", true);
            render.sprite = item.itemSprite;
            receivedItem.SetActive(true);
        }

        internal void Attack()
        {
            StartCoroutine(AttackCo());
            Collider2D[] enemys = Physics2D.OverlapCircleAll((Vector2)(playerScript._hitPoint.transform.position), playerScript._attackRadius, playerScript._EnemyLayerMask);
            foreach (Collider2D e in enemys)
            {
                if (e.GetComponent<Ennemi>() is IEnnemy)
                    e.GetComponent<Ennemi>().Knock(playerScript._rb, 1, 1);
            }

            Collider2D[] breakables = Physics2D.OverlapCircleAll(playerScript._hitPoint.transform.position, playerScript._attackRadius, playerScript._BreakableLayerMask);
            foreach (Collider2D b in breakables)
            {
                b.GetComponent<Breakable>().Smash();
            }
            
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(playerScript._hitPoint.transform.position, playerScript._attackRadius);
        }

        private IEnumerator AttackCo()
        {
            playerScript._anim.SetBool("isAttacking", true);
            playerScript.SetState(PlayerState.attack);
            yield return null; // 1 frame
            playerScript._anim.SetBool("isAttacking", false);
            yield return new WaitForSeconds(.33f);
            if (playerScript._actualState != PlayerState.interact)
                playerScript.SetState(PlayerState.idle);


        }
    }
}
