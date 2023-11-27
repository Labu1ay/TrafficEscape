using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    public Dictionary<Vector2Int, Cell> Cells = new Dictionary<Vector2Int, Cell>();
    public Dictionary<List<Vector2Int>, CarController> Cars = new Dictionary<List<Vector2Int>, CarController>();

    private void Start() {
        foreach (KeyValuePair<Vector2Int, Cell> cell in Cells) {
            KeyValuePair<Vector2Int, Cell> firstCell = cell;
            foreach (KeyValuePair<List<Vector2Int>, CarController> car in Cars) {
                for (int j = 0; j < car.Key.Count; j++) {
                    if (firstCell.Key == car.Key[j]) cell.Value.Car = car.Value;
                }
            }
            int i = 0;
            
            foreach (var tempCell in Cells) {
                KeyValuePair<Vector2Int, Cell> secondCell = tempCell;
                if (firstCell.Key == secondCell.Key) continue;

                if (Mathf.Abs(Vector2Int.Distance(firstCell.Key, secondCell.Key)) <= 1) i++;
            }

            if (i >= 3) {
                Debug.Log(i);
                cell.Value.IsCrossroad = true;
            }
        }
    }

    public void AddCell(Vector2Int position, Cell cell) => Cells.Add(position, cell);
    public void AddCar(List<Vector2Int> position, CarController car) => Cars.Add(position, car);
}