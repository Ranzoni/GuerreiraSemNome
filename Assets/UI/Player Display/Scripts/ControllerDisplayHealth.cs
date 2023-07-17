using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class ControllerDisplayHealth : MonoBehaviour
{
    [Tooltip("Prefab com o script de Controle de Impulso")]
    [SerializeField] Health health;

    Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();
    }

    void Update()
    {
        slider.value = (float)health.HealthAmount / (float)health.MaxHealth;
    }
}
