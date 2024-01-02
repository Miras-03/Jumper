using UnityEngine;

public sealed class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private Animator anim;
    private Death death;

    private const int forceAmount = 700;
    private bool isOnGround = true;
    private bool isGameOver = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        death = Death.Instance;
    }

    private void Update() => CheckOrJump();

    private void OnCollisionEnter(Collision collision)
    {
        Collider other = collision.collider;
        if (other.CompareTag("Ground"))
            isOnGround = true;
        else if (other.CompareTag("Obstacle"))
        {
            isGameOver = true;
            AnimateDeath();
            death.NotifyObservers();
        }
    }

    private void CheckOrJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !isGameOver)
        {
            Jump();
            isOnGround = !isOnGround;
        }
    }

    private void Jump()
    {
        anim.SetTrigger("Jump_trig");
        rb.AddForce(Vector3.up * forceAmount, ForceMode.Impulse);
    }

    private void AnimateDeath()
    {
        anim.SetBool("Death_b", true);
        anim.SetInteger("DeathType_int", 1);
    }
}