using System;
using System.Collections.Generic;

// 1. Define Separate Interfaces for Each Class

public interface ILiveMatchService
{
    void GetMatchUpdates();
}

public interface IFixturesService
{
    void GetFixtures();
    void GetResults();
}

public interface ITeamService
{
    void GetTeamProfile(string teamName);
}

public interface IPlayerService
{
    void GetPlayerProfile(string playerName);
}

public interface ILeagueService
{
    void GetLeagueStandings();
}

public interface INewsService
{
    void GetLatestNews();
}

public interface IFanInteractionService
{
    void SubmitComment(string comment);
    void ShowComments();
}

public interface IVideoService
{
    void GetMatchHighlights(string matchId);
}

public interface ISearchService
{
    void Search(string query);
}
public interface IService
{
    void Execute();
}


// 2. Implementations of Each Interface

public sealed class LiveMatchService : ILiveMatchService
{
    public void GetMatchUpdates()
    {
        Console.WriteLine("Live Match Updates:");
        Console.WriteLine("Match: Manchester United vs Chelsea | Score: 2-1 | Time: 75 mins");
    }
}

public sealed class FixturesService : IFixturesService
{
    public void GetFixtures()
    {
        Console.WriteLine("Upcoming Fixtures:");
        Console.WriteLine("Manchester United vs Liverpool | 2024-11-15");
        Console.WriteLine("Chelsea vs Arsenal | 2024-11-16");
    }

    public void GetResults()
    {
        Console.WriteLine("Recent Results:");
        Console.WriteLine("Manchester City 3-1 Tottenham | 2024-11-10");
        Console.WriteLine("Arsenal 2-2 Chelsea | 2024-11-09");
    }
}

public sealed class TeamService : ITeamService
{
    public void GetTeamProfile(string teamName)
    {
        Console.WriteLine($"Team Profile: {teamName}");
        Console.WriteLine("Top Players: Bruno Fernandes, Marcus Rashford");
        Console.WriteLine("Coach: Erik ten Hag");
    }
}

public sealed class PlayerService : IPlayerService
{
    public void GetPlayerProfile(string playerName)
    {
        Console.WriteLine($"Player Profile: {playerName}");
        Console.WriteLine("Position: Forward");
        Console.WriteLine("Goals: 15 | Assists: 8");
    }
}

public sealed class LeagueService : ILeagueService
{
    public void GetLeagueStandings()
    {
        Console.WriteLine("League Standings:");
        Console.WriteLine("1. Manchester City - 28 points");
        Console.WriteLine("2. Arsenal - 26 points");
        Console.WriteLine("3. Liverpool - 24 points");
        Console.WriteLine("4. Manchester United - 22 points");
    }
}

public sealed class NewsService : INewsService
{
    public void GetLatestNews()
    {
        Console.WriteLine("Latest Football News:");
        Console.WriteLine("Cristiano Ronaldo breaks another record!");
        Console.WriteLine("Premier League: Upcoming Clash Between Giants");
    }
}

public sealed class FanInteractionService : IFanInteractionService
{
    private readonly List<string> _comments = new();

    public void SubmitComment(string comment)
    {
        _comments.Add(comment);
        Console.WriteLine("Comment Submitted!");
    }

    public void ShowComments()
    {
        Console.WriteLine("Fan Comments:");
        foreach (var comment in _comments)
        {
            Console.WriteLine($"- {comment}");
        }
    }
}

public sealed class VideoService : IVideoService
{
    public void GetMatchHighlights(string matchId)
    {
        Console.WriteLine($"Fetching highlights for match ID: {matchId}");
        Console.WriteLine("Highlights: Goal by Rashford at 45', Goal by Havertz at 60'");
    }
}

public sealed class SearchService : ISearchService
{
    public void Search(string query)
    {
        Console.WriteLine($"Search Results for '{query}':");
        Console.WriteLine("Manchester United vs Chelsea");
        Console.WriteLine("Cristiano Ronaldo");
        Console.WriteLine("Premier League");
    }
}

// 3. Decorators for Additional Functionality

public abstract class ServiceDecorator : IService
{
    protected readonly IService _service;

    protected ServiceDecorator(IService service)
    {
        _service = service;
    }

    public abstract void Execute();
}
//
public class LoggingDecorator : ServiceDecorator
{
    public LoggingDecorator(IService service) : base(service) { }

    public override void Execute()
    {
        Console.WriteLine("Logging: Service execution started.");
        _service.Execute();
        Console.WriteLine("Logging: Service execution completed.");
    }
}

public class PerformanceDecorator : ServiceDecorator
{
    public PerformanceDecorator(IService service) : base(service) { }

    public override void Execute()
    {
        var startTime = DateTime.Now;
        _service.Execute();
        var endTime = DateTime.Now;
        Console.WriteLine($"Performance: Execution time was {endTime - startTime}.");
    }
}

// 4. Main Program

public class Program
{
    public static void Main(string[] args)
    {
        ILiveMatchService liveMatchService = new LiveMatchService();
        IFixturesService fixturesService = new FixturesService();
        ITeamService teamService = new TeamService();
        IPlayerService playerService = new PlayerService();
        ILeagueService leagueService = new LeagueService();
        INewsService newsService = new NewsService();
        IFanInteractionService fanService = new FanInteractionService();
        IVideoService videoService = new VideoService();
        ISearchService searchService = new SearchService();

        bool running = true;

        while (running)
        {
            Console.WriteLine("\nFootball Service Menu:");
            Console.WriteLine("1. Get Live Match Updates");
            Console.WriteLine("2. Get Fixtures and Results");
            Console.WriteLine("3. Get Team Profile");
            Console.WriteLine("4. Get Player Profile");
            Console.WriteLine("5. Get League Standings");
            Console.WriteLine("6. Get Latest News");
            Console.WriteLine("7. Submit and Show Fan Comments");
            Console.WriteLine("8. Get Match Highlights");
            Console.WriteLine("9. Search");
            Console.WriteLine("0. Exit");

            Console.Write("\nEnter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    liveMatchService.GetMatchUpdates();
                    break;
                case "2":
                    fixturesService.GetFixtures();
                    fixturesService.GetResults();
                    break;
                case "3":
                    Console.Write("Enter team name: ");
                    string teamName = Console.ReadLine();
                    teamService.GetTeamProfile(teamName);
                    break;
                case "4":
                    Console.Write("Enter player name: ");
                    string playerName = Console.ReadLine();
                    playerService.GetPlayerProfile(playerName);
                    break;
                case "5":
                    leagueService.GetLeagueStandings();
                    break;
                case "6":
                    newsService.GetLatestNews();
                    break;
                case "7":
                    Console.Write("Enter a comment: ");
                    string comment = Console.ReadLine();
                    fanService.SubmitComment(comment);
                    fanService.ShowComments();
                    break;
                case "8":
                    Console.Write("Enter match ID: ");
                    string matchId = Console.ReadLine();
                    videoService.GetMatchHighlights(matchId);
                    break;
                case "9":
                    Console.Write("Enter search query: ");
                    string query = Console.ReadLine();
                    searchService.Search(query);
                    break;
                case "0":
                    running = false;
                    Console.WriteLine("Exiting the program...");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
}