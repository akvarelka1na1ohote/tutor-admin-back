using backend.DataAccess;
using backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace backend.InfoJSON
{
    public static class Info
    {
        public static async Task SeedData(ApplicationDbContext context)
        {
            try
            {
                var basePath = AppDomain.CurrentDomain.BaseDirectory;
                Console.WriteLine($"Base directory: {basePath}");

                if (!context.Users.Any())
                {
                    var users = await LoadFromJson<List<DbUser>>(Path.Combine(basePath, "InfoJSON/user.json"));
                    Console.WriteLine($"Loaded {users.Count} users");
                    await context.Users.AddRangeAsync(users);
                }

                if (!context.Performers.Any())
                {
                    var performers = await LoadFromJson<List<DbPerformer>>(Path.Combine(basePath, "InfoJSON/performer.json"));
                    Console.WriteLine($"Loaded {performers.Count} performers");
                    await context.Performers.AddRangeAsync(performers);
                }

                if (!context.Clients.Any())
                {
                    var clients = await LoadFromJson<List<DbClient>>(Path.Combine(basePath, "InfoJSON/client.json"));
                    Console.WriteLine($"Loaded {clients.Count} clients");
                    await context.Clients.AddRangeAsync(clients);
                }

                if (!context.Subjects.Any())
                {
                    var subjects = await LoadFromJson<List<DbSubject>>(Path.Combine(basePath, "InfoJSON/subject.json"));
                    Console.WriteLine($"Loaded {subjects.Count} subjects");
                    await context.Subjects.AddRangeAsync(subjects);
                }

                if (!context.MatchClients.Any())
                {
                    var matchClients = await LoadFromJson<List<DbMatchClient>>(Path.Combine(basePath, "InfoJSON/match_client.json"));
                    Console.WriteLine($"Loaded {matchClients.Count} client matches");
                    await context.MatchClients.AddRangeAsync(matchClients);
                }

                if (!context.MatchPerformers.Any())
                {
                    var matchPerformers = await LoadFromJson<List<DbMatchPerformer>>(Path.Combine(basePath, "InfoJSON/match_performer.json"));
                    Console.WriteLine($"Loaded {matchPerformers.Count} performer matches");
                    await context.MatchPerformers.AddRangeAsync(matchPerformers);
                }

                if (!context.TimetableClients.Any())
                {
                    var timetableClients = await LoadFromJson<List<DbTimetableClient>>(Path.Combine(basePath, "InfoJSON/timetable_client.json"));
                    Console.WriteLine($"Loaded {timetableClients.Count} client timetables");
                    await context.TimetableClients.AddRangeAsync(timetableClients);
                }

                if (!context.TimetablePerformers.Any())
                {
                    var timetablePerformers = await LoadFromJson<List<DbTimetablePerformer>>(Path.Combine(basePath, "InfoJSON/timetable_performer.json"));
                    Console.WriteLine($"Loaded {timetablePerformers.Count} performer timetables");
                    await context.TimetablePerformers.AddRangeAsync(timetablePerformers);
                }

                var changes = await context.SaveChangesAsync();
                Console.WriteLine($"Saved {changes} changes to database");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in SeedData: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
                }
                throw;
            }
        }

        private static async Task<T> LoadFromJson<T>(string filePath)
        {
            try
            {
                Console.WriteLine($"Loading JSON from: {filePath}");

                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException($"JSON file not found at: {filePath}");
                }

                var json = await File.ReadAllTextAsync(filePath);
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    AllowTrailingCommas = true
                };

                var result = JsonSerializer.Deserialize<T>(json, options) ??
                    throw new InvalidOperationException("Deserialization returned null");

                Console.WriteLine($"Successfully loaded {result.GetType().Name}");
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading JSON from {filePath}: {ex.Message}");
                throw;
            }
        }
    }
}