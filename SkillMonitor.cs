using System.Collections.Generic;
using UnityEngine;

public class SkillData {
    public int useCount = 0;
    public int hitCount = 0;
    public float totalDamage = 0f;
}

public class SkillMonitor : DroneProgramBase {
    private Dictionary<string, SkillData> skillRecords
        = new Dictionary<string, SkillData>();

    void Start() {
        CombatEventHub.Instance.OnSkillUsed += RecordSkillUse;
        CombatEventHub.Instance.OnDamageDealt += RecordDamage;
    }

    void Update() {
        // O키를 누르면 전체 스킬 기록 출력
        if (Input.GetKeyDown(KeyCode.O)) {
            PrintSkillResults();
        }
    }

    private void RecordSkillUse(string skillName) {
        if (!IsProgramActive)
            return;

        CreateSkillData(skillName);

        skillRecords[skillName].useCount++;
    }

    private void RecordDamage(string skillName, float damage) {
        if (!IsProgramActive)
            return;

        CreateSkillData(skillName);

        skillRecords[skillName].hitCount++;
        skillRecords[skillName].totalDamage += damage;
    }

    private void CreateSkillData(string skillName) {
        if (!skillRecords.ContainsKey(skillName)) {
            skillRecords.Add(skillName, new SkillData());
        }
    }

    private void PrintSkillResults() {
        if (skillRecords.Count == 0) {
            Debug.Log("저장된 스킬 기록이 없습니다.");
            return;
        }

        string resultText = "===== 전체 스킬 기록 =====\n";

        foreach (KeyValuePair<string, SkillData> skill in skillRecords) {
            resultText +=
                skill.Key + "\n" +
                "사용 : " + skill.Value.useCount + "회\n" +
                "적중 : " + skill.Value.hitCount + "회\n" +
                "총 데미지 : " + skill.Value.totalDamage + "\n\n";
        }

        Debug.Log(resultText);
    }

    private void OnDestroy() {
        if (CombatEventHub.Instance == null)
            return;

        CombatEventHub.Instance.OnSkillUsed -= RecordSkillUse;
        CombatEventHub.Instance.OnDamageDealt -= RecordDamage;
    }
}