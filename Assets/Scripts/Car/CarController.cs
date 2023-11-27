using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour {
    [field: SerializeField] public Turn CurrentTurn { get; private set; }
    public Turn StartTurn { get; private set; }
    public List<Vector2Int> Position { get; private set; } = new List<Vector2Int>();
    
    [SerializeField] private int cells = 2;


    private void Awake() {
        SetPositionCells();
        FindObjectOfType<GameController>().AddCar(Position, this);
    }

    private void SetPositionCells() {
        for (int i = 0; i < cells; i++) {
            switch (transform.eulerAngles.y) {
                case 0f:
                    Position.Add(new Vector2Int(Mathf.RoundToInt(transform.position.x),
                        Mathf.RoundToInt(transform.position.z) - i));
                    break;
                case 90f:
                    Position.Add(new Vector2Int(Mathf.RoundToInt(transform.position.x) - i,
                        Mathf.RoundToInt(transform.position.z)));
                    break;
                case 180f:
                    Position.Add(new Vector2Int(Mathf.RoundToInt(transform.position.x),
                        Mathf.RoundToInt(transform.position.z) + i));
                    break;
                case 270f:
                    Position.Add(new Vector2Int(Mathf.RoundToInt(transform.position.x) + i,
                        Mathf.RoundToInt(transform.position.z)));
                    break;
            }
        }
    }

    private void Start() {
        StartTurn = CurrentTurn;
    }

}