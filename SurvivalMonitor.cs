using UnityEngine;

public class SurvivalMonitor : DroneProgramBase {
    private int damageTakenCount = 0;
    private float totalDamageTaken = 0f;
    private float maxSingleDamage = 0f;

    public int DamageTakenCount => damageTakenCount;
    public float TotalDamageTaken => totalDamageTaken;
    public float MaxSingleDamage => maxSingleDamage;

    public float AverageDamage {
        get {
            if (damageTakenCount == 0)
                return 0f;

            return totalDamageTaken / damageTakenCount;
        }
    }

    private void Start() {
        CombatEventHub.Instance.OnDamageTaken += RecordDamageTaken;
    }

    private void RecordDamageTaken(float damage) {
        if (!IsProgramActive)
            return;

        damageTakenCount++;
        totalDamageTaken += damage;

        if (damage > maxSingleDamage) {
            maxSingleDamage = damage;
        }

        Debug.Log(
            "===== SURVIVAL MONITOR =====\n" +
            "피격 횟수 : " + damageTakenCount +
            "\n누적 피해 : " + totalDamageTaken
        );
    }

    private void OnDestroy() {
        if (CombatEventHub.Instance == null)
            return;

        CombatEventHub.Instance.OnDamageTaken -= RecordDamageTaken;
    }
}