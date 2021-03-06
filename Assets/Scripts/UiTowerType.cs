using NaughtyAttributes;
using ScriptableVariable.Unite2017.Variables;
using UnityEngine;
using UnityEngine.UI;

public class UiTowerType : MonoBehaviour
{
    private Image img;
    private SimpleTooltip tp;
    [BoxGroup("Zone")] [SerializeField] Sprite spriteZoneAttack;
    [BoxGroup("Ciblée")] [SerializeField] Sprite spriteAttackOne;
    [BoxGroup("Zone")] [SerializeField] string textZoneAttack;
    [BoxGroup("Ciblée")] [SerializeField] string textAttackOne;
    [BoxGroup("Ref")] [SerializeField] GameObjectVariable tower;
    void Start()
    {
        img = GetComponent<Image>();
        tp = GetComponent<SimpleTooltip>();
    }


    public void UpdateUi()
    {
        if (tower.Value != null)
        {
            bool isZoneAttack = tower.Value.gameObject.GetComponent<Tower>().stat.isZoneAttack;
            if (spriteZoneAttack && spriteAttackOne && img) img.sprite = isZoneAttack ? spriteZoneAttack : spriteAttackOne;
            if (tp) tp.infoLeft = isZoneAttack ? textZoneAttack : textAttackOne;

        }
    }

    public void UpdateUi(TowerStat stat)
    {
        img.sprite = stat.isZoneAttack ? spriteZoneAttack : spriteAttackOne;
        tp.infoLeft = stat.isZoneAttack ? textZoneAttack : textAttackOne;
    }
}
