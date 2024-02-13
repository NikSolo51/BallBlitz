using Mirror;
using UnityEngine;
using UnityEngine.UI;


public class PlayerUI : MonoBehaviour
{
    public Text playerName;
    public Image teamImage;

    [ClientCallback]
    public void SetPlayerInfo(PlayerInfo info)
    {
        playerName.text = $"Player {info.playerIndex}";
        playerName.color = info.ready ? Color.green : Color.red;
        teamImage.color = GetColorByTeam(info);
    }

    private Color GetColorByTeam(PlayerInfo info)
    {
        switch (info.team)
        {
            case 0: return Color.red;
            case 1: return Color.blue;
            case 2: return Color.yellow;
            case 3: return Color.cyan;
            default: return Color.white;
        }
    }
}