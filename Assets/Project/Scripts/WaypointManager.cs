using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    public static WaypointManager Instance { get; private set; }
    public Transform[] Waypoints { get => waypoints;}

    [SerializeField] private Transform[] waypoints;

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
    public Transform GetWayPoint(int index)
    {
        return Waypoints[index];
    }
}
