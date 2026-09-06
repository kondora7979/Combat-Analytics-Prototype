using UnityEngine;

public class PlayerHealth : MonoBehaviour {
    [SerializeField]
    private float maxHealth = 10000f;

    private float currentHealth;

    public float CurrentHealth => currentHealth;
    public float MaxHealth => maxHealth;

    private void Start() {
        currentHealth = maxHealth;
    }

    private void Update() {
        // 테스트용
        // H를 누르면 플레이어가 1000 피해를 받음
        if (Input.GetKeyDown(KeyCode.H)) {
            TakeDamage(1000f);
        }

        // Y를 누르면 HP 회복
        if (Input.GetKeyDown(KeyCode.Y)) {
            ResetHealth();
        }
    }

    public void TakeDamage(float damage) {
        float actualDamage =
            Mathf.Min(damage, currentHealth);

        currentHealth -= actualDamage;

        Debug.Log(
            "Player Damage : " +
            actualDamage +
            " / HP : " +
            currentHealth +
            " / " +
            maxHealth
        );

        CombatEventHub.Instance.DamageTaken(
            actualDamage
        );

        if (currentHealth <= 0) {
            Debug.Log("Player Defeated!");
        }
    }

    public void ResetHealth() {
        currentHealth = maxHealth;

        Debug.Log(
            "Player HP Reset : " +
            currentHealth
        );
    }
}
