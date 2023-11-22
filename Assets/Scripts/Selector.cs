using UnityEngine;

public class Selector : MonoBehaviour {
    private Camera _camera;

    private void Start() {
        _camera = GetComponent<Camera>();
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit)) {
                if (!hit.transform.CompareTag("Car")) return;
                
                hit.transform.TryGetComponent(out CarController carController);
                if(!carController || carController.CurrentCarState != CarState.IDLE) return;

                carController.CarMove.StartMove();
                
            }
        }
    }
}