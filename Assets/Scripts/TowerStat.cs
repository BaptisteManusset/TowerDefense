using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TowerStat : ScriptableObject
{
    [Space(20)] public int radius;
    public int radiusUpgradeCost;

    [Space(20)] public int damage;
    public int damageUpgradeCost;
    
    [Space(20)] public int reloadSpeed;
    public int reloadSpeedUpgradeCost;

    [Space(20)] public bool isZoneAttack;
    public bool isZoneAttackUpgradeCost;
}
