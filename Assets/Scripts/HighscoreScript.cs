using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;
using UnityEngine.UI;

public class HighscoreScript : MonoBehaviour {

    [SerializeField]
    private ScrollRect _scrollNameRect;
    [SerializeField]
    private ScrollRect _scrollScoreRect;
    [SerializeField]
    private Text _nameText;
    [SerializeField]
    private Text _scoreText;
    private BinaryFormatter _bf = new BinaryFormatter();
    private Dictionary<string, int> _highscoreList = new Dictionary<string, int>();
    private Dictionary<string, int> _sortedList = new Dictionary<string, int>();

    private string[] _letters = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

    private void Start()
    {
        CompileList();
        PrintList();
        SortList();
        DisplayScores();
    }

    private void DisplayScores()
    {
        _nameText.text = "";
        _scoreText.text = "";
        foreach (KeyValuePair<string,int> kvp in _highscoreList)
        {
            _nameText.text += kvp.Key + "\n";
            _scrollNameRect.verticalNormalizedPosition = 0;
            _scoreText.text += kvp.Value + "\n";
            _scrollScoreRect.verticalNormalizedPosition = 0;
        }

        
    }

    private void AddScore(string pName, int pScore)
    {
        if (_highscoreList.ContainsKey(pName) && pScore > _highscoreList[pName])
        {
            _highscoreList[pName] = pScore;
            SortList();
        }
        else if (_highscoreList.ContainsKey(pName) && pScore < _highscoreList[pName])
        {
            return;
        }
        else if (_highscoreList.Count < 10)
        {
            _highscoreList.Add(pName, pScore);
            SortList();
        }
        else
        {
            _highscoreList.Add(pName, pScore);
            SortList();
            _highscoreList.Remove(_highscoreList.Last().Key);
        }
    }

    private bool IsItHighscore(int pScore)
    {
        return false;
    }

    private void PrintList()
    {
        foreach (KeyValuePair<string, int> item in _highscoreList)
        {
            print("key: " + item.Key + " value: " + item.Value);
        }
    }

    private void SortList()
    {
        _sortedList.Clear();
        foreach (KeyValuePair<string, int> item in _highscoreList.OrderByDescending(key => key.Value))
        {
            _sortedList.Add(item.Key, item.Value);
        }
        _highscoreList = _sortedList;
    }

    private void CompileList()
    {
        _highscoreList.Add("me",20);
        _highscoreList.Add("you",10);
        _highscoreList.Add("the", 250);
        _highscoreList.Add("he",15);
        _highscoreList.Add("them",50);
        _highscoreList.Add("me2", 20);
        _highscoreList.Add("you2", 10);
        _highscoreList.Add("the2", 50);
        _highscoreList.Add("he2", 15);
        _highscoreList.Add("them2", 50);
    }

    private void SaveList()
    {
        FileStream stream = new FileStream(Application.dataPath + "/highscore.dat", FileMode.Create);

        try
        {
            _bf.Serialize(stream, _highscoreList);
        }
        catch
        {
            print("Failed");
        }

        stream.Close();
    }

    private void LoadList()
    {
        FileStream stream = new FileStream(Application.dataPath + "/highscore.txt", FileMode.Open);

        try
        {
            object list = _bf.Deserialize(stream);
            _highscoreList = list as Dictionary<string, int>;
        }
        catch
        {
            print("failed");
            stream.Close();
        }
        finally
        {
            stream.Close();
        }
    }
}
