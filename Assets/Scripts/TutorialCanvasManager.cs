using UnityEngine;
using UnityEngine.UI;

public class TutorialCanvasManager : MonoBehaviour

{

    [SerializeField] private Sprite[] TutorialSprites;//ステージ最初に表示されるそのステージ用の説明画像


    [SerializeField] private GameObject TutorialCanvas;//今回のステージでの新説明

    [SerializeField] private GameObject nextButton;

    [SerializeField] private GameObject backButton;

     [SerializeField] private Image TutorialImage;




    private int currentIndex = 0;//現在表示している画像の配列のインデックス



    //  ステージを選択した最初のプレイ時にGameManagerから呼び出し
    public void DisplayTutorial() 
    {

        TutorialCanvas.SetActive(true);
        TutorialImage.sprite = TutorialSprites[currentIndex];
        if (TutorialSprites.Length == 1)//画像が一枚だけの時は次ボタン非表示
        {
            nextButton.SetActive(false);

        }
        if (currentIndex == 0)//最初の画像を表示するとき前ボタンを非表示
        {

            backButton.SetActive(false);

        }


        if (currentIndex == TutorialSprites.Length - 1)//最後の画像を表示するとき次ボタンを非表示
        {

            nextButton.SetActive(false);

        }

        GameEvents.DisplayTutorial?.Invoke();
    }

    public void PushTutorialCloseButton()
    {
        GameEvents.TutorialClose?.Invoke();

        TutorialCanvas.SetActive(false);
       
    }



    public void PushNextButton()
    {

        if (currentIndex == 0)//最初の画像を表示しているときに次ボタンを押した場合，非表示になっている前ボタンを表示
        {

            backButton.SetActive(true);

        }


        currentIndex = currentIndex + 1;
        TutorialImage.sprite = TutorialSprites[currentIndex];
        if (currentIndex == TutorialSprites.Length - 1)//最後の画像を表示しているとき次ボタンを非表示
        {

            nextButton.SetActive(false);
        }
    }

    public void PushBackButton()
    {
        if (currentIndex == TutorialSprites.Length - 1)// 最後の画像を表示しているときに前ボタンを押した場合，非表示になっている次ボタンを表示
        {

            nextButton.SetActive(true);

        }

        currentIndex = currentIndex - 1;
        TutorialImage.sprite = TutorialSprites[currentIndex];//最初の画像を表示しているとき前ボタンを非表示
        if (currentIndex == 0)
        {

            backButton.SetActive(false);

        }
    }




}
