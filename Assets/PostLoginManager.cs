using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;
using Photon.Pun;

public class PostLoginManager : MonoBehaviour
{
    private VisualElement root;
    private Button joinRoomButton;
    private ListView listView;

    private List<int> items = new List<int>();

    public void Initialize(VisualElement rootElement)
    {
        root = rootElement;
        joinRoomButton = root.Q<Button>("joinRoom");
        listView = root.Q<ListView>("listView");

        InitializeListView();

        joinRoomButton.clicked += JoinRoomClicked;

        Debug.Log("PostLoginManager initialized.");
    }

    private void InitializeListView()
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
            label.text = $"{items[i]} Hello World!";
            Debug.Log($"Binding item: {i}");
        };

        listView.fixedItemHeight = 60;
        listView.itemsSource = items;
    }

    private void JoinRoomClicked()
    {
        Debug.Log("Join room button clicked!");
        items.Add(items.Count);

        Debug.Log($"Items count: {items.Count}");
        for (int i = 0; i < items.Count; i++)
        {
            Debug.Log($"Item {i}: {items[i]}");
        }

        listView.RefreshItems();

        Debug.Log($"ListView elements children count after RefreshItems: {listView.childCount}");
        foreach (var child in listView.Children())
        {
            Debug.Log($"Child: {child}");
        }
    }

    public void ShowWelcomeScreen()
    {
        var welcomeScreen = root.Q<VisualElement>("welcomeScreen");
        welcomeScreen.style.display = DisplayStyle.Flex;
    }
}