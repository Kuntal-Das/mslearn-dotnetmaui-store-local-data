using SQLite;
using People.Models;

namespace People;

public class PersonRepository(string dbPath)
{
    public string StatusMessage { get; set; }

    // TODO: Add variable for the SQLite connection
    private SQLiteAsyncConnection _conn;

    private async Task Init()
    {
        // TODO: Add code to initialize the repository    
        _conn = new SQLiteAsyncConnection(dbPath);
        await _conn.CreateTableAsync<Person>();
    }

    public async Task AddNewPerson(string name)
    {
        try
        {
            // TODO: Call Init()
            await Init();

            // basic validation to ensure a name was entered
            if (string.IsNullOrEmpty(name))
                throw new Exception("Valid name required");

            // TODO: Insert the new person into the database
            var result = await _conn.InsertAsync(new Person { Name = name });

            StatusMessage = $"{result} record(s) added (Name: {name})";
        }
        catch (Exception ex)
        {
            StatusMessage = $"Failed to add {name}. Error: {ex.Message}";
        }
    }

    public async Task<List<Person>> GetAllPeople()
    {
        // TODO: Init then retrieve a list of Person objects from the database into a list
        try
        {
            await Init();
            return await _conn.Table<Person>().ToListAsync();
        }
        catch (Exception ex)
        {
            StatusMessage = $"Failed to retrieve data. {ex.Message}";
        }

        return [];
    }
}