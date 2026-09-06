using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class BurstComparisonHUD : MonoBehaviour {
    [SerializeField]
    private BurstAnalyzer burstAnalyzer;

    [SerializeField]
    private TMP_Text compareText;

    private void Update() {
        if (burstAnalyzer == null || compareText == null)
            return;

        if (burstAnalyzer.ResultCount < 2) {
            compareText.text =
                "===== BURST COMPARISON =====\n\n" +
                "Need at least 2 Burst records.";

            return;
        }

        BurstResult first = burstAnalyzer.GetResult(0);
        BurstResult second = burstAnalyzer.GetResult(1);

        StringBuilder text = new StringBuilder();

        text.AppendLine("===== BURST COMPARISON =====");
        text.AppendLine();

        text.AppendLine("[ BURST #1 ]");
        text.AppendLine("Damage : " + first.totalDamage.ToString("N0"));
        text.AppendLine("DPS : " + first.dps.ToString("N0"));

        text.AppendLine();

        text.AppendLine("[ BURST #2 ]");
        text.AppendLine("Damage : " + second.totalDamage.ToString("N0"));
        text.AppendLine("DPS : " + second.dps.ToString("N0"));

        text.AppendLine();

        float damageDifference =
            second.totalDamage - first.totalDamage;

        float dpsDifference =
            second.dps - first.dps;

        text.AppendLine("[ DIFFERENCE ]");

        text.AppendLine(
            "Damage : " +
            FormatDifference(damageDifference)
        );

        text.AppendLine(
            "DPS : " +
            FormatDifference(dpsDifference)
        );

        text.AppendLine();
        text.AppendLine("===== SKILL DIFFERENCE =====");

        CompareSkills(first, second, text);

        compareText.text = text.ToString();
    }

    private void CompareSkills(
        BurstResult first,
        BurstResult second,
        StringBuilder text
    ) {
        Dictionary<string, BurstSkillData> firstSkills
            = ConvertToDictionary(first);

        Dictionary<string, BurstSkillData> secondSkills
            = ConvertToDictionary(second);

        HashSet<string> allSkillNames =
            new HashSet<string>();

        foreach (string skillName in firstSkills.Keys) {
            allSkillNames.Add(skillName);
        }

        foreach (string skillName in secondSkills.Keys) {
            allSkillNames.Add(skillName);
        }

        foreach (string skillName in allSkillNames) {
            BurstSkillData firstSkill = null;
            BurstSkillData secondSkill = null;

            firstSkills.TryGetValue(
                skillName,
                out firstSkill
            );

            secondSkills.TryGetValue(
                skillName,
                out secondSkill
            );

            int firstUse =
                firstSkill != null
                ? firstSkill.useCount
                : 0;

            int secondUse =
                secondSkill != null
                ? secondSkill.useCount
                : 0;

            float firstDamage =
                firstSkill != null
                ? firstSkill.totalDamage
                : 0f;

            float secondDamage =
                secondSkill != null
                ? secondSkill.totalDamage
                : 0f;

            text.AppendLine();
            text.AppendLine("[" + skillName + "]");

            text.AppendLine(
                "#1 Use : " +
                firstUse +
                " / Damage : " +
                firstDamage.ToString("N0")
            );

            text.AppendLine(
                "#2 Use : " +
                secondUse +
                " / Damage : " +
                secondDamage.ToString("N0")
            );

            text.AppendLine(
                "Use Difference : " +
                FormatDifference(secondUse - firstUse)
            );

            text.AppendLine(
                "Damage Difference : " +
                FormatDifference(
                    secondDamage - firstDamage
                )
            );
        }
    }

    private Dictionary<string, BurstSkillData>
        ConvertToDictionary(BurstResult result) {
        Dictionary<string, BurstSkillData> dictionary =
            new Dictionary<string, BurstSkillData>();

        foreach (BurstSkillData skill in result.skillResults) {
            dictionary[skill.skillName] = skill;
        }

        return dictionary;
    }

    private string FormatDifference(float value) {
        if (value > 0) {
            return "+" +
                value.ToString("N0");
        }

        return value.ToString("N0");
    }

    private string FormatDifference(int value) {
        if (value > 0) {
            return "+" +
                value.ToString();
        }

        return value.ToString();
    }
}