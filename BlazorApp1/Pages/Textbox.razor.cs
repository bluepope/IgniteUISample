﻿using IgniteUI.Blazor.Controls;

using Microsoft.AspNetCore.Components;

namespace BlazorApp1.Pages
{
    public partial class Textbox : ComponentBase
    {
        [Inject]
        IIgniteUIBlazor IgniteUIBlazor { get; set; }
        private IgbIcon IconRef { get; set; }

        public IgbInput Num1Ref { get; set; }

        string Str1 { get; set; } = "";

        int Num1 { get; set; }
        string Num1Str
        {
            //10000
            //10,000
            get => Num1.ToString();//"#,###" error
            set
            {
                if (int.TryParse(value, out int n))
                {
                    this.Num1 = n;
                }
                else
                {
                    this.Num1 = 0;
                    StateHasChanged();
                }
            }
        }

        protected override void OnInitialized()
        {
            IgbIconModule.Register(IgniteUIBlazor);
            IgbInputModule.Register(IgniteUIBlazor);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender && this.IconRef != null)
            {
                await this.IconRef.EnsureReady();
                string phoneIcon = "<svg xmlns='http://www.w3.org/2000/svg' height='24px' viewBox='0 0 24 24' width='24px' fill='#000000'><path d='M0 0h24v24H0z' fill='none'/><path d='M6.62 10.79c1.44 2.83 3.76 5.14 6.59 6.59l2.2-2.2c.27-.27.67-.36 1.02-.24 1.12.37 2.33.57 3.57.57.55 0 1 .45 1 1V20c0 .55-.45 1-1 1-9.39 0-17-7.61-17-17 0-.55.45-1 1-1h3.5c.55 0 1 .45 1 1 0 1.25.2 2.45.57 3.57.11.35.03.74-.25 1.02l-2.2 2.2z'/></svg>'";
                await this.IconRef.RegisterIconFromTextAsync("phone", phoneIcon, "material");
            }
        }
    }
}