using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
  [Label("Monnaies")] [SerializeField] private TextMeshProUGUI TxtMoney;
  [BoxGroup("Vie")] [Label("Vies")] [SerializeField] private TextMeshProUGUI TxtLife;
  [BoxGroup("Vie")] [Label("Background")] [SerializeField] private Image TxtLifeBackground;

  static public MainUI instance;
  Main main;
  void Awake()
  {
    main = GetComponent<Main>();
  }
  void Start()
  {
    if (Main.instance == null) MainUI.instance = this;
  }
  public void UpdateAllUi()
  {
    UpdateLifesUi();
    UpdateMoneyUi();
  }
  public void UpdateLifesUi()
  {
    TxtLife.text = main.lifes + "Pv";
    TxtLifeBackground.fillAmount = (float)main.lifes / 20;
  }

  public void UpdateMoneyUi()
  {
    TxtMoney.text = main.money + "$";
  }
}
