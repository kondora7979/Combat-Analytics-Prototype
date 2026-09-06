using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class BurstResult {
    public int burstNumber;
    public float totalDamage;
    public float dps;

    public List<BurstSkillData> skillResults;

    public BurstResult(
        int burstNumber,
        float totalDamage,
        float dps,
        List<BurstSkillData> skillResults
    ) {
        this.burstNumber = burstNumber;
        this.totalDamage = totalDamage;
        this.dps = dps;
        this.skillResults = skillResults;
    }
}

[System.Serializable]
public class BurstSkillData {
    public string skillName;
    public int useCount;
    public int hitCount;
    public float totalDamage;

    public BurstSkillData(string skillName) {
        this.skillName = skillName;
        useCount = 0;
        hitCount = 0;
        totalDamage = 0f;
    }
}

public class BurstAnalyzer : DroneProgramBase {
    private Dictionary<string, BurstSkillData> currentSkillRecords
    = new Dictionary<string, BurstSkillData>();

    private List<BurstResult> burstResults = new List<BurstResult>();
    // 극딜 측정 시작 기준 스킬
    private string burstStartSkill = "OriginSkill";

    public string BurstStartSkill => burstStartSkill;

    // 극딜 측정 시간
    private float measureTime = 10f;

    // 현재 측정 중인지
    private bool isMeasuring = false;

    // 측정한 시간
    private float timer = 0f;

    // 측정 중 누적 데미지
    private float totalDamage = 0f;

    // 몇 번째 극딜인지
    private int burstCount = 0;

    public int ResultCount {
        get {
            return burstResults.Count;
        }
    }

    public BurstResult GetResult(int index) {
        if (index < 0 || index >= burstResults.Count)
            return null;

        return burstResults[index];
    }

    public bool IsMeasuring => isMeasuring;
    public float Timer => timer;
    public float MeasureTime => measureTime;
    public float TotalDamage => totalDamage;
    public int BurstCount => burstCount;

    public BurstResult LastResult {
        get {
            if (burstResults.Count == 0)
                return null;

            return burstResults[burstResults.Count - 1];
        }
    }

    void Start() {
        // "스킬을 사용했다"는 정보 받기
        CombatEventHub.Instance.OnSkillUsed += HandleSkillUsed;

        // "데미지를 줬다"는 정보 받기
        CombatEventHub.Instance.OnDamageDealt += RecordDamage;
    }

    void Update() {
        if (!IsProgramActive)
            return;

        // 극딜 시작 스킬 선택
        if (Input.GetKeyDown(KeyCode.F1)) {
            SetBurstStartSkill("NormalAttack");
        }

        if (Input.GetKeyDown(KeyCode.F2)) {
            SetBurstStartSkill("StrongAttack");
        }

        if (Input.GetKeyDown(KeyCode.F3)) {
            SetBurstStartSkill("OriginSkill");
        }

        if (isMeasuring) {
            timer += Time.deltaTime;

            if (timer >= measureTime) {
                EndBurst();
            }
        }

        if (Input.GetKeyDown(KeyCode.P)) {
            PrintAllResults();
        }
    }

    private void SetBurstStartSkill(string skillName) {
        burstStartSkill = skillName;

        Debug.Log(
            "극딜 시작 스킬 변경 : " +
            burstStartSkill
        );
    }

    // 사용한 스킬이 극딜 시작 스킬인지 확인
    private void HandleSkillUsed(string skillName) {
        if (!IsProgramActive)
            return;

        // 기준 스킬을 사용하면 극딜 측정 시작
        if (skillName == burstStartSkill && !isMeasuring) {
            StartBurst();
        }

        // 측정 중이 아니라면 기록하지 않음
        if (!isMeasuring)
            return;

        CreateBurstSkillData(skillName);

        currentSkillRecords[skillName].useCount++;
    }

    // 극딜 측정 시작
    private void StartBurst() {
        burstCount++;

        isMeasuring = true;
        timer = 0f;
        totalDamage = 0f;

        currentSkillRecords.Clear();

        Debug.Log(
            "===== 극딜 " +
            burstCount +
            "회차 측정 시작 ====="
        );
    }

    // 데미지 기록
    private void RecordDamage(string skillName, float damage) {
        if (!IsProgramActive)
            return;

        if (!isMeasuring)
            return;

        totalDamage += damage;

        CreateBurstSkillData(skillName);

        currentSkillRecords[skillName].hitCount++;
        currentSkillRecords[skillName].totalDamage += damage;

        Debug.Log(
            "[측정 중] " +
            skillName +
            " / +" +
            damage +
            " / 누적 : " +
            totalDamage
        );
    }

    private void CreateBurstSkillData(string skillName) {
        if (!currentSkillRecords.ContainsKey(skillName)) {
            currentSkillRecords.Add(
                skillName,
                new BurstSkillData(skillName)
            );
        }
    }

    // 극딜 측정 종료
    private void EndBurst() {
        isMeasuring = false;

        float dps = totalDamage / measureTime;

        List<BurstSkillData> skillResults
            = new List<BurstSkillData>();

        foreach (BurstSkillData skill in currentSkillRecords.Values) {
            skillResults.Add(skill);
        }

        BurstResult result = new BurstResult(
            burstCount,
            totalDamage,
            dps,
            skillResults
        );

        burstResults.Add(result);

        Debug.Log(
            "===== 극딜 " +
            burstCount +
            "회차 결과 =====\n" +
            "총 데미지 : " +
            totalDamage +
            "\n평균 DPS : " +
            dps +
            "\n저장된 극딜 기록 : " +
            burstResults.Count +
            "개"
        );
    }

    private void OnDestroy() {
        if (CombatEventHub.Instance == null)
            return;

        CombatEventHub.Instance.OnSkillUsed -= HandleSkillUsed;
        CombatEventHub.Instance.OnDamageDealt -= RecordDamage;
    }
    private void PrintAllResults() {
        if (burstResults.Count == 0) {
            Debug.Log("저장된 극딜 기록이 없습니다.");
            return;
        }

        string resultText = "===== 전체 극딜 기록 =====\n\n";

        foreach (BurstResult result in burstResults) {
            resultText +=
                "◆ " + result.burstNumber + "회차\n" +
                "총 데미지 : " + result.totalDamage + "\n" +
                "평균 DPS : " + result.dps + "\n";

            foreach (BurstSkillData skill in result.skillResults) {
                resultText +=
                    "\n[" + skill.skillName + "]\n" +
                    "사용 : " + skill.useCount + "회\n" +
                    "적중 : " + skill.hitCount + "회\n" +
                    "데미지 : " + skill.totalDamage + "\n";
            }

            resultText += "\n--------------------\n";
        }

        Debug.Log(resultText);
    }
}
