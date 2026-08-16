using System;
using System.Collections;
using System.Collections.Generic;
using Map;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMainInfoPanel : BasePanel
{
	public TextMeshProUGUI ResourceText;
	
	public TextMeshProUGUI UnitCountText;
	
	public UnitDetailsPanel UnitDetailsPanel;

	public UnitBuildPanel UnitBuildPanel;
	
	public UnitBuildablePanel UnitBuildablePanel;

	public Button NextRoundButton;
	
	public Button GameOverButton;
	
	public TMP_Text MessageText;
	
	public CanvasGroup MessagePanel;
	
	public override void Init()
	{
		UpdateResourceText();
		UpdateUnitCountText();
		UnitDetailsPanel.Init();
		UnitBuildPanel.Init();
		UnitBuildablePanel.Init();
		NextRoundButton.onClick.AddListener((() =>
		{
			GameManager.Event.Broadcast(HexEvents.NextRound.ToString(), GameEventParameter.Empty);
		}));
		GameOverButton.onClick.AddListener(() =>
		{
			GameManager.Event.Broadcast(HexEvents.GameOver.ToString(), GameEventParameter.Empty);
		});
		MessageText.text = "Message";
		MessagePanel.alpha = 0;
	}

	protected override void Awake()
	{
		base.Awake();
		
		GameManager.Event.Register(HexEvents.ResourceChange.ToString(), new GameEvent<int>(UpdateResource));
		
		GameManager.Event.Register(HexEvents.UnitCountChange.ToString(), new GameEvent<int>(UpdateUnitCount));
	}

	private void OnDestroy()
	{
		GameManager.Event.Unregister(HexEvents.ResourceChange.ToString(), new GameEvent<int>(UpdateResource));
		
		GameManager.Event.Unregister(HexEvents.UnitCountChange.ToString(), new GameEvent<int>(UpdateUnitCount));
	}

	void UpdateResource(int value)
	{
		GameManager.RunTimeData.ResourceCount += value;
		UpdateResourceText();
	}
	void UpdateResourceText()
	{
		ResourceText.text = GameManager.RunTimeData.ResourceCount.ToString() + " / " + GameManager.RunTimeData.maxResourceCount.ToString();
	}

	void UpdateUnitCount(int value)
	{
		GameManager.RunTimeData.UnitCount -= value;
		UpdateUnitCountText();
	}
	void UpdateUnitCountText()
	{
		UnitCountText.text = GameManager.RunTimeData.UnitCount.ToString() + " / " + GameManager.RunTimeData.maxUnitCount.ToString();
	}
}
