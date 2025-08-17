using CommunityToolkit.Mvvm.ComponentModel;
using SimpleToDoList.Models;

namespace SimpleToDoList.ViewModels;

/*   NOTE:
 *
 *   用了CommunityToolkit.Mvvm package for the ViewModels
 *
 */


/// <summary>
/// ViewModel which represents a <see cref="Models.ToDoItem"/>
/// </summary>
public partial class ToDoItemViewModel : ViewModelBase
{
    /// <summary>
    /// 创建新的空的ToDoItemViewModel
    /// </summary>
    public ToDoItemViewModel()
    {
        // empty
    }
    
    /// <summary>
    /// 创建一个新的ToDoItemViewModel for the given <see cref="Models.ToDoItem"/>
    /// </summary>
    /// <param name="item">The item to load</param>
    public ToDoItemViewModel(ToDoItem item)
    {
        // Init the properties with the given values
        IsChecked = item.IsChecked;
        Content = item.Content;
    }
    
    /// <summary>
    /// Gets or sets the checked status of each item
    /// </summary>
    // NOTE: This property is made without source generator. Uncomment the line below to use the source generator
    // ObservableProperty：属性值发生变化时，它能自动通知 UI 进行更新，实现数据与视图的双向绑定
    // [ObservableProperty] 
    private bool _isChecked;

    public bool IsChecked
    {
        get { return _isChecked; }
        set { SetProperty(ref _isChecked, value); }
    }
    
    /// <summary>
    /// Gets or sets the content of the to-do item
    /// </summary>
    [ObservableProperty] 
    private string? _content;
    
    /// <summary>
    /// Gets a ToDoItem of this ViewModel
    /// </summary>
    /// <returns>The ToDoItem</returns>
    public ToDoItem GetToDoItem()
    {
        return new ToDoItem()
        {
            IsChecked = this.IsChecked,
            Content = this.Content
        };
    }
}