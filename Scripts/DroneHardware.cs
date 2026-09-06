using UnityEngine;

public class DroneHardware : MonoBehaviour {
    public enum HardwareGrade {
        MkI,
        MkII,
        MkIII
    }

    [SerializeField]
    private HardwareGrade grade = HardwareGrade.MkI;

    public HardwareGrade Grade => grade;

    public int MaxCost {
        get {
            switch (grade) {
                case HardwareGrade.MkI:
                    return 6;

                case HardwareGrade.MkII:
                    return 8;

                case HardwareGrade.MkIII:
                    return 10;

                default:
                    return 6;
            }
        }
    }

    public void Upgrade() {
        if (grade == HardwareGrade.MkI) {
            grade = HardwareGrade.MkII;
        }
        else if (grade == HardwareGrade.MkII) {
            grade = HardwareGrade.MkIII;
        }
        else {
            Debug.Log("이미 최고 등급입니다.");
            return;
        }

        Debug.Log(
            "Hardware Upgrade : " +
            grade +
            " / Max Cost : " +
            MaxCost
        );
    }
}
