using UnityEngine;

public class GameBootstrapper : MonoBehaviour, ICoroutineRunner {
    public static LoadingCurtain Curtain { get; private set; }
    
    private AllServices _services;
    
    private void Start() {
        _services = AllServices.Container;
        RegisterService();
        
        Curtain = _services.Single<IAsset>().Instantiate(MyPath.Curtain).GetComponent<LoadingCurtain>();
        Curtain.Hide();
        
        AllServices.Container.Single<ISceneLoader>().Load(Constants.GameSceneName);
        
        DontDestroyOnLoad(this);
    }
    
    private void RegisterService() {
        _services.RegisterSingle<ISceneLoader>(new SceneLoader(this));
        _services.RegisterSingle<IAsset>(new AssetProvider());
    }
}