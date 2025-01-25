using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    //editable values
    [SerializeField] float m_speed;
    [SerializeField] GameObject m_gunObject;
    [SerializeField] GunScript m_gunScript;

    //private values
    Vector2 m_walkValue;
    Vector2 m_walkDirection;
    Vector2 m_previousDirection;
    SpriteRenderer m_spriteRenderer;


    // Start is called before the first frame update
    void Start()
    {
        m_spriteRenderer = this.GetComponent<SpriteRenderer>();
        m_gunScript = m_gunObject.GetComponent<GunScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if(m_walkDirection != Vector2.zero)
        {
            m_gunScript.ShootDirection = m_walkDirection;
        }
        
        Vector3 a = m_walkDirection * m_speed;
        this.transform.position += a;


    }


    public void OnMovement(InputAction.CallbackContext _context)
    {
        if (_context.ReadValue<Vector2>() == Vector2.zero)
        {
            m_walkDirection = Vector2.zero;
        }
        m_previousDirection = _context.ReadValue<Vector2>();
    }

    public void OnLeftPressed(InputAction.CallbackContext _context)
    {
        if (_context.started)
        {
            m_walkDirection = Vector2.left;
            FlipSprite(true);
        }
        if (_context.canceled)
        {
            m_walkDirection.x = m_previousDirection.x;
        }
    }
    
    public void OnRightPressed(InputAction.CallbackContext _context)
    {
        if (_context.started)
        {
            m_walkDirection = Vector2.right;
            FlipSprite(false);
        }
        if (_context.canceled)
        {
            m_walkDirection.x = m_previousDirection.x;
        }

    }
    public void OnUpPressed(InputAction.CallbackContext _context)
    {
        if (_context.started)
        {
            m_walkDirection = Vector2.up;
        }
        if (_context.canceled)
        {
            m_walkDirection.y = m_previousDirection.y;
        }
    }
    public void OnDownPressed(InputAction.CallbackContext _context)
    {
        if (_context.started)
        {
            m_walkDirection = Vector2.down;
        }
        if (_context.canceled)
        {
            m_walkDirection.y = m_previousDirection.y;
        }
    }

    public void FlipSprite(bool flip)
    {
        if(flip)
        {
            this.transform.rotation = Quaternion.Euler(0f,180f,0f);
        }
        else
        {
            this.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }

}


public enum E_PlayerWalkStates
{
    NONE,

    HORIZONTAL,

    VERTICAL
}
