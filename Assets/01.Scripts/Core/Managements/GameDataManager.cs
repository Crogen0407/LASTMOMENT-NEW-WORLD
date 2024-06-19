using Crogen.JsamJson;

public class GameDataManager : MonoSingleton<GameDataManager>
{
    private GameData _gameData;

    public void SaveData()
    {
        JsamJson.Save(_gameData, false, true);
        _gameData = JsamJson.Load<GameData>(false);
    }
    
    public void LoadData()
    {
        _gameData = JsamJson.Load<GameData>(false);
        if (_gameData == null)
        {
            JsamJson.Save(new GameData(), false, true);
            _gameData = JsamJson.Load<GameData>(false);
        }
    }

    public void ResetSettingArray()
    {
        GameData.settingArray = new GameData().settingArray;
    }
    
    public GameData GameData
    {
        get
        {
            if (_gameData == null)
                LoadData();
            return _gameData;
        } 
        set => _gameData = value;
    }
    public int Gold
    {
        get => GameData.gold;
        set => GameData.gold = value;
    }
    public int[] SettingArray
    {
        get => GameData.settingArray;
        set => GameData.settingArray = value;
    }

    public int[] WeaponOwnStateArray
    {
        get => GameData.weaponOwnStateArray;
        set => GameData.weaponOwnStateArray = value;
    }

    public int[] CurrentWeaponArray
    {
        get => GameData.currentWeaponArray;
        set => GameData.currentWeaponArray = value;
    }
    
    public void AddGold(int goldValue)
    {
        GameData gameData = JsamJson.Load<GameData>(false);
        if(gameData.gold + goldValue >= 0)
            gameData.gold += goldValue;
        JsamJson.Save(gameData, false, false);
    }
}
