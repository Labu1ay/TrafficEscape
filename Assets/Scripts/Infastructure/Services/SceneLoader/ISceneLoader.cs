using System;

public interface ISceneLoader : IService {
    void Load(string name, Action callback = null);
}