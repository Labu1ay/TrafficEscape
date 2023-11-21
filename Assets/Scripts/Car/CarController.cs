using UnityEngine;

public enum CarState {
    IDLE = 0,
    MOVING = 1,
    ACCIDENT = 2,
    ROLLBACK = 3
}

[RequireComponent(typeof(CarMove))]
public class CarController : MonoBehaviour {
    [field: SerializeField] public Turn CurrentTurn { get; private set; }
    [field: SerializeField] public CarMove CarMove { get; private set; }
    public Turn StartTurn { get; private set; }
    [field: SerializeField] public CarState CurrentCarState { get; private set; }                      //To do

    private void OnValidate() {
        CarMove ??= GetComponent<CarMove>();
    }

    private void Start() {
        StartTurn = CurrentTurn;
        SetCurrentCarState(CarState.IDLE);
        
        CarMove.Init(this);
    }

    public void SetCurrentTurn(Turn turn) => CurrentTurn = turn;
    public void SetCurrentCarState(CarState state) => CurrentCarState = state;
}