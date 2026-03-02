using UnityEngine;

public class BallManager : MonoBehaviour
{

    [SerializeField] private GameObject ballPrefab;


    //Ballオブジェクトを参照
    [SerializeField] private GameObject ball;

  
    private BallBehaviour ballBehaviour;
    private ShotLine shotLine;
   
    void OnEnable()
    {
        GameEvents.BallGround += BallDelete;
      
    }

    void OnDisable()
    {
        GameEvents.BallGround -= BallDelete;
       
    }


    void Awake()
    {
        ballBehaviour = GetComponentInChildren<BallBehaviour>();

        shotLine = GetComponentInChildren<ShotLine>();

    }

    void Start()
    {
        //最初のボールを生成
        if (ball != null)
        {
            BallDelete();
        }
        BallGenerate();

    }

    // Update is called once per frame
    void Update()
    {
        if (ballBehaviour != null && ballBehaviour.IsBallGround)
        {
            BallGenerate();

        }

    }

    //publicはGameManagerより呼び出し

    public void BallShot(Vector3 Mousepos3)
    {
        if (!ballBehaviour.IsBallMoving)
        {
            GameEvents.BallShot?.Invoke();

            ballBehaviour.ChangeBallMovingTrue();
            ballBehaviour.BallDirectionChange(Mousepos3);

        }

    }

  
    public void DrawLine(Vector3 Mousepos3)
    {
        if (!ballBehaviour.IsBallMoving )
        {
            shotLine.DrawLine(Mousepos3);
        }

    }

    public void DeleteLine()
    {
        if (!ballBehaviour.IsBallMoving)
        {
            shotLine.DeleteLine();
        }

    }

     private void BallGenerate()
    {
        Destroy(ball);

        ball = (GameObject)Instantiate(ballPrefab);

        //Ballが新しくなったので，BallBehaviourを新しいBall(インスタンス)のものにする
        ballBehaviour = ball.GetComponent<BallBehaviour>();
        shotLine = ball.GetComponentInChildren<ShotLine>();


        GameEvents.BallNew?.Invoke();

        ballBehaviour.ChangeBallGroundfalse();
        ballBehaviour.ChangeBallMovingfalse();



    }
   

    private void BallDelete()
    {
        if(ballBehaviour != null){ 

        Destroy(ball);
        }
    }

}
