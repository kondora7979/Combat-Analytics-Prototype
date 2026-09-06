using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour {
    [SerializeField]
    private BossHealth bossHealth;

    [SerializeField]
    private Slider hpSlider;

    private void Update() {
        if (bossHealth == null || hpSlider == null)
            return;

        hpSlider.value =
            bossHealth.CurrentHealth /
            bossHealth.MaxHealth;
    }
}
