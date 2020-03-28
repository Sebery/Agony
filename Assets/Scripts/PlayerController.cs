using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Components")]
    private Animator anim;
    private Rigidbody2D rbPlayer;
    public float speed;
    private float xPos;
    private float yPos;
    private bool moving;
    private float lastMoveX;
    private float lastMoveY;



    private void Awake() {
        rbPlayer = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate() {
        PlayerMovement();
    }

    public void PlayerMovement() {
        xPos = Input.GetAxisRaw("Horizontal");
        yPos = Input.GetAxisRaw("Vertical");

        anim.SetFloat("xPos", xPos);

        if (xPos != 0 || yPos != 0) {
            if (!moving) {
                moving = true;
                anim.SetBool("Moving", moving);
                lastMoveX = xPos;
                lastMoveY = yPos;
                anim.SetFloat("LastMoveX", lastMoveX);
            }

        } else {
            if (moving) {
                moving = false;
                anim.SetBool("Moving", moving);
            }

        }

        rbPlayer.velocity = new Vector2(xPos, yPos) * speed;
    }


}
