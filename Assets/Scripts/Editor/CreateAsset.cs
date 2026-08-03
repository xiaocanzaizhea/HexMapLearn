using System.Collections;
using System.Collections.Generic;
using System.IO;
using OfficeOpenXml;
using UnityEditor;
using UnityEngine;

public class CreateAsset : UnityEditor.Editor
{
    private enum ExcelTitleEnum
    {
        Id,
        Name,
        Hp,
        ATK,
        Speed,
        IconPath
    }

    private enum PlayerEnum
    {
        id,
        maxHp,
        attack,
        defense,
        moveRange,
        sightRange,
        unitValue,
        unitOccupancySize,
        unitBuildTimeRequired,
        retreatTimeRequired,
        icon
    }
    
    [MenuItem("Tools/CreateAssetFromExcel/CreatePlayerAsset")]
    static void CreatePlayerAssetFromExcel()
    {
        string excelPath = Path.Combine(Application.dataPath, "Scripts/Editor/DataSO.xlsx");
    
        if (!File.Exists(excelPath))
        {
            return;
        }
    
        using var package = new ExcelPackage(new FileInfo(excelPath));
    
        foreach (var worksheet in package.Workbook.Worksheets)
        {
            CreatePlayerScriptableObject(worksheet);
        }
    
        AssetDatabase.Refresh();
    }

    private static void CreatePlayerScriptableObject(ExcelWorksheet worksheet)
    {
        // PlayerUnitsData playerUnitsData = CreateInstance<PlayerUnitsData>();
        // int startRow = 2, startCol = 1;
        // for (int i = startRow; i <= worksheet.Dimension.Rows; i++)
        // {
        //     PlayerHexUnitDataEntity playerHexUnitDataEntity = CreateInstance<PlayerHexUnitDataEntity>();
        //     playerHexUnitDataEntity.id = worksheet.Cells[i, startCol].Value.ToString();
        //     playerHexUnitDataEntity.maxhp = int.Parse(worksheet.Cells[i, startCol+(int)PlayerEnum.maxHp].Text);
        //     playerHexUnitDataEntity.attack = int.Parse(worksheet.Cells[i, startCol+(int)PlayerEnum.attack].Text);
        //     playerHexUnitDataEntity.defense = int.Parse(worksheet.Cells[i, startCol+(int)PlayerEnum.defense].Text);
        //     playerHexUnitDataEntity.moveRange = int.Parse(worksheet.Cells[i, startCol+(int)PlayerEnum.moveRange].Text);
        //     playerHexUnitDataEntity.sightRange = int.Parse(worksheet.Cells[i, startCol+(int)PlayerEnum.sightRange].Text);
        //     playerHexUnitDataEntity.unitValue = int.Parse(worksheet.Cells[i, startCol+(int)PlayerEnum.unitValue].Text);
        //     playerHexUnitDataEntity.unitOccupancySize = int.Parse(worksheet.Cells[i, startCol+(int)PlayerEnum.unitOccupancySize].Text);
        //     playerHexUnitDataEntity.unitBuildTimeRequired = int.Parse(worksheet.Cells[i, startCol+(int)PlayerEnum.unitBuildTimeRequired].Text);
        //     playerHexUnitDataEntity.retreatTimeRequired = int.Parse(worksheet.Cells[i, startCol+(int)PlayerEnum.retreatTimeRequired].Text);
        //
        //     string iconPath = worksheet.Cells[i, startCol+(int)PlayerEnum.icon].Text;
        //     playerHexUnitDataEntity.sprite = AssetDatabase.LoadAssetAtPath<Sprite>(iconPath);
        //
        //     string fileName = playerHexUnitDataEntity.id;
        //     string savePath = $"Assets/AddressableAssets/SO/Player/{fileName}.asset";
        //     if (!Directory.Exists(Path.GetDirectoryName(savePath))) 
        //         Directory.CreateDirectory(Path.GetDirectoryName(savePath));
        //
        //     AssetDatabase.CreateAsset(playerHexUnitDataEntity, savePath);
        //     AssetDatabase.SaveAssets();
        //     
        //     playerUnitsData.data.Add(playerHexUnitDataEntity);
        // }
        //
        // string savePath2 = "Assets/AddressableAssets/SO/Player/PlayerUnitsData.asset";
        // AssetDatabase.CreateAsset(playerUnitsData, savePath2);
        // AssetDatabase.SaveAssets();
    }
}
