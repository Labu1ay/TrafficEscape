using UnityEngine;

public class Cell : MonoBehaviour {
    
    public bool IsFinish;
    public CarController Car;
    public bool IsCrossroad;
    public Vector2Int Position { get; private set; }

    private void Awake() {
        Position = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z));
        FindObjectOfType<GameController>().AddCell(Position, this);
    }
}