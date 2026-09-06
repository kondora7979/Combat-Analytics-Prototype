using UnityEngine;

public class BossHealth : MonoBehaviour {
    [SerializeField]
    private float maxHealth = 1000000f;

    private float currentHealth;

    public float CurrentHealth => currentHealth;
    public float MaxHealth => maxHealth;

    private void Start() {
        currentHealth = maxHealth;
    }

    private void Update() {
        // T를 누르면 보스 HP 초기화
        if (Input.GetKeyDown(KeyCode.T)) {
            ResetHealth();
        }
    }

    public void TakeDamage(string skillName, float damage) {
        // 실제로 보스에게 들어갈 수 있는 데미지만 계산
        float actualDamage = Mathf.Min(damage, currentHealth);

        currentHealth -= actualDamage;

        Debug.Log(
            "Boss Damage : " +
            actualDamage +
            " / HP : " +
            currentHealth +
            " / " +
            maxHealth
        );

        // 분석기에도 실제 데미지만 전달
        CombatEventHub.Instance.DamageDealt(
            skillName,
            actualDamage
        );

        if (currentHealth <= 0) {
            Debug.Log("Boss Defeated!");
        }
    }

    public void ResetHealth() {
        currentHealth = maxHealth;

        Debug.Log(
            "Boss HP Reset : " +
            currentHealth
        );
    }
}