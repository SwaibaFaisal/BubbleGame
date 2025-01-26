using System.Collections;
using System.Collections.Generic;
using Unity.Android.Gradle.Manifest;
using UnityEngine;

public class HeartsScript : MonoBehaviour
{
    [SerializeField] List<GameObject> m_hearts = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
      
    }

    public void Onhit(Component _sender, object _data)
    {
       GetActiveHeart().SetActive(false);

    }

    GameObject GetActiveHeart()
    {
        for(int i = 0; i <  m_hearts.Count; i++) 
        {
            if (m_hearts[i].active)
            {
                return m_hearts[i];
            }
        }
        return null;
    }
    public void OnReset()
    {
        for(int i = 0; i <  m_hearts.Count; i++) 
        {
            m_hearts[i].SetActive(true);
        }
    }
}
