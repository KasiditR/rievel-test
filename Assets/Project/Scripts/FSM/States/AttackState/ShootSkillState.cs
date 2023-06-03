using System.Collections.Generic;
using UnityEngine;

public class ShootSkillState : State
{
    private GameObject projectilePrefab;
    private List<GameObject> projectilePool = new List<GameObject>();
    
    public override void Enter()
    {
        Debug.Log("Entering Shoot Skill State");
        
        // Create and initialize the projectile pool
        projectilePrefab = Resources.Load("bullet") as GameObject;
        
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
    
    public override void Update()
    {
        if (player != null)
        {
            GameObject projectile = GetInactiveProjectile();
            if (projectile != null)
            {
                projectile.transform.position = agent.transform.GetChild(0).position;
                Transform target = player.GetChild(0).transform;
                projectile.SetActive(true);
                projectile.GetComponent<Bullet>().Launch(target.position);
                fsm.SetState("Idle");
            }
        }
    }
    
    public override void Exit()
    {
        Debug.Log("Exiting Shoot Skill State");
        
        // Deactivate any remaining projectiles in the pool
        foreach (GameObject projectile in projectilePool)
        {
            if (projectile.activeSelf)
            {
                projectile.SetActive(false);
            }
        }
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
