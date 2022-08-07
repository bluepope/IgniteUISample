using IgniteUI.Blazor.Controls;

using Microsoft.AspNetCore.Components;

namespace BlazorApp1.Pages
{
    public partial class DataGrid : ComponentBase
    {
        [Inject]
        IIgniteUIBlazor IgniteUIBlazor { get; set; }

        private List<SaleInfo> Data;

        private Random Rand = new Random();

        public IgbDataGrid GridRef { get; set; }

        protected override void OnInitialized()
        {
            IgbDataGridModule.Register(IgniteUIBlazor);
            GenerateData();
        }

        void OnCellEditStarted(IgbGridCellEditStartedEventArgs e)
        {
            var grid = e.Parent as IgbDataGrid;
            var item = e.Item as SaleInfo;

            if (item.ProductID > 1005)
            {
                grid.CancelEdits();
            }
        }

        void OnCellEditEnded(IgbGridCellEditEndedEventArgs e)
        {
            var grid = e.Parent as IgbDataGrid;
            var item = e.Item as SaleInfo;

            //e.Row //현재 그리드에서 표시되는 Row Index -- DataItem이 아님

            int idx = this.Data.IndexOf(item);

            if (new string[] { "ProductPrice", "OrderItems" }.Contains(e.Column.Field))
            {
                grid.NotifyUpdateItem(this.Data, idx, item);
            }
        }

        public void GenerateData()
        {
            string[] names = new string[] {
            "Intel CPU", "AMD CPU",
            "Intel Motherboard", "AMD Motherboard", "Nvidia Motherboard",
            "Nvidia GPU", "Gigabyte GPU", "Asus GPU", "AMD GPU", "MSI GPU",
            "Corsair Memory", "Patriot Memory", "Skill Memory",
            "Samsung HDD", "WD HDD", "Seagate HDD", "Intel HDD", "Asus HDD",
            "Samsung SSD", "WD SSD", "Seagate SSD", "Intel SSD", "Asus SSD",
            "Samsung Monitor", "Asus Monitor", "LG Monitor", "HP Monitor" };

            string[] countries = new string[] {
            "USA", "UK", "France", "Canada", "Poland",
            "Denmark", "Croatia", "Australia", "Seychelles",
            "Sweden", "Germany", "Japan", "Ireland",
            "Barbados", "Jamaica", "Cuba", "Spain", };
            string[] status = new string[] { "Packing", "Shipped", "Delivered" };

            var sales = new List<SaleInfo>();

            for (var i = 0; i < 200; i++)
            {
                var price = GetRandomNumber(10000, 90000) / 100;
                var items = GetRandomNumber(4, 30);
                var margin = GetRandomNumber(2, 5);
                var profit = Math.Round((price * margin / 100) * items);
                var country = GetRandomItem(countries);

                var item = new SaleInfo()
                {
                    Country = country,
                    CountryFlag = GetCountryFlag(country),
                    Margin = margin,
                    OrderDate = GetRandomDate(),
                    OrderItems = items,
                    ProductID = 1001 + i,
                    ProductName = GetRandomItem(names),
                    ProductPrice = price,
                    Profit = Math.Round(profit),
                    Status = GetRandomItem(status)
                };
                sales.Add(item);
            }

            this.Data = sales;
        }

        public double GetRandomNumber(double min, double max)
        {
            return Math.Round(min + (Rand.NextDouble() * (max - min)));
        }

        public string GetRandomItem(string[] array)
        {
            var index = (int)Math.Round(GetRandomNumber(0, array.Length - 1));
            return array[index];
        }

        public DateTime GetRandomDate()
        {
            var today = DateTime.Today;
            var year = today.Year;
            var month = this.GetRandomNumber(1, 9);
            var day = this.GetRandomNumber(10, 27);
            return new DateTime(year, (int)month, (int)day);
        }

        public string GetCountryFlag(string country)
        {
            var flag = "https://static.infragistics.com/xplatform/images/flags/" + country + ".png";
            return flag;
        }

        public class SaleInfo
        {
            public string Status { get; set; }
            public string ProductName { get; set; }
            public string CountryFlag { get; set; }
            public string Country { get; set; }
            public DateTime OrderDate { get; set; }
            public double Profit { get; set; }
            public double ProductPrice { get; set; }
            public double ProductID { get; set; }
            public double OrderValue => Math.Round(ProductPrice * OrderItems);
            public double OrderItems { get; set; }
            public double Margin { get; set; }
        }
    }
}
