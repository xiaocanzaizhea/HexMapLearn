using System;
using System.Collections;
using System.Collections.Generic;
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
			GameManager.Event.Broadcast(Events.NextRound.ToString(), GameEventParameter.Empty);
		}));
		MessageText.text = "Message";
		MessagePanel.alpha = 0;
	}

	protected override void Awake()
	{
		base.Awake();
		
		GameManager.Event.Register(Events.ResourceChange.ToString(), new GameEvent<int>(UpdateResource));
		
		GameManager.Event.Register(Events.UnitCountChange.ToString(), new GameEvent<int>(UpdateUnitCount));
	}

	private void OnDestroy()
	{
		GameManager.Event.Unregister(Events.ResourceChange.ToString(), new GameEvent<int>(UpdateResource));
		
		GameManager.Event.Unregister(Events.UnitCountChange.ToString(), new GameEvent<int>(UpdateUnitCount));
	}

	void UpdateResource(int value)
	{
		GameManager.RunTimeData.ResourceCount -= value;
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
