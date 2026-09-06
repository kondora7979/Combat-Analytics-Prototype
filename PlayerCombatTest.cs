using UnityEngine;

public class PlayerCombatTest : MonoBehaviour {
    [SerializeField]
    private BossHealth boss;

    private void Update() {
        // Q : 일반 공격
        if (Input.GetKeyDown(KeyCode.Q)) {
            UseSkill("NormalAttack", 1000f);
        }

        // E : 강한 공격
        if (Input.GetKeyDown(KeyCode.E)) {
            UseSkill("StrongAttack", 3000f);
        }

        // R : 극딜 시작 기준 스킬
        if (Input.GetKeyDown(KeyCode.R)) {
            UseSkill("OriginSkill", 5000f);
        }
    }

    private void UseSkill(
        string skillName,
        float damage
    ) {
        // 스킬을 사용했다는 정보 전달
        CombatEventHub.Instance.SkillUsed(
            skillName
        );

        // Boss가 실제로 데미지를 받음
        if (boss != null) {
            boss.TakeDamage(
                skillName,
                damage
            );
        }
        else {
            Debug.LogError(
                "BossHealth가 연결되지 않았습니다."
            );
        }

        Debug.Log(
            skillName +
            " 사용 / Damage : " +
            damage
        );
    }
}