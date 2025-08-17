using System.Collections.ObjectModel;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SimpleToDoList.Models;
using SimpleToDoList.Services;

namespace SimpleToDoList.ViewModels;

/// <summary>
/// MainViewModel定义ViewModel逻辑（View和TodoItems交互）
/// </summary>
public partial class MainViewModel : ViewModelBase
{
    public MainViewModel()
    {
        // 新增items
        if (Design.IsDesignMode)
        {
            // ObservableCollection可以通知绑定到它的UI元素数据源的变化
            ToDoItems = new ObservableCollection<ToDoItemViewModel>(new[]
            {
                new ToDoItemViewModel() { Content = "Hello" },
                new ToDoItemViewModel() { Content = "Avalonia", IsChecked = true}
            });
        }
    }
    
    /// <summary>
    /// 一个可增删items的集合<see cref="ToDoItem"/>
    /// </summary>
    public ObservableCollection<ToDoItemViewModel> ToDoItems { get; } = new ObservableCollection<ToDoItemViewModel>();

    
    // -- Adding new Items --

    /// <summary>
    /// 新增Item到List
    /// </summary>
    // 中继命令（将命令的执行逻辑和可执行状态判断封装在 ViewModel 中）
    [RelayCommand (CanExecute = nameof(CanAddItem))]
    private void AddItem()
    {
        // 新增
        ToDoItems.Add(new ToDoItemViewModel() {Content = NewItemContent});
        
        // 清空NewItemContent
        NewItemContent = null;
    }

    /// <summary>
    /// 取/设新item. 不空则AddItemCommand自动生效
    /// </summary>
    [ObservableProperty] 
    // _newItemContent变化则执行AddItemCommand
    [NotifyCanExecuteChangedFor(nameof(AddItemCommand))]
    private string? _newItemContent;

    /// <summary>
    /// 是否允许新增item
    /// </summary>
    private bool CanAddItem() => !string.IsNullOrWhiteSpace(NewItemContent);
    
    // -- Removing Items --
    
    /// <summary>
    /// 从list移除指定item
    /// </summary>
    /// <param name="item">the item to remove</param>
    // 中继命令（将命令的执行逻辑和可执行状态判断封装在 ViewModel 中）
    [RelayCommand]
    private void RemoveItem(ToDoItemViewModel item)
    {
        // 移除item
        ToDoItems.Remove(item);
    }
}