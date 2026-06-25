using UnityEngine;

public class IsGroundChecker : MonoBehaviour
{
    [SerializeField] private PlayerContoroller player;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            player.isGrounded = true;
            player.isJump = false;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            player.isGrounded = false;
        }
    }
}
