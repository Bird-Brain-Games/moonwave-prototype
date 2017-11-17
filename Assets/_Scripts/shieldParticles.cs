using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
public class shieldParticles : MonoBehaviour
{

    public float m_Attraction;
    public float m_SpawnDistance;
    public float m_DistanceVariance;
    public float m_AngleVariance;
    public int m_numParticles;
    public bool m_lockZ;

    public bool m_initialized;

    // The life of a particle... In variables [Jack]
    public float m_lifeSpanMax;
    public float m_lifeSpanMin;


    // The object that will be our particle
    public GameObject m_SpawnableObject;


    // Use this for initialization
    #region dllImports
    [StructLayout(LayoutKind.Sequential)]
    public struct Vec
    {
        public float x, y, z;
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct Particle
    {
        public Vec Pos;
        public Vec Vel;
        public float lifeSpan;
    }
    [DllImport("shieldParticles")]
    public static extern void initSystem();
    [DllImport("shieldParticles")]
    public static extern void initialize(Particle[] p, int size, float spawnDistance, bool lockZ, float distanceRan, float angleRan, float lifeSpanMax, float lifeSpanMin);
    [DllImport("shieldParticles")]
    public static extern void updateTargetPos(float x, float y, float z);
    [DllImport("shieldParticles")]
    public static extern void updateAttraction(float a);
    [DllImport("shieldParticles")]
    public static extern void updateParticle(Particle[] p, float dt);

    #endregion


    Particle[] particleArray = new Particle[50];
    GameObject[] particleArr = new GameObject[50];


    void Start()
    {
        m_initialized = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Shield>().m_percentSheild <= 0 && !m_initialized && Time.time > 3)
        {
            initParticles();
        }

        particleDeath();

        if (m_initialized) {
            Vector3 pos = transform.position;
            updateTargetPos(pos.x, pos.y, pos.z);
            updateParticle(particleArray, Time.deltaTime);
            setPosition();
        }
    }

    void setPosition()
    {
        for (int i = 0; i < m_numParticles; i++)
        {
            if (particleArr[i] != null)
                particleArr[i].transform.position = new Vector3(particleArray[i].Pos.x, particleArray[i].Pos.y, particleArray[i].Pos.z);
        }
    }

    void initParticles()
    {
        for (int i = 0; i < m_numParticles; i++)
        {
            particleArr[i] = Instantiate<GameObject>(m_SpawnableObject);
        }

        initSystem();
        updateAttraction(m_Attraction);
        updateTargetPos(transform.position.x, transform.position.y, transform.position.z);
        initialize(particleArray, m_numParticles, m_SpawnDistance, m_lockZ, m_DistanceVariance, m_AngleVariance, m_lifeSpanMax, m_lifeSpanMin);
        setPosition();


        m_initialized = true;
    }

    void particleDeath()
    {
        for (int i = 0; i < m_numParticles; i++)
        {
            if (particleArr[i] != null)
            {
                if (particleArray[i].lifeSpan <= 0)
                {
                    Object.Destroy(particleArr[i]);
                }
                
            }
        }
        if (particleArr.Length == 0)
        {
            m_initialized = false;
        }
    }

}
