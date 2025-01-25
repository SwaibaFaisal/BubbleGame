using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    //editable values
    [SerializeField] float m_speed;
  

    //private values
    Vector2 m_walkValue;
    E_PlayerWalkStates m_walkStates;
    E_PlayerWalkStates m_previousWalkStates;
    Rigidbody2D m_rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        m_rigidBody = this.GetComponent<Rigidbody2D>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 a = m_walkValue * m_speed;
        this.transform.position += a;


       
    }

    void SetGunPosition()
    {
        if(m_walkDirection. x < -0.01f)
        {
            m_gunObject.transform.position = new Vector3(0.38f, -0.2f);
        }
    }


    public void OnWalkHorizontal(InputAction.CallbackContext _context)
    {
        if(_context.started)
        {
            m_walkStates = E_PlayerWalkStates.HORIZONTAL;
            m_walkValue.x = _context.ReadValue<Vector2>().x;
        }

        if(_context.canceled)
        {
            m_walkStates = m_previousWalkStates;
            m_previousWalkStates = E_PlayerWalkStates.HORIZONTAL;
        }
    }

    public void OnWalkVertical(InputAction.CallbackContext _context)
    {
        if(_context.started)
        {
            m_walkStates = E_PlayerWalkStates.VERTICAL;
            m_walkValue.y = _context.ReadValue<Vector2>().y;
        }

        if(_context.canceled)
        {
            m_walkStates = m_previousWalkStates;
            m_previousWalkStates = E_PlayerWalkStates.VERTICAL;
        }
    }

    public void OnMovement(InputAction.CallbackContext _context)
    {
        m_walkValue = _context.ReadValue<Vector2>();
    }

}



public enum E_PlayerWalkStates
{
    NONE,

    HORIZONTAL,

    VERTICAL
}
