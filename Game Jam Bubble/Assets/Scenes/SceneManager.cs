using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    [SerializeField] GameObject m_deathText;

    public void Awake()
    {
        if(m_deathText != null)
        {
            m_deathText.SetActive(false);
        }
    }
    public void LoadScene(int _scene)
    {
        SceneManager.LoadScene(_scene);
    }
    
    public void OnDeath()
    {
        m_deathText.SetActive(true);
    }
}
