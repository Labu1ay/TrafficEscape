using System;
using UnityEngine;

public class Crossroad : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        other.TryGetComponent(out CarController car);
        if (!car || car.CurrentCarState != CarState.MOVING) return;
        car.GetComponent<CarRollback>().SavePosition();
        
        switch (car.CurrentTurn) { 
            case Turn.DIRECTLY: return;
            case Turn.LEFT: car.CarMove.RotateCar(-1); break;
            case Turn.RIGHT: car.CarMove.RotateCar(1); break;
            case Turn.DOUBLE_LEFT: car.CarMove.RotateCar(-1, Turn.LEFT); break;
            case Turn.DOUBLE_RIGHT: car.CarMove.RotateCar(1, Turn.RIGHT); break;
        }
    }

    private void OnTriggerExit(Collider other) {
        other.TryGetComponent(out CarController car);
        if (!car || car.CurrentCarState != CarState.MOVING) return;
        car.GetComponent<CarRollback>().SavePosition();
    }
}