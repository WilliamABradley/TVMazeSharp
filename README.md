#TVMazeSharp
**A C# wrapper for the TVMaze API.**

I wrote up this API wrapper in 7 Hours, it should hopefully be pretty easy to use. It covers all of the current functionality of the TVMaze API (As of 12/09/2015 (September)).

=========

**Unfortunately this Project file only works in Visual Studio 2015, as it uses Nuget 3.0. This can however be fixed by creating a new Class Library in VS2013, going into Nuget Manager and getting Newtonsoft.Json, and then adding my Models folder and the class file into the project.**

**If you do this however, make sure that the Namespaces are the same.**

**VS2015 Community is free and supports extensions so it would be better getting it.**

=========

**For more information about this API and its more complex functionality (Such as get Series at Page), visit: http://www.tvmaze.com/api**

This API doesn't have to be instantiated as it doesn't require an API key to access the API.

Heres an example of using this API to get a Series using Single Search in Windows 10:

    var series = await TVMaze.FindSingleSeries("Dexter", FetchEpisodes: true);
    if (series.status != "404")
    {
        string typeofSeries = "";
        if (series.webChannel == null) typeofSeries = $"Network: {series.network.name}({series.network.country.code}) \n";
        else if (series.network == null) typeofSeries = $"WebChannel: {series.webChannel.name}({series.webChannel.country.code}) \n";
        await new ContentDialog
        {
            Title = series.name,
            Content = new TextBlock
            {
                Text = $"{typeofSeries}{series.Episodes.Count} Episodes \nFirst Aired: {series.premiered.ToString("dd/MM/yyyy")} \nRuntime: {series.runtime} minutes."
            },
            PrimaryButtonText = "OK"
        }.ShowAsync();
    }
    else await new ContentDialog { Title = series.name, PrimaryButtonText = "OK" }.ShowAsync();

This displays:  
![alt tag](http://puu.sh/k8h6M/c14b06ca62.PNG)
