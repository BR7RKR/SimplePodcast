using System;
using Avalonia.Controls;

namespace SimplePodcast;

public static class DesignTime
{
    public static void ThrowIfNotDesignTime()
    {
        if (!Design.IsDesignMode)
        {
            throw new Exception("Not in design mode");
        }
    }
}