using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class BulletScript : MonoBehaviour
{

    Vector2 m_moveDirection;
    [SerializeField] float m_speed;
    [SerializeField] float m_activeDuration;
    [SerializeField] LayerMask m_destructibleLayer;
    [SerializeField] LayerMask m_enemyLayer;
    [SerializeField] Animator m_animator;
    

    bool m_destroyTimerStarted = false;
    float m_counter;
    [SerializeField] float m_destroyTime = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        ResetBullet();
        m_animator = this.GetComponent<Animator>();
    }


    public void ResetBullet()
    {
        m_counter = 0;
        m_destroyTimerStarted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(m_destroyTimerStarted)
        {
            if(m_counter <= m_destroyTime)
            {

                m_counter += Time.deltaTime;
            }
            else
            {
                this.gameObject.SetActive(false);
            }


        }
        else
        {
            Vector3 moveValue = new Vector3(m_moveDirection.x * m_speed, m_moveDirection.y * m_speed, 0);
            this.transform.position += moveValue;

            if (Physics2D.OverlapCircle(this.transform.position, 0.2f, m_destructibleLayer))
            {
                m_animator.Play("Popped");

                m_destroyTimerStarted = true;
            }

            if (Physics2D.OverlapCircle(this.transform.position, 0.2f, m_enemyLayer) != null)
            {
                Collider2D collider = Physics2D.OverlapCircle(this.transform.position, 0.2f, m_enemyLayer);

                if (collider.GetComponentInParent<Enemy>() != null)
                {
                    Enemy enemyScript = collider.GetComponentInParent<Enemy>();

                    if (enemyScript != null)
                    {
                        enemyScript.ChangeToBubble();

                        this.gameObject.SetActive(false);

                    }
                }

            }
        }
        
    }

    
    

    public Vector2 MoveDirection { get { return m_moveDirection; } set { m_moveDirection = value; } }

}
