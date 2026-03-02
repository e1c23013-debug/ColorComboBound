using UnityEngine;
using UnityEngine.UI;

public class InstructingCanvasManager : MonoBehaviour
{


    [SerializeField] private Sprite[] InstructingSprites;//今回と今までの説明をまとめた画像

    [SerializeField] private GameObject nextButton;

    [SerializeField] private GameObject backButton;

    [SerializeField] private Image InstructingImage;

    [SerializeField] private GameObject InstructingCanvas;//今回の説明と今までのものも含めた説明

    

    private int currentIndex = 0;//現在表示している画像の配列のインデックス


    public void PushInstructionButton()
    {

        InstructingCanvas.SetActive(true);

        InstructingImage.sprite = InstructingSprites[currentIndex];
        if (InstructingSprites.Length == 1)//画像が一枚だけの時は次ボタン非表示
        {
            nextButton.SetActive(false);

        }

        if (currentIndex == 0)//最初のの画像を表示するとき前ボタンを非表示
        {

            backButton.SetActive(false);

        }


        if (currentIndex == InstructingSprites.Length - 1)//最後の画像を表示するとき次ボタンを非表示
        {

            nextButton.SetActive(false);

        }



        GameEvents.InstructionButtonPushed?.Invoke();
    }

    public void PushInstructionCloseButton()
    {

        InstructingCanvas.SetActive(false);
        GameEvents.InstructionCloseButtonPushed?.Invoke();
    }



    public void PushNextButton()
    {

        if (currentIndex == 0)//最初の画像を表示しているときに次ボタンを押した場合，非表示になっている前ボタンを表示
        {

            backButton.SetActive(true);

        }


        currentIndex = currentIndex + 1;
        InstructingImage.sprite = InstructingSprites[currentIndex];
        if(currentIndex== InstructingSprites.Length-1)//最後の画像を表示しているとき次ボタンを非表示
        {

            nextButton.SetActive(false);
        }
    }

    public void PushBackButton()
    {
        if (currentIndex == InstructingSprites.Length-1)// 最後の画像を表示しているときに前ボタンを押した場合，非表示になっている次ボタンを表示
        {

            nextButton.SetActive(true);

        }

            currentIndex = currentIndex - 1;
        InstructingImage.sprite = InstructingSprites[currentIndex];//最初の画像を表示しているとき前ボタンを非表示
        if (currentIndex == 0)
        {

            backButton.SetActive(false);

        }
    }


}
