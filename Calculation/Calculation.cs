using System;

using Xamarin.Forms;

namespace Calculation {
    public class App : Application {
        public App() {
            // The root page of your application
            #region テストコード（未使用）
#if false
            var content = new ContentPage
            {
                Title = "Calculation",
                Content = new StackLayout
                {
                    VerticalOptions = LayoutOptions.Center,
                    Children = {
                        new Label {
                            HorizontalTextAlignment = TextAlignment.Center,
                            Text = "Welcome to Xamarin Forms!",
                            BackgroundColor = Color.Red,
                        }
                    }
                }
            };
#endif
            #endregion

            MainPage = new VerticalCalculationPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
