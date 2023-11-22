using UnityEngine;

[RequireComponent(typeof(CarController))]
public class CarDirection : MonoBehaviour {
    [SerializeField] private CarController _carController;

    private void OnValidate() {
        _carController ??= GetComponent<CarController>();
    }

    private void Start() {
        AllServices.Container.Single<IAsset>().Instantiate(GetPath(), transform);
    }
    
    private string GetPath() {
        if (_carController.CurrentTurn == Turn.LEFT) return "Left";
        if (_carController.CurrentTurn == Turn.RIGHT) return "Right";
        if (_carController.CurrentTurn == Turn.DOUBLE_LEFT) return "DoubleLeft";
        if (_carController.CurrentTurn == Turn.DOUBLE_RIGHT) return "DoubleRight";
        return "Directly";
    }
}