public class Task
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Category { get; set; }
    public string Priority { get; set; }
    public DateTime Deadline { get; set; }

    public Task() { }

    public Task(string title, string category, string priority, DateTime deadline)
    {
        Title = title;
        Category = category;
        Priority = priority;
        Deadline = deadline;
    }

    public virtual void Display()
    {
        Console.WriteLine($"ID: {Id} | Title: {Title} | Category: {Category} | Priority: {Priority} | Deadline: {Deadline}");
    }
}
