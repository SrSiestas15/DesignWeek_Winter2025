using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    RaycastHit2D hitInfo;
    Vector3 hitDirection;
    Vector3 oppositeHitDirection;

    RaycastHit2D playerHitInfo;
    [SerializeField] LayerMask stopperLayer;
    [SerializeField] LayerMask playerLayer;

    public bool hasChip;

    Vector3 obstructionDirection;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


            if(gameObject.layer == 6)
            {
                Debug.DrawLine(transform.position + obstructionDirection, transform.position + (obstructionDirection * .25f) );

                if(!Physics2D.Linecast(transform.position - obstructionDirection, transform.position - obstructionDirection, playerLayer) && !Physics2D.Linecast(transform.position - obstructionDirection, transform.position - obstructionDirection, stopperLayer))
                {
                    gameObject.layer = 0;
                }
            }
            else
            {

                for(int i = 0; i < 4; i++)
                {
                    if(i == 0)
                    {
                        hitDirection = Vector3.up;
                    }
                    else if(i == 1)
                    {
                        hitDirection = Vector3.right;

                    }
                    else if(i == 2)
                    {
                        hitDirection = Vector3.down;

                    }
                    else if(i == 3)
                    {
                        hitDirection = Vector3.left;

                    }
                
                    hitInfo = Physics2D.Linecast(transform.position + hitDirection, transform.position + hitDirection);
                    Debug.DrawLine(transform.position + hitDirection, transform.position + hitDirection);


                    if (hitInfo.collider != null && (hitInfo.collider.gameObject.tag == "Bot" || hitInfo.collider.gameObject.tag == "Box"))
                    {
                        if (hitInfo.collider != null && hitInfo.collider.gameObject.tag == "Box")
                        {
                            hitInfo = Physics2D.Linecast(transform.position - hitDirection, transform.position - hitDirection);
                            if (hitInfo.collider != null && hitInfo.collider.gameObject.layer == 6)
                            {
                                obstructionDirection = -hitDirection;
                                gameObject.layer = 6;
                                //Debug.Log(i);

                                oppositeHitDirection.x = hitDirection.y;
                                oppositeHitDirection.y = hitDirection.x;
                                if(Physics2D.Linecast(transform.position - oppositeHitDirection, transform.position - oppositeHitDirection, playerLayer) || Physics2D.Linecast(transform.position + oppositeHitDirection, transform.position + oppositeHitDirection, playerLayer))
                                {
                                    gameObject.layer = 0;
                                }
                            }
                        }

                        else if(hitInfo.collider.gameObject.tag == "Bot")
                        {
                            hitInfo = Physics2D.Linecast(transform.position - hitDirection, transform.position - hitDirection);
                            if (hitInfo.collider != null && hitInfo.collider.gameObject.layer == 6)
                            {

                                obstructionDirection = -hitDirection;
                                gameObject.layer = 6;
                                //Debug.Log(i);
                            }
                            else gameObject.layer = 0;

                        }

                    }
            }
        }

        //hitInfo = Physics2D.Linecast(transform.position, hitDirection);
    }
}
