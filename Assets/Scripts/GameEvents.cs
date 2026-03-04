using System;
using UnityEngine;

public static class GameEvents
{
    //ボールが新しくなった時時に通知
    public static Action BallNew;
    //ターンが変わった時に通知
    public static Action<int> OnTurnChanged;
    //ターン全消費後通知
    public static Action OnTurnRetry;

    //クリア通知
    public static Action Clear;

    //リトライボタン選択通知
    public static Action PushRetryButton;

    //ステージセレクトボタンが押されたとき通知
    public static Action PushStageSelectButton;

    //ボールがものにぶつかった時通知
    public static Action BallBound;

    //敵がダメージを受けたら通知
    public static Action BallBoundEnemy;

    //敵が倒された時通知
    public static Action EnemyBeated;

    //ボールの最大速度を通知し，速度を変更するギミックに知らせる
    public static Action<float> BallMaxSpeed;

    //ボールが地面に到達したとき通知
    public static Action BallGround;

    //ボールをショットするとき通知
    public static Action BallShot;

    //ボールが敵に衝突したときにダメージと敵の色を通知
    public static Action<int,EnemyColor> BallCollisionEnemy;

    //敵のHPが減った時，どの色の敵が攻撃を受けたか,ダメージを受けた後のHP,最大HPを通知
    public static Action<EnemyColor,int,int> EnemyHitColor;

    //最初にEnemyが自分の色を知らせてUIに存在する敵の色を通知

    public static Action<EnemyColor> StartEnemyColor;

    //コンボが変更されたときに通知

    public static Action<int> ComboChageInform;

    //コンボが増えて，与えるダメージを増加させるときに通知
    public static Action IncreaseDamageCombo;

    //マレットスイッチが押されたときに通知
    public static Action PushMalletSwitch;

    //ボールがマレットに接触したときに通知
    public static Action BallCollisionMallet;

    //マレットでボールを打ち返したと判定されたときに通知
    public static Action MalletPushBall;

    //敵が残り１匹になったら通知
    public static Action OneEnemyLeft;

    //ボールの通常ヒットダメージを通知
    public static Action<int> NormalDamage;

    //攻撃が無効化されたときに通知
    public static Action InvalidAttack;

    //clear時のマックスコンボを通知
    public static Action<int> ClearMaxCombo;

    //拡散スイッチが押されたときに通知
    public static Action DiffusionSwitch;

    //レーザースイッチが押されたときに通知
    public static Action LaserSwitch;

    //レーザーが敵に当たった時，ダメージと敵の色を通知
    public static Action<int, EnemyColor> LaserCollisionEnemy;

    //拡散スイッチが作動するときに通知
    public static Action<float,Vector2> Diffusion;

    //メニュー表示ボタンが押されたら通知
    public static Action MenuButtonPushed;

    //メニューのクローズボタンが押されたら通知
    public static Action MenuCloseButtonPushed;

    //クリア画面のnextボタンが押されたら通知
    public static Action PushNextStageButton;


    //説明表示ボタンが押されたら通知
    public static Action InstructionButtonPushed;

    //説明のクローズボタンが押されたら通知
    public static Action InstructionCloseButtonPushed;

    //チュートリアル説明を表示するときに通知
    public static Action DisplayTutorial;

    //チュートリアル説明のクローズボタンが押されたら通知
    public static Action TutorialClose;

    //ショットチュートリアルアニメーションの再生開始時通知
    public static Action StartShotTutorialAnimation;

    //ショットチュートリアルアニメーションの再生終了時通知
    public static Action EndShotTutorialAnimation;


}