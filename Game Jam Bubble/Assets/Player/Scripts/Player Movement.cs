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
  

    //private values
    Vector2 m_walkValue;
    Vector2 m_walkDirection;
    Vector2 m_previousDirection;
    SpriteRenderer m_spriteRenderer;


    // Start is called before the first frame update
    void Start()
    {
        m_spriteRenderer = this.GetComponent<SpriteRenderer>();  
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 a = m_walkDirection * m_speed;
        this.transform.position += a;

       
    }


    public void OnMovement(InputAction.CallbackContext _context)
    {
        if(_context.ReadValue<Vector2>() == Vector2.zero)
        {
            m_walkDirection = Vector2.zero;
        }
        m_previousDirection = _context.ReadValue<Vector2>();
    }

    public void OnLeftPressed(InputAction.CallbackContext _context)
    {
        if(_context.started)
        {
            m_walkDirection = Vector2.left;
            m_spriteRenderer.flipX = true;
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
            m_spriteRenderer.flipX = false;
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
        if(_context.canceled)
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

    Vector2 GetWalkDirection(Vector2 _input)
    {
        Vector2 _finalDirection = Vector2.zero;

        if(_input.y > 0.01f)
        {
            _finalDirection = Vector2.up;
        }
        else if (_input.y < -0.01f)
        {
            _finalDirection = Vector2.down;
        }
        else if (_input.x > 0.01f)
        {
            _finalDirection = Vector2.right;
        }
        else if (_input.x < -0.01f)
        {
            _finalDirection = Vector2.left;
        }
       
        return _finalDirection;
    }

}



public enum E_PlayerWalkStates
{
    NONE,

    HORIZONTAL,

    VERTICAL
}
