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
	
	public bool isPause => UI.IsShow<PauseMenuPanel>();
	
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
	
	public PlayerDataInstance playerDataInstance;
	
	public PlayerDataEntitySO PlayerInitialData;
	
	// 单位查找表，所有的单位
	private UnitDataList playerDataList;
	public Dictionary<string, HexUnitDataSO> PlayerDataDic = new Dictionary<string, HexUnitDataSO>();
	
	private UnitDataList enemyDataList;
	public Dictionary<string, HexUnitDataSO> EnemyData = new Dictionary<string, HexUnitDataSO>();
	
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
		mGameRunTimeManager = new GameRunTimeManager();
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
		Event.Register(HexEvents.SceneChange.ToString(), new GameEvent<string>(OnSceneChanged));
	}

	private async void Start()
	{
		// player unit data
		playerDataList = await AssetLoader.LoadAsset<UnitDataList>("PlayerDataList");
		foreach (var data in playerDataList.data)
		{
			PlayerDataDic.Add(data.id, data);
		}
		
		// enemy data
		enemyDataList = await AssetLoader.LoadAsset<UnitDataList>("EnemyDataList");
		foreach (var data in enemyDataList.data)
		{
			EnemyData.Add(data.id, data);
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
					Event.Broadcast(HexEvents.SceneChange.ToString(), 
						new GameEventParameter<string>(loadedScene.Scene.name));
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

	public HexUnitDataSO GetUnitDataFromId(string id, int teamId)
	{
		if (teamId == 0)
		{
			return PlayerDataDic[id];
		}
		else
		{
			return EnemyData[id];
		}
	}
}
