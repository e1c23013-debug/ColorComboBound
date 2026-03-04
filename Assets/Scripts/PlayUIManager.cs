using UnityEngine;
using TMPro;
public class PlayUIManager : MonoBehaviour
{


    [SerializeField] private GameObject turnUI;

    [SerializeField] private GameObject comboUI;

    private TextMeshProUGUI tmpTurn;

    private TextMeshProUGUI tmpCombo;

    private const string TurnLimitFormat = "TURN\nLimit ";

    private const string ComboFormat = "Combo\n";

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
        tmpTurn.text = TurnLimitFormat;
        tmpCombo.text = ComboFormat;

    }
    
  
    
    private void ChangeTurnUI(int RestTurn)
    {

        tmpTurn.text = TurnLimitFormat + (RestTurn + 1);

    }

    private void ChangeComboUI(int combo)
    {

        tmpCombo.text = ComboFormat + (combo);


    }
}
