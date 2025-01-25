using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public static BulletSpawner Instance;
    [SerializeField] int m_maxBullets;
    [SerializeField] GameObject m_bulletObject;
    List<GameObject> m_bulletList = new List<GameObject>();

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        InstantiateBullets();
    }
    public void InstantiateBullets()
    {
        for(int i = 0; i < m_maxBullets;  i++) 
        {
           GameObject obj = Instantiate(m_bulletObject);
           m_bulletObject.SetActive(false);
           m_bulletList.Add(obj);
        }

    }

    public GameObject GetInactiveBullet()
    {
        for(int i = 0; i < m_bulletList.Count; i++) 
        {
            if (!m_bulletList[i].activeInHierarchy)
            {
                return m_bulletList[i];
            }
        }
       return null;
    }
}
