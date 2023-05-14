using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private MovementHandler move;
    [SerializeField] private JumpHandler jump;
    [SerializeField] private GroundedCheck ground;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer sprite;

    private bool grounded, acting, recalling;
    private float recallTimestamp;
    private bool hasKnife = true;
    [Header("Gameplay")]
    [SerializeField] private float recallScalar;
    [SerializeField] private AnimationCurve recallCurve;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        bool moving = false;

        grounded = ground.CheckGrounded();


        if (InputHandler.Instance.move.pressed) {
            move.StartAcceleration(InputHandler.Instance.dir);
            moving = true;
        } else if (InputHandler.Instance.move.down) {
            move.UpdateMovement(InputHandler.Instance.dir);
            moving = true;
            if (InputHandler.Instance.dir != 0)
                sprite.flipX = InputHandler.Instance.dir < 0;
        } else if (InputHandler.Instance.move.released) {
            move.StartDeceleration();
        }

        if ((grounded || recalling) && InputHandler.Instance.jump.pressed) {
            jump.StartJump();
            animator.SetTrigger("jump");
            if (recalling) {

                EndAction();
            }
        } else if (InputHandler.Instance.primary.pressed) { // attack and grapple
            StartAction();
            if (hasKnife) {
                animator.SetTrigger("attack");
            } else {

            }
        } else if (InputHandler.Instance.secondary.pressed) { // throw and recall
            StartAction();
            if (hasKnife) {
                animator.SetTrigger("throw");
                hasKnife = false;
            } else {
                animator.SetTrigger("recall");
                recalling = true;
                recallTimestamp = Time.time;
                if (!grounded)
                    move.setSpeedScalar(0.5f);
                jump.ForceLanding();
                jump.DisableGravity();
                hasKnife = true;
            }
        }

        if (recalling && Time.time - recallTimestamp < 0.2f) {
            jump.ForceVelocity(recallScalar * recallCurve.Evaluate(Time.time - recallTimestamp));
        } else if (recalling) {
            jump.ResetGravity();
        }


        animator.SetBool("running", moving);
        //animator.SetBool("sprinting", moving);
        animator.SetBool("grounded", grounded && !jump.IsJumping());
     }

    private void StartAction() {
        acting = true;
        animator.SetBool("acting", acting);
    }

    private void EndAction() {
        acting = false;
        recalling = false;
        animator.SetBool("acting", acting);
        move.setSpeedScalar(1);
        jump.ResetGravity();
    }
}
