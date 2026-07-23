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
		ResourceText.text = "Resource Text";
		UnitCountText.text = "Unit Count";
		UnitDetailsPanel.Init();
		UnitBuildPanel.Init();
		UnitBuildablePanel.Init();
		NextRoundButton.onClick.AddListener((() =>
		{
			Debug.Log("Next Round");
		}));
		MessageText.text = "Message";
		MessagePanel.alpha = 0;
	}
}
