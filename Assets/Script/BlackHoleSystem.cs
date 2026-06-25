using System.Collections;
using UnityEngine;

public class BlackHoleSystem : MonoBehaviour
{
    [SerializeField] private float destroyTime = 5.0f;
    [SerializeField] private LayerMask groundLayer;
    void Start()
    {
        StartCoroutine(DestroyTimer());
    }
    private IEnumerator DestroyTimer()
    {
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject);
        //if (collision.gameObject.tag != "Player" || collision.gameObject.tag != "PlayerStompCheck") 
        if (collision.gameObject.tag == "Enemy") 
        {
            //Debug.Log(collision.gameObject.tag);
            //Rigidbody2D rb = collision.gameObject.GetComponentInParent<Rigidbody2D>();
            //rb.gravityScale = 0.0f;
        }
        EnemyPatrol enemyPatrol=collision.GetComponentInParent<EnemyPatrol>();
        if (enemyPatrol != null) enemyPatrol.isBlackHoleStay = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //if (collision.gameObject.tag != "Player" || collision.gameObject.tag != "PlayerStompCheck") 
        if (collision.gameObject.tag == "Enemy")
        {
            //Rigidbody2D rb = collision.gameObject.GetComponentInParent<Rigidbody2D>();
            //rb.gravityScale = 1.0f;
        }
        EnemyPatrol enemyPatrol = collision.gameObject.GetComponentInParent<EnemyPatrol>();
        if (enemyPatrol != null)
        {
            enemyPatrol.isBlackHoleStay = false;
            if (collision.gameObject.GetComponent<Collider2D>().IsTouchingLayers(groundLayer)) enemyPatrol.isStart = true;
        }
    }
}
