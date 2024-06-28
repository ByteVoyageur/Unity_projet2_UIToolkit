using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

public class PostLoginManager : MonoBehaviour
{
    private VisualElement root;
    private Button joinRoomButton;
    private ListView listView;

    private List<int> items = new List<int>();

    // 初始化方法
    public void Initialize(VisualElement rootElement)
    {
        root = rootElement;
        joinRoomButton = root.Q<Button>("joinRoom");
        listView = root.Q<ListView>("listView");

        // 初始化 ListView
        InitializeListView();

        joinRoomButton.clicked += JoinRoomClicked;
    }

    private void InitializeListView()
    {
        listView.style.display = DisplayStyle.Flex; // 确保 ListView 是显示状态而非隐藏状态

        // Debug 信息
        Debug.Log("Initializing ListView");

        listView.makeItem = () =>
        {
            var label = new Label();
            Debug.Log("Creating new ListView item");
            label.style.color = Color.red; // 设置颜色以便调试观察
            return label;
        };
        
        listView.bindItem = (element, i) =>
        {
            Debug.Log("Binding item: " + i);
            (element as Label).text = items[i] + " Hello World!";
        };

        listView.fixedItemHeight = 60;
        listView.itemsSource = items;
    }

    private void JoinRoomClicked()
    {
        Debug.Log("Join room button clicked!");
        items.Add(items.Count);

        // 调试信息
        Debug.Log($"Items count: {items.Count}");
        items.ForEach(item => Debug.Log($"Item: {item}"));

        listView.RefreshItems();

        // 调试信息: 检查 ListView 的子元素
        Debug.Log($"ListView elements children count: {listView.childCount}");
    }

    // 显示欢迎界面
    public void ShowWelcomeScreen()
    {
        var welcomeScreen = root.Q<VisualElement>("welcomeScreen");
        welcomeScreen.style.display = DisplayStyle.Flex;
    }
}