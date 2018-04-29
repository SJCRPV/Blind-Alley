using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;
using System;

namespace Blind_Alley
{
    [Activity(Label = "Blind_Alley", MainLauncher = true)]
    public class MainActivity : Activity
    {
        View mainView;
        Button forwardBtn;
        Button leftBtn;
        Button rightBtn;
        Button backwardBtn;
        Button interactBtn;

        private void setListeners()
        {
            forwardBtn.Click += (sender, e) =>
            {
                Console.WriteLine("Forward!");
            };

            leftBtn.Click += (sender, e) =>
            {
                Console.WriteLine("Turn Left!");
            };

            interactBtn.Click += (sender, e) =>
            {
                 Console.WriteLine("Wut?");
            };

            rightBtn.Click += (sender, e) =>
            {
                Console.WriteLine("Turn Right!");
            };

            backwardBtn.Click += (sender, e) =>
            {
                Console.WriteLine("Backward!");
            };
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            mainView = Window.DecorView;
            forwardBtn = mainView.FindViewById<Button>(Resource.Id.buttonForward);
            leftBtn = mainView.FindViewById<Button>(Resource.Id.buttonLeft);
            rightBtn = mainView.FindViewById<Button>(Resource.Id.buttonRight);
            interactBtn = mainView.FindViewById<Button>(Resource.Id.buttonInteract);
            backwardBtn = mainView.FindViewById<Button>(Resource.Id.buttonBackward);

            setListeners();
        }
    }
}

