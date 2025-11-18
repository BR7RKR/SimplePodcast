using System;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using SimplePodcast.ViewModels;

namespace SimplePodcast;

public class ViewLocator : IDataTemplate
{
    public Control? Build(object? param)
    {
        if (param is null)
            return null;

        var name = param.GetType().FullName?.Replace("ViewModel", "View", StringComparison.Ordinal);

        if (name is null)
        {
            throw new Exception("Could not find view for " + param.GetType());
        }
        
        var type = Type.GetType(name);

        if (type is not null)
        {
            return (Control?)Activator.CreateInstance(type);
        }

        return new TextBlock { Text = "Not Found: " + name };
    }

    public bool Match(object? data)
    {
        return data is ViewModelBase;
    }
}