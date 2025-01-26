using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunScript : MonoBehaviour
{
    [SerializeField] Vector2 m_shootDirection;
    [SerializeField] GameObject m_bullet;
    [SerializeField] Transform m_bulletSpawnPosition;
    [SerializeField] GameObject m_gunSprite;
    public AudioSource m_audioSource;
    public AudioClip m_bubbleSound;
    Vector3 m_mousePos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {        
        CalculateShootDirection();
        RotateGunSprite();
    }
    void CalculateShootDirection()
    {
        Vector3 a = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        m_mousePos = new Vector3(a.x, a.y, 0);
        Vector2 shootDirection = (m_mousePos - this.transform.position).normalized;

        m_shootDirection = shootDirection;

    }

    void RotateGunSprite()
    {

        float lookAngle = CalculateAngle(m_gunSprite.transform.position, m_mousePos); 

        m_gunSprite.transform.eulerAngles = new Vector3(0,0,lookAngle + 180);
        //m_gunSprite.transform.Rotate(new Vector3(0,0,lookAngle));
    }

    float CalculateAngle(Vector2 a, Vector2 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
    public void OnShoot(InputAction.CallbackContext _context)
    {
        if(_context.started)
        {
            GameObject bullet = BulletSpawner.Instance.GetInactiveBullet();

            m_audioSource.PlayOneShot(m_bubbleSound);

            if (bullet != null)
            {
                BulletScript script = bullet.GetComponent<BulletScript>();

                if (script != null)
                {
                    bullet.transform.position = m_bulletSpawnPosition.position;
                    script.MoveDirection = m_shootDirection;
                    script.ResetBullet();
                    bullet.gameObject.SetActive(true);
                    
                }
                else
                {
                    print("null");
                }

            }
        }
        

    }



    public Vector2 ShootDirection { get { return m_shootDirection; } set { m_shootDirection = value; } }

}
