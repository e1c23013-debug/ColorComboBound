using UnityEngine;

public class TurnManager : MonoBehaviour
{

    //インスペクター側で設定
    [SerializeField] private int TotalTurn;

    private int NowTurn;
    private int RestTurn;

  
    void OnEnable()
    {
        GameEvents.BallNew += TurnPlus;

       

    
    }
    void OnDisable()
    {
        GameEvents.BallNew -= TurnPlus;

       


    }


    void Awake()
    {
        NowTurn = 0;
        RestTurn = TotalTurn - NowTurn;

    }

    void Start()
    {
        GameEvents.OnTurnChanged?.Invoke(RestTurn);

    }

    //GamaManagerより呼び出し．初めのターン設定に使う
    public void StartTurnChange()
    {
        GameEvents.OnTurnChanged?.Invoke(RestTurn);
    }



    private void TurnPlus()
    {
       

        NowTurn = NowTurn+1;
        RestTurn = TotalTurn - NowTurn;
       

        GameEvents.OnTurnChanged?.Invoke(RestTurn);

        if (RestTurn < 0)
        {
            GameEvents.OnTurnRetry?.Invoke();
;
        }

    }
    
   

}
