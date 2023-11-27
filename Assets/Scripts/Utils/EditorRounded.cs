using UnityEngine;

[ExecuteInEditMode]
public class EditorRounded : MonoBehaviour {
    private void OnEnable() => UnityEditor.EditorApplication.update += OnUpdate;
    private void OnDisable() => UnityEditor.EditorApplication.update -= OnUpdate;

    private void OnUpdate() {
        if (!Application.isPlaying && transform.hasChanged) {
            RoundUp();
            transform.hasChanged = false;
        }
    }

    private void RoundUp() {
        Vector3 roundedPosition = new Vector3(Mathf.Round(transform.position.x), 0f, Mathf.Round(transform.position.z));
        transform.position = roundedPosition;
    }
    
    private void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position, new Vector3(1f, 0f, 1f));
    }
}