using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class Enemy : MonoBehaviour
{

    public GameObject m_player;
    public GameObject m_bubblePrefab;
    [SerializeField] float m_bubbleTimer = 3;
    [SerializeField] float m_maxTime = 3;
    private bool m_timerIsRunning;

    

    public float m_speed;

    private float m_distance;
    private Rigidbody2D m_rigidBody;
    public E_EnemyStates m_enemyStates;
    private bool m_isBubble;

    private float amp = 0.1f;
    private float m_bobSpeed = 6f;
    private GameObject m_myBubble;
    private float originalY;



    // Start is called before the first frame update
    void Start()
    {
        m_rigidBody = this.GetComponent<Rigidbody2D>();

        m_myBubble = Instantiate(m_bubblePrefab, m_bubblePrefab.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - (m_bubblePrefab.GetComponent<SpriteRenderer>().bounds.size.y / 2), this.transform.position.z), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        switch (m_enemyStates)
        {
            case E_EnemyStates.FOLLOW:

                FollowPlayer();

                break;


            case E_EnemyStates.BUBBLE:
               
                
                if (m_bubbleTimer > 0)
                {
                    IsBubble();
                    m_bubbleTimer -= Time.deltaTime;
                }
                else
                {
                    m_enemyStates = E_EnemyStates.FOLLOW;
                    
                }

               
                

                break;

            case E_EnemyStates.POPPED:

                IsPopped();

                break;

            //case E_EnemyStates.ESCAPED:

                //IsEscaped();

                //break;
        }

      


    }

    public void ChangeToBubble()
    {
        m_bubbleTimer = m_maxTime;
        m_enemyStates = E_EnemyStates.BUBBLE;

        originalY = this.transform.position.y;

        
    }


    public void FollowPlayer()
    {
        m_myBubble.SetActive(false);
        this.GetComponent<SpriteRenderer>().flipY = false;
        m_speed = 1;

        m_distance = Vector2.Distance(transform.position, m_player.transform.position);
        Vector2 direction = m_player.transform.position - transform.position;

        transform.position = Vector2.MoveTowards(this.transform.position, m_player.transform.position, m_speed * Time.deltaTime);

        
        
 
        m_timerIsRunning = false;
    }

    public void IsBubble()
    {
        m_speed = 0;

        this.GetComponent<SpriteRenderer>().flipY = true;

        transform.position = new Vector2(this.transform.position.x, (originalY + (Mathf.Sin(Time.time * m_bobSpeed) * amp)));

       

        m_myBubble.transform.position = new Vector3(this.transform.position.x, this.transform.localPosition.y - (m_myBubble.GetComponent<SpriteRenderer>().bounds.size.y / 2), this.transform.position.z);
        m_myBubble.SetActive(true);

        

      


    }

    public void BubbleDebugPressed(InputAction.CallbackContext _context)
    {
        if (m_enemyStates == E_EnemyStates.BUBBLE)
        {
            m_enemyStates = E_EnemyStates.FOLLOW;
        }
        else
        {
            m_enemyStates = E_EnemyStates.BUBBLE;
        }
    }
    void IsPopped()
    {

        Destroy(m_myBubble);
        Destroy(this);
        /*this.gameObject.SetActive(false);
        m_myBubble.SetActive(false);
        m_timerIsRunning = false;*/
    }




    public void PoppedDebugPressed(InputAction.CallbackContext _context)
    {
        if(m_enemyStates == E_EnemyStates.BUBBLE)
        {
            m_enemyStates = E_EnemyStates.POPPED;
        }
    }


    public enum E_EnemyStates
    {
        FOLLOW,

        BUBBLE,

        POPPED,

        ESCAPED
    }

   
}
