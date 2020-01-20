using NaughtyAttributes;
using UnityEngine;

public class MainUi : MonoBehaviour
{
    public enum InGameUiState
    {
        Noone,
        UpgradeUi,
        ShopUi
    }

    public static InGameUiState ui = InGameUiState.Noone;
 
}
