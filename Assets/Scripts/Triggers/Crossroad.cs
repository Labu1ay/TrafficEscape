using UnityEngine;

public class Crossroad : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        other.TryGetComponent(out CarController carController);
        if (!carController || carController.CurrentCarState != CarState.MOVING) return;
        
        carController.CarRollback.SavePosition();
        
        switch (carController.CurrentTurn) { 
            case Turn.DIRECTLY: return;
            case Turn.LEFT: carController.CarMove.RotateCar(-1); break;
            case Turn.RIGHT: carController.CarMove.RotateCar(1); break;
            case Turn.DOUBLE_LEFT: carController.CarMove.RotateCar(-1, Turn.LEFT); break;
            case Turn.DOUBLE_RIGHT: carController.CarMove.RotateCar(1, Turn.RIGHT); break;
        }
    }
}