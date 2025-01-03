using System;
using System.Collections.Generic;
using System.Runtime.Intrinsics.X86;

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
    void GetNewsByPreference(string preference);
}


public interface IFanInteractionService
{
    void Interact(string type, string input);
}

public interface IVideoService
{
    void FetchVideo(string type);
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

public sealed class TrendingNewsService : INewsService
{
    public void GetNewsByPreference(string preference)
    {
        Console.WriteLine($"Fetching trending news based on preference: {preference}");
        if (preference == "sports")
        {
            Console.WriteLine("Trending News: Ronaldo scores a hat-trick in the derby!");
        }
        else if (preference == "politics")
        {
            Console.WriteLine("Trending News: Elections 2024—latest updates.");
        }
        else
        {
            Console.WriteLine("Trending News: Climate action summit gains momentum.");
        }
    }
}

public sealed class PersonalizedNewsService : INewsService
{
    public void GetNewsByPreference(string preference)
    {
        Console.WriteLine($"Fetching personalized news for preference: {preference}");
        if (preference == "football")
        {
            Console.WriteLine("Personalized News: Your favorite team won the match!");
        }
        else if (preference == "technology")
        {
            Console.WriteLine("Personalized News: AI breakthroughs in 2025.");
        }
        else
        {
            Console.WriteLine("Personalized News: Top stories you might like.");
        }
    }
}

public sealed class FanPollService : IFanInteractionService
{
    public void Interact(string type, string input)
    {
        Console.WriteLine($"Processing fan poll of type: {type}");
        if (type == "vote" && input == "teamA")
        {
            Console.WriteLine("Vote registered for Team A.");
        }
        else if (type == "vote" && input == "teamB")
        {
            Console.WriteLine("Vote registered for Team B.");
        }
        else
        {
            Console.WriteLine("Invalid poll type or input.");
        }
    }
}

public sealed class FanTriviaService : IFanInteractionService
{
    public void Interact(string type, string input)
    {
        Console.WriteLine($"Processing trivia of type: {type}");
        if (type == "question" && input == "correct")
        {
            Console.WriteLine("Correct answer! You earn 10 points.");
        }
        else if (type == "question" && input == "wrong")
        {
            Console.WriteLine("Wrong answer. Better luck next time!");
        }
        else
        {
            Console.WriteLine("Invalid trivia type or input.");
        }
    }
}

public sealed class ShortHighlightVideoService : IVideoService
{
    public void FetchVideo(string type)
    {
        Console.WriteLine($"Fetching short highlight video of type: {type} for match ID:");
        if (type == "goal")
        {
            Console.WriteLine("Highlight: Rashford's stunning goal in the 45th minute!");
        }
        else if (type == "save")
        {
            Console.WriteLine("Highlight: De Gea's crucial save in the 70th minute!");
        }
        else
        {
            Console.WriteLine("No highlights found for the specified type.");
        }
    }
}

public sealed class FullMatchReplayVideoService : IVideoService
{
    public void FetchVideo(string type)
    {
        Console.WriteLine($"Fetching full match replay video of type: {type} for match ID: ");
        if (type == "full")
        {
            Console.WriteLine($"Replaying the full match for ID:");
        }
        else if (type == "extended")
        {
            Console.WriteLine($"Replaying extended highlights for match ID: ");
        }
        else
        {
            Console.WriteLine("Invalid type for match replay.");
        }
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

public sealed class UserAuthenticationService
{
    private readonly Dictionary<string, string> _userCredentials = new();

    public bool Authenticate(string username, string password)
    {
        return _userCredentials.ContainsKey(username) && _userCredentials[username] == password;
    }

    public bool CreateUser(string username, string password)
    {
        if (_userCredentials.ContainsKey(username))
        {
            Console.WriteLine("User already exists.");
            return false;
        }

        _userCredentials[username] = password;
        Console.WriteLine("User created successfully.");
        return true;
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
        UserAuthenticationService authService = new UserAuthenticationService();
        bool loggedIn = false;
        string currentUser = string.Empty;

        Console.WriteLine("Welcome to the Football Service System!");

        while (!loggedIn)
        {
            Console.WriteLine("\n1. Login");
            Console.WriteLine("2. Create User");
            Console.WriteLine("0. Exit");
            Console.Write("Enter your choice: ");
            string authChoice = Console.ReadLine();

            switch (authChoice)
            {
                case "1":
                    Console.Write("Please enter your username: ");
                    string username = Console.ReadLine();
                    Console.Write("Please enter your password: ");
                    string password = Console.ReadLine();

                    if (authService.Authenticate(username, password))
                    {
                        Console.WriteLine("Login successful! Welcome, " + username + "!");
                        loggedIn = true;
                        currentUser = username;
                    }
                    else
                    {
                        Console.WriteLine("Invalid username or password. Please try again.");
                    }
                    break;

                case "2":
                    Console.Write("Enter a username to create: ");
                    string newUsername = Console.ReadLine();
                    Console.Write("Enter a password: ");
                    string newPassword = Console.ReadLine();

                    authService.CreateUser(newUsername, newPassword);
                    break;

                case "0":
                    Console.WriteLine("Exiting program...");
                    return;

                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }

        ILiveMatchService liveMatchService = new LiveMatchService();
        IFixturesService fixturesService = new FixturesService();
        ITeamService teamService = new TeamService();
        IPlayerService playerService = new PlayerService();
        ILeagueService leagueService = new LeagueService();
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
                    Console.WriteLine("Enter the Preference : ");
                    String preference = Console.ReadLine();
                    if (preference == "sports")
                    {
                        INewsService newsService = new TrendingNewsService();
                        newsService.GetNewsByPreference(preference);
                    }
                    else if (preference == "politics")
                    {
                        INewsService newsService = new TrendingNewsService();
                        newsService.GetNewsByPreference(preference);
                    }
                    else if (preference == "football")
                    {
                        INewsService newsService = new PersonalizedNewsService();
                        newsService.GetNewsByPreference(preference);
                    }
                    else if (preference == "technology")
                    {
                        INewsService newsService = new PersonalizedNewsService();
                        newsService.GetNewsByPreference(preference);
                    }
                    else if (preference == "") 
                    {
                        INewsService newsService = new TrendingNewsService();
                        newsService.GetNewsByPreference(preference);
                        INewsService newsService1 = new PersonalizedNewsService();
                        newsService1.GetNewsByPreference(preference);
                    }
                    break;
                case "7":
                    Console.WriteLine("Enter the type and input");
                    string type = Console.ReadLine();
                    string input = Console.ReadLine();
                    if (type == "vote" && input == "teamA")
                    {
                        IFanInteractionService poll = new FanPollService();
                        poll.Interact(type, input);
                    }
                    else if (type == "vote" && input == "teamB")
                    {
                        IFanInteractionService poll = new FanPollService();
                        poll.Interact(type, input);
                    }
                    else if (type == "question" && input == "correct")
                    {
                        IFanInteractionService poll = new FanTriviaService();
                        poll.Interact(type, input);
                    }
                    else if (type == "question" && input == "wrong")
                    {
                        IFanInteractionService poll = new FanTriviaService();
                        poll.Interact(type, input);
                    }
                    else if (type == " " && input == " ")
                    {
                        IFanInteractionService poll = new FanPollService();
                        poll.Interact(type, input);
                        IFanInteractionService poll1 = new FanTriviaService();
                        poll1.Interact(type, input);
                    }
                    break;
                case "8":
                    Console.WriteLine("Enter the type");
                    string type1 = Console.ReadLine();
                    if (type1 == "goal")
                    {
                        IVideoService videoService = new ShortHighlightVideoService();
                        videoService.FetchVideo(type1);
                    }
                    else if (type1 == "save")
                    {
                        IVideoService videoService = new ShortHighlightVideoService();
                        videoService.FetchVideo(type1);
                    }
                    if (type1 == "full")
                    {
                        IVideoService videoService = new FullMatchReplayVideoService();
                        videoService.FetchVideo(type1);
                    }
                    else if (type1 == "extended")
                    {
                        IVideoService videoService = new FullMatchReplayVideoService();
                        videoService.FetchVideo(type1);
                    }
                    else if (type1 == " ")
                    {
                        IVideoService videoService = new ShortHighlightVideoService();
                        videoService.FetchVideo(type1);
                        IVideoService videoService1 = new FullMatchReplayVideoService();
                        videoService1.FetchVideo(type1);
                    }
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