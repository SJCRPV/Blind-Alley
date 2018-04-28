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
        Button forwardBtn;
        Button leftBtn;
        Button rightBtn;
        Button backwardBtn;
        Button interactBtn;

        public void moveForward(View v)
        {
            Console.WriteLine("Forward!");
        }

        public void turnLeft(View v)
        {
            Console.WriteLine("Turn Left!");
        }

        public void checkForInteraction(View v)
        {
            Console.WriteLine("Wut?");
        }

        public void turnRight(View v)
        {
            Console.WriteLine("Turn Right!");
        }

        public void moveBackward(View v)
        {
            Console.WriteLine("Backward!");
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            forwardBtn = FindViewById<Button>(Resource.Id.buttonForward);
            leftBtn = FindViewById<Button>(Resource.Id.buttonLeft);
            rightBtn = FindViewById<Button>(Resource.Id.buttonRight);
            interactBtn = FindViewById<Button>(Resource.Id.buttonInteract);
            backwardBtn = FindViewById<Button>(Resource.Id.buttonBackward);
        }
    }
}

