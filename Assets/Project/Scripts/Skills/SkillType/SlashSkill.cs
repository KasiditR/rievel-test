using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashSkill : MonoBehaviour
{
    public static SlashSkill Instance { get; private set; }
    private GameObject projectilePrefab;
    private List<GameObject> projectilePool = new List<GameObject>();
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        projectilePrefab = Resources.Load("Bullet") as GameObject;
        
        if (projectilePool.Count == 0)
        {
            projectilePool.Clear();
            for (int i = 0; i < 2; i++)
            {
                GameObject projectile = MonoBehaviour.Instantiate(projectilePrefab);
                projectile.SetActive(false);
                projectilePool.Add(projectile);
            }
        }
    }
    public void Slash()
    {
        // GameObject projectile = GetInactiveProjectile();
        // if (projectile != null)
        // {
        //     projectile.transform.position = agent.transform.GetChild(0).position;
        //     Transform target = player.GetChild(0).transform;
        //     projectile.SetActive(true);
        //     projectile.GetComponent<ObjectPooling>().Launch("Player",target.position);
        // }
    }
    private GameObject GetInactiveProjectile()
    {
        // Find and return an inactive projectile from the pool
        foreach (GameObject projectile in projectilePool)
        {
            if (!projectile.activeSelf)
            {
                return projectile;
            }
        }
        
        return null;
    }
}
