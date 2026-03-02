using UnityEngine;

public class Mallet : MonoBehaviour
{

    //衝突時に重複して呼び出されるのを防ぐためのフラグ
    private　bool isProcessingHitMallet = false;

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

        Invoke(nameof(ResetFlag), 0.1f);

    }

    private void ResetFlag()
    {
        isProcessingHitMallet = false; 

    }



}
