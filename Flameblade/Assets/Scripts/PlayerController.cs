using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator playerAnimator;
    private Rigidbody2D playerRB;
    [SerializeField] private float jumpForce;
    private float gravityModifier = 2;
    private bool isOnGround = true;
    private float moveSpeed = 0.075f;
    private float bound = 9;

    private int health = 100;
    [SerializeField] private Transform attackPoint1;
    private float attackRange1 = 0.75f;
    [SerializeField] private LayerMask enemyLayers;
    private float attackRate = 2f;
    private float nextAttackTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerRB = GetComponent<Rigidbody2D>();
        Physics2D.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
            playerAnimator.SetBool("isRunning", true);
            if (transform.position.x < bound)
            {
                transform.position = new Vector3(transform.position.x + moveSpeed, transform.position.y, transform.position.z);
            }
        } else if (Input.GetKey(KeyCode.A))
        {
            transform.rotation = new Quaternion(0, 180, 0, 0);
            playerAnimator.SetBool("isRunning", true);
            if (transform.position.x > -bound)
            {
                transform.position = new Vector3(transform.position.x - moveSpeed, transform.position.y, transform.position.z);
            }
        } else
        {
            playerAnimator.SetBool("isRunning", false);
        }
        if (Input.GetKeyDown(KeyCode.W) && isOnGround)
        {
            playerAnimator.SetTrigger("jump");
            playerRB.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            moveSpeed /= 2f;
            isOnGround = false;
        } 
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                playerAnimator.SetTrigger("atk1");
                Attack(attackPoint1, attackRange1, 5);
                nextAttackTime = Time.time + 1f / attackRate;
            }
            else if (Input.GetKeyDown(KeyCode.K))
            {
                playerAnimator.SetTrigger("atk2");
                Attack(attackPoint1, attackRange1, 10);
                nextAttackTime = Time.time + 1f / attackRate;
            }
            else if (Input.GetKeyDown(KeyCode.L))
            {
                playerAnimator.SetTrigger("atk3");
                Attack(attackPoint1, attackRange1, 5);
                nextAttackTime = Time.time + 1f / attackRate;
            }
            else if (Input.GetKeyDown(KeyCode.Semicolon))
            {
                playerAnimator.SetTrigger("atk4");
                Attack(attackPoint1, attackRange1, 10);
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    void Attack(Transform attackPoint, float attackRange, int damage)
    {
        // Find enemies in range and deal damage
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyController>().TakeDamage(damage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint1.position, attackRange1);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            if (moveSpeed < 0.075f)
            {
                moveSpeed *= 2;
            }
        }
    }
}
