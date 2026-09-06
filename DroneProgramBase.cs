using UnityEngine;

public abstract class DroneProgramBase : MonoBehaviour {
    [Header("Drone Program")]

    [SerializeField]
    private string programName = "Program";

    [SerializeField]
    private int cost = 1;

    [SerializeField]
    private bool activeAtStart = true;

    public string ProgramName => programName;

    public int Cost => cost;

    public bool IsProgramActive { get; private set; }

    protected virtual void Awake() {
        IsProgramActive = activeAtStart;
    }

    public void SetProgramActive(bool active) {
        IsProgramActive = active;

        Debug.Log(
            ProgramName +
            (active ? " : ON" : " : OFF")
        );
    }
}