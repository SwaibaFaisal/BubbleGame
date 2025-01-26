using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int m_maxHearts;
    [SerializeField] LayerMask m_enemyLayer;
    int m_currentHearts;


    // Start is called before the first frame update
    void Start()
    {
        RestartRound();
    }

    // Update is called once per frame
    void Update()
    {


        if (Physics2D.OverlapCircle(this.transform.position, 0.1f, m_enemyLayer) != null)
        {
            Collider2D collider = Physics2D.OverlapCircle(this.transform.position, 0.1f, m_enemyLayer);

            if (collider.GetComponentInParent<Enemy>() != null)
            {
                Enemy enemyScript = collider.GetComponentInParent<Enemy>();

                if (enemyScript != null)
                {
                    if(enemyScript.m_enemyStates == Enemy.E_EnemyStates.FOLLOW)
                    {
                        Hit();
                    }
                }
            }

        }
    }

    public void RestartRound()
    {
        m_currentHearts = m_maxHearts;
    }

    public void Hit()
    {
        this.transform.position = Vector2.zero;
        m_currentHearts--;
        if(m_currentHearts <= 0)
        {
            print("Death");
        }
    }

}
