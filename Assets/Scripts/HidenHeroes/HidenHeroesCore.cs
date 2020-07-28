using Photon.Pun;
using UnityEngine;
using UnityEngine.Events;

public class HidenHeroesCore : MonoBehaviour, IPunObservable
{
    public int kills;
    public int heroesCount = 60;
    
    /* data */
    public TargestList targestList;

    /* На объекты */
    [SerializeField] private EndGame endGame;
    [SerializeField] private HeroesPool heroesPool;
    [SerializeField] private PlayersController playersController;
    public GridController gridController;

    /* Event */
    [SerializeField] private UnityEvent startNewRoundForClient;

    private void Start()
    {
        kills = 0;
        trigger = 0;
    }

    /* Подумать о переносе в playerControllers */
    public void UpdateScore(bool isFirstPlayer, int point)
    {
        if (isFirstPlayer)
            playersController.players[0].playerScore += point;
        else 
            playersController.players[1].playerScore += point;

        if (playersController.players[0].playerScore < 0)
            { playersController.players[0].playerScore = 0; }
        if (playersController.players[1].playerScore < 0)
            { playersController.players[1].playerScore = 0; }

        playersController.firstPlayerScoreLabel.text =
             playersController.players[0].playerScore.ToString();
        playersController.secondPlayerScoreLabel.text =
             playersController.players[1].playerScore.ToString();
    }

    public void IsNewRound()
    {
        if (kills % 3 == 0 && kills < 12 && PhotonNetwork.IsMasterClient == true)
        {
            StartNewRound();
        }
        else if (kills == 12)
        { endGame.StartEndGameProcess(); }

        trigger = 0;
    }

    public void ClientStartRound()
    {
        if (trigger == 2 && PhotonNetwork.IsMasterClient == false)
            StartNewRound();
    }

    [SerializeField] int[] gamePool = new int[60];
    string[] targets = new string[3];
    public void StartNewRound()
    {
        /* только для мастера */
        if (PhotonNetwork.IsMasterClient == true)
        {
            gamePool = heroesPool.CreateGamePool(heroesCount);
            targets  = targestList.SetTargets(gamePool);
        }
        
        /* для каждого игрока */
        targestList.SpawnContractsInGrid(targets);
        gridController.FillGrid(gamePool);


    }

    private int trigger;
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting && PhotonNetwork.IsMasterClient == true)
        {
            Debug.Log("Отправляю");
            stream.SendNext(gamePool);
            stream.SendNext(targets);
        }
        else if (PhotonNetwork.IsMasterClient != true)
        {   
            trigger +=1;
            Debug.Log("Принимаю");
            gamePool = (int[]) stream.ReceiveNext();
            targets  = (string[]) stream.ReceiveNext();
            
            startNewRoundForClient.Invoke();
        }
    }
}
