using UnityEngine;

public class BossAttackTest : MonoBehaviour {
    private PlayerHealth playerHealth;

    private void Start() {
        playerHealth = FindAnyObjectByType<PlayerHealth>();

        if (playerHealth == null) {
            Debug.LogError("PlayerHealth를 찾지 못했습니다.");
        }
        else {
            Debug.Log("PlayerHealth 자동 연결 성공");
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.J)) {
            AttackPlayer(500f);
        }

        if (Input.GetKeyDown(KeyCode.K)) {
            AttackPlayer(1500f);
        }

        if (Input.GetKeyDown(KeyCode.L)) {
            AttackPlayer(3000f);
        }
    }

    private void AttackPlayer(float damage) {
        if (playerHealth == null)
            return;

        playerHealth.TakeDamage(damage);

        Debug.Log("Boss Attack : " + damage);
    }
}
