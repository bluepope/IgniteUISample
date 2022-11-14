using IgniteUI.Blazor.Controls;

using Microsoft.AspNetCore.Components;

namespace BlazorApp1.Pages
{
    public partial class Chart : ComponentBase
    {
        [Inject]
        IIgniteUIBlazor IgniteUIBlazor { get; set; }

        IgbLegend ColumnChartLegend { get; set; }
        IgbLegend LineChartLegend { get; set; }


        protected override void OnInitialized()
        {
        }

        protected override void OnAfterRender(bool firstRender)
        {
            chart2.MarkerTypes.Clear();
            chart2.MarkerTypes.Add(MarkerType.Triangle);
            chart2.MarkerTypes.Add(MarkerType.Square);
        }

        private IgbCategoryChart chart1;
        private IgbCategoryChart chart2;

        public HighestGrossingMovies HighestGrossingMovies { get; set; }

        void OnSearch()
        {
            HighestGrossingMovies = new HighestGrossingMovies();
        }
    }

    public class HighestGrossingMoviesItem
    {
        public string Franchise { get; set; }
        public double TotalRevenue { get; set; }
        public double HighestGrossing { get; set; }
    }

    public class HighestGrossingMovies : List<HighestGrossingMoviesItem>
    {
        public HighestGrossingMovies()
        {
            this.Add(new HighestGrossingMoviesItem()
            {
                Franchise = @"Marvel Universe",
                TotalRevenue = new Random().Next(10, 30),
                HighestGrossing = 2.8
            });
            this.Add(new HighestGrossingMoviesItem()
            {
                Franchise = @"Star Wars",
                TotalRevenue = new Random().Next(10, 30),
                HighestGrossing = 2.07
            });
            this.Add(new HighestGrossingMoviesItem()
            {
                Franchise = @"Harry Potter",
                TotalRevenue = new Random().Next(10, 30),
                HighestGrossing = 1.34
            });
            this.Add(new HighestGrossingMoviesItem()
            {
                Franchise = @"Avengers",
                TotalRevenue = new Random().Next(10, 30),
                HighestGrossing = 2.8
            });
            this.Add(new HighestGrossingMoviesItem()
            {
                Franchise = @"Spider Man",
                TotalRevenue = new Random().Next(10, 30),
                HighestGrossing = 1.28
            });
            this.Add(new HighestGrossingMoviesItem()
            {
                Franchise = @"James Bond",
                TotalRevenue = new Random().Next(10, 30),
                HighestGrossing = 1.11
            });
        }
    }

}

