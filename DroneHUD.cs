using System.Text;
using TMPro;
using UnityEngine;

public class DroneHUD : MonoBehaviour {
    [SerializeField]
    private TMP_Text droneText;

    [SerializeField]
    private BurstAnalyzer burstAnalyzer;

    private void Update() {
        if (droneText == null || burstAnalyzer == null)
            return;

        StringBuilder text = new StringBuilder();

        text.AppendLine("===== TACTICAL DRONE =====");
        text.AppendLine();

        text.AppendLine(
            "Burst Trigger : " +
            burstAnalyzer.BurstStartSkill
            );

        text.AppendLine();

        // 현재 극딜 측정 중
        if (burstAnalyzer.IsMeasuring) {
            text.AppendLine(
                "BURST #" + burstAnalyzer.BurstCount
            );

            text.AppendLine("Status : Measuring");

            text.AppendLine(
                "Time : " +
                burstAnalyzer.Timer.ToString("F1") +
                " / " +
                burstAnalyzer.MeasureTime.ToString("F1")
            );

            text.AppendLine(
                "Total Damage : " +
                burstAnalyzer.TotalDamage.ToString("N0")
            );
        }

        // 측정 중이 아닐 때
        else {
            BurstResult result =
                burstAnalyzer.LastResult;

            if (result == null) {
                text.AppendLine(
                    "Use " +
                    burstAnalyzer.BurstStartSkill +
                    " to start Burst Analysis."
                    );
            }
            else {
                text.AppendLine(
                    "BURST #" + result.burstNumber
                );

                text.AppendLine("Status : Complete");

                text.AppendLine();

                text.AppendLine(
                    "Total Damage : " +
                    result.totalDamage.ToString("N0")
                );

                text.AppendLine(
                    "Average DPS : " +
                    result.dps.ToString("N0")
                );

                text.AppendLine();
                text.AppendLine("===== SKILL ANALYSIS =====");

                foreach (
                    BurstSkillData skill
                    in result.skillResults
                ) {
                    text.AppendLine();

                    text.AppendLine(
                        "[" + skill.skillName + "]"
                    );

                    text.AppendLine(
                        "Use : " +
                        skill.useCount
                    );

                    text.AppendLine(
                        "Hit : " +
                        skill.hitCount
                    );

                    text.AppendLine(
                        "Damage : " +
                        skill.totalDamage.ToString("N0")
                    );
                }
            }
        }

        droneText.text = text.ToString();
    }
}