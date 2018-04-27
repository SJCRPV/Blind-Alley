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


        //These need to be triggered from inside an OnClick function
        public void moveForward()
        {
            Console.WriteLine("Forward!");
        }

        public void turnLeft()
        {
            Console.WriteLine("Turn Left!");
        }

        public void checkForInteraction()
        {
            Console.WriteLine("Wut?");
        }

        public void turnRight()
        {
            Console.WriteLine("Turn Right!");
        }

        public void moveBackward()
        {
            Console.WriteLine("Backward!");
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            mainView = FindViewById(Resource.Layout.Main);
            forwardBtn = FindViewById<Button>(Resource.Id.buttonForward);
            leftBtn = FindViewById<Button>(Resource.Id.buttonLeft);
            rightBtn = FindViewById<Button>(Resource.Id.buttonRight);
            interactBtn = FindViewById<Button>(Resource.Id.buttonInteract);
            backwardBtn = FindViewById<Button>(Resource.Id.buttonBackward);
        }
    }
}

