using MauiEfCorePOC.Context;
using MauiEfCorePOC.Entities;

namespace MauiEfCorePOC;

public partial class MainPage : ContentPage
{
    private readonly AppDbContext _dbContext;
    int count = 0;

	public MainPage(AppDbContext dbContext)
	{
		InitializeComponent();

        _dbContext = dbContext;

        // Example usage
        _dbContext.Database.EnsureCreated();

        if (!_dbContext.Blogs.Any())
        {
            _dbContext.Blogs.Add(new Blog { Url = "https://devblogs.microsoft.com/dotnet" });
            _dbContext.SaveChanges();
        }

        var blogs = _dbContext.Blogs.ToList();
        // Display blogs or perform other operations
    }

    private void OnCounterClicked(object sender, EventArgs e)
	{
		count++;

		if (count == 1)
			CounterBtn.Text = $"Clicked {count} time";
		else
			CounterBtn.Text = $"Clicked {count} times";

		SemanticScreenReader.Announce(CounterBtn.Text);
	}
}


