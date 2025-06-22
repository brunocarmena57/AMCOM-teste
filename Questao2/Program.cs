using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Collections.Generic;

public class Program
{
    // Classes para desserializar a resposta JSON da API
    public class Match
    {
        public string team1 { get; set; }
        public string team2 { get; set; }
        public string team1goals { get; set; }
        public string team2goals { get; set; }
    }

    public class ApiResponse
    {
        public int page { get; set; }
        public int total_pages { get; set; }
        public List<Match> data { get; set; }
    }

    private static readonly HttpClient client = new HttpClient();

    public static void Main()
    {
        string teamName = "Paris Saint-Germain";
        int year = 2013;
        int totalGoals = getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        teamName = "Chelsea";
        year = 2014;
        totalGoals = getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

    }

    public static int getTotalScoredGoals(string team, int year)
    {
        int goalsAsTeam1 = fetchGoalsPerPage(team, year, "team1");

        int goalsAsTeam2 = fetchGoalsPerPage(team, year, "team2");

        return goalsAsTeam1 + goalsAsTeam2;
    }

    private static int fetchGoalsPerPage(string team, int year, string teamRole)
    {
        int totalGoals = 0;
        int currentPage = 1;
        int totalPages = 1;

        string encodedTeamName = WebUtility.UrlEncode(team);

        while (currentPage <= totalPages)
        {
            string url = $"https://jsonmock.hackerrank.com/api/football_matches?year={year}&{teamRole}={encodedTeamName}&page={currentPage}";

            string jsonResponse = client.GetStringAsync(url).Result;

            var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(jsonResponse);

            if (apiResponse != null)
            {
                totalPages = apiResponse.total_pages;

                if (apiResponse.data != null)
                {
                    foreach (var match in apiResponse.data)
                    {
                        if (teamRole == "team1")
                        {
                            totalGoals += int.Parse(match.team1goals);
                        }
                        else
                        {
                            totalGoals += int.Parse(match.team2goals);
                        }
                    }
                }
            }
            currentPage++;
        }
        return totalGoals;
    }
}