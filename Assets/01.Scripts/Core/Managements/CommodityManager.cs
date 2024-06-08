using Crogen.JsamJson;

public class CommodityManager : MonoSingleton<CommodityManager>
{
    public int GetGold()
    {
        GameData gameData = JsamJson.Load<GameData>(false);
        return gameData.gold;
    }

    public int GetPreamble()
    {
        GameData gameData = JsamJson.Load<GameData>(false);
        return gameData.preamble;
    }
    
    public void AddGoldAndPreamble(int goldValue, int preambleValue)
    {
        GameData gameData = JsamJson.Load<GameData>(false);
        if(gameData.gold + goldValue >= 0)
            gameData.gold += goldValue;
        if(gameData.preamble + preambleValue >= 0)        
            gameData.preamble += preambleValue;
        JsamJson.Save(gameData, false);
    }
}
