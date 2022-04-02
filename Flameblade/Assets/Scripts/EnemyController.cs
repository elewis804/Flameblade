using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private int health = 100;
    private Animator enemyAnimator;
    [SerializeField] private GameObject healthBar;
    private float healthBarStartSize;

    // Start is called before the first frame update
    void Start()
    {
        enemyAnimator = GetComponent<Animator>();
        healthBarStartSize = healthBar.transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            health = 0;
        }
        enemyAnimator.SetTrigger("hit");
        healthBar.transform.localScale = new Vector3(healthBarStartSize * health / 100, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
        if (health <= 0)
        {
            enemyAnimator.SetBool("dead", true);
            this.enabled = false;
            Debug.Log("Enemy dead");
        }
    }
}
