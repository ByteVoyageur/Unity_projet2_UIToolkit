using UnityEngine;
using UnityEngine.UIElements;

public class PostLoginManager : MonoBehaviour
{
    private VisualElement welcomeScreen;
    private Button joinRoomButton;

    public void Initialize(VisualElement root)
    {
        welcomeScreen = root.Q<VisualElement>("welcomeScreen");

        joinRoomButton = root.Q<Button>("joinRoom");
        joinRoomButton.clicked += OnJoinRoomButtonClicked;
    }

    private void OnJoinRoomButtonClicked()
    {
        Debug.Log("Join Room button clicked");
        // Add logic for joining the room here
    }

    public void ShowWelcomeScreen()
    {
        welcomeScreen.style.display = DisplayStyle.Flex;
    }
}
