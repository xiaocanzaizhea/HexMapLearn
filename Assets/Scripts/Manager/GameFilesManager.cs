using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFilesManager
{
#if UNITY_EDITOR
	private static string filePath = Application.dataPath + "/gameFile.da";
#else
    private static string filePath = Application.persistentDataPath + "/gameFile.da";
#endif
    [SerializeField]
    public GameFileData gameFileData;
    public GameFile CurrentGameFile => gameFileData.gameFiles.Find(gf=>gf.fileName == gameFileData.currentGameFile);

	public bool SaveGameFiles()
    {
		UpdateGameFileData();
		string jsonData = JsonUtility.ToJson(gameFileData, true);
        File.WriteAllText(filePath, jsonData);
		GameManager.Instance.firstTimeEnterGame = false;
		return true;
    }

    public bool LoadGameFiles() 
    {
        if (File.Exists(filePath))
        {
			string data = File.ReadAllText(filePath);
			gameFileData = JsonUtility.FromJson<GameFileData>(data);
            if (gameFileData.gameFiles.Count > 0)
            {
				gameFileData.currentGameFile = gameFileData.gameFiles.Last().fileName;
            }
        }
        else 
        {
            gameFileData = new GameFileData();
            gameFileData.gameFiles.Add(
                new GameFile
                {
                    fileName = "New Start",
                    createTime = DateTime.Now.ToString(),
                    playerData = GameManager.Instance.PlayerInitialData.GetInstance()
                }
            ) ;
			gameFileData.currentGameFile = gameFileData.gameFiles.Last().fileName;
            GameManager.Instance.firstTimeEnterGame = true;
		}
        // GameManager.Instance.playerData = CurrentGameFile.playerData;
        return true;
    }

    public void UpdateGameFileData() 
    {
        // CurrentGameFile.playerData = GameManager.Instance.playerData;
    }

    public void ResetGameFile() 
    {
        gameFileData.gameFiles.Clear();
		gameFileData.gameFiles.Add(
				new GameFile
				{
					fileName = "New Start",
					createTime = DateTime.Now.ToString(),
					playerData = GameManager.Instance.PlayerInitialData.GetInstance()
				}
			);
		gameFileData.currentGameFile = gameFileData.gameFiles.Last().fileName;
	}
}

[System.Serializable]
public class GameFileData 
{
    public string currentGameFile;
    public List<GameFile> gameFiles=new List<GameFile>();
}