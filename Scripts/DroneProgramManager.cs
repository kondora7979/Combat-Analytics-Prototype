using UnityEngine;

public class DroneProgramManager : MonoBehaviour {
    private BurstAnalyzer burstAnalyzer;
    private SkillMonitor skillMonitor;
    private SurvivalMonitor survivalMonitor;
    private DroneHardware hardware;

    public int MaxCost {
        get {
            if (hardware == null)
                return 0;

            return hardware.MaxCost;
        }
    }

    public int CurrentCost => GetCurrentCost();

    public BurstAnalyzer BurstAnalyzer => burstAnalyzer;
    public SkillMonitor SkillMonitor => skillMonitor;
    public SurvivalMonitor SurvivalMonitor => survivalMonitor;
    public DroneHardware Hardware => hardware;

    private void Start() {
        burstAnalyzer = GetComponent<BurstAnalyzer>();
        skillMonitor = GetComponent<SkillMonitor>();
        survivalMonitor = GetComponent<SurvivalMonitor>();
        hardware = GetComponent<DroneHardware>();

        Debug.Log("BurstAnalyzer 찾음 : " + (burstAnalyzer != null));
        Debug.Log("SkillMonitor 찾음 : " + (skillMonitor != null));
        Debug.Log("SurvivalMonitor 찾음 : " + (survivalMonitor != null));
        Debug.Log("DroneHardware 찾음 : " + (hardware != null));
    }

    private void Update() {
        // 1 : Burst Analyzer
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            ToggleProgram(burstAnalyzer);
        }

        // 2 : Skill Monitor
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            ToggleProgram(skillMonitor);
        }

        // 3 : Survival Monitor
        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            ToggleProgram(survivalMonitor);
        }

        // U : Hardware Upgrade
        if (Input.GetKeyDown(KeyCode.U)) {
            hardware.Upgrade();
        }
    }

    private int GetCurrentCost() {
        int total = 0;

        if (burstAnalyzer != null &&
            burstAnalyzer.IsProgramActive) {
            total += burstAnalyzer.Cost;
        }

        if (skillMonitor != null &&
            skillMonitor.IsProgramActive) {
            total += skillMonitor.Cost;
        }

        if (survivalMonitor != null &&
            survivalMonitor.IsProgramActive) {
            total += survivalMonitor.Cost;
        }

        return total;
    }

    private void ToggleProgram(DroneProgramBase program) {
        if (program == null)
            return;

        // ON 상태라면 OFF
        if (program.IsProgramActive) {
            program.SetProgramActive(false);

            Debug.Log(
                "Current Cost : " +
                GetCurrentCost() +
                " / " +
                MaxCost
            );

            return;
        }

        // 새로 켰을 때 필요한 Cost 계산
        int nextCost =
            GetCurrentCost() +
            program.Cost;

        // 최대 Cost 초과
        if (nextCost > MaxCost) {
            Debug.Log(
                "Cost 부족! " +
                nextCost +
                " / " +
                MaxCost
            );

            return;
        }

        program.SetProgramActive(true);

        Debug.Log(
            "Current Cost : " +
            GetCurrentCost() +
            " / " +
            MaxCost
        );
    }
}
