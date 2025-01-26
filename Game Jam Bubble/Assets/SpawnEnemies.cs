using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    [SerializeField] public Transform[] m_spawnPositions;
    [SerializeField] List<GameObject> m_enemies = new List<GameObject>();
    public GameObject m_enemyPrefab;
    public GameObject player;
    private Transform m_spawnPosition;

    public float m_spawnTimerMax = 8;
    public float m_spawnTimer = 8;
    public bool spawnTimerRunning;

    private GameObject m_enemyToSpawn;


    // Start is called before the first frame update
    void Start()
    {
        spawnTimerRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnTimerRunning)
        {
            if (m_spawnTimer > 0)
            {
                m_spawnTimer -= Time.deltaTime; 
            }
            else
            {
                for (int i = 0; i < m_spawnPositions.Length; i++)
                {
                    m_spawnPosition = m_spawnPositions[Random.Range(0, m_spawnPositions.Length)];

                    m_enemyToSpawn = Instantiate (m_enemyPrefab);

                   
                    Enemy _script = m_enemyToSpawn.GetComponent<Enemy>();
                    if (_script != null )
                    {
                        _script.m_player = player;
                    }
                    m_enemies.Add(m_enemyToSpawn);
                    
                }

                m_spawnTimer = m_spawnTimerMax;
            }
         

        }

    }

    public void OnReset(Component _sender, object _data)
    {
        for(int i = 0; i < m_enemies.Count; i++) 
        {
            if (m_enemies[i] != null && m_enemies[i].GetComponent<Enemy>().m_enemyStates != Enemy.E_EnemyStates.BUBBLE) 
            {
                Destroy(m_enemies[i]);
            }
            
        }
        m_enemies?.Clear();
       

        for (int i = 0; i < m_spawnPositions.Length; i++)
        { 
           m_spawnPosition = m_spawnPositions[Random.Range(0, m_spawnPositions.Length)];
           m_enemyToSpawn = Instantiate(m_enemyPrefab);


            Enemy _script = m_enemyToSpawn.GetComponent<Enemy>();
            if (_script != null)
            {
                _script.m_player = player;
            }

            m_enemyToSpawn.transform.position = m_spawnPosition.transform.position;
            m_enemies.Add(m_enemyToSpawn);

        }
        m_spawnTimer = m_spawnTimerMax;
    }
}
