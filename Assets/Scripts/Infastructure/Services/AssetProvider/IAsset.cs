using UnityEngine;

public interface IAsset : IService {
    GameObject Instantiate(string path);
    GameObject Instantiate(string path, Transform parent);
}

