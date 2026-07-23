using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
	public bool firstTimeEnterGame;
	
	public bool completeGameInitialze;
	
	public bool sceneControllerInitialFinish;
	
	public bool readyToActiveLoadedScene;
	
	public bool isPause => GameManager.UI.IsShow<PauseMenuPanel>();
	
	public SceneInstance loadedScene 
	{
		get { return _loadedScene; }
		set {
			previousSceneName = _loadedScene.Scene.name;
			if (string.IsNullOrEmpty(previousSceneName)) 
			{
				previousSceneName = SceneManager.GetActiveScene().name;
			}
			_loadedScene = value; 
		}
	}
	private SceneInstance _loadedScene;
	private string previousSceneName;
	
	public PlayerDataInstance playerData;
	
	public PlayerDataEntitySO PlayerInitialData;
	
	public HexGrid currentActiveHexGrid;
	
	// 所有单位
	private PlayerUnitsData playerUnitsData;
	public Dictionary<string, PlayerUnitDataEntity> PlayerUnitsData = new Dictionary<string, PlayerUnitDataEntity>();
	
	private EnemiesData enemiesData;
	public Dictionary<string, EnemyDataEntity> EnemyData = new Dictionary<string, EnemyDataEntity>();
	
	private GameBuffData gameBuffData;
	public Dictionary<int, GameBuffDataEntity> GameBuffData = new Dictionary<int, GameBuffDataEntity>();
	
	private GameSoundDataSO gameSoundData;
	public Dictionary<string, GameSoundGroupDataSO> GameSoundData = new Dictionary<string, GameSoundGroupDataSO>();
	
	public static GameAssetLoader AssetLoader => mGameAssetLoader;
	private static GameAssetLoader mGameAssetLoader;
	
	public static GameRunTimeManager RunTimeData => mGameRunTimeManager;
	private static GameRunTimeManager mGameRunTimeManager;
	
	public static GameCameraManager Camera => mGameCameraManager;
	private static GameCameraManager mGameCameraManager;
	
	public static GameFilesManager Files => mGameFilesManager;
	private static GameFilesManager mGameFilesManager;
	
	public static GameTimeScaleManager TimeScale => mGameTimeScaleManager;
	private static GameTimeScaleManager mGameTimeScaleManager;
	
	public static GameInputManager Input=>mGameInputManager;
	private static GameInputManager mGameInputManager;
	
	public static GameUIManager UI => mGameUIManager;
	private static GameUIManager mGameUIManager;
	
	public static GameEventManager Event => mGameEventManager;
	private static GameEventManager mGameEventManager;
	
	public static GameSceneManager Scene => mGameSceneManager;
	private static GameSceneManager mGameSceneManager;
	
	public static GamePropsBehaviorManager PropBehavior => mGamePropsBehaviorManager;
	private static GamePropsBehaviorManager mGamePropsBehaviorManager;
	
	public static GameBuffManager Buff => mGameBuffManager;
	private static GameBuffManager mGameBuffManager;
	
	public static GameMessageManager Message => mGameMessageManager;
	private static GameMessageManager mGameMessageManager;
	
	public static GameAudioManager Audio => mGameAudioManager;
	private static GameAudioManager mGameAudioManager;
	[Header("Audio")]
	public AudioSource BGMAudioSource;
	public AudioSource UIAudioSource;
	public AudioSource SceneAudioSource;

	protected override void Awake()
	{
		base.Awake();
		DontDestroyOnLoad(gameObject);
		mGameEventManager = new GameEventManager();
		mGameRunTimeManager = new GameRunTimeManager(currentActiveHexGrid);
		mGameAssetLoader = new GameAssetLoader();
		mGameFilesManager = new GameFilesManager();
		mGameInputManager = new GameInputManager();
		mGameUIManager = new GameUIManager();
		mGameTimeScaleManager = new GameTimeScaleManager();
		mGameSceneManager = new GameSceneManager();
		mGameCameraManager = new GameCameraManager();
		mGameBuffManager = new GameBuffManager();
		mGamePropsBehaviorManager = new GamePropsBehaviorManager();
		mGameMessageManager = new GameMessageManager();
		mGameAudioManager = new GameAudioManager(BGMAudioSource,SceneAudioSource,UIAudioSource);
		// GameManager.Event.Register("SceneChange", new GameEvent<string>(OnSceneChanged));
	}

	private async void Start()
	{
		// player unit data
		playerUnitsData = await AssetLoader.LoadAsset<PlayerUnitsData>("PlayerUnitsData");
		foreach (var data in playerUnitsData.data)
		{
			PlayerUnitsData.Add(data.id, data);
		}
		
		// enemy data
		enemiesData = await AssetLoader.LoadAsset<EnemiesData>("EnemiesData");
		foreach (var data in enemiesData.data)
		{
			EnemyData.Add(data.Id, data);
		}
		
		// game buff
		// gameBuffData = await AssetLoader.LoadAsset<GameBuffData>("GameBuffData");
		// foreach (var data in gameBuffData.gameBuffDatas)
		// {
		// 	GameBuffData.Add(data.Id, data);
		// }
		
		// sound data
		gameSoundData = await AssetLoader.LoadAsset<GameSoundDataSO>("GameSoundData");
		foreach (var group in gameSoundData.gameSoundGroups)
		{
			GameSoundData.Add(group.GroupName, group);
			group.Init();
		}
		
		Audio.PlayBGM("GameStart");
	}

	private void Update()
	{
		if (readyToActiveLoadedScene)
		{
			Scene.OnSceneExit(previousSceneName);
			AsyncOperation handle = loadedScene.ActivateAsync();
			handle.completed += (ao) =>
			{
				if (ao.isDone)
				{
					Debug.Log(previousSceneName + " -> " + loadedScene.Scene.name);
					// GameManager.Event.Broadcast("SceneChange");
				}
			};
			readyToActiveLoadedScene = false;
		}
		Audio.ProcessBGM();
	}

	private void LateUpdate()
	{
		Input.ResetAllButtonValueOnLateUpdate();
	}
	
	// 场景改变时， 调用新场景OnSceneEnter
	private async void OnSceneChanged(string newScene)
	{
		await UniTask.WaitUntil(()=>sceneControllerInitialFinish);
		Scene.OnSceneEnter(newScene);
		sceneControllerInitialFinish = false;
	}
}
