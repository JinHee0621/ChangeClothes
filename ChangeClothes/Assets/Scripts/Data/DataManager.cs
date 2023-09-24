using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public ChallengeManager challengeManager;
    public CheckResultManager checkResultManager;

    DataVo data = new DataVo
    {
        ViewEnding = false,
        GetTrasure = false,
        Challenge1 = false,
        Challenge2 = false,
        Challenge3 = false,
        Challenge4 = false,
        Challenge5 = false,
        Challenge6 = false,
        Challenge7 = false,
        Challenge8 = false,
        Challenge9 = false,
        Challenge10 = false,
        Challenge11 = false,
        Challenge12 = false,
        ClearCount = 0
    };
    string filePath = "SaveData/data";
    public static bool[] challengeClearArr = { false, false, false, false, false, false, false, false, false, false, false, false };
    public static bool dataTrasure = false;
    public static bool viewEnding = false;
    public static int dataClearCount = 0;
    void Start()
    {
        CheckDataFile();
    }

    private void CheckDataFile()
    {
        if (!File.Exists(filePath))
        {
            //File.CreateText(filePath);
            string json = JsonConvert.SerializeObject(data);
            File.WriteAllText(filePath, json);
        } else
        {
            StreamReader file = File.OpenText(filePath);
            JsonTextReader jsonData = new JsonTextReader(file);
            JObject loadData = (JObject)JToken.ReadFrom(jsonData);

            data.Challenge1 = bool.Parse(loadData["Challenge1"].ToString());
            challengeClearArr[0] = data.Challenge1;

            data.Challenge2 = bool.Parse(loadData["Challenge2"].ToString());
            challengeClearArr[1] = data.Challenge2;

            data.Challenge3 = bool.Parse(loadData["Challenge3"].ToString());
            challengeClearArr[2] = data.Challenge3;

            data.Challenge4 = bool.Parse(loadData["Challenge4"].ToString());
            challengeClearArr[3] = data.Challenge4;

            data.Challenge5 = bool.Parse(loadData["Challenge5"].ToString());
            challengeClearArr[4] = data.Challenge5;

            data.Challenge6 = bool.Parse(loadData["Challenge6"].ToString());
            challengeClearArr[5] = data.Challenge6;

            data.Challenge7 = bool.Parse(loadData["Challenge7"].ToString());
            challengeClearArr[6] = data.Challenge7;

            data.Challenge8 = bool.Parse(loadData["Challenge8"].ToString());
            challengeClearArr[7] = data.Challenge8;

            data.Challenge9 = bool.Parse(loadData["Challenge9"].ToString());
            challengeClearArr[8] = data.Challenge9;

            data.Challenge10 = bool.Parse(loadData["Challenge10"].ToString());
            challengeClearArr[9] = data.Challenge10;

            data.Challenge10 = bool.Parse(loadData["Challenge11"].ToString());
            challengeClearArr[10] = data.Challenge10;

            data.Challenge10 = bool.Parse(loadData["Challenge12"].ToString());
            challengeClearArr[11] = data.Challenge10;

            data.GetTrasure = bool.Parse(loadData["GetTrasure"].ToString());
            dataTrasure = data.GetTrasure;

            data.ViewEnding = bool.Parse(loadData["ViewEnding"].ToString());
            viewEnding = data.ViewEnding;

            data.ClearCount = int.Parse(loadData["ClearCount"].ToString());
            dataClearCount = data.ClearCount;
            checkResultManager.clearCount = dataClearCount;

            for (int i = 0; i < challengeClearArr.Length; i++)
            {
                if(challengeClearArr[i])
                {
                    challengeManager.LoadClearChallenge(i);
                }
            }
        }
    }

    public static void SaveFile()
    {
        string filePath = "SaveData/data";
        DataVo data = new DataVo
        {
            Challenge1 = challengeClearArr[0],
            Challenge2 = challengeClearArr[1],
            Challenge3 = challengeClearArr[2],
            Challenge4 = challengeClearArr[3],
            Challenge5 = challengeClearArr[4],
            Challenge6 = challengeClearArr[5],
            Challenge7 = challengeClearArr[6],
            Challenge8 = challengeClearArr[7],
            Challenge9 = challengeClearArr[8],
            Challenge10 = challengeClearArr[9],
            Challenge11 = challengeClearArr[10],
            Challenge12 = challengeClearArr[11],
            GetTrasure = dataTrasure,
            ViewEnding = viewEnding,
            ClearCount = dataClearCount
        };
        string json = JsonConvert.SerializeObject(data);
        File.WriteAllText(filePath, json);
    }

    public static void ChanegeChallengeState(int challengeNum)
    {
        challengeClearArr[challengeNum - 1] = true;
    }

}
