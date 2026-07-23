using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBuffManager
{
	// private List<GameBuffInstance> needUnregisterBuffs = new List<GameBuffInstance>();
	//
	// public void RegisterBuff(GameBuffDataEntity buff)
	// {
	// 	GameManager.Instance.playerData.gameBuffs.Add(buff.GetInstance());
	// }
	//
	// public void UnregisterBuff(GameBuffInstance instance)
	// {
	// 	GameBuffDataEntity entity = instance.GetBuffEntity();
	// 	if (entity.Interval == -1)
	// 	{
	// 		ApplyBuffModify(entity.Target, -entity.Value, GameManager.Instance.playerData);
	// 	}
	// 	GameManager.Instance.playerData.gameBuffs.Remove(instance);
	// }
 //
	//
	// public void ExecuteBuff(float deltaTime) 
	// {
	// 	PlayerDataInstance playerData = GameManager.Instance.playerData;
	// 	needUnregisterBuffs.Clear();
	// 	foreach (GameBuffInstance buff in GameManager.Instance.playerData.gameBuffs)
	// 	{
	// 		GameBuffDataEntity buffEntity = buff.GetBuffEntity();
	// 		if (buffEntity.Type == BuffType.NoLimit)
	// 		{
	// 			ExecuteNoLimitBuff(buffEntity,buff,playerData,deltaTime);
	// 		}
	// 		else 
	// 		{
	// 			ExecuteLimitBuff(buffEntity, buff, playerData, deltaTime);
	// 		}
 //        }
	// 	UnregisterBuffWhichNeeded();
	// }
 //
	// private void ExecuteNoLimitBuff(GameBuffDataEntity entity,GameBuffInstance instance, PlayerDataInstance playerData,float deltaTime)
	// {
	// 	if (entity.Interval == -1)
	// 	{
	// 		if (instance.timer == 0) 
	// 		{
	// 			instance.timer = 1;
	// 			ApplyBuffModify(entity.Target, entity.Value, playerData);
	// 		}
	// 	}
	// 	else 
	// 	{
	// 		if (instance.timer < entity.Interval)
	// 		{
	// 			instance.timer += deltaTime;
	// 		}
	// 		else 
	// 		{
	// 			instance.timer = 0;
	// 			ApplyBuffModify(entity.Target, entity.Value,playerData);
	// 		}
	// 	}
	// }
 //
	// private void ExecuteLimitBuff(GameBuffDataEntity entity, GameBuffInstance instance, PlayerDataInstance playerData, float deltaTime) 
	// {
	// 	if (entity.Interval == -1)
	// 	{
	// 		if (instance.timer == 0)
	// 		{
	// 			ApplyBuffModify(entity.Target, entity.Value, playerData);
	// 		}
	// 		else if (instance.timer >= entity.LimitValue) 
	// 		{
	// 			needUnregisterBuffs.Add(instance);
	// 		}
	// 		instance.timer += deltaTime;
	// 	}
	// 	else
	// 	{
	// 		if (instance.timer < entity.Interval)
	// 		{
	// 			instance.timer += deltaTime;
	// 		}
	// 		else
	// 		{
	// 			instance.timer = 0;
	// 			ApplyBuffModify(entity.Target, entity.Value, playerData);
	// 			if (entity.Type == BuffType.TimeLimit)
	// 			{
	// 				instance.passedLimit += entity.Interval;
	// 			}
	// 			else if (entity.Type == BuffType.NumericalLimit) 
	// 			{
	// 				instance.passedLimit += entity.Value;
	// 			}
	// 			if (instance.passedLimit >= entity.LimitValue) 
	// 			{
	// 				needUnregisterBuffs.Add(instance);
	// 			}
	// 		}
	// 	}
	// }
 //
	// private void ApplyBuffModify(BuffTarget buffTarget,int value, PlayerDataInstance playerData) 
	// {
	// 	switch (buffTarget)
	// 	{
	// 		case BuffTarget.HP:
	// 			playerData.ModifyHP(value);
	// 			break;
	// 		case BuffTarget.MaxHP:
	// 			playerData.ModifyMaxHP(value);
	// 			break;
	// 		case BuffTarget.MP:
	// 			playerData.ModifyMP(value);
	// 			break;
	// 		case BuffTarget.MaxMP:
	// 			playerData.ModifyMaxMP(value);
	// 			break;
	// 		case BuffTarget.EP:
	// 			playerData.ModifyEP(value);
	// 			break;
	// 		case BuffTarget.MaxEP:
	// 			playerData.ModifyMaxEP(value);
	// 			break;
	// 		case BuffTarget.ATK:
	// 			playerData.ModifyATK(value);
	// 			break;
	// 		case BuffTarget.DEF:
	// 			playerData.ModifyDEF(value);
	// 			break;
	// 		case BuffTarget.SPD:
	// 			break;
	// 		case BuffTarget.Buff:
	// 			break;
	// 		default:
	// 			break;
	// 	}
	// }
 //
	// private void UnregisterBuffWhichNeeded()
	// {
	// 	foreach (var buff in needUnregisterBuffs)
	// 	{
	// 		UnregisterBuff(buff);
	// 	}
	// }
}
