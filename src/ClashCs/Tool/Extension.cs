using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia;
using Microsoft.Extensions.DependencyInjection;

namespace ClashCs.Tool;

public static class Extension
{
    public static T GetService<T>(this Application? application) where T : class
    {
        return (application as App)!.Services.GetService<T>()!;
    }

    public static void AddRange<T>(this ObservableCollection<T> observableCollection, IEnumerable<T> list,
        bool isRefresh = false)
    {
        ArgumentNullException.ThrowIfNull(list);

        if (isRefresh)
        {
            observableCollection.Clear();
        }
        foreach (var item in list) observableCollection.Add(item);
    }
}