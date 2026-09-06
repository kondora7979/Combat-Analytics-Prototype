using System.Text;
using TMPro;
using UnityEngine;

public class DroneProgramHUD : MonoBehaviour {
    [SerializeField]
    private DroneProgramManager programManager;

    [SerializeField]
    private TMP_Text programText;

    private void Update() {
        if (programManager == null || programText == null)
            return;

        BurstAnalyzer burst =
            programManager.BurstAnalyzer;

        SkillMonitor skill =
            programManager.SkillMonitor;

        SurvivalMonitor survival =
            programManager.SurvivalMonitor;

        if (burst == null ||
            skill == null ||
            survival == null) {
            return;
        }

        StringBuilder text =
            new StringBuilder();

        text.AppendLine("===== DRONE PROGRAM =====");
        text.AppendLine();

        text.AppendLine(
            "Hardware : " +
            programManager.Hardware.Grade
            );

        text.AppendLine(
            "Cost : " +
            programManager.CurrentCost +
            " / " +
            programManager.MaxCost
        );

        text.AppendLine();

        text.AppendLine(
            "[1] " +
            burst.ProgramName +
            "   " +
            GetStateText(burst) +
            "   Cost " +
            burst.Cost
        );

        text.AppendLine(
            "[2] " +
            skill.ProgramName +
            "   " +
            GetStateText(skill) +
            "   Cost " +
            skill.Cost
        );

        text.AppendLine(
            "[3] " +
            survival.ProgramName +
            "   " +
            GetStateText(survival) +
            "   Cost " +
            survival.Cost
        );

        if (survival.IsProgramActive) {
            text.AppendLine();

            text.AppendLine(
                "Hits Taken : " +
                survival.DamageTakenCount
            );

            text.AppendLine(
                "Damage Taken : " +
                survival.TotalDamageTaken.ToString("N0")
            );

            text.AppendLine(
                "Average Damage : " +
                survival.AverageDamage.ToString("N0")
            );

            text.AppendLine(
                "Max Hit : " +
                survival.MaxSingleDamage.ToString("N0")
            );
        }

        programText.text =
            text.ToString();
    }

    private string GetStateText(
        DroneProgramBase program
    ) {
        if (program.IsProgramActive)
            return "ON";

        return "OFF";
    }
}
