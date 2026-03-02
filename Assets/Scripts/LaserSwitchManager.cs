using UnityEngine;

public class LaserSwitchManager : MonoBehaviour
{


    private LaserLauncherManager laserLaunchercs;
    
    void Start()
    {
        laserLaunchercs = GetComponentInChildren<LaserLauncherManager>();
    }

  
    void OnTriggerEnter2D(Collider2D other)
    {
        if (GameManager.Instance.IsGameClear) return;

        if (other.CompareTag("Ball"))
        {
            GameEvents.LaserSwitch?.Invoke();

            laserLaunchercs.DrawLaser();

        }



    }

}
