using IgniteUI.Blazor.Controls;

using Microsoft.AspNetCore.Components;

namespace BlazorApp1.Pages
{
    public partial class DatePicker : ComponentBase
    {

        [Inject]
        IIgniteUIBlazor IgniteUIBlazor { get; set; }

        public IgbDatePicker Date1Ref { get; set; }
        public DateTime Date1 { get; set; }// = DateTime.Now;


        public DateTime MinDate { get; set; }
        public DateTime MaxDate { get; set; }

        protected override void OnInitialized()
        {
            IgbDatePickerModule.Register(IgniteUIBlazor);
            Date1 = DateTime.Today.AddDays(-2);
            MinDate = DateTime.Now.AddDays(-3);
            MaxDate = DateTime.Now.AddDays(3);
        }
    }
}
