using CommunityToolkit.Mvvm.ComponentModel;

namespace SimpleToDoList.Models;

/// <summary>
/// ToDoItem的Model
/// </summary>
public class ToDoItem
{
    /// <summary>
    /// Gets or sets the checked status of each item
    /// </summary>
    public bool IsChecked { get; set; }

    /// <summary>
    /// Gets or sets the content of the to-do item
    /// </summary>
    public string? Content { get; set; }
}