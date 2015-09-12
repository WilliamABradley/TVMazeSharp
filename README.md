# TVMazeSharp
.Net wrapper for the TVMaze API

TVMazeSharp
=========

**A C# wrapper for the TVMaze API.**

I wrote up this API wrapper in 7 Hours, it should hopefully be pretty easy to use. It covers all of the current functionality (As of 12/09/2015 (September)) of the TVMaze API.
For more information about this API and its more complex functionality (Such as get Series at Page), visit: http://www.tvmaze.com/api

This API doens't have to be instantiated as it doesn't require an API key to access the API.

Heres an example of using this API to get a Series using Single Search in Windows 10:

    var series = await TVMaze.FindSingleSeries("Dexter", FetchEpisodes: true);
    string typeofSeries = "";
    if (series.webChannel == null) typeofSeries = $"Network: {series.network.name}({series.network.country.code}) \n";
    else if (series.network == null) typeofSeries = $"WebChannel: {series.webChannel.name}({series.webChannel.country.code}) \n";
    await new ContentDialog()
    {
        Title = series.name,
        Content = new TextBlock
        {
            Text = $"{typeofSeries}{series.Episodes.Count} Episodes \nFirst Aired: {series.premiered.ToString("dd/MM/yyyy")} \nRuntime: {series.runtime} minutes."
        },
        PrimaryButtonText = "OK"
    }.ShowAsync();
    
    Which displays: ![alt tag](http://puu.sh/k8h6M/c14b06ca62.PNG)

**Notes**  
If you encounter any issues or have a suggestion: don't hesitate to open a ticket, you will be helping me too.  
Should you wish to do so, you can contact me at `williamabradley@outlook.com`.
