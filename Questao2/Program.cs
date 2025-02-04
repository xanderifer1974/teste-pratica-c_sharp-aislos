using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class Program
{
    public static async Task  Main()
    {
        string teamName = "Paris Saint-Germain";
        int year = 2013;
        int totalGoals = await getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team "+ teamName +" scored "+ totalGoals.ToString() + " goals in "+ year);

        teamName = "Chelsea";
        year = 2014;
        totalGoals =  await getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        // Output expected:
        // Team Paris Saint - Germain scored 109 goals in 2013
        // Team Chelsea scored 92 goals in 2014
    }

    public static async Task<int> getTotalScoredGoals(string team, int year)
    {
        int totalGoals = 0;
        int currentPage = 1;
        bool hasMorePages = true;

        using (HttpClient client = new HttpClient())
        {
            while (hasMorePages)
            {
                string url = $"https://jsonmock.hackerrank.com/api/football_matches?year={year}&team1={team}&page={currentPage}";
                HttpResponseMessage response = await client.GetAsync(url);
                string responseBody = await response.Content.ReadAsStringAsync();
                JObject json = JObject.Parse(responseBody);

                foreach (var match in json["data"])
                {
                    totalGoals += (int)match["team1goals"];
                }

                int totalPages = (int)json["total_pages"];
                currentPage++;
                hasMorePages = currentPage <= totalPages;
            }
        }

        currentPage = 1;
        hasMorePages = true;

        using (HttpClient client = new HttpClient())
        {
            while (hasMorePages)
            {
                string url = $"https://jsonmock.hackerrank.com/api/football_matches?year={year}&team2={team}&page={currentPage}";
                HttpResponseMessage response = await client.GetAsync(url);
                string responseBody = await response.Content.ReadAsStringAsync();
                JObject json = JObject.Parse(responseBody);

                foreach (var match in json["data"])
                {
                    totalGoals += (int)match["team2goals"];
                }

                int totalPages = (int)json["total_pages"];
                currentPage++;
                hasMorePages = currentPage <= totalPages;
            }
        }

        return totalGoals;
    }

    // public static int getTotalScoredGoals(string team, int year)
    // {
        
    //     return 0;
    // }

}