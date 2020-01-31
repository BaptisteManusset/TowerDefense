using ScriptableVariable.Unite2017.Variables;
using UnityEngine;
using UnityEngine.Rendering;

public class UpdateUi : MonoBehaviour
{
  [SerializeField] Volume vol;
  [SerializeField] private FloatVariable vie;
  [SerializeField] float minVie;

  void Awake()
  {
    vol.weight = 0;
  }

  void Update()
  {
    if (vie.Value < minVie)
    {
      vol.weight = Mathf.InverseLerp(1, 0, vie.Value / minVie);
    }
  }
}
