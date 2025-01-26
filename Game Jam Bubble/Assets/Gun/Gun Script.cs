using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunScript : MonoBehaviour
{
    [SerializeField] Vector2 m_shootDirection;
    [SerializeField] GameObject m_bullet;
    [SerializeField] Transform m_bulletSpawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        m_shootDirection = new Vector2(1,0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnShoot(InputAction.CallbackContext _context)
    {
        GameObject bullet = BulletSpawner.Instance.GetInactiveBullet();

        if (bullet != null)
        {
            BulletScript script = bullet.GetComponent<BulletScript>();

            if (script != null)
            {
                bullet.transform.position = m_bulletSpawnPosition.position;
                script.MoveDirection = m_shootDirection;
                bullet.gameObject.SetActive(true);
            }
            else
            {
                print("null");
            }

        }

    }



    public Vector2 ShootDirection { get { return m_shootDirection; } set { m_shootDirection = value; } }

}
