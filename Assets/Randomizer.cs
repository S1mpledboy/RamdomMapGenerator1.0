using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Randomizer : MonoBehaviour
{
    int _m = (int)Mathf.Pow(2, 32), _c;

    [SerializeField] List<GameObject> _inputTiles = new List<GameObject>();
    List<int[]> _bindsList = new List<int[]>();
    int[] _intesityArray = new int[5];
    [SerializeField]
    [Range(1, 10)]
    int _cityIntesity = 2;
    [SerializeField]
    [Range(1, 10)]
    int _groundIntesity = 3;
    [SerializeField]
    [Range(1, 10)]
    int _mountainIntesity = 1;
    [SerializeField]
    [Range(1, 10)]
    int _waterIntesity = 1;
    [SerializeField]
    [Range(1, 10)]
    int _forestIntesity = 2;
    int[] _cityBinds, _groundBinds, _mountainBinds, _waterBinds, _forestBinds;
    Dictionary<int[], GameObject> _tileDictionary = new Dictionary<int[], GameObject>();
    [SerializeField] int _inputSeed;
    IntToArray _seed;

    private void Start()
    {
        _seed = new IntToArray(_inputSeed);
        _c = (int)((3 - Mathf.Sqrt(3)) / 6) * _m;
        _bindsList.Add(_cityBinds);
        _bindsList.Add(_groundBinds);
        _bindsList.Add(_mountainBinds);
        _bindsList.Add(_waterBinds);
        _bindsList.Add(_forestBinds);
        _intesityArray[0] = _cityIntesity;
        _intesityArray[1] = _groundIntesity;
        _intesityArray[2] = _mountainIntesity;
        _intesityArray[3] = _waterIntesity;
        _intesityArray[4] = _forestIntesity;

        PopulateBinds();
        TileListToTileDict();
        //_maxIntInBinds = _bindsList.SelectMany(arr => arr).Max();
        GenerateWorld(100);
    }
    void GenerateWorld(int x)
    {
        int y = x;
        for (int i = 0; i < y; i++)
        {
            for (int j = 0; j < x; j++)
            {

                int index = (j + 1) % _seed.DigitsArray.Length ;
                int randomValue = GenerateRandomNumber(_seed.DigitsArray[index]);
                print(randomValue);
                _seed.DigitsArray[index] = randomValue + GenerateRandomNumber((j + 1) % _seed.DigitsArray.Length);
                GameObject tile = CreateTerrain(randomValue);
                if (tile != null)
                {
                    Instantiate(tile, new Vector3(j, i, 0), Quaternion.identity);
                }
                
            }
        }
    }
    GameObject CreateTerrain(int value)
    { 
        foreach(KeyValuePair <int[], GameObject> entry in _tileDictionary)
        {
            
           if(entry.Key.Contains(value))
            {
                return entry.Value;
            }else if (entry.Key.Contains(value/10))
            {
                return entry.Value;
            }
        }
        return _tileDictionary.Values.Last();
    }
    void PopulateBinds()
    {
        int previousMaxBind = 0;
        for(int i = 0; i< _intesityArray.Length; i++)
        {
            _bindsList[i] = Enumerable.Range(previousMaxBind, _intesityArray[i]).ToArray();
           
            previousMaxBind = _intesityArray[i];
        }
        
    }
    void TileListToTileDict()
    {
        for(int i = 0; i < _bindsList.Count-1; i++)
        {
            _tileDictionary.Add(_bindsList[i], _inputTiles[i]);
        }
    }
    int GenerateRandomNumber(int baseValue)
    {

        int randomNumber = ((1103515245 * baseValue) + _c) %_m;
        return Mathf.Abs((int)randomNumber%100);
    }
}
