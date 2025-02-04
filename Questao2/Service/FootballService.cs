using Newtonsoft.Json.Linq;

namespace Questao2.Service;

public class FootballService
{
    private readonly HttpClient _client;

        public FootballService()
        {
            _client = new HttpClient();
        }

        public async Task<int> GetTotalScoredGoals(string team, int year)
        {
            int totalGoals = 0;
            totalGoals += await GetGoalsByTeamAndYear(team, year, "team1");
            totalGoals += await GetGoalsByTeamAndYear(team, year, "team2");
            return totalGoals;
        }

        private async Task<int> GetGoalsByTeamAndYear(string team, int year, string teamType)
        {
            int totalGoals = 0;
            int currentPage = 1;
            bool hasMorePages = true;

            while (hasMorePages)
            {
                string url = $"https://jsonmock.hackerrank.com/api/football_matches?year={year}&{teamType}={team}&page={currentPage}";
                HttpResponseMessage response = await _client.GetAsync(url);
                string responseBody = await response.Content.ReadAsStringAsync();
                JObject json = JObject.Parse(responseBody);

                foreach (var match in json["data"]!)
                {
                    totalGoals += (int?)match[$"{teamType}goals"] ?? 0;
                }

                int totalPages = (int)json["total_pages"]!;
                currentPage++;
                hasMorePages = currentPage <= totalPages;
            }

            return totalGoals;
        }
}
