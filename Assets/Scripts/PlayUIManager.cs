using UnityEngine;
using TMPro;
public class PlayUIManager : MonoBehaviour
{


    [SerializeField] private GameObject turnUI;

    [SerializeField] private GameObject comboUI;

    private TextMeshProUGUI tmpTurn;

    private TextMeshProUGUI tmpCombo;

   
    void OnEnable()
    {
        GameEvents.OnTurnChanged += ChangeTurnUI;
        GameEvents.ComboChageInform += ChangeComboUI;
    }

    void OnDisable()
    {
        GameEvents.OnTurnChanged -= ChangeTurnUI;
        GameEvents.ComboChageInform -= ChangeComboUI;
    }

    void Awake()
    {
       tmpTurn = turnUI.GetComponent<TextMeshProUGUI>();
        tmpCombo = comboUI.GetComponent<TextMeshProUGUI>();
        tmpTurn.text = "TURN\nLimit " ;
        tmpCombo.text = "Combo\n" ;

    }
    
  
    
    private void ChangeTurnUI(int RestTurn)
    {

        tmpTurn.text = "TURN\nLimit " + (RestTurn + 1);

    }

    private void ChangeComboUI(int combo)
    {

        tmpCombo.text = "Combo\n"+ (combo);


    }
}
