using UnityEngine;

public class Mallet : MonoBehaviour
{

    //衝突時に重複して呼び出されるのを防ぐためのフラグ
    private　bool isProcessingHitMallet = false;

    private float invalidTime=0.01f;

    void OnEnable()
    {
        GameEvents.BallCollisionMallet += InformMalletPushBall;

    }

    void OnDisable()
    {
        GameEvents.BallCollisionMallet -= InformMalletPushBall;

    }


    private void InformMalletPushBall()
    {
        if (isProcessingHitMallet == true) return;//一度の衝突で判定が重複しないように   

        isProcessingHitMallet = true;

        GameEvents.MalletPushBall?.Invoke();

        Invoke(nameof(ResetFlag), invalidTime);

    }

    private void ResetFlag()
    {
        isProcessingHitMallet = false; 

    }



}
