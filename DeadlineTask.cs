public class DeadlineTask : Task
{
    public new DateTime Deadline { get; set; }  

    public DeadlineTask(string title, string category, string priority, DateTime deadline)
        : base(title, category, priority, deadline)  
    {
        Deadline = deadline;
    }

    public override void Display()
    {
        base.Display();  
        Console.WriteLine($"Deadline: {Deadline:yyyy-MM-dd}");  
    }
}
