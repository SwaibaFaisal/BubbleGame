using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    Vector2 m_moveDirection;
    [SerializeField] float m_speed;
    [SerializeField] float m_activeDuration;
    [SerializeField] LayerMask m_destructibleLayer;
    bool m_isActive;
    float m_timeElapsed;


    // Start is called before the first frame update
    void Start()
    {
        m_timeElapsed = 0;
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 moveValue = new Vector3(m_moveDirection.x * m_speed, m_moveDirection.y * m_speed, 0);
        this.transform.position += moveValue;

        if(Physics2D.OverlapCircle(this.transform.position, 0.1f, m_destructibleLayer ))
        {
            this.gameObject.SetActive(false);
        }

    }

    public Vector2 MoveDirection { get { return m_moveDirection; } set { m_moveDirection = value; } }


}
