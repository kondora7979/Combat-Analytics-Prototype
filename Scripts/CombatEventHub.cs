using System;
using UnityEngine;

public class CombatEventHub : MonoBehaviour {
    public static CombatEventHub Instance { get; private set; }

    // 스킬을 사용했을 때 알림
    public event Action<string> OnSkillUsed;

    // 데미지를 줬을 때 알림
    public event Action<string, float> OnDamageDealt;

    public event Action<float> OnDamageTaken;

    private void Awake() {
        Instance = this;
    }

    public void SkillUsed(string skillName) {
        OnSkillUsed?.Invoke(skillName);
    }

    public void DamageDealt(string skillName, float damage) {
        OnDamageDealt?.Invoke(skillName, damage);
    }

    public void DamageTaken(float damage) {
        OnDamageTaken?.Invoke(damage);
    }
}
