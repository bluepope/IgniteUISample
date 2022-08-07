using IgniteUI.Blazor.Controls;

using Microsoft.AspNetCore.Components;

namespace BlazorApp1.Pages
{
    public partial class FormInput : ComponentBase
    {
        [Inject]
        IIgniteUIBlazor IgniteUIBlazor { get; set; }

        public IgbCheckbox Check1Ref { get; set; }
        public bool Check1 { get; set; }

        public bool Switch1 { get; set; }

        public IgbRadioGroup RadioGroup1Ref { get; set; }
        public string Radio1Str { get; set; } = "Apple";

        public bool Radio1
        {
            get
            {
                return Radio1Str == "Apple";
            }
            set
            {
                if (value == true)
                {
                    Radio1Str = "Apple";
                    StateHasChanged();
                }
            }
        }

        public bool Radio2
        {
            get
            {
                return Radio1Str == "Banana";
            }
            set
            {
                if (value == true)
                {
                    Radio1Str = "Banana";
                    StateHasChanged();
                }
            }
        }

        public bool Radio3
        {
            get
            {
                return Radio1Str == "Mango";
            }
            set
            {
                if (value == true)
                {
                    Radio1Str = "Mango";
                    StateHasChanged();
                }
            }
        }

        public bool Radio4
        {
            get
            {
                return Radio1Str == "Orange";
            }
            set
            {
                if (value == true)
                {
                    Radio1Str = "Orange";
                    StateHasChanged();
                }
            }
        }

       

        protected override void OnInitialized()
        {
            //Checkbox
            IgbCheckboxModule.Register(IgniteUIBlazor);

            //Switch
            IgbSwitchModule.Register(IgniteUIBlazor);

            //Radio
            IgbRadioModule.Register(IgniteUIBlazor);
            IgbRadioGroupModule.Register(IgniteUIBlazor);
        }

        void OnBtnClick1()
        {
            Check1 = !Check1;
            //Check1Ref.Checked = Check1;


            StateHasChanged();
        }
    }
}
