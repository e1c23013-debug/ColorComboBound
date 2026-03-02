using UnityEngine;
using System.Collections;

public class DiffusionSwitchManager : MonoBehaviour
{


    
    [SerializeField] private float spreadAngle = 60f; // 左右への広がり角（度）

    [SerializeField] private float speedMultiplier = 1.1f; // 当たった際に速度を何倍にするか

    private float maxSpeed;         // ボールの上限スピード

    private bool isReady = true;
    private float coolTime = 0.3f;

    void OnEnable()
    {
        GameEvents.BallMaxSpeed +=SetMaxSpeed;

    }

    void OnDisable()
    {

        GameEvents.BallMaxSpeed -= SetMaxSpeed;

    }


    void Awake()
    {
        //イベントでボールの最高速度を伝えてもらう前に初期化
        maxSpeed = 0;


    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (GameManager.Instance.IsGameClear) return;

        if (other.CompareTag("Ball")&&isReady==true)
        {

            isReady = false;
            StartCoroutine(StartCoolTime());

            GameEvents.DiffusionSwitch?.Invoke();

            BallBehaviour ballBehaviour = other.gameObject.GetComponent<BallBehaviour>();

            Rigidbody2D rb = ballBehaviour.GetComponent<Rigidbody2D>();

            float newSpeed = rb.linearVelocity.magnitude * speedMultiplier;

            if (newSpeed> maxSpeed)
            {
                newSpeed = maxSpeed;
            }

           rb.linearVelocity = Vector2.zero;

            // ランダムな角度を計算（真上を0度として、左右にspreadAngle分）
            float randomAngle = Random.Range(-spreadAngle, spreadAngle);

            // 角度をベクトルに変換,Vector2.up(真上方向)を基準に-spreadAngle～spreadAngle度
           Vector2 splashDirection = Quaternion.Euler(0, 0, randomAngle) * Vector2.up;

            GameEvents.Diffusion?.Invoke(newSpeed,splashDirection);

        }



    }


    IEnumerator StartCoolTime()
    {
        isReady = false;
        yield return new WaitForSeconds(coolTime);
        isReady = true;

    }

    private void SetMaxSpeed(float ballMaxSpeed)
    {

        maxSpeed = ballMaxSpeed;


    }

}
