using UnityEngine;

public sealed class PlayerController : MonoBehaviour
{
    [Space(10)]
    [Header("Particles")]
    [SerializeField] private ParticleSystem explosionParticle;
    [SerializeField] private ParticleSystem dirtSplatter;

    [Space(20)]
    [Header("AudioClips")]
    [SerializeField] private AudioClip crashSound;
    [SerializeField] private AudioClip jumpSound;

    private AudioSource audioPlayer;
    private Rigidbody rb;
    private Animator anim;
    private Death death;

    private const int forceAmount = 700;
    private bool isOnGround = true;
    private bool isGameOver = false;

    private void Awake()
    {
        audioPlayer = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        death = Death.Instance;
    }

    private void Update() => CheckOrJump();

    private void OnCollisionEnter(Collision collision)
    {
        Collider other = collision.collider;
        if (!isGameOver)
        {
            if (other.CompareTag("Ground"))
            {
                isOnGround = true;
                dirtSplatter.Play();
            }
            else if (other.CompareTag("Obstacle"))
            {
                isGameOver = true;
                audioPlayer.PlayOneShot(crashSound, 0.7f);
                AnimateDeath();
                dirtSplatter.Stop();
                explosionParticle.Play();
                death.NotifyObservers();
            }
        }
    }

    private void CheckOrJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !isGameOver)
        {
            dirtSplatter.Stop();
            Jump();
            isOnGround = !isOnGround;
        }
    }

    private void Jump()
    {
        anim.SetTrigger("Jump_trig");
        rb.AddForce(Vector3.up * forceAmount, ForceMode.Impulse);
        audioPlayer.PlayOneShot(jumpSound, 1);
    }

    private void AnimateDeath()
    {
        anim.SetBool("Death_b", true);
        anim.SetInteger("DeathType_int", 1);
    }
}