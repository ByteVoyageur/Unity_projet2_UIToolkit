using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;

public class PostLoginManager : MonoBehaviourPunCallbacks
{
    private VisualElement root;
    private Button joinRoomButton;
    private ListView listView;
    private Label welcomeMessage;

    private List<string> items = new List<string>();

    private string userEmail;

    public void Initialize(VisualElement rootElement, string email)
    // Initializes the PostLoginManager with the root UI element and user's email.
    // This method is called by the LoginValidator class after the user has successfully logged in.
    {
        root = rootElement;
        joinRoomButton = root.Q<Button>("joinRoom");
        listView = root.Q<ListView>("listView");
        welcomeMessage = root.Q<Label>("welcomeMessage");

        userEmail = email;
        Debug.Log($"PostLoginManager initialized with userEmail: {userEmail}");

        InitializeListView();

        joinRoomButton.clicked += JoinRoomClicked;

        Debug.Log("PostLoginManager initialized.");
    }

    private void InitializeListView()
    //prepares the ListView to display usernames by adding new Label elements.
    {
        Debug.Log("Initializing ListView");

        listView.makeItem = () =>
        {
            var label = new Label();
            label.AddToClassList("listViewItem");
            Debug.Log("Creating new ListView item");
            return label;
        };

        listView.bindItem = (element, i) =>
        {
            var label = element as Label;
            label.text = items[i];
            Debug.Log($"Binding item: {i}");
        };

        listView.fixedItemHeight = 60;
        listView.itemsSource = items;
    }
    //Handles the logic when the join room button is clicked.
    // Sets the user's email ais their Photon nickname
    private void JoinRoomClicked()
    {
        Debug.Log("Join room button clicked!");
        PhotonNetwork.NickName = userEmail;  
        // Set the user's email as their nickname

        if (!PhotonNetwork.IsConnected)
        {
            Debug.Log("Not connected to Photon. Connecting...");
            PhotonNetwork.ConnectUsingSettings();
        }
        else
        {
            JoinRoom();
        }
    }

    private void JoinRoom()
    {
        Debug.Log("Joining room...");
        PhotonNetwork.JoinRandomOrCreateRoom();
    }

    public void ShowWelcomeScreen()
    {
        var welcomeScreen = root.Q<VisualElement>("welcomeScreen");
        welcomeScreen.style.display = DisplayStyle.Flex;
    }

    public void AddPlayerToList(string playerName)
    {
        items.Add(playerName);
        listView.RefreshItems();
    }

    public override void OnConnectedToMaster()
    // Callback method called when the client successfully joins a room.
    // Uadates the player list to reflect the current room members.
    {
        base.OnConnectedToMaster();
        Debug.Log("Connected to Master");

        JoinRoom();
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log($"Joined room. Player: {PhotonNetwork.NickName}");

        UpdatePlayerList();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    // Adds the new player's name to the player list.
    {
        base.OnPlayerEnteredRoom(newPlayer);
        Debug.Log($"Player entered room: {newPlayer.NickName}");

        AddPlayerToList(newPlayer.NickName);
    }

    private void UpdatePlayerList()
    // Updates the ListView with the current list of players in the room.
    // Clears the current list and repopulates it with the latest player names.
    {
        items.Clear();
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            items.Add(player.NickName);
        }
        listView.RefreshItems();
    }
}
