using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    private int remainingEnemies;

    void OnEnable()
    {
        GameEvents.EnemyBeated += EnemyInvisible;
    }

    void OnDisable()
    {
        GameEvents.EnemyBeated -= EnemyInvisible;
    }


    void Start()
    {
        
        remainingEnemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None).Length;


    }


    private void EnemyInvisible()
    {
        remainingEnemies--;

        if(remainingEnemies ==1)
        {
            GameEvents.OneEnemyLeft?.Invoke();
        }
        if (remainingEnemies <= 0)
        {
           
            GameEvents.Clear?.Invoke();
        }


    }
}
